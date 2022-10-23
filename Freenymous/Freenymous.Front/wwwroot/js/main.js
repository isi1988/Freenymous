window.blazor_setExitEvent = function (dotNetHelper) {
    blazor_dotNetHelper = dotNetHelper;
    window.addEventListener("beforeunload", blazor_SetSPAClosed);
}

var blazor_dotNetHelper;

window.blazor_SetSPAClosed = function() {
    blazor_dotNetHelper.invokeMethodAsync('SPASessionClosed')
    blazor_dotNetHelper.dispose();
}
