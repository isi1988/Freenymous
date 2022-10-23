using Freenymous.Data;
using Freenymous.Data.Models;
using Freenymous.Data.Topics;
using Freenymous.Data.Users;

namespace Freenymous.Front.Services;

public interface ITopicService
{

    Task<int> Count(string search);
    Task<Topic> Get(long id);
    Task<IEnumerable<TopicModel>> Get(int skip = 0, int take = 10);
    
    Task<IEnumerable<Comment>> Comments(long topicId, int skip = 0, int take = 10);

    Task<IEnumerable<TopicModel>> Get(string search, int skip = 0, int take = 10);
    
    Task<Comment> Comment(Comment comment);
    
    Task<int> Count();
    
    Task Post(Topic topic);

}

internal class TopicService : ITopicService
{
    private readonly IHttpService _httpService;

    public TopicService(IHttpService httpService)
    {
        _httpService = httpService;
    }


    public async Task<Topic> Get(long id)
    {
        return await _httpService.Get<Topic>($"/Topic/{id}");
    }

    public async Task<IEnumerable<TopicModel>> Get(int skip = 0, int take = 10)
    {
        return await _httpService.Get<IEnumerable<TopicModel>>($"/Topic/{skip}/{take}");
    }
    
    public async Task<IEnumerable<TopicModel>> Get(string search, int skip = 0, int take = 10)
    {
        return await _httpService.Get<IEnumerable<TopicModel>>($"/Topic/{search}/{skip}/{take}");
    }

    public async Task<IEnumerable<Comment>> Comments(long topicId, int skip = 0, int take = 10)
    {
        return await _httpService.Get<IEnumerable<Comment>>($"/Topic/Comment/{topicId}/{skip}/{take}");
    }

    public async Task<Comment> Comment(Comment comment)
    {
        return await _httpService.Post<Comment>($"/Topic/Comment", comment);
    }

    public async Task<int> Count()
    {
        var s = await _httpService.GetString($"/Topic/Count");
        return int.Parse(s);
    }
    public async Task<int> Count(string search)
    {
        var s = await _httpService.GetString($"/Topic/Count/{search}");
        return int.Parse(s);
    }

    public async Task Post(Topic topic)
    { 
        await _httpService.Post($"/Topic/", topic);
    }
}