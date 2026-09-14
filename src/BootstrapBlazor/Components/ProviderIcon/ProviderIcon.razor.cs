// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Provider icon — image when an icon path is supplied, fallback icon otherwise.
/// </summary>
public sealed partial class ProviderIcon
{
    /// <summary>
    /// Gets or sets the provider display name.
    /// </summary>
    [Parameter]
    public string ProviderName { get; set; } = "";

    /// <summary>
    /// Gets or sets the icon URL (e.g. an SVG static-asset path). Null renders the fallback icon.
    /// </summary>
    [Parameter]
    public string? IconPath { get; set; }

    /// <summary>
    /// Gets or sets the FontAwesome fallback icon class.
    /// </summary>
    [Parameter]
    public string FallbackIconClass { get; set; } = "fa-solid fa-network-wired";

    private string? IconSrc => IconPath;

    private string GetDisplayName() => string.IsNullOrEmpty(ProviderName) ? "Unknown" : ProviderName;

    private string? ClassString => CssBuilder.Default("provider-icon")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? AdditionalClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
