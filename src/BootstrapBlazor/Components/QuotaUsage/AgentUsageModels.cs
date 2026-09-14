// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Agent-usage card family models — per-model cost rows, the cost summary, the KPI report,
/// the disk-usage report and per-project attribution rows.
/// </summary>

/// <summary>Per-model cost row: the agent-reported cost vs the computed vs the effective one.</summary>
public sealed record AgentModelCost
{
    /// <summary>Model identifier.</summary>
    public string Model { get; set; } = "";

    /// <summary>Cost reported by the agent itself (often 0 for non-Anthropic models).</summary>
    public double ReportedCostUsd { get; set; }

    /// <summary>Cost computed from token counts and prices.</summary>
    public double ComputedCostUsd { get; set; }

    /// <summary>Effective cost charged (computed when the report is unreliable).</summary>
    public double CostUsd { get; set; }
}

/// <summary>Cost buckets for one scanned agent, plus the per-model breakdown.</summary>
public sealed record AgentCostSummary
{
    /// <summary>Cost today (USD).</summary>
    public double Today { get; set; }

    /// <summary>Cost this month (USD).</summary>
    public double ThisMonth { get; set; }

    /// <summary>Cost over the last 7 days (USD).</summary>
    public double Last7Days { get; set; }

    /// <summary>Projected month-end cost (USD).</summary>
    public double MonthlyProjection { get; set; }

    /// <summary>All-time cost (USD).</summary>
    public double AllTime { get; set; }

    /// <summary>Cost-calculation strategy label (e.g. "opus-fallback").</summary>
    public string Strategy { get; set; } = "";

    /// <summary>Per-model cost rows.</summary>
    public IReadOnlyList<AgentModelCost> PerModel { get; set; } = [];
}

/// <summary>KPI report for one scanned agent — headline totals plus cost buckets.</summary>
public sealed record AgentUsageReport
{
    /// <summary>Agent identifier (config name).</summary>
    public string Agent { get; set; } = "";

    /// <summary>Friendly display name; falls back to <see cref="Agent"/> when empty.</summary>
    public string DisplayName { get; set; } = "";

    /// <summary>Total messages across all sessions.</summary>
    public long TotalMessages { get; set; }

    /// <summary>Total sessions scanned.</summary>
    public long TotalSessions { get; set; }

    /// <summary>Cost buckets + per-model breakdown.</summary>
    public AgentCostSummary Cost { get; set; } = new();
}

/// <summary>One top-level disk entry under an agent's base directory.</summary>
public sealed record AgentDiskEntry
{
    /// <summary>Entry name.</summary>
    public string Name { get; set; } = "";

    /// <summary>Entry kind — "Dir" or "File".</summary>
    public string Kind { get; set; } = "File";

    /// <summary>Entry size in bytes.</summary>
    public long Bytes { get; set; }

    /// <summary>True when the entry is safe to delete.</summary>
    public bool SafeToDelete { get; set; }

    /// <summary>True when the entry must be kept.</summary>
    public bool Keep { get; set; }
}

/// <summary>Disk-usage report for one scanned agent's base directory.</summary>
public sealed record AgentDiskReport
{
    /// <summary>Total size of the base directory (bytes).</summary>
    public long TotalBytes { get; set; }

    /// <summary>Top-level entries with sizes and safety flags.</summary>
    public IReadOnlyList<AgentDiskEntry> Entries { get; set; } = [];
}

/// <summary>Per-project attribution row derived from an agent's history log.</summary>
public sealed record AgentProjectUsage
{
    /// <summary>Project / repository name (cwd-derived).</summary>
    public string Project { get; set; } = "";

    /// <summary>All-time messages attributed to the project.</summary>
    public long MessagesTotal { get; set; }

    /// <summary>Messages attributed to the project this month.</summary>
    public long MessagesThisMonth { get; set; }

    /// <summary>Sessions attributed to the project this month.</summary>
    public long SessionsThisMonth { get; set; }
}
