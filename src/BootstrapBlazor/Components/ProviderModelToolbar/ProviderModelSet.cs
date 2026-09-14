// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One AI provider and its selectable models — decoupled from any catalog/DTO shape; consumers
/// map their own provider records into it.
/// </summary>
public sealed record ProviderModelSet
{
    /// <summary>Provider identifier (e.g. "openrouter").</summary>
    public string Name { get; init; } = "";

    /// <summary>Optional display name — falls back to <see cref="Name"/>.</summary>
    public string? DisplayName { get; init; }

    /// <summary>Models offered by this provider.</summary>
    public List<ProviderModelEntry> Models { get; init; } = [];
}
