// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One provider API key's quota status row — token/request/cost usage against their limits,
/// with the derived percentages and over-quota flags the quota card renders.
/// </summary>
public sealed record ModelQuotaStatus
{
    /// <summary>Provider the quota belongs to.</summary>
    public string ProviderId { get; init; } = "";

    /// <summary>API key prefix identifying the key (per-key burn).</summary>
    public string ApiKeyPrefix { get; init; } = "";

    /// <summary>Tokens used in the period.</summary>
    public long TokensUsed { get; init; }

    /// <summary>Token ceiling for the period (0 = unlimited / not configured).</summary>
    public long TokenLimit { get; init; }

    /// <summary>Cost accrued in the period (USD).</summary>
    public double CostUsd { get; init; }

    /// <summary>Cost ceiling for the period (USD, 0 = not configured).</summary>
    public double CostLimitUsd { get; init; }

    /// <summary>Requests made in the period.</summary>
    public int RequestsUsed { get; init; }

    /// <summary>Request ceiling for the period (0 = not configured).</summary>
    public int RequestLimit { get; init; }

    /// <summary>Human-readable period label (e.g. "September 2026").</summary>
    public string Period { get; init; } = "";

    /// <summary>Token utilisation percent (0 when no token limit).</summary>
    public double TokenUsagePercent => TokenLimit > 0 ? (double)TokensUsed / TokenLimit * 100 : 0;

    /// <summary>Cost utilisation percent (0 when no cost limit).</summary>
    public double CostUsagePercent => CostLimitUsd > 0 ? CostUsd / CostLimitUsd * 100 : 0;

    /// <summary>Request utilisation percent (0 when no request limit).</summary>
    public double RequestUsagePercent => RequestLimit > 0 ? (double)RequestsUsed / RequestLimit * 100 : 0;

    /// <summary>True when tokens used meet/exceed the token limit.</summary>
    public bool IsOverTokenQuota => TokenLimit > 0 && TokensUsed >= TokenLimit;

    /// <summary>True when cost accrued meets/exceeds the cost limit.</summary>
    public bool IsOverCostQuota => CostLimitUsd > 0 && CostUsd >= CostLimitUsd;

    /// <summary>True when requests made meet/exceed the request limit.</summary>
    public bool IsOverRequestQuota => RequestLimit > 0 && RequestsUsed >= RequestLimit;

    /// <summary>True when any quota category is over its limit.</summary>
    public bool IsOverAnyQuota => IsOverTokenQuota || IsOverCostQuota || IsOverRequestQuota;
}
