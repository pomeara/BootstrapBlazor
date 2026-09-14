// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Quota card for one provider API key — token/request/cost progress bars against their
/// limits, a quota-exceeded badge, and an optional health chip from the latest check.
/// </summary>
public sealed partial class ProviderQuotaCard
{
    /// <summary>The quota status row to render.</summary>
    [Parameter]
    [EditorRequired]
    public ModelQuotaStatus Status { get; set; } = new();

    /// <summary>Latest health verdict for this provider — renders a health chip in the header when set.</summary>
    [Parameter]
    public ProviderHealth? Health { get; set; }

    /// <summary>Highest utilisation across the configured quota categories.</summary>
    private double MaxUsagePercent
    {
        get
        {
            var percents = new List<double>();
            if (Status.TokenLimit > 0) percents.Add(Status.TokenUsagePercent);
            if (Status.RequestLimit > 0) percents.Add(Status.RequestUsagePercent);
            if (Status.CostLimitUsd > 0) percents.Add(Status.CostUsagePercent);
            return percents.Count > 0 ? percents.Max() : 0;
        }
    }

    /// <summary>Badge colour class for the health chip.</summary>
    private string HealthChipClass => Health?.Status switch
    {
        ProviderHealth.Healthy => "text-bg-success",
        ProviderHealth.Degraded => "text-bg-warning",
        _ => "text-bg-danger"
    };

    /// <summary>Icon class for the health chip.</summary>
    private string HealthIcon => Health?.Status switch
    {
        ProviderHealth.Healthy => "fa-solid fa-circle-check",
        ProviderHealth.Degraded => "fa-solid fa-triangle-exclamation",
        _ => "fa-solid fa-circle-xmark"
    };

    /// <summary>Tooltip for the health chip (status + message + local check time).</summary>
    private string HealthTitle => Health is null ? "" : $"{Health.Status}: {Health.Message} ({Health.CheckedAt.ToLocalTime():HH:mm})";

    /// <summary>Grades through the shared <see cref="QuotaBands"/> lines (80 warning / 95 critical) — same language as the live-quota family.</summary>
    private static Color GetProgressColor(double percent) => percent switch
    {
        >= QuotaBands.CriticalPercent => Color.Danger,
        >= QuotaBands.WarningPercent => Color.Warning,
        _ => Color.Success
    };

    /// <summary>Compact number format — 1.5M / 23.4K / plain below a thousand.</summary>
    private static string FormatNumber(long value) =>
        value >= 1_000_000 ? $"{value / 1_000_000.0:F1}M"
        : value >= 1_000 ? $"{value / 1_000.0:F1}K"
        : value.ToString();
}
