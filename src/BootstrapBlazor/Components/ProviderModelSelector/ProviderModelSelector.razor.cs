// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provider + model dropdown pair sharing one selection policy via <see cref="ModelSelectorBase"/> —
/// picking a provider re-resolves its models and (with auto-select on) picks the default one.
/// Data comes entirely from the <see cref="ModelSelectorBase.Providers"/> parameter; refresh is a
/// host callback, so the component never touches a catalog store.
/// </summary>
public sealed partial class ProviderModelSelector
{
    /// <summary>Shows the label above the dropdown pair.</summary>
    [Parameter]
    public bool ShowLabel { get; set; } = true;

    /// <summary>Label text.</summary>
    [Parameter]
    public string LabelText { get; set; } = "Connection";

    /// <summary>Shows the refresh button; click raises <see cref="OnRefresh"/>.</summary>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>Raised when the user clicks refresh — the host re-feeds <see cref="ModelSelectorBase.Providers"/>.</summary>
    [Parameter]
    public EventCallback OnRefresh { get; set; }

    /// <summary>Disables both dropdowns.</summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>True while the host is re-fetching data — disables the model dropdown and refresh.</summary>
    [Parameter]
    public bool IsLoading { get; set; }

    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

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
            return new SelectedItem(p.Name, $"{name} ({(p.IsOnline ? "Online" : "Offline")})");
        }).ToList();
        _modelItems = CurrentModels.Select(m =>
        {
            var parts = new List<string> { m.Id };
            if (m.SizeBytes.HasValue)
            {
                parts.Add(FormatSize(m.SizeBytes));
            }
            if (!string.IsNullOrEmpty(m.ParameterCount))
            {
                parts.Add(m.ParameterCount);
            }
            return new SelectedItem(m.Id, string.Join(" · ", parts));
        }).ToList();
    }

    private Task RefreshAsync() => OnRefresh.InvokeAsync();
}
