using System.Timers;
using Freenymous.Data;
using Freenymous.Data.Users;

namespace Freenymous.Front.Controls;

public partial class MyUser
{
    public User? User { get; private set; }
    private TimeSpan _refreshTime;
    private System.Timers.Timer?  _timer;
    private bool _needProlong = false;
    
    private async void Prolong()
    {
        if (User != null)
        {
            User = await UserService.Prolong(User.RefreshUpdateCode);
            if (User != null)
            {
                ResetTimer();
                _needProlong = false;
            }
        }

        await InvokeAsync(StateHasChanged);
    }

    private void ResetTimer()
    {
        _refreshTime = TimeSpan.FromMinutes(1);
        if (_timer != null)
        {
            _timer.Dispose();
        }
        _timer = new System.Timers.Timer(1000);
        _timer.Elapsed += CountDownTimer;
        _timer.Enabled = true;
    }
    
    private async void RenewUser(long? oldId = null)
    {
        User = oldId!=null?await UserService.Random(oldId.Value):await UserService.Random();
        if (User == null) return;
        ResetTimer();
    }
    
    protected override async Task OnInitializedAsync()
    {
        RenewUser();
    }
    

    public void CountDownTimer(object? source, ElapsedEventArgs e)
    {
        _refreshTime = _refreshTime.Add(new TimeSpan(0, 0, -1));
        if (_refreshTime == TimeSpan.Zero)
        { 
            RenewUser(User.Id);
        }

        _needProlong = _refreshTime < TimeSpan.FromSeconds(30);
        InvokeAsync(StateHasChanged);
    }
}