using System.Timers;
using Freenymous.Data;
using Freenymous.Data.Models;
using Freenymous.Data.Topics;
using Freenymous.Front.Controls;
using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Pages;

public partial class Index
{
    [Parameter]
    public string? SearchStr { get; set; }
    
  private List<TopicModel> _topics = new List<TopicModel>();

  protected override async Task OnParametersSetAsync()
  {
      InitSearch();
  }

  private void Search()
  {
      NavigationManager.NavigateTo(string.IsNullOrEmpty(SearchStr) ? "/" : $"/Search/{SearchStr}");
      InitSearch();
  }

  private async void InitSearch()
  {
      _totalLoad = 10;
      if (string.IsNullOrEmpty(SearchStr))
      {
          _topics = (await TopicService.Get())?.ToList() ?? new List<TopicModel>();
          _totalTopics = await TopicService.Count();
      }
      else
      {
          _topics = (await TopicService.Get(SearchStr))?.ToList() ?? new List<TopicModel>();
          _totalTopics = await TopicService.Count(SearchStr);
      }

  
      _canLoadMore = _totalLoad < _totalTopics;
      await InvokeAsync(StateHasChanged);
  }
  
  protected override async Task OnInitializedAsync()
  {
       InitSearch();

  }
  
  private bool _canLoadMore { get; set; }
  private int _totalLoad { get; set; } = 10;
  
  private int _totalTopics { get; set; } 
    
  private async void LoadMore()
  {
      if (string.IsNullOrEmpty(SearchStr))
          _topics.AddRange((await TopicService.Get(_totalLoad, 10))?.ToList()?? new List<TopicModel>());
      else
          _topics.AddRange((await TopicService.Get(SearchStr,_totalLoad, 10))?.ToList()?? new List<TopicModel>());
     
      _totalLoad += 10;
      _canLoadMore = _totalLoad < _totalTopics;
      await InvokeAsync(StateHasChanged);
  }
}