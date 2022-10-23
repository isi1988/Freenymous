using Freenymous.Data.Users;
using Freenymous.Front.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Freenymous.Front.Shared;

public partial class MainLayout
{
    [Inject]
    private IJSRuntime _js { get; set; }
    private MyUser? _myUserControl;
    private bool _anotherPage = false;

    protected override async Task OnInitializedAsync()
    {
        var user = await LocalStorageService.GetItem<User>("my_user");
        if (user != null)
        {
            _anotherPage = true;
        }
    }
    
    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var dotNetReference = DotNetObjectReference.Create(this);
            await _js.InvokeVoidAsync("window.blazor_setExitEvent", dotNetReference);
        }
    }

    [JSInvokable("SPASessionClosed")]
    public void SPASessionClosed()
    {
        if (!_anotherPage)
            LocalStorageService.RemoveItem("my_user");
    }
}