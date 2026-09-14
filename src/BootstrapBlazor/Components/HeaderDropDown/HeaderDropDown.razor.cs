// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Toolbar-friendly dropdown for page headers.
/// </summary>
public sealed partial class HeaderDropDown
{
    /// <summary>
    /// Gets or sets the optional dropdown button icon.
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the dropdown items.
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the selected item value.
    /// </summary>
    [Parameter]
    public string? SelectedValue { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when the selected value changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when the user selects an item.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnSelected { get; set; }

    private string _selectedValue = "";

    private string? ClassString => CssBuilder.Default("header-dropdown")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Syncs the external <see cref="SelectedValue"/> into the internal dropdown value.
    /// </summary>
    protected override void OnParametersSet()
    {
        if (SelectedValue != null)
            _selectedValue = SelectedValue;
    }

    private async Task HandleSelected(SelectedItem item)
    {
        _selectedValue = item.Value;
        await SelectedValueChanged.InvokeAsync(item.Value);
        await OnSelected.InvokeAsync(item.Value);
    }
}
