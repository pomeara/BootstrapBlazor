// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Detailed model-card browser with filters (build.nvidia.com/models style) — a filter bar
/// (search + capability toggles + free-only) drives a responsive card grid; each card shows
/// the model's family icon, name, context, capability badges, pricing, and a Select action
/// that raises <see cref="SelectedModelChanged"/>. Pure UI over <see cref="ProviderModelEntry"/>
/// — the host owns the catalog and the selection.
/// </summary>
public sealed partial class ProviderSelectorDetailed
{
    /// <summary>Models to browse.</summary>
    [Parameter]
    public IEnumerable<ProviderModelEntry> Models { get; set; } = [];

    /// <summary>Currently selected model (matched by <see cref="ProviderModelEntry.Id"/>).</summary>
    [Parameter]
    public ProviderModelEntry? SelectedModel { get; set; }

    /// <summary>Raised when the user picks a model — null never raised; deselect is the host's call.</summary>
    [Parameter]
    public EventCallback<ProviderModelEntry?> SelectedModelChanged { get; set; }

    /// <summary>Additional CSS class for the wrapper.</summary>
    [Parameter]
    public string? CssClass { get; set; }

    private string _search = "";
    private bool _freeOnly;

    // Modality filters (text | vision | audio | video) — matched against Capabilities.
    private bool _fText, _fVision, _fAudio, _fVideo;

    // Secondary capability filters (reasoning | tools).
    private bool _fReason, _fTools;

    private bool HasCapability(ProviderModelEntry m, string capability) =>
        m.Capabilities.Contains(capability, StringComparer.OrdinalIgnoreCase);

    private List<ProviderModelEntry> Filtered => Models.Where(m =>
        (string.IsNullOrWhiteSpace(_search)
            || m.Id.Contains(_search, StringComparison.OrdinalIgnoreCase)
            || (m.DisplayName?.Contains(_search, StringComparison.OrdinalIgnoreCase) ?? false)
            || (m.Family?.Contains(_search, StringComparison.OrdinalIgnoreCase) ?? false))
        && (!_freeOnly || m.IsFree)
        && (!_fText || HasCapability(m, "text"))
        && (!_fVision || HasCapability(m, "vision"))
        && (!_fAudio || HasCapability(m, "audio"))
        && (!_fVideo || HasCapability(m, "video"))
        && (!_fReason || HasCapability(m, "reasoning"))
        && (!_fTools || HasCapability(m, "tools"))).ToList();

    private Task SelectAsync(ProviderModelEntry m) => SelectedModelChanged.InvokeAsync(m);

    private static string ShortenModelName(string id)
    {
        var slash = id.LastIndexOf('/');
        return slash >= 0 && slash < id.Length - 1 ? id[(slash + 1)..] : id;
    }

    private static string FormatTokens(int? tokens) => tokens switch
    {
        null or 0 => "",
        >= 1_000_000 => $"{tokens.Value / 1_000_000}M",
        >= 1_000 => $"{tokens.Value / 1_000}K",
        _ => tokens.Value.ToString()
    };

    private static string PriceLabel(ProviderModelEntry m)
    {
        if (m.IsFree)
        {
            return "Free";
        }
        return m.PricePerMillionInput.HasValue switch
        {
            false => "—",
            true when m.PricePerMillionInput.Value < 0.01m => $"${m.PricePerMillionInput.Value:F4}",
            _ => $"${m.PricePerMillionInput.Value:F2}"
        };
    }
}
