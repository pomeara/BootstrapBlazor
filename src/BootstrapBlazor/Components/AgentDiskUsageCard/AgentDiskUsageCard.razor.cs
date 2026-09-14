// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Disk-usage card. Renders each top-level entry under an agent's base directory with its
/// size and a safe-to-delete / keep badge.
/// </summary>
public sealed partial class AgentDiskUsageCard
{
    /// <summary>The disk-usage report for the scanned agent.</summary>
    [Parameter]
    [EditorRequired]
    public AgentDiskReport Disk { get; set; } = new();

    /// <summary>Format a byte count as a human-readable string (KB/MB/GB).</summary>
    public static string FormatBytes(long bytes)
    {
        return bytes switch
        {
            < 1024 => $"{bytes} B",
            < 1024 * 1024 => $"{bytes / 1024.0:F1} KB",
            < 1024L * 1024 * 1024 => $"{bytes / (1024.0 * 1024):F1} MB",
            _ => $"{bytes / (1024.0 * 1024 * 1024):F2} GB"
        };
    }
}
