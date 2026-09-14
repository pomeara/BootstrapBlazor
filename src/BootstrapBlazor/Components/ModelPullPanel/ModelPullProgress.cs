// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// One progress update from a model-pull stream — a status line plus, when known,
/// the download percentage.
/// </summary>
/// <param name="Status">Human-readable progress text (e.g. "pulling manifest", "verifying sha256").</param>
/// <param name="Percent">Download percentage 0-100, when the source reports it.</param>
public sealed record ModelPullProgress(string Status, double? Percent = null)
{
    /// <summary>Formats the update as display text ("verifying sha256: 42.0%").</summary>
    public string DisplayText => Percent.HasValue ? $"{Status}: {Percent.Value:F1}%" : Status;
}
