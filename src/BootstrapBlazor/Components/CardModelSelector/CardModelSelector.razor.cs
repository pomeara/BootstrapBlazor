// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Card-based model selector — provider tiles on top, a grid of model cards below, each
/// showing price/size/quantization/context badges. Inherits the shared selection policy
/// from <see cref="ModelSelectorBase"/>; with <see cref="ModelSelectorBase.AutoSelect"/> on,
/// an unset provider falls to the favourite-online set and an unset model to the
/// favourite/free model, so the selector is never left empty against populated data.
/// </summary>
public sealed partial class CardModelSelector
{
    /// <summary>Shows the label row above the provider cards.</summary>
    [Parameter]
    public bool ShowLabel { get; set; } = true;

    /// <summary>Label text.</summary>
    [Parameter]
    public string LabelText { get; set; } = "Select Model";

    /// <summary>Shows the refresh button; click raises <see cref="OnRefresh"/>.</summary>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>Raised when the user clicks refresh — the host re-feeds <see cref="ModelSelectorBase.Providers"/>.</summary>
    [Parameter]
    public EventCallback OnRefresh { get; set; }

    /// <summary>True while the host is re-fetching provider/model data — disables refresh and shows a spinner.</summary>
    [Parameter]
    public bool IsLoading { get; set; }

    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

    private Task RefreshAsync() => OnRefresh.InvokeAsync();

    /// <summary>Offline tiles are visible but not selectable — ignores the click.</summary>
    private void SelectProviderIfOnline(string name)
    {
        var set = Sets.FirstOrDefault(p => p.Name == name);
        if (set is null || set.IsOnline)
        {
            SelectProvider(name);
        }
    }
}
