// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Model capability badge with key-specific color.
/// </summary>
public sealed partial class ModelCapabilityBadge
{
    /// <summary>
    /// Gets or sets the capability name (vision, tools, streaming, coding, reasoning, audio…).
    /// </summary>
    [Parameter]
    public string CapabilityName { get; set; } = "";

    private Color GetColor() => CapabilityName.ToLowerInvariant() switch
    {
        "vision" => Color.Info,
        "function_calling" or "tools" => Color.Primary,
        "streaming" => Color.Success,
        "code" or "coding" => Color.Warning,
        "reasoning" => Color.Warning,
        "audio" => Color.Success,
        _ => Color.Secondary
    };
}
