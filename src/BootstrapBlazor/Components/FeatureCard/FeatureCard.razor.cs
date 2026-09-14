// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Action card with icon, title, description, and call-to-action button.
/// </summary>
public sealed partial class FeatureCard
{
    /// <summary>
    /// Gets or sets the FontAwesome icon class.
    /// </summary>
    [Parameter]
    public string Icon { get; set; } = "fa-solid fa-circle";

    /// <summary>
    /// Gets or sets the call-to-action button color.
    /// </summary>
    [Parameter]
    public Color ButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// Gets or sets the icon color CSS class (e.g. "text-primary").
    /// </summary>
    [Parameter]
    public string IconColor { get; set; } = "text-primary";

    /// <summary>
    /// Gets or sets the card title.
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "";

    /// <summary>
    /// Gets or sets the card description.
    /// </summary>
    [Parameter]
    public string Description { get; set; } = "";

    /// <summary>
    /// Gets or sets the call-to-action button text.
    /// </summary>
    [Parameter]
    public string ButtonText { get; set; } = "Go";

    /// <summary>
    /// Gets or sets the callback fired when the call-to-action button is clicked.
    /// </summary>
    [Parameter]
    public EventCallback OnClick { get; set; }

    private string? ClassString => CssBuilder.Default("card h-100 shadow-sm border-0")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
