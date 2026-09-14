// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Fallback-chain editor: renders the resolver's exact candidate order
/// (requested → favourites → fallbacks) with a favourite toggle per step — the same flag the
/// resolver uses to build the chain, so the editor IS the configuration surface, not a parallel
/// one. Fully param-driven: the host resolves and supplies <see cref="Candidates"/> and handles
/// <see cref="OnFavouriteChanged"/> (persist + refresh) — the component renders and reports only.
/// </summary>
public sealed partial class ProviderFallbackChainView
{
    /// <summary>Resolved chain steps in resolver order.</summary>
    [Parameter]
    public IReadOnlyList<FallbackChainCandidate> Candidates { get; set; } = [];

    /// <summary>True while the host is resolving the chain — renders a loading row.</summary>
    [Parameter]
    public bool Loading { get; set; }

    /// <summary>Host-side resolution error — renders a danger alert when set.</summary>
    [Parameter]
    public string? Error { get; set; }

    /// <summary>Text for the empty-chain state.</summary>
    [Parameter]
    public string EmptyText { get; set; } = "No fallback candidates — configure provider keys and favourite models.";

    /// <summary>Raised when a step's favourite toggle flips — payload is (candidate, new value).</summary>
    [Parameter]
    public EventCallback<(FallbackChainCandidate Candidate, bool Favourite)> OnFavouriteChanged { get; set; }

    private Task OnToggleAsync(FallbackChainCandidate candidate, bool favourite) =>
        OnFavouriteChanged.InvokeAsync((candidate, favourite));
}
