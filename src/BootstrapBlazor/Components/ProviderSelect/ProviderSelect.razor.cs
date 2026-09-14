// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provider dropdown for toolbars and forms — lists the given <see cref="ProviderModelSet"/>s
/// with optional online status, bound to a provider name.
/// </summary>
public sealed partial class ProviderSelect
{
    /// <summary>Provider sets to pick from.</summary>
    [Parameter]
    public IEnumerable<ProviderModelSet> Providers { get; set; } = [];

    /// <summary>Selected provider name — matches <see cref="ProviderModelSet.Name"/>.</summary>
    [Parameter]
    public string? SelectedProvider { get; set; }

    /// <summary>Raised when the user picks a provider.</summary>
    [Parameter]
    public EventCallback<string?> SelectedProviderChanged { get; set; }

    /// <summary>Shows the label above the select.</summary>
    [Parameter]
    public bool ShowLabel { get; set; }

    /// <summary>Label text.</summary>
    [Parameter]
    public string LabelText { get; set; } = "Provider";

    /// <summary>Placeholder shown when nothing is selected.</summary>
    [Parameter]
    public string PlaceHolder { get; set; } = "Select provider...";

    /// <summary>Appends the online/offline status to each item's text.</summary>
    [Parameter]
    public bool ShowStatus { get; set; } = true;

    /// <summary>Select size.</summary>
    [Parameter]
    public Size Size { get; set; } = Size.Small;

    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

    private List<SelectedItem> _items = [];

    private string? SelectedValue
    {
        get => SelectedProvider;
        set
        {
            if (value is not null && value != SelectedProvider)
            {
                SelectedProvider = value;
                _ = SelectedProviderChanged.InvokeAsync(value);
            }
        }
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        _items = Providers
            .Where(p => !string.IsNullOrEmpty(p.Name))
            .DistinctBy(p => p.Name, StringComparer.OrdinalIgnoreCase)
            .Select(p =>
            {
                var status = ShowStatus ? $" ({(p.IsOnline ? "Online" : "Offline")})" : "";
                return new SelectedItem(p.Name, $"{(string.IsNullOrEmpty(p.DisplayName) ? p.Name : p.DisplayName)}{status}");
            })
            .ToList();
    }
}
