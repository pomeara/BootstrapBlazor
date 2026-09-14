// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Capability chip with icon (vision/code/reasoning/tools/…).
/// </summary>
public sealed partial class CapabilityTag
{
    /// <summary>
    /// Gets or sets the capability name.
    /// </summary>
    [Parameter]
    public string Capability { get; set; } = "";

    private string? ClassString => CssBuilder.Default("capability-tag")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string GetIconClass() => Capability.ToLowerInvariant() switch
    {
        "vision" => "fa-solid fa-eye",
        "code" or "coding" => "fa-solid fa-code",
        "function-calling" or "tools" => "fa-solid fa-wrench",
        "reasoning" => "fa-solid fa-brain",
        "creative" => "fa-solid fa-palette",
        "multilingual" => "fa-solid fa-globe",
        "long-context" => "fa-solid fa-file-lines",
        "streaming" => "fa-solid fa-stream",
        "audio" => "fa-solid fa-microphone",
        "image" => "fa-solid fa-image",
        "video" => "fa-solid fa-video",
        _ => "fa-solid fa-circle-info"
    };
}
