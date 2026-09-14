// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Search input with clear button.
/// </summary>
public sealed partial class SearchInput
{
    /// <summary>
    /// Gets or sets the current query text.
    /// </summary>
    [Parameter]
    public string Query { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the query text changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> QueryChanged { get; set; }

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "Search...";

    /// <summary>
    /// Gets or sets the clear button tooltip text.
    /// </summary>
    [Parameter]
    public string ClearTooltipText { get; set; } = "Clear";

    private string? ClassString => CssBuilder.Default("search-input d-flex align-items-center gap-2")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnQueryChanged(string value)
    {
        Query = value;
        await QueryChanged.InvokeAsync(value);
    }

    private async Task Clear()
    {
        Query = "";
        await QueryChanged.InvokeAsync("");
    }
}
