// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Latest health verdict for one provider account, rendered as the quota card's health chip.
/// Status is one of <see cref="ProviderHealth.Healthy"/> / <see cref="Degraded"/> / <see cref="Unhealthy"/>.
/// </summary>
public sealed record ProviderHealth
{
    /// <summary>Status constant for a passing check.</summary>
    public const string Healthy = "Healthy";

    /// <summary>Status constant for a check that passed with warnings.</summary>
    public const string Degraded = "Degraded";

    /// <summary>Status constant for a failing check.</summary>
    public const string Unhealthy = "Unhealthy";

    /// <summary>Healthy / Degraded / Unhealthy.</summary>
    public string Status { get; init; } = Healthy;

    /// <summary>Human-readable check outcome.</summary>
    public string Message { get; init; } = "";

    /// <summary>When the check ran.</summary>
    public DateTimeOffset CheckedAt { get; init; }
}
