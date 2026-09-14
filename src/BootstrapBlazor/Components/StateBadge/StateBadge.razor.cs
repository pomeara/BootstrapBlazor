// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Enabled/disabled/unknown state badge.
/// </summary>
public sealed partial class StateBadge
{
    /// <summary>
    /// Gets or sets the state value: "enabled" | "disabled" | "unknown" (or true/false).
    /// </summary>
    [Parameter]
    public string State { get; set; } = "enabled";

    private string Key => (State ?? string.Empty).Trim().ToLowerInvariant();

    private Color StateColor => Key switch
    {
        "enabled" or "true" => Color.Success,
        "disabled" or "false" => Color.Danger,
        _ => Color.Secondary
    };

    private string StateLabel => Key switch
    {
        "enabled" or "true" => "enabled",
        "disabled" or "false" => "disabled",
        _ => "unknown"
    };

    private string Icon => Key switch
    {
        "enabled" or "true" => "fa-solid fa-circle-check",
        "disabled" or "false" => "fa-solid fa-circle-xmark",
        _ => "fa-solid fa-circle-question"
    };
}
