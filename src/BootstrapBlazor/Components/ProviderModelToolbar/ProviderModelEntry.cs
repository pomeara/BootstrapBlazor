// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One selectable model with capability tags for filtering plus the display metadata
/// selectors and model cards render (family, free/state/favourite flags, leaderboard, context, price).
/// </summary>
public sealed record ProviderModelEntry
{
    /// <summary>Model identifier (e.g. "llama3.2").</summary>
    public string Id { get; init; } = "";

    /// <summary>Optional display name — falls back to <see cref="Id"/>.</summary>
    public string? DisplayName { get; init; }

    /// <summary>Capability tags (e.g. text, vision, audio, video, code, reasoning).</summary>
    public List<string> Capabilities { get; init; } = [];

    /// <summary>Model family (e.g. "llama") — drives grouped selectors; falls back to "Other".</summary>
    public string? Family { get; init; }

    /// <summary>True when the model is free to call.</summary>
    public bool IsFree { get; init; }

    /// <summary>Enabled state (e.g. "enabled", "disabled") — empty or "enabled" means usable.</summary>
    public string? State { get; init; }

    /// <summary>True when the user starred this model.</summary>
    public bool Favourite { get; init; }

    /// <summary>Selection tier label (e.g. "best", "fast", "coding").</summary>
    public string? Tier { get; init; }

    /// <summary>Chatbot-arena ELO score, when ranked.</summary>
    public int? ArenaElo { get; init; }

    /// <summary>Leaderboard position, when ranked.</summary>
    public int? LeaderboardRank { get; init; }

    /// <summary>Context window in tokens.</summary>
    public int? ContextWindow { get; init; }

    /// <summary>Input price per million tokens, when paid.</summary>
    public decimal? PricePerMillionInput { get; init; }

    /// <summary>Human parameter-count label (e.g. "8B", "70B") — card selectors show it as text.</summary>
    public string? ParameterCount { get; init; }

    /// <summary>On-disk size in bytes (local runtimes) — card selectors show it as a badge.</summary>
    public long? SizeBytes { get; init; }

    /// <summary>Quantization label (e.g. "Q4_K_M") — card selectors show it as a badge.</summary>
    public string? Quantization { get; init; }
}
