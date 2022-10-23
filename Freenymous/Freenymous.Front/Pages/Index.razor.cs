using System.Timers;
using Freenymous.Data;
using Freenymous.Data.Models;
using Freenymous.Data.Topics;
using Freenymous.Front.Controls;

namespace Freenymous.Front.Pages;

public partial class Index
{
  private IEnumerable<TopicModel> _topics = new List<TopicModel>();

  protected override async Task OnInitializedAsync()
  {
      _topics = await TopicService.Get();
  }
}