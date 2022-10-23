using Freenymous.Data.Models;
using Freenymous.Data.Topics;
using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Controls;

public partial class TopicCard
{
    [Parameter]
    public TopicModel Topic { get; set; }
}