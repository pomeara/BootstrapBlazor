// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// The shared utilisation band for a quota value — the one threshold set every surface
/// (window bars, strips, cards) grades against.
/// </summary>
public enum QuotaBand
{
    /// <summary>Plenty of headroom (below the warm line).</summary>
    Healthy,

    /// <summary>Half-used — visible but calm.</summary>
    Warm,

    /// <summary>At/over the warning line — attention warranted.</summary>
    Warning,

    /// <summary>At/over the critical line — effectively exhausted / rate-limited territory.</summary>
    Critical,

    /// <summary>No usable data (fetch error, no windows reported) — never colour as healthy.</summary>
    Unknown
}

/// <summary>Threshold constants + the single <see cref="QuotaBand"/> classifier.</summary>
public static class QuotaBands
{
    /// <summary>Warm line (utilisation percent at/above this reads as half-used).</summary>
    public const double WarmPercent = 50;

    /// <summary>Warning line — matches the window-bar warning colour.</summary>
    public const double WarningPercent = 80;

    /// <summary>Critical line — matches rate-limited territory.</summary>
    public const double CriticalPercent = 95;

    /// <summary>
    /// Classify one utilisation percent. <paramref name="includeWarm"/> collapses Warm into
    /// Healthy for surfaces that only speak the 80/95 language.
    /// </summary>
    public static QuotaBand FromPercent(double percent, bool includeWarm = true) => percent switch
    {
        >= CriticalPercent => QuotaBand.Critical,
        >= WarningPercent => QuotaBand.Warning,
        >= WarmPercent when includeWarm => QuotaBand.Warm,
        _ => QuotaBand.Healthy
    };
}
