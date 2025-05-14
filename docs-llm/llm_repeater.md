# Repeater Component

## Overview

The Repeater component in BootstrapBlazor provides a flexible way to render collections of data with custom templates. It serves as a lightweight alternative to more complex data display components like Table or ListView, allowing developers to iterate through data collections and apply custom rendering logic for each item. The Repeater is particularly useful for simple data presentations where the full feature set of a Table or ListView is not required.

## Features

- **Collection Rendering**: Efficiently renders any enumerable collection of data
- **Custom Item Templates**: Flexible templating system for customizing how each item is displayed
- **Empty State Handling**: Configurable empty state display when the collection contains no items
- **Conditional Rendering**: Support for conditional display logic within item templates
- **Index Tracking**: Access to the current item's index during rendering
- **Metadata Support**: Ability to include additional metadata with each item
- **Performance Optimization**: Efficient rendering of large collections with minimal overhead

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Items` | `IEnumerable<TItem>` | `null` | The collection of items to render. This can be any enumerable collection of the specified type. |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Template used to render each item in the collection. This is required for the component to function. |
| `EmptyTemplate` | `RenderFragment` | `null` | Template displayed when the Items collection is null or empty. |
| `ItemsChanged` | `EventCallback<IEnumerable<TItem>>` | - | Event callback invoked when the Items collection changes. |
| `Context` | `object` | `null` | Additional context data that can be accessed within the item template. |
| `ItemsPerRow` | `int` | `1` | Number of items to display per row when using grid layout. |
| `WrapperClass` | `string` | `""` | CSS class applied to the wrapper element containing all items. |
| `ItemClass` | `string` | `""` | CSS class applied to each item's wrapper element. |
| `EnableLogging` | `bool` | `false` | When true, enables logging of component lifecycle events for debugging. |

## Events

| Event | Description |
|-------|-------------|
| `OnItemRendered` | Triggered after each item is rendered. Provides the item and its index. |
| `OnAllItemsRendered` | Triggered after all items in the collection have been rendered. |
| `OnEmptyTemplateRendered` | Triggered when the empty template is rendered due to no items being present. |

## Usage Examples

### Example 1: Basic Repeater with Simple Items

A simple repeater displaying a list of string items:

```razor
<Repeater Items="@_items">
    <ItemTemplate>
        <div class="alert alert-info mb-2">@context</div>
    </ItemTemplate>
    <EmptyTemplate>
        <div class="alert alert-warning">No items available.</div>
    </EmptyTemplate>
</Repeater>

@code {
    private List<string> _items = new()
    {
        "Item 1",
        "Item 2",
        "Item 3"
    };
}
```

### Example 2: Repeater with Complex Objects and Custom Styling

Displaying a collection of product objects with custom styling:

```razor
<Repeater Items="@_products" 
          WrapperClass="product-grid" 
          ItemClass="product-card">
    <ItemTemplate>
        <div class="card h-100">
            <img src="@context.ImageUrl" class="card-img-top" alt="@context.Name">
            <div class="card-body">
                <h5 class="card-title">@context.Name</h5>
                <p class="card-text">@context.Description</p>
                <div class="d-flex justify-content-between align-items-center">
                    <span class="price">@context.Price.ToString("C")</span>
                    <Button Color="Color.Primary" Size="Size.Small">Add to Cart</Button>
                </div>
            </div>
        </div>
    </ItemTemplate>
    <EmptyTemplate>
        <div class="alert alert-info">
            <i class="fa-solid fa-info-circle me-2"></i>
            <span>No products available. Please check back later.</span>
        </div>
    </EmptyTemplate>
</Repeater>

<style>
    .product-grid {
        display: grid;
        grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
        gap: 1.5rem;
    }
    
    .product-card {
        transition: transform 0.2s;
    }
    
    .product-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 20px rgba(0,0,0,0.1);
    }
    
    .price {
        font-weight: bold;
        color: #dc3545;
    }
</style>

@code {
    private List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop with SSD", Price = 999.99m, ImageUrl = "/images/laptop.jpg" },
        new Product { Id = 2, Name = "Smartphone", Description = "Latest model with advanced camera", Price = 699.99m, ImageUrl = "/images/smartphone.jpg" },
        new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling wireless headphones", Price = 199.99m, ImageUrl = "/images/headphones.jpg" }
    };
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
```

### Example 3: Repeater with Grid Layout and Index Tracking

Displaying items in a grid layout with different styling based on index:

```razor
<Repeater Items="@_tiles" 
          ItemsPerRow="3" 
          WrapperClass="tile-container">
    <ItemTemplate>
        @{
            var index = _tiles.IndexOf(context);
            var colorClass = index % 3 == 0 ? "bg-primary" : 
                            index % 3 == 1 ? "bg-success" : "bg-info";
        }
        <div class="tile @colorClass">
            <div class="tile-index">@(index + 1)</div>
            <div class="tile-content">@context</div>
        </div>
    </ItemTemplate>
</Repeater>

<style>
    .tile-container {
        display: grid;
        grid-template-columns: repeat(3, 1fr);
        gap: 1rem;
    }
    
    .tile {
        height: 150px;
        border-radius: 8px;
        padding: 1rem;
        color: white;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
        transition: all 0.3s ease;
    }
    
    .tile:hover {
        transform: scale(1.05);
    }
    
    .tile-index {
        font-size: 2rem;
        font-weight: bold;
    }
    
    .tile-content {
        margin-top: 0.5rem;
        text-align: center;
    }
</style>

@code {
    private List<string> _tiles = new()
    {
        "Analytics", "Reports", "Users",
        "Settings", "Messages", "Files",
        "Calendar", "Tasks", "Help"
    };
}
```

### Example 4: Dynamic Repeater with Add/Remove Functionality

A repeater with the ability to add and remove items dynamically:

```razor
<div class="mb-3">
    <div class="input-group">
        <input @bind="_newItem" class="form-control" placeholder="Enter new item..." />
        <Button Color="Color.Primary" OnClick="AddItem">Add Item</Button>
    </div>
</div>

<Repeater Items="@_dynamicItems" @ref="_repeater">
    <ItemTemplate>
        <div class="d-flex justify-content-between align-items-center p-2 border mb-2 rounded">
            <span>@context</span>
            <Button Color="Color.Danger" 
                    Size="Size.Small" 
                    OnClick="() => RemoveItem(context)">Remove</Button>
        </div>
    </ItemTemplate>
    <EmptyTemplate>
        <div class="alert alert-secondary">
            <i class="fa-solid fa-list me-2"></i>
            <span>Your list is empty. Add some items above.</span>
        </div>
    </EmptyTemplate>
</Repeater>

@code {
    private Repeater<string> _repeater;
    private List<string> _dynamicItems = new();
    private string _newItem;

    private void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(_newItem))
        {
            _dynamicItems.Add(_newItem);
            _newItem = string.Empty;
            
            // Force the repeater to refresh
            _repeater.Refresh();
        }
    }

    private void RemoveItem(string item)
    {
        _dynamicItems.Remove(item);
        
        // Force the repeater to refresh
        _repeater.Refresh();
    }
}
```

### Example 5: Nested Repeaters for Hierarchical Data

Using nested repeaters to display hierarchical data:

```razor
<Repeater Items="@_categories">
    <ItemTemplate>
        <div class="category mb-4">
            <h4>@context.Name</h4>
            <div class="ps-4 border-start">
                <Repeater Items="@context.Items">
                    <ItemTemplate>
                        <div class="d-flex align-items-center py-2">
                            <i class="@context.Icon me-2"></i>
                            <span>@context.Name</span>
                        </div>
                    </ItemTemplate>
                    <EmptyTemplate>
                        <div class="text-muted fst-italic">No items in this category</div>
                    </EmptyTemplate>
                </Repeater>
            </div>
        </div>
    </ItemTemplate>
</Repeater>

@code {
    private List<Category> _categories = new()
    {
        new Category
        {
            Name = "Electronics",
            Items = new List<CategoryItem>
            {
                new CategoryItem { Name = "Laptops", Icon = "fa-solid fa-laptop" },
                new CategoryItem { Name = "Smartphones", Icon = "fa-solid fa-mobile-alt" },
                new CategoryItem { Name = "Tablets", Icon = "fa-solid fa-tablet-alt" }
            }
        },
        new Category
        {
            Name = "Clothing",
            Items = new List<CategoryItem>
            {
                new CategoryItem { Name = "Men's", Icon = "fa-solid fa-male" },
                new CategoryItem { Name = "Women's", Icon = "fa-solid fa-female" }
            }
        },
        new Category
        {
            Name = "Books",
            Items = new List<CategoryItem>()
        }
    };

    public class Category
    {
        public string Name { get; set; }
        public List<CategoryItem> Items { get; set; } = new();
    }

    public class CategoryItem
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}
```

### Example 6: Repeater with Pagination

Implementing a simple pagination system with the Repeater component:

```razor
<div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <h5 class="mb-0">User List</h5>
        <div>
            <span>Page @(_currentPage + 1) of @_totalPages</span>
        </div>
    </div>
    <div class="card-body">
        <Repeater Items="@GetCurrentPageItems()">
            <ItemTemplate>
                <div class="user-item d-flex align-items-center p-2 border-bottom">
                    <div class="user-avatar me-3">
                        <img src="@context.AvatarUrl" alt="@context.Name" class="rounded-circle" width="48" height="48">
                    </div>
                    <div>
                        <div class="fw-bold">@context.Name</div>
                        <div class="text-muted small">@context.Email</div>
                    </div>
                </div>
            </ItemTemplate>
            <EmptyTemplate>
                <div class="text-center py-4">
                    <i class="fa-solid fa-users fa-2x mb-2 text-muted"></i>
                    <p>No users found</p>
                </div>
            </EmptyTemplate>
        </Repeater>
    </div>
    <div class="card-footer">
        <div class="d-flex justify-content-between">
            <Button Color="Color.Secondary" 
                    OnClick="PreviousPage" 
                    IsDisabled="@(_currentPage <= 0)">
                <i class="fa-solid fa-chevron-left me-1"></i> Previous
            </Button>
            <Button Color="Color.Secondary" 
                    OnClick="NextPage" 
                    IsDisabled="@(_currentPage >= _totalPages - 1)">
                Next <i class="fa-solid fa-chevron-right ms-1"></i>
            </Button>
        </div>
    </div>
</div>

@code {
    private List<User> _allUsers = new();
    private int _currentPage = 0;
    private int _itemsPerPage = 5;
    private int _totalPages => (int)Math.Ceiling(_allUsers.Count / (double)_itemsPerPage);

    protected override void OnInitialized()
    {
        // Generate sample users
        for (int i = 1; i <= 23; i++)
        {
            _allUsers.Add(new User
            {
                Id = i,
                Name = $"User {i}",
                Email = $"user{i}@example.com",
                AvatarUrl = $"/images/avatar-{i % 10}.jpg"
            });
        }
    }

    private List<User> GetCurrentPageItems()
    {
        return _allUsers
            .Skip(_currentPage * _itemsPerPage)
            .Take(_itemsPerPage)
            .ToList();
    }

    private void PreviousPage()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
        }
    }

    private void NextPage()
    {
        if (_currentPage < _totalPages - 1)
        {
            _currentPage++;
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
    }
}
```

### Example 7: Repeater with Drag and Drop Reordering

Implementing drag and drop reordering of items in a Repeater:

```razor
@page "/task-list"
@using System.Text.Json

<div class="card">
    <div class="card-header">
        <h5 class="mb-0">Task List</h5>
    </div>
    <div class="card-body">
        <div class="mb-3">
            <div class="input-group">
                <input @bind="_newTaskTitle" class="form-control" placeholder="Enter new task..." />
                <Button Color="Color.Primary" OnClick="AddTask">Add Task</Button>
            </div>
        </div>
        
        <div class="task-list" ondragover="event.preventDefault();">
            <Repeater Items="@_tasks">
                <ItemTemplate>
                    @{
                        var task = context;
                        var index = _tasks.IndexOf(task);
                    }
                    <div class="task-item @(task.IsCompleted ? "completed" : "")" 
                         draggable="true"
                         @ondragstart="@(() => HandleDragStart(index))"
                         @ondrop="@(() => HandleDrop(index))"
                         @ondragenter="@(() => HandleDragEnter(index))">
                        <div class="d-flex align-items-center">
                            <div class="drag-handle me-2">
                                <i class="fa-solid fa-grip-vertical text-muted"></i>
                            </div>
                            <Checkbox @bind-Value="task.IsCompleted" ShowLabel="false" />
                            <span class="ms-2 task-title">@task.Title</span>
                        </div>
                        <Button Color="Color.Danger" 
                                Size="Size.Small" 
                                OnClick="() => RemoveTask(task)">
                            <i class="fa-solid fa-trash"></i>
                        </Button>
                    </div>
                </ItemTemplate>
                <EmptyTemplate>
                    <div class="text-center py-4">
                        <i class="fa-solid fa-check-square fa-2x mb-2 text-muted"></i>
                        <p>No tasks yet. Add some tasks to get started.</p>
                    </div>
                </EmptyTemplate>
            </Repeater>
        </div>
    </div>
</div>

<style>
    .task-list {
        display: flex;
        flex-direction: column;
        gap: 0.5rem;
    }
    
    .task-item {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 0.75rem;
        border: 1px solid #dee2e6;
        border-radius: 0.25rem;
        background-color: white;
        transition: all 0.2s;
    }
    
    .task-item:hover {
        background-color: #f8f9fa;
    }
    
    .task-item.completed .task-title {
        text-decoration: line-through;
        color: #6c757d;
    }
    
    .task-item.drag-over {
        border: 1px dashed #007bff;
        background-color: rgba(0, 123, 255, 0.05);
    }
    
    .drag-handle {
        cursor: grab;
    }
</style>

@code {
    private List<TaskItem> _tasks = new();
    private string _newTaskTitle;
    private int _draggedIndex = -1;

    protected override void OnInitialized()
    {
        // Initialize with sample tasks
        _tasks = new List<TaskItem>
        {
            new TaskItem { Title = "Complete project documentation", IsCompleted = false },
            new TaskItem { Title = "Review pull requests", IsCompleted = true },
            new TaskItem { Title = "Prepare for team meeting", IsCompleted = false },
            new TaskItem { Title = "Update dependencies", IsCompleted = false }
        };
    }

    private void AddTask()
    {
        if (!string.IsNullOrWhiteSpace(_newTaskTitle))
        {
            _tasks.Add(new TaskItem { Title = _newTaskTitle, IsCompleted = false });
            _newTaskTitle = string.Empty;
        }
    }

    private void RemoveTask(TaskItem task)
    {
        _tasks.Remove(task);
    }

    private void HandleDragStart(int index)
    {
        _draggedIndex = index;
    }

    private void HandleDragEnter(int index)
    {
        if (_draggedIndex != index)
        {
            // Add visual feedback for the drop target
            // In a real implementation, you would add/remove CSS classes here
        }
    }

    private void HandleDrop(int index)
    {
        if (_draggedIndex != -1 && _draggedIndex != index)
        {
            // Reorder the tasks
            var item = _tasks[_draggedIndex];
            _tasks.RemoveAt(_draggedIndex);
            _tasks.Insert(index, item);
            
            _draggedIndex = -1;
        }
    }

    public class TaskItem
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
```

## CSS Customization

The Repeater component itself doesn't apply specific styling to the rendered content, allowing for complete customization through the following approaches:

1. **WrapperClass Property**: Apply CSS classes to the container element that wraps all items
2. **ItemClass Property**: Apply CSS classes to each individual item's wrapper element
3. **Custom CSS**: Define your own CSS rules targeting the content within item templates
4. **CSS Grid/Flexbox**: Use modern CSS layout techniques within templates for responsive designs

## JavaScript Interop

The Repeater component primarily operates on the server side and doesn't require JavaScript interop for its core functionality. However, you can implement JavaScript interop within the item templates for advanced scenarios such as:

- Animation effects when items are added or removed
- Drag and drop reordering of items
- Client-side filtering or sorting of items
- Lazy loading of content within items

## Accessibility Considerations

When using the Repeater component, consider the following accessibility best practices:

1. Include proper semantic HTML elements within item templates (headings, lists, etc.)
2. Ensure sufficient color contrast for text and interactive elements
3. Add appropriate ARIA attributes for complex interactive elements within templates
4. Maintain keyboard navigability for interactive elements within repeater items
5. Consider screen reader announcements for dynamically changing content

## Browser Compatibility

The Repeater component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. There are no specific browser compatibility issues to be aware of, as the component primarily relies on standard HTML rendering.

## Integration with Other Components

The Repeater component works well with:

- **Form Components**: For editing or displaying form fields for each item
- **Button Components**: For actions related to individual items
- **Card Components**: For structured display of complex items
- **Icon Components**: For visual indicators within item templates
- **Modal/Dialog Components**: For detailed views or editing of items

## Best Practices

1. Keep item templates as simple as possible for better performance
2. Use the Context property to pass additional data to templates when needed
3. Consider implementing virtualization for very large collections
4. Use the OnItemRendered event for performance monitoring or custom initialization
5. Implement proper empty state handling for better user experience
6. Consider using CSS Grid or Flexbox for responsive layouts within the repeater