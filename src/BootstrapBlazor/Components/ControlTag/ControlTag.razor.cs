// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Click-to-copy identifier badge. Renders a tag showing a name; clicking copies it
/// to the clipboard. An optional category label renders as a sub-badge.
/// </summary>
public sealed partial class ControlTag
{
    /// <summary>
    /// Name shown and copied to the clipboard on click
    /// </summary>
    [Parameter]
    public string Name { get; set; } = "";

    /// <summary>
    /// Optional category label rendered as a sub-badge (e.g. "Organism")
    /// </summary>
    [Parameter]
    public string? Category { get; set; }

    [Inject]
    private IJSRuntime JS { get; set; } = null!;

    [Inject]
    private ILogger<ControlTag> Logger { get; set; } = null!;

    private bool _copied;

    private async Task CopyToClipboard()
    {
        try
        {
            await JS.InvokeVoidAsync("navigator.clipboard.writeText", Name);
            _copied = true;
            StateHasChanged();
            await Task.Delay(1000);
            _copied = false;
        }
        catch (JSDisconnectedException)
        {
            // Circuit disconnected mid-copy — expected during shutdown, not a clipboard failure.
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Clipboard API unavailable for ControlTag copy");
        }
    }
}
