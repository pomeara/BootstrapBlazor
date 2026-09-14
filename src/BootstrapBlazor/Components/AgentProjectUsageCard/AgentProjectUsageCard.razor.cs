// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Per-project attribution card. Renders message/session counts per project (cwd) derived
/// from the agent's history log.
/// </summary>
public sealed partial class AgentProjectUsageCard
{
    /// <summary>Per-project usage rows, busiest first.</summary>
    [Parameter]
    [EditorRequired]
    public IReadOnlyList<AgentProjectUsage> Projects { get; set; } = [];
}
