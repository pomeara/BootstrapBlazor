// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Quota usage badge — green/amber/red by usage percent.
/// </summary>
public sealed partial class QuotaStatusBadge
{
    /// <summary>
    /// Gets or sets a value indicating whether the quota is exceeded (forces the danger color).
    /// </summary>
    [Parameter]
    public bool IsExceeded { get; set; }

    /// <summary>
    /// Gets or sets the usage percentage.
    /// </summary>
    [Parameter]
    public double UsagePercent { get; set; }

    /// <summary>
    /// Gets or sets the status text override — rendered instead of the percentage when set.
    /// </summary>
    [Parameter]
    public string? StatusText { get; set; }

    private Color StatusColor => IsExceeded ? Color.Danger
        : UsagePercent > 90 ? Color.Danger
        : UsagePercent > 70 ? Color.Warning
        : Color.Success;

    private string? ClassString => CssBuilder.Default("quota-status-badge")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
