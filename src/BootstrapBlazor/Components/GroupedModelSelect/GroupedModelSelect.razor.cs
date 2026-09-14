// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Family-grouped model dropdown for one provider — models grouped by
/// <see cref="ProviderModelEntry.Family"/>, items carry per-model atoms (favourite star, ELO,
/// tier, state) via the base <see cref="ModelSelectorBase"/> filtering and auto-select policy.
/// </summary>
public sealed partial class GroupedModelSelect
{
    /// <summary>Shows the label above the select.</summary>
    [Parameter]
    public bool ShowLabel { get; set; }

    /// <summary>Label text.</summary>
    [Parameter]
    public string LabelText { get; set; } = "Model";

    /// <summary>Placeholder shown when nothing is selected.</summary>
    [Parameter]
    public string PlaceHolder { get; set; } = "Select model...";

    /// <summary>Select size.</summary>
    [Parameter]
    public Size Size { get; set; } = Size.Small;

    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

    private List<SelectedItem> _items = [];

    private string? SelectedValue
    {
        get => SelectedModel;
        set
        {
            if (value is not null && ModelsById.TryGetValue(value, out var model) && model.Id != SelectedModel)
            {
                SelectModel(model);
            }
        }
    }

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _items = CurrentModels
            .OrderBy(m => GroupKeyOf(m), StringComparer.OrdinalIgnoreCase)
            .ThenBy(m => m.Id, StringComparer.OrdinalIgnoreCase)
            .Select(m =>
            {
                var text = ShortenModelName(m.Id);
                if (m.ContextWindow.HasValue)
                {
                    text += $" ({FormatTokens(m.ContextWindow)})";
                }
                return new SelectedItem(m.Id, text) { GroupName = GroupKeyOf(m) };
            })
            .ToList();
    }
}
