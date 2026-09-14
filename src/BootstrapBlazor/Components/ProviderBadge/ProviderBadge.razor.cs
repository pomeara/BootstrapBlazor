// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Provider chip — icon + name, optional favourite/state meta.
/// </summary>
public sealed partial class ProviderBadge
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
    /// Gets or sets a value indicating whether to render the provider icon.
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; } = true;

    /// <summary>
    /// Gets or sets the lifecycle state ("enabled"/"disabled"/"unknown") — shown when <see cref="ShowMeta"/>.
    /// </summary>
    [Parameter]
    public string State { get; set; } = "enabled";

    /// <summary>
    /// Gets or sets the user-pinned favourite — shows a star when true + <see cref="ShowMeta"/>.
    /// </summary>
    [Parameter]
    public bool Favourite { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to render a <see cref="FavouriteStar"/> and
    /// (non-enabled) <see cref="StateBadge"/> beside the name.
    /// </summary>
    [Parameter]
    public bool ShowMeta { get; set; }

    private string GetDisplayName() => string.IsNullOrEmpty(ProviderName) ? "Unknown" : ProviderName;

    private string GetProviderClass() => GetDisplayName().ToLowerInvariant() switch
    {
        "openrouter" => "provider-openrouter",
        "openai" => "provider-openai",
        "anthropic" or "claude" => "provider-anthropic",
        "google" or "googleai" => "provider-google",
        "groq" => "provider-groq",
        "ollama" => "provider-ollama",
        "together" => "provider-together",
        "lmstudio" => "provider-lmstudio",
        "zhipu" => "provider-zhipu",
        "azureopenai" or "azure" => "provider-azure",
        _ => "provider-default"
    };

    private string? ClassString => CssBuilder.Default("provider-badge")
        .AddClass(GetProviderClass())
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
