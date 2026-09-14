// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Parameter-driven provider/model toolbar with capability filtering — provider select, model
/// select, optional refresh button, and capability filter badges. Fully data-driven via
/// <see cref="Providers"/> for use in standalone apps (no service injection).
/// </summary>
public sealed partial class ProviderModelToolbar
{
    /// <summary>
    /// Gets or sets the providers and their models to pick from.
    /// </summary>
    [Parameter]
    public List<ProviderModelSet> Providers { get; set; } = [];

    /// <summary>
    /// Gets or sets the selected provider name.
    /// </summary>
    [Parameter]
    public string SelectedProvider { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the selected provider changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedProviderChanged { get; set; }

    /// <summary>
    /// Gets or sets the selected model id.
    /// </summary>
    [Parameter]
    public string SelectedModel { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the selected model changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedModelChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the toolbar is in a loading state
    /// (spins the refresh icon and disables the refresh button).
    /// </summary>
    [Parameter]
    public bool IsLoading { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to render the refresh button.
    /// </summary>
    [Parameter]
    public bool ShowRefresh { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to render the capability filter badges.
    /// </summary>
    [Parameter]
    public bool ShowCapabilities { get; set; } = true;

    /// <summary>
    /// Gets or sets the callback fired when the refresh button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnRefresh { get; set; }

    private string CurrentProvider { get; set; } = "";

    private string CurrentModel { get; set; } = "";

    private HashSet<string> ActiveCapabilities { get; set; } = [];

    private static readonly string[] CapabilityFilters = ["text", "vision", "audio", "video", "code", "reasoning"];

    private List<SelectedItem> ProviderItems { get; set; } = [];

    private List<SelectedItem> FilteredModelItems =>
        CurrentProviderSet is not { } provider
            ? []
            : provider.Models
                .Where(m => ActiveCapabilities.Count == 0
                    || ActiveCapabilities.All(cap => m.Capabilities.Contains(cap, StringComparer.OrdinalIgnoreCase)))
                .Select(m => new SelectedItem { Value = m.Id, Text = m.DisplayName ?? m.Id })
                .ToList();

    private ProviderModelSet? CurrentProviderSet => Providers.FirstOrDefault(p =>
        string.Equals(p.Name, CurrentProvider, StringComparison.OrdinalIgnoreCase));

    private string? ClassString => CssBuilder.Default("provider-model-toolbar d-flex align-items-center gap-2")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Syncs the external <see cref="SelectedProvider"/>/<see cref="SelectedModel"/> values and
    /// rebuilds the provider dropdown items.
    /// </summary>
    protected override void OnParametersSet()
    {
        CurrentProvider = SelectedProvider;
        CurrentModel = SelectedModel;

        ProviderItems = Providers
            .Where(p => p.Models.Count > 0)
            .Select(p => new SelectedItem { Value = p.Name, Text = $"{p.DisplayName ?? p.Name} ({p.Models.Count})" })
            .ToList();
    }

    private async Task OnProviderChangedAsync(SelectedItem item)
    {
        CurrentProvider = item.Value;
        CurrentModel = "";
        await SelectedProviderChanged.InvokeAsync(item.Value);
        await SelectedModelChanged.InvokeAsync("");

        var models = FilteredModelItems;
        if (models.Count > 0)
        {
            CurrentModel = models[0].Value;
            await SelectedModelChanged.InvokeAsync(CurrentModel);
        }
    }

    private async Task OnRefreshAsync() => await OnRefresh.InvokeAsync();

    private void ToggleCapability(string cap)
    {
        if (!ActiveCapabilities.Add(cap))
        {
            ActiveCapabilities.Remove(cap);
        }
    }

    private static Color GetCapabilityColor(string cap) => cap.ToLowerInvariant() switch
    {
        "text" => Color.Primary,
        "vision" => Color.Info,
        "audio" => Color.Success,
        "video" => Color.Warning,
        "code" => Color.Danger,
        "reasoning" => Color.Dark,
        _ => Color.Secondary
    };

    private static string GetCapabilityIcon(string cap) => cap.ToLowerInvariant() switch
    {
        "text" => "fa-solid fa-font",
        "vision" => "fa-solid fa-eye",
        "audio" => "fa-solid fa-microphone",
        "video" => "fa-solid fa-video",
        "code" => "fa-solid fa-code",
        "reasoning" => "fa-solid fa-brain",
        _ => "fa-solid fa-circle"
    };
}
