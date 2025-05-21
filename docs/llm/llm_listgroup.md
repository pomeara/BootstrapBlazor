# ListGroup Component

## Overview
The ListGroup component in BootstrapBlazor provides a flexible and powerful way to display a series of content items as a vertically stacked list. It's commonly used for displaying simple collections of elements, navigation menus, or grouped content with headers. The component supports various styling options, interactive states, and custom content, making it versatile for many UI scenarios.

## Features
- **Multiple Content Types**: Support for text, links, buttons, and custom content
- **Interactive States**: Hover, active, and disabled states for list items
- **Contextual Classes**: Color variations for different states or meanings
- **Flush Styling**: Option for borderless list items
- **Horizontal Layout**: Alternative horizontal display mode
- **Custom Content**: Support for complex content with badges, icons, and custom templates
- **Actionable Items**: Click events and navigation support
- **Grouped Headers**: Support for group headers to organize content
- **Nested Lists**: Ability to create hierarchical list structures
- **Accessibility Support**: Proper ARIA attributes for screen readers

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<ListGroupItem>` | `null` | Collection of items to display in the list group |
| `ActiveItem` | `ListGroupItem` | `null` | The currently active item in the list group |
| `IsFlush` | `bool` | `false` | When true, removes outer borders and rounded corners |
| `IsHorizontal` | `bool` | `false` | When true, displays items horizontally instead of vertically |
| `IsBordered` | `bool` | `true` | When true, adds borders between list items |
| `IsActionable` | `bool` | `false` | When true, makes items interactive with hover and active states |
| `ItemTemplate` | `RenderFragment<ListGroupItem>` | `null` | Custom template for rendering list items |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the list group header |
| `FooterTemplate` | `RenderFragment` | `null` | Custom template for the list group footer |

## Events

| Event | Description |
| --- | --- |
| `OnItemClick` | Triggered when a list item is clicked |
| `OnActiveItemChanged` | Triggered when the active item changes |

## Usage Examples

### Example 1: Basic ListGroup

```razor
<ListGroup>
    <Items>
        <ListGroupItem>Item 1</ListGroupItem>
        <ListGroupItem>Item 2</ListGroupItem>
        <ListGroupItem>Item 3</ListGroupItem>
        <ListGroupItem>Item 4</ListGroupItem>
        <ListGroupItem>Item 5</ListGroupItem>
    </Items>
</ListGroup>
```

This example shows a basic list group with five simple text items.

### Example 2: ListGroup with Active and Disabled Items

```razor
<ListGroup IsActionable="true">
    <Items>
        <ListGroupItem>Regular item</ListGroupItem>
        <ListGroupItem IsActive="true">Active item</ListGroupItem>
        <ListGroupItem IsDisabled="true">Disabled item</ListGroupItem>
        <ListGroupItem>Another regular item</ListGroupItem>
    </Items>
</ListGroup>
```

This example demonstrates a list group with interactive items, including an active item and a disabled item.

### Example 3: ListGroup with Contextual Classes

```razor
<ListGroup>
    <Items>
        <ListGroupItem Color="Color.Primary">Primary</ListGroupItem>
        <ListGroupItem Color="Color.Secondary">Secondary</ListGroupItem>
        <ListGroupItem Color="Color.Success">Success</ListGroupItem>
        <ListGroupItem Color="Color.Danger">Danger</ListGroupItem>
        <ListGroupItem Color="Color.Warning">Warning</ListGroupItem>
        <ListGroupItem Color="Color.Info">Info</ListGroupItem>
        <ListGroupItem Color="Color.Light">Light</ListGroupItem>
        <ListGroupItem Color="Color.Dark">Dark</ListGroupItem>
    </Items>
</ListGroup>
```

This example shows a list group with items styled using different contextual colors.

### Example 4: ListGroup with Custom Content

```razor
<ListGroup>
    <Items>
        <ListGroupItem>
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">List group item heading</h5>
                <small>3 days ago</small>
            </div>
            <p class="mb-1">Some placeholder content in a paragraph.</p>
            <small>And some small print.</small>
        </ListGroupItem>
        <ListGroupItem>
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">Another list group item</h5>
                <small>2 days ago</small>
            </div>
            <p class="mb-1">Some more placeholder content.</p>
            <small>And some more small print.</small>
        </ListGroupItem>
        <ListGroupItem>
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">Third list group item</h5>
                <small>1 day ago</small>
            </div>
            <p class="mb-1">Yet more placeholder content.</p>
            <small>And yet some more small print.</small>
        </ListGroupItem>
    </Items>
</ListGroup>
```

This example demonstrates a list group with custom content including headings, paragraphs, and small text.

### Example 5: Horizontal ListGroup

```razor
<ListGroup IsHorizontal="true">
    <Items>
        <ListGroupItem>Home</ListGroupItem>
        <ListGroupItem IsActive="true">Profile</ListGroupItem>
        <ListGroupItem>Messages</ListGroupItem>
        <ListGroupItem>Settings</ListGroupItem>
    </Items>
</ListGroup>
```

This example shows a horizontally displayed list group that could be used for navigation.

### Example 6: ListGroup with Badges

```razor
<ListGroup>
    <Items>
        <ListGroupItem>
            <div class="d-flex justify-content-between align-items-center">
                <span>Unread messages</span>
                <Badge Color="Color.Primary">14</Badge>
            </div>
        </ListGroupItem>
        <ListGroupItem>
            <div class="d-flex justify-content-between align-items-center">
                <span>Pending requests</span>
                <Badge Color="Color.Warning">2</Badge>
            </div>
        </ListGroupItem>
        <ListGroupItem>
            <div class="d-flex justify-content-between align-items-center">
                <span>Tasks completed</span>
                <Badge Color="Color.Success">8</Badge>
            </div>
        </ListGroupItem>
    </Items>
</ListGroup>
```

This example demonstrates a list group with badges to display counts or status indicators.

### Example 7: Interactive ListGroup with Event Handling

```razor
<div class="row">
    <div class="col-md-4">
        <ListGroup IsActionable="true" OnItemClick="HandleItemClick">
            <Items>
                @foreach (var item in categories)
                {
                    <ListGroupItem IsActive="item.Id == activeCategory?.Id" Value="@item.Id">
                        @item.Name
                    </ListGroupItem>
                }
            </Items>
        </ListGroup>
    </div>
    <div class="col-md-8">
        @if (activeCategory != null)
        {
            <div class="card">
                <div class="card-header">
                    <h5 class="mb-0">@activeCategory.Name</h5>
                </div>
                <div class="card-body">
                    <p>@activeCategory.Description</p>
                    <ul class="list-group list-group-flush">
                        @foreach (var item in GetItemsByCategory(activeCategory.Id))
                        {
                            <li class="list-group-item">@item.Name</li>
                        }
                    </ul>
                </div>
            </div>
        }
        else
        {
            <div class="alert alert-info">Select a category to view details</div>
        }
    </div>
</div>

@code {
    private List<Category> categories = new List<Category>
    {
        new Category { Id = 1, Name = "Electronics", Description = "Electronic devices and accessories" },
        new Category { Id = 2, Name = "Clothing", Description = "Apparel and fashion items" },
        new Category { Id = 3, Name = "Home & Kitchen", Description = "Household items and appliances" },
        new Category { Id = 4, Name = "Books", Description = "Books and publications" }
    };
    
    private Category activeCategory;
    
    private List<Item> items = new List<Item>
    {
        new Item { Id = 1, CategoryId = 1, Name = "Smartphone" },
        new Item { Id = 2, CategoryId = 1, Name = "Laptop" },
        new Item { Id = 3, CategoryId = 1, Name = "Headphones" },
        new Item { Id = 4, CategoryId = 2, Name = "T-Shirt" },
        new Item { Id = 5, CategoryId = 2, Name = "Jeans" },
        new Item { Id = 6, CategoryId = 3, Name = "Blender" },
        new Item { Id = 7, CategoryId = 3, Name = "Coffee Maker" },
        new Item { Id = 8, CategoryId = 4, Name = "Novel" },
        new Item { Id = 9, CategoryId = 4, Name = "Cookbook" }
    };
    
    private void HandleItemClick(ListGroupItem item)
    {
        if (item.Value != null && int.TryParse(item.Value.ToString(), out int categoryId))
        {
            activeCategory = categories.FirstOrDefault(c => c.Id == categoryId);
        }
    }
    
    private List<Item> GetItemsByCategory(int categoryId)
    {
        return items.Where(i => i.CategoryId == categoryId).ToList();
    }
    
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    
    public class Item
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
```

This example demonstrates an interactive list group that displays categories. When a category is clicked, it becomes active and displays related items in a card on the right.

## Customization Notes

### CSS Variables

The ListGroup component can be customized using CSS variables:

```css
:root {
    --bb-list-group-color: #212529;
    --bb-list-group-bg: #fff;
    --bb-list-group-border-color: rgba(0, 0, 0, 0.125);
    --bb-list-group-border-width: 1px;
    --bb-list-group-border-radius: 0.25rem;
    
    --bb-list-group-item-padding-x: 1.25rem;
    --bb-list-group-item-padding-y: 0.75rem;
    
    --bb-list-group-hover-bg: #f8f9fa;
    --bb-list-group-active-color: #fff;
    --bb-list-group-active-bg: #0d6efd;
    --bb-list-group-active-border-color: #0d6efd;
    
    --bb-list-group-disabled-color: #6c757d;
    --bb-list-group-disabled-bg: #fff;
    
    --bb-list-group-action-color: #495057;
    --bb-list-group-action-hover-color: #495057;
    
    --bb-list-group-action-active-color: #212529;
    --bb-list-group-action-active-bg: #e9ecef;
}
```

### Custom Templates

The ListGroup component provides several template parameters for customization:

1. `ItemTemplate`: Defines how each item is rendered
2. `HeaderTemplate`: Customizes the list group header
3. `FooterTemplate`: Customizes the list group footer

### Responsive Behavior

When using the horizontal layout mode, you can control the breakpoint at which the list group switches from vertical to horizontal:

```razor
<ListGroup IsHorizontal="true" HorizontalBreakpoint="Breakpoint.MD">
    <!-- Items -->
</ListGroup>
```

This creates a list group that displays horizontally on medium-sized screens and larger, but vertically on smaller screens.

### Integration with Other Components

The ListGroup component works well with:

1. **Badge Component**: For displaying counts or status indicators
2. **Button Component**: For actionable items
3. **Icon Component**: For visual indicators
4. **Card Component**: For more complex content layouts
5. **Tab Component**: For tabbed interfaces