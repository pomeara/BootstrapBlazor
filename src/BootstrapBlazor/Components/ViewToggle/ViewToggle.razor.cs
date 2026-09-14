// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Toggle between two view modes (e.g. Card vs Table).
/// Supports <see cref="ViewToggleMode.DualButton"/> (two visible buttons)
/// and <see cref="ViewToggleMode.SingleButton"/> (one toggle button).
/// </summary>
public sealed partial class ViewToggle
{
    /// <summary>
    /// Gets or sets the currently active view identifier.
    /// </summary>
    [Parameter]
    public string CurrentView { get; set; } = "card";

    /// <summary>
    /// Gets or sets the callback fired when the user selects a different view.
    /// </summary>
    [Parameter]
    public EventCallback<string> CurrentViewChanged { get; set; }

    /// <summary>
    /// Gets or sets the display mode — two buttons or single toggle.
    /// </summary>
    [Parameter]
    public ViewToggleMode Mode { get; set; } = ViewToggleMode.DualButton;

    /// <summary>
    /// Gets or sets the identifier for the primary view (default: "card").
    /// </summary>
    [Parameter]
    public string PrimaryView { get; set; } = "card";

    /// <summary>
    /// Gets or sets the FontAwesome icon for the primary view.
    /// </summary>
    [Parameter]
    public string PrimaryIcon { get; set; } = "fa-solid fa-grip";

    /// <summary>
    /// Gets or sets the tooltip for the primary view button.
    /// </summary>
    [Parameter]
    public string PrimaryTitle { get; set; } = "Card View";

    /// <summary>
    /// Gets or sets the identifier for the secondary view (default: "row").
    /// </summary>
    [Parameter]
    public string SecondaryView { get; set; } = "row";

    /// <summary>
    /// Gets or sets the FontAwesome icon for the secondary view.
    /// </summary>
    [Parameter]
    public string SecondaryIcon { get; set; } = "fa-solid fa-list";

    /// <summary>
    /// Gets or sets the tooltip for the secondary view button.
    /// </summary>
    [Parameter]
    public string SecondaryTitle { get; set; } = "Table View";

    private string? ClassString => CssBuilder.Default("view-toggle")
        .AddClass("btn-group", Mode == ViewToggleMode.DualButton)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task SetView(string view)
    {
        if (view == CurrentView) return;
        CurrentView = view;
        await CurrentViewChanged.InvokeAsync(view);
    }
}
