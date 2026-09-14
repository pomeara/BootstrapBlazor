// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Segmented button-group selector for small choice sets (log levels, nav modes, tab styles).
/// Renders every option as a button — one click, no dropdown — with the active choice highlighted.
/// </summary>
public sealed partial class ChoiceGroup
{
    /// <summary>
    /// Gets or sets the accessible group label.
    /// </summary>
    [Parameter]
    public string Label { get; set; } = "";

    /// <summary>
    /// Gets or sets the choices to render, in order.
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the currently selected value.
    /// </summary>
    [Parameter]
    public string Value { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback raised when a choice is clicked.
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    private string? ClassString => CssBuilder.Default("btn-group btn-group-sm choice-group")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task Select(string value)
    {
        if (string.Equals(Value, value, StringComparison.OrdinalIgnoreCase)) return;
        Value = value;
        await ValueChanged.InvokeAsync(value);
    }
}
