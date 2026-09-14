// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Dark catalog banner header — icon, title, badge, tab pills, and stat/toolbar/action slots.
/// </summary>
public sealed partial class CatalogHeader
{
    /// <summary>
    /// Gets or sets the FontAwesome icon class. Default is the plug icon.
    /// </summary>
    [Parameter]
    public string Icon { get; set; } = "fa-solid fa-plug";

    /// <summary>
    /// Gets or sets the extra CSS class(s) applied to the icon.
    /// </summary>
    [Parameter]
    public string IconColor { get; set; } = "text-info";

    /// <summary>
    /// Gets or sets the header title.
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "Catalog";

    /// <summary>
    /// Gets or sets the optional subtitle rendered next to the title.
    /// </summary>
    [Parameter]
    public string? SubTitle { get; set; }

    /// <summary>
    /// Gets or sets the optional badge text. Blank renders no badge.
    /// </summary>
    [Parameter]
    public string? BadgeText { get; set; }

    /// <summary>
    /// Gets or sets the badge colour.
    /// </summary>
    [Parameter]
    public Color BadgeColor { get; set; } = Color.Success;

    /// <summary>
    /// Gets or sets the primary tab label. Default is "Providers".
    /// </summary>
    [Parameter]
    public string PrimaryTabText { get; set; } = "Providers";

    /// <summary>
    /// Gets or sets the primary tab FontAwesome icon class. Default is the building icon.
    /// </summary>
    [Parameter]
    public string PrimaryTabIcon { get; set; } = "fa-solid fa-building";

    /// <summary>
    /// Gets or sets the secondary tab label. Default is "Models".
    /// </summary>
    [Parameter]
    public string SecondaryTabText { get; set; } = "Models";

    /// <summary>
    /// Gets or sets the secondary tab FontAwesome icon class. Default is the cube icon.
    /// </summary>
    [Parameter]
    public string SecondaryTabIcon { get; set; } = "fa-solid fa-cube";

    /// <summary>
    /// Gets or sets the active tab key ("providers" or "models").
    /// </summary>
    [Parameter]
    public string ActiveTab { get; set; } = "providers";

    /// <summary>
    /// Gets or sets the callback fired when a tab pill is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnTabChanged { get; set; }

    /// <summary>
    /// Gets or sets the stat widgets rendered between the badge and the tabs.
    /// </summary>
    [Parameter]
    public RenderFragment? Stats { get; set; }

    /// <summary>
    /// Gets or sets the filter toolbar rendered after the tabs.
    /// </summary>
    [Parameter]
    public RenderFragment? Toolbar { get; set; }

    /// <summary>
    /// Gets or sets the trailing actions rendered at the end of the header.
    /// </summary>
    [Parameter]
    public RenderFragment? Actions { get; set; }

    private string? ClassString => CssBuilder.Default("catalog-header")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string GetTabClass(string tab) => ActiveTab == tab ? "bg-info text-white" : "bg-secondary";

    private Task SelectTab(string tab) => OnTabChanged.InvokeAsync(tab);
}
