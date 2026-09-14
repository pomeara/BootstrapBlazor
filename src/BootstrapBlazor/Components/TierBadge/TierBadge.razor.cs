// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Tier badge (best/fast/vision/coding/reasoning). Renders nothing without a tier.
/// </summary>
public sealed partial class TierBadge
{
    /// <summary>
    /// Gets or sets the tier label: "best", "fast", "vision", "coding"/"code", "reasoning"/"reason".
    /// </summary>
    [Parameter]
    public string? Tier { get; set; }

    private string Key => (Tier ?? string.Empty).Trim().ToLowerInvariant();

    private Color TierColor => Key switch
    {
        "best" => Color.Primary,
        "fast" => Color.Success,
        "vision" => Color.Info,
        "coding" or "code" => Color.Danger,
        "reasoning" or "reason" => Color.Warning,
        _ => Color.Secondary
    };

    private string TierIcon => Key switch
    {
        "best" => "fa-solid fa-trophy",
        "fast" => "fa-solid fa-bolt",
        "vision" => "fa-solid fa-eye",
        "coding" or "code" => "fa-solid fa-code",
        "reasoning" or "reason" => "fa-solid fa-brain",
        _ => "fa-solid fa-tag"
    };
}
