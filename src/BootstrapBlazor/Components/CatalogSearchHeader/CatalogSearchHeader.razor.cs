// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Dark search/filter banner header for catalog model browsing — search input, family and
/// provider dropdowns, and a favourites toggle. Pairs with <see cref="CatalogHeader"/>.
/// </summary>
public sealed partial class CatalogSearchHeader
{
    /// <summary>
    /// Gets or sets the header title. Default is "Models".
    /// </summary>
    [Parameter]
    public string Title { get; set; } = "Models";

    /// <summary>
    /// Gets or sets the optional subtitle rendered next to the title.
    /// </summary>
    [Parameter]
    public string? SubTitle { get; set; }

    /// <summary>
    /// Gets or sets the optional badge text. Blank renders no badge.
    /// </summary>
    [Parameter]
    public string? BadgeText { get; set; }

    /// <summary>
    /// Gets or sets the search text.
    /// </summary>
    [Parameter]
    public string SearchText { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the search text changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SearchTextChanged { get; set; }

    /// <summary>
    /// Gets or sets the model family filter options.
    /// </summary>
    [Parameter]
    public List<SelectedItem> FamilyItems { get; set; } = [];

    /// <summary>
    /// Gets or sets the selected family filter value.
    /// </summary>
    [Parameter]
    public string SelectedFamily { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the family filter changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedFamilyChanged { get; set; }

    /// <summary>
    /// Gets or sets the provider filter options.
    /// </summary>
    [Parameter]
    public List<SelectedItem> ProviderItems { get; set; } = [];

    /// <summary>
    /// Gets or sets the selected provider filter value.
    /// </summary>
    [Parameter]
    public string SelectedProvider { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the provider filter changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedProviderChanged { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether only favourites are shown.
    /// </summary>
    [Parameter]
    public bool ShowOnlyFavourites { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when the favourites toggle changes.
    /// </summary>
    [Parameter]
    public EventCallback<bool> ShowOnlyFavouritesChanged { get; set; }

    private string? ClassString => CssBuilder.Default("catalog-header catalog-search-header")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
