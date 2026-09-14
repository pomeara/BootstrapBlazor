// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact provider tile grid (g4f.dev/#/providers style) — one clickable icon chip per
/// provider. Click raises <see cref="SelectedProviderChanged"/> with the provider name
/// (two-way bindable); offline providers render dimmed and ignore clicks. Pure UI over
/// <see cref="ProviderModelSet"/> — the host owns the catalog.
/// </summary>
public sealed partial class ProviderSelectorSlim
{
    /// <summary>Provider sets to show as chips.</summary>
    [Parameter]
    public IEnumerable<ProviderModelSet> Providers { get; set; } = [];

    /// <summary>Selected provider name — matches <see cref="ProviderModelSet.Name"/>.</summary>
    [Parameter]
    public string? SelectedProvider { get; set; }

    /// <summary>Raised when the user clicks a chip.</summary>
    [Parameter]
    public EventCallback<string?> SelectedProviderChanged { get; set; }

    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

    private IEnumerable<ProviderModelSet> Sets =>
        Providers.Where(p => !string.IsNullOrEmpty(p.Name)).DistinctBy(p => p.Name, StringComparer.OrdinalIgnoreCase);

    private static string ChipTitle(ProviderModelSet set)
    {
        var name = string.IsNullOrEmpty(set.DisplayName) ? set.Name : set.DisplayName;
        var title = $"{name} · {set.Models.Count} models";
        return set.IsOnline ? title : $"{title} · Offline";
    }

    private Task SelectAsync(ProviderModelSet set) =>
        set.IsOnline ? SelectedProviderChanged.InvokeAsync(set.Name) : Task.CompletedTask;
}
