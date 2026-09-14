// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Favourite star — filled when favourite, outline otherwise.
/// </summary>
public sealed partial class FavouriteStar
{
    /// <summary>
    /// Gets or sets a value indicating whether the star is filled.
    /// </summary>
    [Parameter]
    public bool Favourite { get; set; }

    private string? ClassString => CssBuilder.Default("favourite-star")
        .AddClass(Favourite ? "on" : "off")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
