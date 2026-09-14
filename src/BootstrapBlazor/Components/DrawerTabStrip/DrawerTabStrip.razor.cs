// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Tab strip for multi-panel drawers — renders nothing for a single panel.
/// </summary>
public sealed partial class DrawerTabStrip
{
    /// <summary>
    /// Gets or sets the drawer panels to render as tabs.
    /// </summary>
    [Parameter]
    public IReadOnlyList<DrawerPanel> Panels { get; set; } = [];

    /// <summary>
    /// Gets or sets the active panel key.
    /// </summary>
    [Parameter]
    public string ActivePanel { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when a tab is selected.
    /// </summary>
    [Parameter]
    public EventCallback<string> ActivePanelChanged { get; set; }

    private string? ClassString => CssBuilder.Default("drawer-tab-strip")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetTabClass(DrawerPanel panel) => CssBuilder.Default("drawer-tab")
        .AddClass("active", ActivePanel == panel.Key)
        .Build();

    private async Task SelectPanel(string key)
    {
        ActivePanel = key;
        await ActivePanelChanged.InvokeAsync(key);
    }
}
