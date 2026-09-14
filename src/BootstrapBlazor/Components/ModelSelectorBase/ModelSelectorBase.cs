// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Base class for model selector components — carries the selection POLICY shared by
/// <see cref="ModelSelect"/> and <see cref="GroupedModelSelect"/>: provider set resolution,
/// duplicate-id filtering, free-when-available narrowing, disabled-state hiding, and
/// favourite-first auto-selection. Data arrives via the <see cref="Providers"/> parameter
/// (list of <see cref="ProviderModelSet"/>) — the base never touches a catalog store, so hosts
/// can feed it from any source; the never-empty-selector invariant is preserved by
/// <see cref="DefaultModel"/> falling through favourite → free → enabled → first.
/// </summary>
public abstract class ModelSelectorBase : BootstrapComponentBase
{
    /// <summary>
    /// Provider sets to pick from — each carries its name, online/favourite flags and models.
    /// </summary>
    [Parameter]
    public IEnumerable<ProviderModelSet> Providers { get; set; } = [];

    /// <summary>Selected provider name — matches <see cref="ProviderModelSet.Name"/>.</summary>
    [Parameter]
    public string? Provider { get; set; }

    /// <summary>Selected model id — matches <see cref="ProviderModelEntry.Id"/>.</summary>
    [Parameter]
    public string? SelectedModel { get; set; }

    /// <summary>Raised when the user (or auto-select) picks a model.</summary>
    [Parameter]
    public EventCallback<string?> SelectedModelChanged { get; set; }

    /// <summary>When true (default), an unset or invalid selection auto-picks the first usable
    /// model of the current provider so hosts never hold a null selection against a populated
    /// provider. Set false to keep a deliberately empty rest state.</summary>
    [Parameter]
    public bool AutoSelect { get; set; } = true;

    /// <summary>Resolved set for the current <see cref="Provider"/> — null when unset or unknown.</summary>
    protected ProviderModelSet? CurrentSet { get; private set; }

    /// <summary>Usable models of <see cref="CurrentSet"/> after filtering.</summary>
    protected List<ProviderModelEntry> CurrentModels { get; private set; } = [];

    /// <summary>Lookup so item templates can render per-model atoms (favourite, ELO, tier, state).</summary>
    protected Dictionary<string, ProviderModelEntry> ModelsById { get; private set; } = new(StringComparer.OrdinalIgnoreCase);

    private string? _lastProvider;

    /// <summary>True when the current <see cref="SelectedModel"/> is missing from <see cref="CurrentModels"/>.</summary>
    protected bool SelectionInvalid =>
        string.IsNullOrEmpty(SelectedModel) || !ModelsById.ContainsKey(SelectedModel!);

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        if (Provider == _lastProvider && CurrentSet is not null)
        {
            return;
        }

        _lastProvider = Provider;
        CurrentSet = FindSet(Providers, Provider);
        CurrentModels = CurrentSet is null ? [] : FilterModels(CurrentSet.Models);
        ModelsById = CurrentModels.ToDictionary(m => m.Id, StringComparer.OrdinalIgnoreCase);

        if (AutoSelect && SelectionInvalid && CurrentModels.Count > 0)
        {
            SelectModel(DefaultModel(CurrentModels));
        }
    }

    /// <summary>Applies the selection and raises <see cref="SelectedModelChanged"/>.</summary>
    protected void SelectModel(ProviderModelEntry? model)
    {
        SelectedModel = model?.Id;
        _ = SelectedModelChanged.InvokeAsync(SelectedModel);
    }

    /// <summary>Finds a set by name (case-insensitive); duplicate ids resolve to the first occurrence.</summary>
    protected static ProviderModelSet? FindSet(IEnumerable<ProviderModelSet> providers, string? name) =>
        string.IsNullOrEmpty(name)
            ? null
            : providers.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Narrows a provider's models for selector display: blank/unknown ids dropped, duplicate ids
    /// removed (first wins), disabled models hidden WHEN other models exist, and the list narrowed
    /// to free models only when the provider has any — the selector is never emptied by a filter.
    /// </summary>
    protected static List<ProviderModelEntry> FilterModels(IEnumerable<ProviderModelEntry> models)
    {
        var list = models
            .Where(m => !string.IsNullOrEmpty(m.Id))
            .DistinctBy(m => m.Id, StringComparer.OrdinalIgnoreCase)
            .ToList();

        var usable = list.Where(IsModelEnabled).ToList();
        if (usable.Count > 0)
        {
            list = usable;
        }

        var free = list.Where(m => m.IsFree).ToList();
        if (free.Count > 0)
        {
            list = free;
        }

        return list;
    }

    /// <summary>True when the entry is enabled (missing state = enabled).</summary>
    protected static bool IsModelEnabled(ProviderModelEntry m) =>
        string.IsNullOrEmpty(m.State) || m.State.Equals("enabled", StringComparison.OrdinalIgnoreCase);

    /// <summary>Default model pick: favourite, else first FREE model, else first — keeps the
    /// initial selection cost-free when possible without emptying the selector.</summary>
    protected static ProviderModelEntry? DefaultModel(List<ProviderModelEntry> models) =>
        models.FirstOrDefault(m => m.Favourite)
        ?? models.FirstOrDefault(m => m.IsFree)
        ?? models.FirstOrDefault();

    /// <summary>Default provider pick: favourite online provider, else first online, else first.</summary>
    protected static ProviderModelSet? DefaultSet(IEnumerable<ProviderModelSet> providers)
    {
        var list = providers.ToList();
        return list.FirstOrDefault(p => p.IsFavourite && p.IsOnline)
            ?? list.FirstOrDefault(p => p.IsOnline)
            ?? list.FirstOrDefault();
    }

    /// <summary>Group key for grouped selectors — <see cref="ProviderModelEntry.Family"/> or "Other".</summary>
    protected static string GroupKeyOf(ProviderModelEntry m) =>
        string.IsNullOrWhiteSpace(m.Family) ? "Other" : m.Family;

    /// <summary>Shortens a model id for display — the segment after the last "/" (e.g.
    /// "meta-llama/llama-3.2" → "llama-3.2").</summary>
    protected static string ShortenModelName(string id)
    {
        var slash = id.LastIndexOf('/');
        return slash >= 0 && slash < id.Length - 1 ? id[(slash + 1)..] : id;
    }

    /// <summary>Formats a token count as a human string (e.g. 131072 → "131K").</summary>
    protected static string FormatTokens(int? tokens) => tokens switch
    {
        null => "",
        >= 1_000_000 => $"{tokens.Value / 1_000_000}M",
        >= 1_000 => $"{tokens.Value / 1_000}K",
        _ => tokens.Value.ToString()
    };

    /// <summary>Formats a price per million tokens (e.g. 0.9 → "$0.90", 0.001 → "$0.0010").</summary>
    protected static string FormatPrice(decimal? price) => price switch
    {
        null or 0 => "Free",
        < 0.01m => $"${price.Value:F4}",
        _ => $"${price.Value:F2}"
    };

    /// <summary>Builds the dropdown text for a model: display name plus context-window and,
    /// when <paramref name="showPricing"/> is set, the input-price detail.</summary>
    protected static string FormatModelDetail(ProviderModelEntry model, bool showPricing)
    {
        var text = string.IsNullOrEmpty(model.DisplayName) ? ShortenModelName(model.Id) : model.DisplayName;
        var parts = new List<string> { text };
        if (model.ContextWindow.HasValue)
        {
            parts.Add($"{FormatTokens(model.ContextWindow)} ctx");
        }
        if (showPricing && !model.IsFree && model.PricePerMillionInput.HasValue)
        {
            parts.Add(FormatPrice(model.PricePerMillionInput));
        }
        return string.Join(" · ", parts);
    }
}
