// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Per-provider usage summary card — requests, tokens, cost and model diversity over the
/// period, with the API-key-prefix chip when the row is a specific key.
/// </summary>
public sealed partial class UsageSummaryCard
{
    /// <summary>The usage summary to render.</summary>
    [Parameter]
    [EditorRequired]
    public ModelUsageSummary Summary { get; set; } = new();
}
