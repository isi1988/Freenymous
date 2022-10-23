using Freenymous.Data.Topics;
using Freenymous.Data.Users;
using Freenymous.Front.Controls;
using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Pages;

public partial class NewTopic
{
    [CascadingParameter]
    public MyUser? MyUser { get; set; }

    private Editor _richEditor;
    
    private Topic _topic = new Topic();
    private User? _user = new User();
    
    private async void Save()
    {
        _user = await LocalStorageService.GetItem<User>("my_user");
        if (_user == null)
            return;
        _topic.AccessCode = _user.AccessCode;
        _topic.Html = await _richEditor.GetHtml();
        _topic.UserId = MyUser?.User?.Id??0;
        _topic.Tags = _topic.StrTags.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
        await TopicService.Post(_topic);
        NavigationManager.NavigateTo("/");
    }
}