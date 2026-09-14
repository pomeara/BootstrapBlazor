// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Renders a collection as Bootstrap badges with a "+X more" overflow indicator.
/// </summary>
public sealed partial class BadgeList
{
    /// <summary>
    /// Items to display as badges
    /// </summary>
    [Parameter]
    public IEnumerable<object>? Items { get; set; }

    /// <summary>
    /// Maximum number of badges to show before the "+X more" indicator. Default 5.
    /// </summary>
    [Parameter]
    public int MaxItems { get; set; } = 5;

    /// <summary>
    /// Badge color class, e.g. "bg-info", "bg-success", "bg-warning text-dark". Default "bg-info".
    /// </summary>
    [Parameter]
    public string BadgeColor { get; set; } = "bg-info";

    /// <summary>
    /// Property name to extract display text from complex objects
    /// </summary>
    [Parameter]
    public string? DisplayProperty { get; set; }

    private IEnumerable<object> DisplayItems => Items?.Take(MaxItems) ?? Enumerable.Empty<object>();

    private int RemainingCount => (Items?.Count() ?? 0) - MaxItems;

    private string? GetItemName(object item)
    {
        if (string.IsNullOrEmpty(DisplayProperty))
            return item?.ToString();

        var prop = item?.GetType().GetProperty(DisplayProperty);
        return prop?.GetValue(item)?.ToString();
    }
}
