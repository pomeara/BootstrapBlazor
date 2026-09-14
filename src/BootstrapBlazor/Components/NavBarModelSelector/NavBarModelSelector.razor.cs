// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact provider/model selector sized for nav bars and page headers — two small
/// dropdowns plus a READY/OFFLINE badge. Shares the selection policy and auto-select
/// behaviour of <see cref="ModelSelectorBase"/>; like the other selectors it is fully
/// parameter-driven and never touches a catalog store.
/// </summary>
public sealed partial class NavBarModelSelector
{
    /// <summary>Shrinks padding/font so the pair fits a slim navbar.</summary>
    [Parameter]
    public bool IsCompact { get; set; }

    /// <summary>Shows the READY/OFFLINE badge for the active provider.</summary>
    [Parameter]
    public bool ShowStatusBadge { get; set; } = true;

    /// <summary>True while the host is re-fetching data — disables both dropdowns.</summary>
    [Parameter]
    public bool IsLoading { get; set; }

    private List<SelectedItem> _providerItems = [];
    private List<SelectedItem> _modelItems = [];

    private string? ProviderValue
    {
        get => Provider;
        set
        {
            if (value is not null)
            {
                SelectProvider(value);
            }
        }
    }

    private string? ModelValue
    {
        get => SelectedModel;
        set
        {
            if (value is null)
            {
                return;
            }
            var model = CurrentModels.FirstOrDefault(m => m.Id == value);
            if (model is not null)
            {
                SelectModel(model);
            }
        }
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _providerItems = Sets.Select(p =>
        {
            var name = string.IsNullOrEmpty(p.DisplayName) ? p.Name : p.DisplayName;
            return new SelectedItem(p.Name, $"{name} ({(p.IsOnline ? "ON" : "OFF")})");
        }).ToList();
        _modelItems = CurrentModels.Select(m =>
        {
            var size = m.SizeBytes.HasValue ? $" ({FormatSize(m.SizeBytes)})" : "";
            return new SelectedItem(m.Id, $"{m.Id}{size}");
        }).ToList();
    }
}
