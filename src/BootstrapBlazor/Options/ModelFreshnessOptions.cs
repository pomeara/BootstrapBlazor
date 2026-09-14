// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Freshness display thresholds — static so any component reads them without DI. The HOST
/// (or a settings panel) may set these at startup; defaults work with zero configuration.
/// </summary>
public static class ModelFreshnessOptions
{
    /// <summary>
    /// Models first seen within this many days show the "new" badge (default 14).
    /// </summary>
    public static int NewWindowDays { get; set; } = 14;

    /// <summary>
    /// Show "new" badges / age tooltips in selectors and catalog views (default true).
    /// </summary>
    public static bool ShowBadges { get; set; } = true;
}
