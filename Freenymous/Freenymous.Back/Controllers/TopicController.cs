using Freenymous.Data;
using Freenymous.Data.Models;
using Freenymous.Data.Topics;
using Freenymous.Data.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Freenymous.Back.Controllers;

[ApiController]
[Route("[controller]")]
public class TopicController : ControllerBase
{
    private MainDbContext _context;

    public TopicController(MainDbContext context)
    {
        _context = context;
    }

    [HttpGet("{skip}/{take}")]
    public IEnumerable<TopicModel>? Get(int skip, int take)
    {
        return _context.Topics.OrderByDescending(t => t.CreationDate).Skip(skip).Take(take).Select(t=>new TopicModel()
        {
            Id = t.Id,
            Name = t.Name,
            Tags = t.Tags,
            CreationDate = t.CreationDate,
            UserId = t.UserId,
            CommentCount = t.CommentCount,
            CommentTopLevelCount = t.CommentTopLevelCount,
            ViewCount = t.ViewIds.Count()
        });
    }
    
    [HttpGet("Count")]
    public int Count()
    {
        return _context.Topics.Count();
    }
    
    [HttpGet("Count/{search}")]
    public int Count(string search)
    {
        return _context.Topics.Count(t=>(t.Tags!= null && t.Tags.Contains(search)) || t.Name.Contains(search));
    }
    
    [HttpGet("{search}/{skip}/{take}")]
    public IEnumerable<TopicModel>? Get(string search, int skip, int take)
    {
        return _context.Topics.Where(t=>(t.Tags!= null && t.Tags.Contains(search)) || t.Name.Contains(search)).OrderByDescending(t => t.CreationDate).Skip(skip).Take(take).Select(t=>new TopicModel()
        {
            Id = t.Id,
            Name = t.Name,
            Tags = t.Tags,
            CreationDate = t.CreationDate,
            UserId = t.UserId,
            CommentCount = t.CommentCount,
            CommentTopLevelCount = t.CommentTopLevelCount,
            ViewCount = t.ViewIds.Count()
        });
    }
    
    
    [HttpGet("Comment/{topicId}/{skip}/{take}")]
    public IEnumerable<Comment>? Get(long topicId, int skip, int take)
    {
        return _context.Comments.Where(c => c.TopicId == topicId && c.ParentCommentId == null)
            .OrderByDescending(t => t.CreationDate).Skip(skip).Take(take);
    }
    
    [HttpGet("{id}")]
    public Topic? GetById(long id)
    {
        return _context.Topics.FirstOrDefault(t => t.Id == id);
    }
    
    [HttpPost]
    public IActionResult Post(Topic topic)
    {
        topic.CreationDate = DateTime.Now;
        var user = _context.Users.FirstOrDefault(u => u.Id == topic.UserId);
        if (user == null)
            return NotFound();
        if (topic.AccessCode != user.AccessCode)
            return BadRequest();
        topic.UserName = user.FirstName + " " + user.LastName;
        _context.Topics.Add(topic);
        _context.SaveChanges();
        return Ok();
    }
    
    [HttpPost("Comment")]
    public Comment? Comment(Comment comment)
    {
        comment.CreationDate = DateTime.Now;
        var user = _context.Users.FirstOrDefault(u => u.Id == comment.UserId);
        if (user == null)
            return null;
        if (comment.AccessCode != user.AccessCode)
            return null;
        var topic = _context.Topics.FirstOrDefault(c => c.Id == comment.TopicId);
        if (topic == null)
            return null;
        topic.CommentCount++;
        if (comment.ParentCommentId == null)
            topic.CommentTopLevelCount++;
        comment.UserName = user.FirstName + " " + user.LastName;
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return comment;
    }
}