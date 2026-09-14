// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact card for inline display in navbars and headers.
/// </summary>
public sealed partial class MiniCard
{
    /// <summary>
    /// Gets or sets the card title.
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "";

    /// <summary>
    /// Gets or sets the FontAwesome icon class.
    /// </summary>
    [Parameter]
    public string Icon { get; set; } = "";

    /// <summary>
    /// Gets or sets the color variant: primary, secondary, success, warning, info, danger, dark, or outline.
    /// </summary>
    [Parameter]
    public string Color { get; set; } = "secondary";

    /// <summary>
    /// Gets or sets the child content rendered after the title.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string ColorClass => Color switch
    {
        "primary" => "primary",
        "success" => "success",
        "warning" => "warning",
        "info" => "info",
        "danger" => "danger",
        "dark" => "dark",
        "outline" => "outline",
        _ => "secondary"
    };

    private string? ClassString => CssBuilder.Default("mini-card")
        .AddClass($"mini-card--{ColorClass}")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
