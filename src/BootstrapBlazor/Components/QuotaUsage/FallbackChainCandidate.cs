// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>One step of a resolved provider fallback chain (requested → favourites → fallbacks).</summary>
/// <param name="ProviderId">Provider the candidate model lives on.</param>
/// <param name="ModelId">Model identifier.</param>
/// <param name="Label">Display label; falls back to <paramref name="ModelId"/> when empty.</param>
/// <param name="Source">Why the step is in the chain — "favourite" or "fallback".</param>
/// <param name="Favourite">Current favourite flag for the candidate.</param>
public sealed record FallbackChainCandidate(
    string ProviderId,
    string ModelId,
    string? Label = null,
    string Source = "fallback",
    bool Favourite = false);
