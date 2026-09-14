// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One selectable model with capability tags for filtering.
/// </summary>
public sealed record ProviderModelEntry
{
    /// <summary>Model identifier (e.g. "llama3.2").</summary>
    public string Id { get; init; } = "";

    /// <summary>Optional display name — falls back to <see cref="Id"/>.</summary>
    public string? DisplayName { get; init; }

    /// <summary>Capability tags (e.g. text, vision, audio, video, code, reasoning).</summary>
    public List<string> Capabilities { get; init; } = [];
}
