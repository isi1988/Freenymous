using Microsoft.AspNetCore.Components;

namespace Freenymous.Front.Controls;

public partial class Disclaimer
{
    [Parameter] public bool Collapsed { get; set; }
    [Parameter] public string Title { get; set; }
    [Parameter] public string Value { get; set; }
 
    void Toggle()
    {
        Collapsed = !Collapsed;
    }
}