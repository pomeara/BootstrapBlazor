// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Filter button group with optional counts.
/// </summary>
public sealed partial class FilterButtonGroup
{
    /// <summary>
    /// Gets or sets the filter key → display text map.
    /// </summary>
    [Parameter]
    public Dictionary<string, string> Filters { get; set; } = [];

    /// <summary>
    /// Gets or sets the currently selected filter key.
    /// </summary>
    [Parameter]
    public string SelectedFilter { get; set; } = "";

    /// <summary>
    /// Gets or sets the optional filter key → count map appended to each button label.
    /// </summary>
    [Parameter]
    public Dictionary<string, int> Counts { get; set; } = [];

    /// <summary>
    /// Gets or sets the callback fired when a filter is selected.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedFilterChanged { get; set; }

    private string? ClassString => CssBuilder.Default("filter-button-group d-flex flex-wrap gap-1")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private int GetCount(string key) => Counts.TryGetValue(key, out var count) ? count : 0;

    private async Task SelectFilter(string key)
    {
        SelectedFilter = key;
        await SelectedFilterChanged.InvokeAsync(key);
    }
}
