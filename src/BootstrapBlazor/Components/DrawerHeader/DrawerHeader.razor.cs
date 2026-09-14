// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer header with title, optional pin, and close actions.
/// </summary>
public sealed partial class DrawerHeader
{
    /// <summary>
    /// Gets or sets the header title.
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "";

    /// <summary>
    /// Gets or sets the optional FontAwesome icon class.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the pin toggle button.
    /// </summary>
    [Parameter]
    public bool ShowPin { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the drawer is currently pinned.
    /// </summary>
    [Parameter]
    public bool IsPinned { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when the close button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnClose { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when the pin button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnPin { get; set; }

    /// <summary>
    /// Gets or sets the pin button tooltip text.
    /// </summary>
    [Parameter]
    public string PinTooltipText { get; set; } = "Pin";

    /// <summary>
    /// Gets or sets the close button tooltip text.
    /// </summary>
    [Parameter]
    public string CloseTooltipText { get; set; } = "Close";

    private string? ClassString => CssBuilder.Default("drawer-header")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private Task HandleClose() => OnClose.InvokeAsync();

    private Task HandlePin() => OnPin.InvokeAsync();
}
