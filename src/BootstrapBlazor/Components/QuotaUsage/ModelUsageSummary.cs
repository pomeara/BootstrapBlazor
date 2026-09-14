// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Aggregated usage summary for one provider (optionally one API key) over a period —
/// requests, tokens, cost and model diversity, rendered by <see cref="UsageSummaryCard"/>.
/// </summary>
public sealed record ModelUsageSummary
{
    /// <summary>Provider the usage belongs to.</summary>
    public string ProviderId { get; init; } = "";

    /// <summary>API key prefix (per-key burn); "*" = all keys.</summary>
    public string ApiKeyPrefix { get; init; } = "*";

    /// <summary>Human-readable period label (e.g. "Last 30 days").</summary>
    public string Period { get; init; } = "";

    /// <summary>Total requests in the period.</summary>
    public int TotalRequests { get; init; }

    /// <summary>Total prompt tokens in the period.</summary>
    public long TotalPromptTokens { get; init; }

    /// <summary>Total completion tokens in the period.</summary>
    public long TotalCompletionTokens { get; init; }

    /// <summary>Total tokens (prompt + completion).</summary>
    public long TotalTokens => TotalPromptTokens + TotalCompletionTokens;

    /// <summary>Total cost accrued in the period (USD).</summary>
    public double TotalCostUsd { get; init; }

    /// <summary>Period start instant.</summary>
    public DateTimeOffset PeriodStart { get; init; }

    /// <summary>Period end instant.</summary>
    public DateTimeOffset PeriodEnd { get; init; }

    /// <summary>Distinct models used in the period.</summary>
    public int UniqueModelsUsed { get; init; }
}
