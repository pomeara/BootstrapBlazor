// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// One-row model picker for toolbars and nav bars — model icon + compact
/// <see cref="Select{TValue}"/> whose items carry favourite/ELO/tier/state atoms, plus an
/// optional ON/OFF status badge for the active provider. Selection policy (auto-pick,
/// disabled hiding, free narrowing) comes from <see cref="ModelSelectorBase"/>.
/// </summary>
public sealed partial class CompactModelPicker
{
    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>Select placeholder shown before a model is picked.</summary>
    [Parameter]
    public string PlaceHolder { get; set; } = "Model...";

    /// <summary>Shows an ON/OFF badge for the active provider.</summary>
    [Parameter]
    public bool ShowStatusBadge { get; set; }

    private List<SelectedItem> _items = [];

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

    private bool CurrentOnline => CurrentSet?.IsOnline ?? false;

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        _items = CurrentModels.Select(m => new SelectedItem(m.Id, FormatModelDetail(m, false))).ToList();
    }
}
