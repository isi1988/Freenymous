using Freenymous.Data;
using Freenymous.Data.Users;
using Microsoft.AspNetCore.Mvc;

namespace Freenymous.Back.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private MainDbContext _context;

    public UserController(MainDbContext context)
    {
        _context = context;
    }

    [HttpGet("Random")]
    [HttpGet("Random/{oldId}")]
    public User? GetRandomUser(long? oldId)
    {
        if (oldId != null)
        {
            var oldUser = _context.Users.FirstOrDefault(u => u.Id == oldId);
            if (oldUser != null)
            {
                oldUser.IsBusy = false;
                _context.SaveChanges();
            }
        }
        
        var user =  _context.Users/*.Where(u=>!u.IsBusy)*/.OrderBy(r => Guid.NewGuid()).FirstOrDefault();
        if (user == null) return null;
        user.ActiveDateTime = DateTime.Now;
        user.IsBusy = true;
        user.RefreshUpdateCode = Guid.NewGuid().ToString();
        user.AccessCode = Guid.NewGuid().ToString();
        _context.SaveChanges();
        return user;
    }
    
    [HttpPatch("Prolong/{code}")]
    public User? ProlongUser(string code)
    {
        var user =  _context.Users.FirstOrDefault(u=>u.RefreshUpdateCode == code);
        if (user == null) return null;
        user.ActiveDateTime = DateTime.Now;
        user.IsBusy = true;
        user.RefreshUpdateCode = Guid.NewGuid().ToString();
        user.AccessCode = Guid.NewGuid().ToString();
        _context.SaveChanges();
        return user;
    }
    
    [HttpGet("Avatar/{id}.jpg")]
    public IActionResult GetUserAvatar(int id)
    {
        var bytes = _context.Users.FirstOrDefault(u => u.Id == id)?.Avatar;
        if (bytes == null)
            return NotFound();
        return File(bytes,"image/jpeg");
    }
}