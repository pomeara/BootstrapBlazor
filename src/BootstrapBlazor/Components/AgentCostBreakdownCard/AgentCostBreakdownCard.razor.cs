// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Per-model cost breakdown card. Renders the reported/computed/effective cost for each model —
/// visualising the opus-fallback fix (non-Anthropic models that report $0 get a computed value
/// instead of being charged at the opus rate).
/// </summary>
public sealed partial class AgentCostBreakdownCard
{
    /// <summary>The computed cost summary (must contain <see cref="AgentCostSummary.PerModel"/>).</summary>
    [Parameter]
    [EditorRequired]
    public AgentCostSummary Cost { get; set; } = new();
}
