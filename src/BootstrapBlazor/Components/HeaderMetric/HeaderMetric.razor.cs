// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact label + value pair for page headers and toolbars.
/// </summary>
public sealed partial class HeaderMetric
{
    /// <summary>
    /// Metric label rendered above the value
    /// </summary>
    [Parameter]
    public string Label { get; set; } = "";

    /// <summary>
    /// Metric value
    /// </summary>
    [Parameter]
    public string Value { get; set; } = "";

    /// <summary>
    /// Optional color class applied to the value element (e.g. "text-warning")
    /// </summary>
    [Parameter]
    public string ValueColor { get; set; } = "";

    private string? ClassString => CssBuilder.Default("header-metric")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
