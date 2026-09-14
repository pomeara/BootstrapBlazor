// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Sign-up teaser card for a not-yet-configured provider — icon, blurb, model stats, sign-up link.
/// </summary>
public sealed partial class ProviderSignupCard
{
    /// <summary>
    /// Gets or sets the catalog provider this card teases.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public ProviderCatalogEntry Entry { get; set; } = null!;

    /// <summary>
    /// Gets or sets the best arena ELO across the provider's enabled models (null hides the badge).
    /// </summary>
    [Parameter]
    public int? TopElo { get; set; }

    /// <summary>
    /// Gets or sets the best leaderboard rank across the provider's enabled models.
    /// </summary>
    [Parameter]
    public int? TopRank { get; set; }

    private string ClassString => "provider-signup-card h-100";

    private string Description => string.IsNullOrWhiteSpace(Entry.Description)
        ? "No description provided."
        : Entry.Description;
}
