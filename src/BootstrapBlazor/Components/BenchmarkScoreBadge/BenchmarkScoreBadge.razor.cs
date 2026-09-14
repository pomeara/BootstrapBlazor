// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Renders one leaderboard benchmark score (category key + value) with a key-specific
/// icon/colour. Mapping is public static so the key taxonomy is unit-testable and reusable.
/// </summary>
public sealed partial class BenchmarkScoreBadge
{
    /// <summary>
    /// Gets or sets the benchmark category key (best, composite, coding, reasoning, math, text, vision…).
    /// </summary>
    [Parameter]
    public string Key { get; set; } = "";

    /// <summary>
    /// Gets or sets the benchmark score. When null, the badge is not rendered.
    /// </summary>
    [Parameter]
    public double? Score { get; set; }

    private Color KeyColor => KeyColorFor(Key);

    private string KeyIcon => KeyIconFor(Key);

    private string Title => $"{Key}: {Score!.Value.ToString("0.0")}";

    /// <summary>
    /// Colour for a benchmark key (case/space-insensitive; unknown keys return <see cref="Color.Secondary"/>).
    /// </summary>
    public static Color KeyColorFor(string? key) => Normalise(key) switch
    {
        "best" => Color.Primary,
        "composite" or "overall" => Color.Success,
        "coding" or "code" or "swe" or "swe-bench" => Color.Danger,
        "reasoning" or "reason" or "agentic" => Color.Warning,
        "math" => Color.Primary,
        "text" => Color.Info,
        "vision" => Color.Info,
        _ => Color.Secondary
    };

    /// <summary>
    /// Icon for a benchmark key (case/space-insensitive; unknown keys return clipboard-check).
    /// </summary>
    public static string KeyIconFor(string? key) => Normalise(key) switch
    {
        "best" => "fa-solid fa-trophy",
        "composite" or "overall" => "fa-solid fa-medal",
        "coding" or "code" or "swe" or "swe-bench" => "fa-solid fa-code",
        "reasoning" or "reason" or "agentic" => "fa-solid fa-brain",
        "math" => "fa-solid fa-square-root-variable",
        "text" => "fa-solid fa-font",
        "vision" => "fa-solid fa-eye",
        _ => "fa-solid fa-clipboard-check"
    };

    private static string Normalise(string? key) => (key ?? string.Empty).Trim().ToLowerInvariant();
}
