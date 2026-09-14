// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// One provider's live quota card — the 5-hour window plus whichever of the weekly/monthly
/// windows the provider reports (z.ai reports both; a monthly-only payload shows just the
/// monthly bar). Molecule: composes up to three <see cref="QuotaWindowBar"/> atoms for one
/// <see cref="ProviderQuotaUsage"/>. Three detail levels (<see cref="LiveQuotaDetailLevel"/>):
/// Compact (header strip, chrome-sized), Standard (window bars — default), Detailed (full
/// dashboard card). The card shell carries a heat class (<see cref="LiveQuotaHeat"/>) so
/// border/background/glow reflect severity.
/// </summary>
public sealed partial class LiveQuotaCard
{
    /// <summary>The live quota snapshot to render.</summary>
    [Parameter]
    [EditorRequired]
    public ProviderQuotaUsage Quota { get; set; } = new();

    /// <summary>How much of the card renders. Default Standard.</summary>
    [Parameter]
    public LiveQuotaDetailLevel Detail { get; set; } = LiveQuotaDetailLevel.Standard;

    /// <summary>Heat-state class for the card shell.</summary>
    private string HeatClass => LiveQuotaHeat.CssClass(Quota);

    /// <summary>Headline percent for Compact — the worst carried window (shared accessor).</summary>
    private double HeadlinePercent => string.IsNullOrEmpty(Quota.Error)
        ? Quota.WorstWindow()?.Percent ?? 0
        : 0;
}
