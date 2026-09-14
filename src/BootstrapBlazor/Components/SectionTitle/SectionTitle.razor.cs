// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Renders a section heading with an optional FontAwesome icon and badge content.
/// </summary>
public sealed partial class SectionTitle
{
    /// <summary>
    /// FontAwesome icon class (without the fa-solid prefix)
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Section title text
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "Section";

    /// <summary>
    /// Icon color class. Use the theme-aware classes "section-title-icon-success",
    /// "section-title-icon-warning", "section-title-icon-danger", "section-title-icon-info"
    /// or "section-title-icon-secondary" (default: "section-title-icon-default").
    /// </summary>
    [Parameter]
    public string IconColor { get; set; } = "section-title-icon-default";

    /// <summary>
    /// Additional CSS classes applied to the heading element
    /// </summary>
    [Parameter]
    public string CssClass { get; set; } = "mb-3";

    /// <summary>
    /// Optional badge render fragment rendered after the title
    /// </summary>
    [Parameter]
    public RenderFragment? Badge { get; set; }
}
