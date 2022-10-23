using Freenymous.Data.Models;
using Freenymous.Data.Topics;
using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Controls;

public partial class CommentCard
{
    [Parameter]
    public Comment Comment { get; set; }
}