// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>Severity bucket for a live quota snapshot — drives the card heat classes.</summary>
public enum LiveQuotaSeverity
{
    /// <summary>Comfortable utilisation.</summary>
    Healthy,

    /// <summary>Half-used — visible but calm.</summary>
    Warm,

    /// <summary>At/over the warning line.</summary>
    Hot,

    /// <summary>At/over the critical line or rate-limited.</summary>
    Limited,

    /// <summary>Fetch error — no usable data.</summary>
    Error
}

/// <summary>
/// Heat-state classification for a live quota snapshot — drives the card background/border/glow
/// and shared colour language across the live-quota family (cards, strips). Grades through the
/// shared <see cref="QuotaBands"/> classifier (80/95 lines + warm 50) so every surface speaks
/// one language.
/// </summary>
public static class LiveQuotaHeat
{
    /// <summary>Severity bucket for one snapshot. Error beats rate-limit beats utilisation band.</summary>
    public static LiveQuotaSeverity Severity(ProviderQuotaUsage quota)
    {
        if (!string.IsNullOrEmpty(quota.Error))
        {
            return LiveQuotaSeverity.Error;
        }
        if (quota.IsRateLimited)
        {
            return LiveQuotaSeverity.Limited;
        }

        return quota.Band() switch
        {
            QuotaBand.Critical => LiveQuotaSeverity.Limited,
            QuotaBand.Warning => LiveQuotaSeverity.Hot,
            QuotaBand.Warm => LiveQuotaSeverity.Warm,
            _ => LiveQuotaSeverity.Healthy
        };
    }

    /// <summary>Scoped CSS class for the card shell (border/background/glow come from the stylesheet).</summary>
    public static string CssClass(ProviderQuotaUsage quota) => Severity(quota) switch
    {
        LiveQuotaSeverity.Limited => "lqc-limited",
        LiveQuotaSeverity.Hot => "lqc-hot",
        LiveQuotaSeverity.Warm => "lqc-warm",
        LiveQuotaSeverity.Error => "lqc-error",
        _ => "lqc-healthy"
    };

    /// <summary>Bootstrap colour for badges/bar fills, kept consistent with <c>LiveQuotaStrip</c>.</summary>
    public static Color BadgeColor(ProviderQuotaUsage quota) => Severity(quota) switch
    {
        LiveQuotaSeverity.Limited => Color.Danger,
        LiveQuotaSeverity.Hot => Color.Warning,
        LiveQuotaSeverity.Warm => Color.Warning,
        LiveQuotaSeverity.Error => Color.Secondary,
        _ => Color.Success
    };
}
