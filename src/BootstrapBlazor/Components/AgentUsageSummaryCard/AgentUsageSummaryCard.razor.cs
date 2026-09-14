// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// KPI summary card for one scanned agent: totals + cost buckets.
/// Renders the headline numbers from an <see cref="AgentUsageReport"/>.
/// </summary>
public sealed partial class AgentUsageSummaryCard
{
    /// <summary>The scanned agent report to display.</summary>
    [Parameter]
    [EditorRequired]
    public AgentUsageReport Report { get; set; } = new();
}
