using System.Text.Json;
using Microsoft.JSInterop;

namespace Freenymous.Front.Services;

public interface ILocalStorageService
{
    Task<T?> GetItem<T>(string key) where T : class;
    Task SetItem<T>(string key, T value);
    Task RemoveItem(string key);
    Task<string?> GetString(string key);

    Task SetString(string key, string value);
}

public class LocalStorageService : ILocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<T?> GetItem<T>(string key) where T : class
    {
        var json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
        return json != null ? JsonSerializer.Deserialize<T>(json) : null;
    }

    public async Task<string?> GetString(string key)
    {
        return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);
    }

    public async Task SetItem<T>(string key, T value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
    }

    public async Task SetString(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, value);
    }

    public async Task RemoveItem(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }
}