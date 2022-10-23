using Freenymous.Data;
using Freenymous.Data.Users;

namespace Freenymous.Front.Services;

public interface IUserService
{
    Task<User> Random();
    Task<User> Random(long oldId);
    
    Task<User> Prolong(string code);
}

public class UserService : IUserService
{
    private readonly IHttpService _httpService;

    public UserService(IHttpService httpService)
    {
        _httpService = httpService;
    }

    public async Task<User> Random(long oldId)
    {
        return await _httpService.Get<User>($"/User/Random/{oldId}");
    }
    
    public async Task<User> Prolong(string code)
    {
        return await _httpService.Patch<User>($"/User/Prolong/{code}", null);
    }
    
    public async Task<User> Random()
    {
        return await _httpService.Get<User>("/User/Random");
    }
    
    
}