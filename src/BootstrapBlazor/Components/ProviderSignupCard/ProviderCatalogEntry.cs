// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Catalog entry describing an AI provider — decoupled from any catalog/DTO shape; consumers
/// map their own provider records into it.
/// </summary>
public sealed record ProviderCatalogEntry
{
    /// <summary>Display label (e.g. "Anthropic").</summary>
    public string Label { get; init; } = "";

    /// <summary>Coarse category (e.g. cloud, local, aggregator) — rendered as a badge.</summary>
    public string Category { get; init; } = "";

    /// <summary>Short description. Blank renders a placeholder line.</summary>
    public string Description { get; init; } = "";

    /// <summary>Icon URL for <see cref="ProviderIcon"/>. Null renders the fallback icon.</summary>
    public string? IconPath { get; init; }

    /// <summary>Sign-up URL. Blank hides the sign-up button.</summary>
    public string Url { get; init; } = "";

    /// <summary>Number of models the provider offers.</summary>
    public int ModelCount { get; init; }
}
