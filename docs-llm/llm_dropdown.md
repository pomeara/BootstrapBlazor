# Dropdown Component

## Overview
The Dropdown component in BootstrapBlazor provides a toggleable menu that allows users to choose one value from a predefined list. It's commonly used for navigation menus, selection controls, and action menus, offering a space-efficient way to present multiple options.

## Features
- Multiple display modes (button, link, split button)
- Custom item templates
- Icon support for items and trigger button
- Divider support for grouping items
- Header support for categorizing items
- Placement options (top, bottom, left, right)
- Disabled state for both dropdown and individual items
- Keyboard navigation support
- Custom trigger element support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Color.Primary` | Sets the color theme of the dropdown button |
| `Items` | `IEnumerable<DropdownItem>` | `null` | Collection of dropdown items to display |
| `DisplayText` | `string` | `null` | Text to display on the dropdown button |
| `Icon` | `string` | `null` | Icon to display on the dropdown button |
| `IsDisabled` | `bool` | `false` | Disables the dropdown when set to true |
| `IsDropup` | `bool` | `false` | When true, the dropdown menu appears above instead of below |
| `IsRight` | `bool` | `false` | When true, the dropdown menu is right-aligned |
| `IsOutsideClick` | `bool` | `true` | When true, clicking outside the dropdown closes it |
| `MenuAlignment` | `Alignment` | `Alignment.Left` | Sets the alignment of the dropdown menu |
| `PlacementTemplate` | `RenderFragment` | `null` | Custom template for dropdown placement |
| `ShowSplit` | `bool` | `false` | When true, displays a split button dropdown |
| `Size` | `Size` | `Size.None` | Sets the size of the dropdown button |
| `TagName` | `string` | `button` | HTML tag name for the dropdown trigger element |
| `ChildContent` | `RenderFragment` | `null` | Custom content for the dropdown button |
| `ItemTemplate` | `RenderFragment<DropdownItem>` | `null` | Custom template for rendering dropdown items |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the dropdown button is clicked |
| `OnClickItem` | Triggered when a dropdown item is clicked |
| `OnVisibleChanged` | Triggered when the dropdown visibility changes |

## Usage Examples

### Example 1: Basic Dropdown
```html
<Dropdown DisplayText="Actions">
    <Items>
        <DropdownItem Text="New" />
        <DropdownItem Text="Edit" />
        <DropdownItem Text="Delete" />
    </Items>
</Dropdown>
```
This example shows a basic dropdown with three action items: New, Edit, and Delete.

### Example 2: Dropdown with Icons
```html
<Dropdown DisplayText="File" Icon="fa fa-file">
    <Items>
        <DropdownItem Text="New" Icon="fa fa-plus" />
        <DropdownItem Text="Open" Icon="fa fa-folder-open" />
        <DropdownItem Text="Save" Icon="fa fa-save" />
        <DropdownItem IsDivider="true" />
        <DropdownItem Text="Exit" Icon="fa fa-sign-out" />
    </Items>
</Dropdown>
```
This example demonstrates a dropdown with icons for both the trigger button and individual items, plus a divider to separate the Exit option.

### Example 3: Split Button Dropdown
```html
<Dropdown DisplayText="Actions" ShowSplit="true" OnClick="@OnMainButtonClick">
    <Items>
        <DropdownItem Text="Option 1" />
        <DropdownItem Text="Option 2" />
        <DropdownItem Text="Option 3" />
    </Items>
</Dropdown>

@code {
    private Task OnMainButtonClick()
    {
        // Handle main button click
        return Task.CompletedTask;
    }
}
```
This example shows a split button dropdown where the main button has its own action, and the dropdown arrow reveals additional options.

### Example 4: Dropdown with Headers and Dividers
```html
<Dropdown DisplayText="User Settings">
    <Items>
        <DropdownItem IsHeader="true" Text="Account" />
        <DropdownItem Text="Profile" />
        <DropdownItem Text="Security" />
        <DropdownItem Text="Notifications" />
        <DropdownItem IsDivider="true" />
        <DropdownItem IsHeader="true" Text="System" />
        <DropdownItem Text="Preferences" />
        <DropdownItem Text="Language" />
        <DropdownItem IsDivider="true" />
        <DropdownItem Text="Logout" />
    </Items>
</Dropdown>
```
This example demonstrates a dropdown with headers to categorize items and dividers to separate different sections.

### Example 5: Custom Item Template
```html
<Dropdown DisplayText="Select User">
    <ItemTemplate Context="item">
        <div class="d-flex align-items-center">
            <img src="@item.Value" class="rounded-circle mr-2" style="width: 24px; height: 24px;" />
            <div>
                <div>@item.Text</div>
                <small class="text-muted">@item.Description</small>
            </div>
        </div>
    </ItemTemplate>
    <Items>
        <DropdownItem Text="John Doe" Description="Administrator" Value="/images/user1.jpg" />
        <DropdownItem Text="Jane Smith" Description="Developer" Value="/images/user2.jpg" />
        <DropdownItem Text="Bob Johnson" Description="Designer" Value="/images/user3.jpg" />
    </Items>
</Dropdown>
```
This example shows how to use a custom template for dropdown items, displaying user avatars alongside names and roles.

### Example 6: Dropdown with Different Placement
```html
<div class="d-flex justify-content-between">
    <Dropdown DisplayText="Dropdown Bottom" />
    
    <Dropdown DisplayText="Dropdown Top" IsDropup="true">
        <Items>
            <DropdownItem Text="Option 1" />
            <DropdownItem Text="Option 2" />
            <DropdownItem Text="Option 3" />
        </Items>
    </Dropdown>
    
    <Dropdown DisplayText="Dropdown Right" IsRight="true">
        <Items>
            <DropdownItem Text="Option 1" />
            <DropdownItem Text="Option 2" />
            <DropdownItem Text="Option 3" />
        </Items>
    </Dropdown>
</div>
```
This example demonstrates different dropdown menu placements: default (bottom-left), top (dropup), and right-aligned.

### Example 7: Dropdown with Event Handling
```html
<Dropdown DisplayText="Actions" OnClickItem="@OnItemClick">
    <Items>
        <DropdownItem Text="Create" Value="create" />
        <DropdownItem Text="Update" Value="update" />
        <DropdownItem Text="Delete" Value="delete" IsDisabled="@isDeleteDisabled" />
    </Items>
</Dropdown>

@code {
    private bool isDeleteDisabled = true;
    
    private async Task OnItemClick(DropdownItem item)
    {
        if (item.Value?.ToString() == "create")
        {
            await CreateItem();
            isDeleteDisabled = false;
        }
        else if (item.Value?.ToString() == "update")
        {
            await UpdateItem();
        }
        else if (item.Value?.ToString() == "delete")
        {
            await DeleteItem();
            isDeleteDisabled = true;
        }
    }
    
    private Task CreateItem() => Task.CompletedTask;
    private Task UpdateItem() => Task.CompletedTask;
    private Task DeleteItem() => Task.CompletedTask;
}
```
This example shows how to handle dropdown item clicks and conditionally enable/disable items based on application state.

## CSS Customization

The Dropdown component can be customized using the following CSS variables:

```css
--bb-dropdown-min-width: 10rem;
--bb-dropdown-padding-y: 0.5rem;
--bb-dropdown-padding-x: 0;
--bb-dropdown-color: #212529;
--bb-dropdown-bg: #fff;
--bb-dropdown-border-color: rgba(0, 0, 0, 0.15);
--bb-dropdown-border-radius: 0.25rem;
--bb-dropdown-border-width: 1px;
--bb-dropdown-box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.175);
--bb-dropdown-divider-bg: #e9ecef;
--bb-dropdown-divider-margin-y: 0.5rem;
--bb-dropdown-item-padding-y: 0.25rem;
--bb-dropdown-item-padding-x: 1.5rem;
--bb-dropdown-header-color: #6c757d;
--bb-dropdown-header-padding: 0.5rem 1.5rem;
--bb-dropdown-item-hover-bg: #f8f9fa;
--bb-dropdown-item-active-color: #fff;
--bb-dropdown-item-active-bg: #0d6efd;
--bb-dropdown-item-disabled-color: #adb5bd;
```

## Notes

1. **Accessibility**: The Dropdown component includes ARIA attributes for better accessibility. It uses `aria-haspopup="true"`, `aria-expanded`, and proper keyboard navigation support.

2. **Keyboard Navigation**: Users can navigate dropdown items using the arrow keys, select with Enter, and close the dropdown with Escape.

3. **Mobile Considerations**: On mobile devices, ensure dropdown items have sufficient touch target size (at least 44x44 pixels) for better usability.

4. **Nesting**: While it's technically possible to nest dropdowns, it's generally not recommended for usability reasons. Consider using a different UI pattern for complex hierarchical menus.

5. **Performance**: For dropdowns with a large number of items, consider implementing virtualization or pagination to improve performance.