// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>The fixed window slots a <see cref="ProviderQuotaUsage"/> carries.</summary>
public enum QuotaWindowKind
{
    /// <summary>The 5-hour / session slot (also carries pseudo-windows such as credit balances).</summary>
    Session,

    /// <summary>The weekly (7-day) slot.</summary>
    Weekly,

    /// <summary>The monthly slot.</summary>
    Monthly
}

/// <summary>Default display labels for the fixed window slots.</summary>
public static class QuotaWindowDefaults
{
    /// <summary>Default label for a window kind.</summary>
    public static string For(QuotaWindowKind kind) => kind switch
    {
        QuotaWindowKind.Session => "5-hour limit",
        QuotaWindowKind.Weekly => "Weekly limit",
        _ => "Monthly limit"
    };
}

/// <summary>
/// One window of a <see cref="ProviderQuotaUsage" />, normalised for any surface that would
/// otherwise re-derive "which windows does this snapshot carry".
/// </summary>
/// <param name="Kind">Which fixed slot this came from.</param>
/// <param name="Percent">Utilisation 0–100.</param>
/// <param name="Used">Used count in the window (0 when not measured).</param>
/// <param name="Limit">Window ceiling (0 = unknown).</param>
/// <param name="ResetsAt">Provider-stated reset instant, when known.</param>
/// <param name="LabelOverride">Provider-specific label (e.g. "Balance (CNY)"); null = default label.</param>
public sealed record QuotaWindowSnapshot(
    QuotaWindowKind Kind,
    double Percent,
    long Used,
    long Limit,
    DateTimeOffset? ResetsAt,
    string? LabelOverride)
{
    /// <summary>Display label — the provider override when present, else the slot default.</summary>
    public string Label => string.IsNullOrWhiteSpace(LabelOverride) ? QuotaWindowDefaults.For(Kind) : LabelOverride!;
}
