// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>One provider tile in the status grid — availability plus model count.</summary>
public sealed record ProviderStatusItem
{
    /// <summary>Display name.</summary>
    public string Name { get; init; } = "";

    /// <summary>Provider identifier raised by <see cref="ProviderStatusGrid.OnProviderSelected"/>.</summary>
    public string ProviderType { get; init; } = "";

    /// <summary>Whether the provider is currently reachable.</summary>
    public bool IsAvailable { get; init; }

    /// <summary>Number of models the provider exposes.</summary>
    public int ModelCount { get; init; }

    /// <summary>Icon URL for the provider; falls back to a generic glyph when empty.</summary>
    public string? IconPath { get; init; }
}
