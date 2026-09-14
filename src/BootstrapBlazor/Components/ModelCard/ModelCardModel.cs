// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Card-shaped summary of an AI model for <see cref="ModelCard"/> — decoupled from any
/// catalog/DTO shape; consumers map their own model records into it.
/// </summary>
public sealed record ModelCardModel
{
    /// <summary>Model identifier (e.g. "llama3.2", "claude-sonnet-4-6").</summary>
    public string Id { get; init; } = "";

    /// <summary>Optional display name — falls back to <see cref="Id"/>.</summary>
    public string? DisplayName { get; init; }

    /// <summary>Model family (e.g. "llama", "gpt").</summary>
    public string? Family { get; init; }

    /// <summary>Icon URL for the card header. Null renders no icon.</summary>
    public string? IconPath { get; init; }

    /// <summary>Tier label for <see cref="TierBadge"/> (best/fast/vision/coding/reasoning).</summary>
    public string? Tier { get; init; }

    /// <summary>Arena ELO for <see cref="EloBadge"/>.</summary>
    public int? ArenaElo { get; init; }

    /// <summary>Leaderboard rank for <see cref="EloBadge"/>.</summary>
    public int? LeaderboardRank { get; init; }

    /// <summary>Parameter count label (e.g. "8B").</summary>
    public string? ParameterCount { get; init; }

    /// <summary>Context window size in tokens.</summary>
    public int? ContextWindow { get; init; }

    /// <summary>Quantization label (e.g. "Q4_K_M").</summary>
    public string? Quantization { get; init; }

    /// <summary>UTC first-seen stamp for <see cref="NewBadge"/>.</summary>
    public DateTimeOffset? FirstSeenUtc { get; init; }

    /// <summary>Runs locally (ollama, LM Studio…).</summary>
    public bool IsLocal { get; init; }

    /// <summary>Recommended by the host application.</summary>
    public bool Recommended { get; init; }

    /// <summary>Supports image input.</summary>
    public bool SupportsVision { get; init; }

    /// <summary>Supports reasoning.</summary>
    public bool SupportsReasoning { get; init; }

    /// <summary>Supports tool/function calling.</summary>
    public bool SupportsTools { get; init; }

    /// <summary>Supports audio input.</summary>
    public bool SupportsAudio { get; init; }

    /// <summary>Free model (zero-cost tier).</summary>
    public bool IsFree { get; init; }

    /// <summary>Price per million input tokens (null or 0 = free).</summary>
    public decimal? PricePerMillionInput { get; init; }

    /// <summary>Price per million output tokens (null or 0 = free).</summary>
    public decimal? PricePerMillionOutput { get; init; }

    /// <summary>Compact context-window display: <c>8K</c>, <c>1M</c>…</summary>
    public string ContextDisplay => ContextWindow switch
    {
        null or < 1 => "",
        < 1000 => $"{ContextWindow}",
        < 1_000_000 => $"{ContextWindow.Value / 1000.0:0.#}K",
        _ => $"{ContextWindow.Value / 1_000_000.0:0.#}M"
    };
}
