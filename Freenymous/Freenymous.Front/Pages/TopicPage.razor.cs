using Freenymous.Data.Topics;
using Freenymous.Front.Controls;
using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Pages;

public partial class TopicPage
{
    [Parameter]
    public long Id { get; set; }

    private Topic? _topic;

    [CascadingParameter]
    public MyUser? MyUser { get; set; }

    private Editor _richEditor;
    
    protected override async Task OnInitializedAsync()
    {
        _topic = await TopicService.Get(Id);
        if (_topic != null)
        {
            _comments = (await TopicService.Comments(_topic.Id))?.ToList()??new List<Comment>();
        }
    }

    private Comment _comment = new Comment();

    private List<Comment> _comments = new List<Comment>();

    private async void Save()
    {
        await InvokeAsync(StateHasChanged);
        _comment.Html = await _richEditor.GetHtml();
        _comment.UserId = MyUser?.User?.Id??0;
        _comment.TopicId = _topic.Id;
        _comment = await TopicService.Comment(_comment);
        _comments.Insert(0, _comment);
        _comment = new Comment();
        _richEditor?.Clear();
        await InvokeAsync(StateHasChanged);
    }
}