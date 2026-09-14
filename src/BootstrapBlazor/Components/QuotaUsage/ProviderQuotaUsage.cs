// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One provider account's live quota snapshot — the 5-hour session window plus whichever of the
/// weekly/monthly windows the provider reports, with the error/rate-limit/outcome flags the
/// live-quota card family renders.
/// </summary>
public sealed record ProviderQuotaUsage
{
    /// <summary>Provider name shown in headers and badges.</summary>
    public string Provider { get; init; } = "";

    /// <summary>Account label distinguishing multiple keys on one provider.</summary>
    public string? Account { get; init; }

    /// <summary>5-hour / session window utilisation percent (0–100).</summary>
    public double SessionPercent { get; init; }

    /// <summary>Session window used count (0 when not measured).</summary>
    public long SessionUsed { get; init; }

    /// <summary>Session window ceiling (0 = unknown).</summary>
    public long SessionLimit { get; init; }

    /// <summary>Provider-stated session reset instant, when known.</summary>
    public DateTimeOffset? SessionResetsAt { get; init; }

    /// <summary>Provider-specific session label override (e.g. "Balance (CNY)").</summary>
    public string? SessionWindowLabel { get; init; }

    /// <summary>Weekly window utilisation percent (0–100).</summary>
    public double WeeklyPercent { get; init; }

    /// <summary>Weekly window used count.</summary>
    public long WeeklyUsed { get; init; }

    /// <summary>Weekly window ceiling (0 = unknown).</summary>
    public long WeeklyLimit { get; init; }

    /// <summary>Provider-stated weekly reset instant, when known.</summary>
    public DateTimeOffset? WeeklyResetsAt { get; init; }

    /// <summary>Provider-specific weekly label override.</summary>
    public string? WeeklyWindowLabel { get; init; }

    /// <summary>Monthly window utilisation percent — null when the provider reports no monthly window.</summary>
    public double? MonthlyPercent { get; init; }

    /// <summary>Monthly window used count.</summary>
    public long MonthlyUsed { get; init; }

    /// <summary>Monthly window ceiling (0 = unknown).</summary>
    public long MonthlyLimit { get; init; }

    /// <summary>Provider-stated monthly reset instant, when known.</summary>
    public DateTimeOffset? MonthlyResetsAt { get; init; }

    /// <summary>Provider-specific monthly label override.</summary>
    public string? MonthlyWindowLabel { get; init; }

    /// <summary>How the snapshot was obtained (e.g. "api", "mcp", "scrape").</summary>
    public string Source { get; init; } = "api";

    /// <summary>Fetch error — when set, the snapshot carries no usable windows.</summary>
    public string? Error { get; init; }

    /// <summary>True when the session or weekly window is at/over the critical line.</summary>
    public bool IsRateLimited => SessionPercent >= QuotaBands.CriticalPercent || WeeklyPercent >= QuotaBands.CriticalPercent;
}

/// <summary>
/// Window-level accessors over <see cref="ProviderQuotaUsage"/> — the single implementation of
/// "which windows does this snapshot carry / which is worst / what band is it in".
/// </summary>
public static class ProviderQuotaUsageExtensions
{
    /// <summary>
    /// All windows this snapshot actually carries, in slot order (session → weekly → monthly).
    /// Weekly counts as carried when it has a nonzero percent or a known reset; monthly only
    /// when the provider reported one.
    /// </summary>
    public static IReadOnlyList<QuotaWindowSnapshot> Windows(this ProviderQuotaUsage quota)
    {
        var windows = new List<QuotaWindowSnapshot>(3)
        {
            new(QuotaWindowKind.Session, quota.SessionPercent, quota.SessionUsed, quota.SessionLimit, quota.SessionResetsAt, quota.SessionWindowLabel)
        };

        if (quota.WeeklyPercent > 0 || quota.WeeklyResetsAt.HasValue)
        {
            windows.Add(new(QuotaWindowKind.Weekly, quota.WeeklyPercent, quota.WeeklyUsed, quota.WeeklyLimit, quota.WeeklyResetsAt, quota.WeeklyWindowLabel));
        }

        if (quota.MonthlyPercent.HasValue)
        {
            windows.Add(new(QuotaWindowKind.Monthly, quota.MonthlyPercent.Value, quota.MonthlyUsed, quota.MonthlyLimit, quota.MonthlyResetsAt, quota.MonthlyWindowLabel));
        }

        return windows;
    }

    /// <summary>
    /// The worst (highest-utilisation) carried window, or null when the snapshot carries none.
    /// Ties resolve to the earlier slot (session beats weekly beats monthly).
    /// </summary>
    public static QuotaWindowSnapshot? WorstWindow(this ProviderQuotaUsage quota)
    {
        QuotaWindowSnapshot? worst = null;
        foreach (var window in quota.Windows())
        {
            if (worst is null || window.Percent > worst.Percent)
            {
                worst = window;
            }
        }

        return worst;
    }

    /// <summary>
    /// The shared <see cref="QuotaBand"/> for this snapshot: worst carried window through
    /// <see cref="QuotaBands.FromPercent"/>; <see cref="QuotaBand.Unknown"/> for error snapshots.
    /// </summary>
    public static QuotaBand Band(this ProviderQuotaUsage quota)
        => string.IsNullOrEmpty(quota.Error)
            ? QuotaBands.FromPercent(quota.WorstWindow()?.Percent ?? 0)
            : QuotaBand.Unknown;
}
