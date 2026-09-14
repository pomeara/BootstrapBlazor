// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact one-line quota strip — per-provider badges (5h % + monthly %). Sized for the chrome:
/// drop into nav bars, page headers or footers to surface live quota without a full dashboard.
/// Composes <see cref="Badge"/> atoms over <see cref="ProviderQuotaUsage"/>; colour follows the
/// 5h utilisation.
/// </summary>
public sealed partial class LiveQuotaStrip
{
    /// <summary>Live quota snapshots; error snapshots are skipped.</summary>
    [Parameter]
    public IReadOnlyList<ProviderQuotaUsage>? Quotas { get; set; }

    /// <summary>Text shown when there are no usable quotas.</summary>
    [Parameter]
    public string EmptyText { get; set; } = "no quota";

    private static Color ColorFor(double percent) => percent >= QuotaBands.CriticalPercent ? Color.Danger
        : percent >= QuotaBands.WarningPercent ? Color.Warning
        : Color.Success;
}
