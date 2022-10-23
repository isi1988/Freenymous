using Freenymous.Data.Topics;
using Freenymous.Data.Users;
using Freenymous.Front.Controls;
using Freenymous.Front.Services;
using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Pages;

public partial class TopicPage
{
    [Parameter]
    public long Id { get; set; }

    private Topic? _topic;

    private Editor _richEditor;

    private User? _user = new User();
    
    protected override async Task OnInitializedAsync()
    {
        _topic = await TopicService.Get(Id);
        if (_topic != null)
        {
            _comments = (await TopicService.Comments(_topic.Id))?.ToList()??new List<Comment>();
            _canLoadMore = _totalLoad < _topic.CommentTopLevelCount;
        }
    }

    protected override async Task OnParametersSetAsync()
    {
        _user = await LocalStorageService.GetItem<User>("my_user");
    }

    private Comment _comment = new Comment();

    private List<Comment> _comments = new List<Comment>();

    private async void Save()
    {       
        await InvokeAsync(StateHasChanged);
        _user = await LocalStorageService.GetItem<User>("my_user");
        if (_user == null)
            return;
        if (_user.Id == 0 || string.IsNullOrEmpty(_user.AccessCode))
            return;

        _comment.Html = await _richEditor.GetHtml();
        _comment.AccessCode = _user.AccessCode;
        _comment.UserId = _user?.Id;
        _comment.TopicId = _topic?.Id;
        _comment = await TopicService.Comment(_comment);
        _comments.Insert(0, _comment);
        _comment = new Comment();
        _richEditor?.Clear();
        await InvokeAsync(StateHasChanged);
    }
    
    private bool _canLoadMore { get; set; }
    private int _totalLoad { get; set; } = 10;
    
    private async void LoadMore()
    {
       _comments.AddRange(await TopicService.Comments(_topic.Id,_totalLoad,10));
       _totalLoad += 10;
       _canLoadMore = _totalLoad < _topic.CommentTopLevelCount;
        await InvokeAsync(StateHasChanged);
    }
}