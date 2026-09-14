// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Settings row cell — the standard compact layout for settings panels, TWO settings per
/// rendered row (each cell is <c>col-12 col-md-6</c> inside a panel <c>row</c>): the label
/// (and an optional info-tooltip) sits on the left of the cell, the control on the right.
/// Wrap runs of rows in <c>&lt;div class="row g-2"&gt;</c>. Long explanatory text belongs in
/// <see cref="Help"/> (hover tooltip), never as a third line of <c>form-text</c> under the
/// control. Set <see cref="Full"/> for a wide control that needs the entire row.
/// </summary>
public sealed partial class SettingRow
{
    /// <summary>
    /// Gets or sets the setting label, left-aligned within the cell.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string Label { get; set; } = "";

    /// <summary>
    /// Gets or sets the hover tooltip explaining the setting — replaces multi-line form-text.
    /// </summary>
    [Parameter]
    public string? Help { get; set; }

    /// <summary>
    /// Gets or sets the control (Switch, input, select, choice group…).
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to span the full row (col-12) instead of
    /// the default half — for wide controls.
    /// </summary>
    [Parameter]
    public bool Full { get; set; }

    private string? ClassString => CssBuilder.Default("setting-cell")
        .AddClass("col-12", Full)
        .AddClass("col-12 col-md-6", !Full)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
