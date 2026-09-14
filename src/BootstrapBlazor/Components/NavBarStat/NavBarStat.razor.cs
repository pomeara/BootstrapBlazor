// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact icon + value + label statistic for placement inside navigation bars and toolbars.
/// </summary>
public sealed partial class NavBarStat
{
    /// <summary>
    /// The statistic value to display
    /// </summary>
    [Parameter]
    public object? Value { get; set; }

    /// <summary>
    /// Label text
    /// </summary>
    [Parameter]
    public string Label { get; set; } = "";

    /// <summary>
    /// FontAwesome icon (without the fa-solid prefix)
    /// </summary>
    [Parameter]
    public string Icon { get; set; } = "";

    /// <summary>
    /// Color theme
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    private string ColorClass => Color switch
    {
        Color.Success => "success",
        Color.Info => "info",
        Color.Warning => "warning",
        Color.Danger => "danger",
        Color.Primary => "primary",
        Color.Secondary => "secondary",
        _ => "primary"
    };

    private string? ClassString => CssBuilder.Default("navbar-stat me-3")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
