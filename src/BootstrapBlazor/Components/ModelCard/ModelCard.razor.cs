// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Compact AI model card — icon, badges, capabilities, pricing.
/// </summary>
public sealed partial class ModelCard
{
    /// <summary>
    /// Gets or sets the model summary to render.
    /// </summary>
    [Parameter]
    [EditorRequired]
    public ModelCardModel Model { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether to render the header icon.
    /// </summary>
    [Parameter]
    public bool ShowIcon { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to render the badge row.
    /// </summary>
    [Parameter]
    public bool ShowBadges { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to render the capability row.
    /// </summary>
    [Parameter]
    public bool ShowCapabilities { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to render the pricing row.
    /// </summary>
    [Parameter]
    public bool ShowPricing { get; set; } = true;

    private string? ClassString => CssBuilder.Default("model-card")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string GetDisplayName() => !string.IsNullOrEmpty(Model.DisplayName)
        ? Model.DisplayName
        : Model.Id;

    private static string FormatPrice(decimal? price) => price switch
    {
        null or 0 => "Free",
        < 0.01m => $"${price.Value:F4}",
        _ => $"${price.Value:F2}"
    };
}
