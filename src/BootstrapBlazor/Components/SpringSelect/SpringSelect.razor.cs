// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Components;

/// <summary>
/// Space-saving select that collapses to a single chip and expands on demand.
/// </summary>
public sealed partial class SpringSelect
{
    /// <summary>
    /// Gets or sets the selectable items.
    /// </summary>
    [Parameter]
    public IEnumerable<SpringSelectItem> Items { get; set; } = [];

    /// <summary>
    /// Gets or sets the selected item value.
    /// </summary>
    [Parameter]
    public string Selected { get; set; } = "";

    /// <summary>
    /// Gets or sets the callback fired when the selection changes.
    /// </summary>
    [Parameter]
    public EventCallback<string> SelectedChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback fired when the user selects an item.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnSelected { get; set; }

    /// <summary>
    /// Gets or sets the placeholder shown when nothing is selected.
    /// </summary>
    [Parameter]
    public string Placeholder { get; set; } = "Select...";

    /// <summary>
    /// Gets or sets the chip tooltip text.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets the full-content template per item, rendered in BOTH modes — the collapsed chip
    /// shows the SELECTED item's template (badges, counts, icons), and every expanded option
    /// button renders the same template. Null = the default icon+label rendering. The fragment
    /// receives the item; rich consumers read their own model back off <c>item.Data</c>.
    /// </summary>
    [Parameter]
    public RenderFragment<SpringSelectItem>? ItemTemplate { get; set; }

    private bool _isExpanded;
    private string _selectedValue = "";

    private SpringSelectItem? _selectedItem =>
        Items.FirstOrDefault(i => i.Value == _selectedValue);

    private string? ClassString => CssBuilder.Default("spring-select")
        .AddClass("spring-expanded", _isExpanded)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Keeps the collapsed chip labelled from the very first render. The parent's
    /// push is adopted only while it names a rendered item; an empty or unmatched selection
    /// falls back to the FIRST item so the default is always visible. Display-only — never
    /// raises SelectedChanged/OnSelected from the lifecycle, which would re-enter the
    /// render loop (the BootstrapBlazor Select re-entrancy lesson).
    /// </summary>
    protected override void OnParametersSet()
    {
        if (Items.Any(i => i.Value == Selected))
            _selectedValue = Selected;

        if (Items.Any() && _selectedItem is null)
            _selectedValue = Items.First().Value;
    }

    private void Expand()
    {
        _isExpanded = true;
    }

    private void Collapse()
    {
        _isExpanded = false;
    }

    private async Task SelectItem(SpringSelectItem item)
    {
        _selectedValue = item.Value;
        Collapse();
        await SelectedChanged.InvokeAsync(item.Value);
        await OnSelected.InvokeAsync(item.Value);
    }
}
