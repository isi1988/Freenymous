using System.Globalization;
using System.Timers;
using Freenymous.Data;
using Freenymous.Data.Users;
using TimeSpan = System.TimeSpan;

namespace Freenymous.Front.Controls;

public partial class MyUser
{
    public User? User { get; private set; }
    private TimeSpan _refreshTime;
    private System.Timers.Timer  _timer = new System.Timers.Timer(1000);
    private bool _needProlong = false;
    private DateTime _exDate = DateTime.Now;

    public delegate void OnUserLoadHandler(User user);

    public event OnUserLoadHandler OnUserLoad;

    private void UserLoad()
    {
        if (User!= null)
            OnUserLoad?.Invoke(User);
    }
    
    private async void Prolong()
    {
        if (User != null)
        {
            User = await UserService.Prolong(User.RefreshUpdateCode);
           
            if (User != null)
            {
                await LocalStorageService.SetItem("my_user", User);
                ResetTimer();
                _needProlong = false;
            }
        }

        await InvokeAsync(StateHasChanged);
    }

    private void ResetTimer()
    {
        _timer.Enabled = true;
        _exDate = DateTime.Now.AddMinutes(1);
        LocalStorageService.SetString("ex_date", _exDate.ToString("dd.MM.yyyy HH:mm:ss"));
    }
    
    private async void RenewUser(long? oldId = null)
    {
        var renew = await LocalStorageService.GetString("renew");
        if (renew == "TRUE")
        {
            while (true)
            {
                renew = await LocalStorageService.GetString("renew");
                if (renew != "TRUE")
                {
                    UpdateFromCache();
                    break;
                }
                Thread.Sleep(200);
                StateHasChanged();
            }
            return;
        }
        await LocalStorageService.SetString("renew", "TRUE");
        User = oldId!=null?await UserService.Random(oldId.Value):await UserService.Random();
        if (User == null) return;
        UserLoad();
        await LocalStorageService.SetItem("my_user", User);
        ResetTimer();
        await InvokeAsync(StateHasChanged);
        await LocalStorageService.RemoveItem("renew");
    }

   
    private async void UpdateFromCache()
    {
        var ex = await LocalStorageService.GetString("ex_date");
        if (ex != null)
        {
            _exDate = DateTime.ParseExact(ex,"dd.MM.yyyy HH:mm:ss",CultureInfo.InvariantCulture);
            var now = DateTime.Now;
            _refreshTime = _exDate - now;
            if (_refreshTime.TotalSeconds <= 0)
                RenewUser();
            else
            {
                User = await LocalStorageService.GetItem<User>("my_user");
                if (User == null)
                {
                    RenewUser();
                }
            }
            _timer.Enabled = true;
        }
        else
        {
            RenewUser();
        }
    }
    
    protected override async Task OnInitializedAsync()
    {
        _timer.Elapsed += CountDownTimer;
        _timer.Enabled = false;
        UpdateFromCache();
    }
    

    public void CountDownTimer(object? source, ElapsedEventArgs e)
    {
        _refreshTime = _exDate - DateTime.Now;

        if (_refreshTime.TotalSeconds <= 0)
        {
            _timer.Enabled = false;
            RenewUser(User.Id);
        }

        _needProlong = _refreshTime < TimeSpan.FromSeconds(30);
        InvokeAsync(StateHasChanged);
    }
}