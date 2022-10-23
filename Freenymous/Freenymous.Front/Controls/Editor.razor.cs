using Microsoft.AspNetCore.Components;
namespace Freenymous.Front.Controls;

public partial class Editor
{
    private string _strSavedContent = "";
    private string? _editorContent;
    private string? _editorHtmlContent;
    private bool _editorEnabled = true;
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await JSRuntime.InvokeAsync<string>(
                "QuillFunctions.createQuill", new []{"divEditorElement"});
        }
    }

    public async Task<string> Clear()
    {
        return await JSRuntime.InvokeAsync<string>(
            "QuillFunctions.loadQuillContent", new []{"divEditorElement"});
    }
    
    public async Task<string> GetHtml()
    {
        return await JSRuntime.InvokeAsync<string>(
            "QuillFunctions.getQuillHTML", new []{"divEditorElement"});
    }
}