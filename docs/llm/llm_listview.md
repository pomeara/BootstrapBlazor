# ListView Component

## Overview
The ListView component in BootstrapBlazor provides a flexible and efficient way to display collections of data in a list format. It combines the simplicity of the ListGroup with the data binding capabilities of more complex components, making it ideal for displaying structured data with custom templates. The ListView is particularly useful for scenarios where you need to present data in a scrollable, selectable list with support for various item states, custom rendering, and event handling.

## Features
- **Data Binding**: Efficiently binds to any enumerable collection of data
- **Item Templates**: Flexible templating system for customizing how each item is displayed
- **Selection Support**: Single or multiple item selection capabilities
- **Virtual Scrolling**: Efficient rendering of large data sets with virtualization
- **Sorting and Filtering**: Built-in support for sorting and filtering list items
- **Grouping**: Ability to group related items with headers
- **Interactive States**: Support for hover, active, and disabled states
- **Custom Styling**: Extensive customization options through CSS variables
- **Keyboard Navigation**: Support for keyboard shortcuts to navigate the list
- **Drag and Drop**: Optional support for reordering items through drag and drop
- **Lazy Loading**: Support for incrementally loading data as the user scrolls
- **Accessibility Support**: ARIA attributes for screen readers

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of items to display in the list view |
| `SelectedItem` | `TItem` | `null` | Currently selected item in the list |
| `SelectedItems` | `IEnumerable<TItem>` | `null` | Collection of selected items when multiple selection is enabled |
| `TextField` | `string` | `null` | Property name to use as the display text for items |
| `ValueField` | `string` | `null` | Property name to use as the value for items |
| `IconField` | `string` | `null` | Property name to use for item icons |
| `IsVirtualized` | `bool` | `false` | Whether to use virtualization for rendering large lists |
| `ItemHeight` | `int` | `40` | Height of each item in pixels when virtualization is enabled |
| `PageSize` | `int` | `20` | Number of items to load at once when using lazy loading |
| `AllowMultipleSelection` | `bool` | `false` | Whether multiple items can be selected simultaneously |
| `IsDisabled` | `bool` | `false` | Whether the list view component is disabled |
| `ShowBorder` | `bool` | `true` | Whether to show borders around the list |
| `ShowHeader` | `bool` | `false` | Whether to show a header for the list |
| `HeaderText` | `string` | `null` | Text to display in the list header when ShowHeader is true |
| `EmptyText` | `string` | `"No items to display"` | Text to display when the list is empty |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Custom template for rendering list items |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for rendering the list header |
| `EmptyTemplate` | `RenderFragment` | `null` | Custom template for rendering when the list is empty |
| `GroupHeaderTemplate` | `RenderFragment<TGroup>` | `null` | Custom template for rendering group headers when grouping is enabled |
| `AllowDragDrop` | `bool` | `false` | Whether drag and drop operations are allowed |

## Events

| Event | Description |
| --- | --- |
| `OnItemClick` | Triggered when an item is clicked |
| `OnItemDoubleClick` | Triggered when an item is double-clicked |
| `OnSelectionChanged` | Triggered when the selection changes |
| `OnItemSelect` | Triggered when an item is selected |
| `OnItemUnselect` | Triggered when an item is unselected |
| `OnScroll` | Triggered when the list is scrolled |
| `OnScrollEnd` | Triggered when the user scrolls to the end of the list |
| `OnItemDragStart` | Triggered when a drag operation starts on an item |
| `OnItemDragOver` | Triggered when a dragged item is over another item |
| `OnItemDrop` | Triggered when an item is dropped onto another item |
| `OnDataRequested` | Triggered when more data is requested during lazy loading |
| `OnGroupHeaderClick` | Triggered when a group header is clicked |

## Usage Examples

### Example 1: Basic ListView

```razor
<ListView Items="@contacts"
         TextField="Name"
         @bind-SelectedItem="selectedContact"
         OnItemClick="HandleItemClick">
</ListView>

<div class="mt-3">
    @if (selectedContact != null)
    {
        <p>Selected: <strong>@selectedContact.Name</strong> (@selectedContact.Email)</p>
    }
</div>

@code {
    private List<Contact> contacts = new List<Contact>();
    private Contact selectedContact;
    
    protected override void OnInitialized()
    {
        // Create sample contact data
        contacts.Add(new Contact { Id = 1, Name = "John Smith", Email = "john@example.com", Phone = "555-1234" });
        contacts.Add(new Contact { Id = 2, Name = "Jane Doe", Email = "jane@example.com", Phone = "555-5678" });
        contacts.Add(new Contact { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", Phone = "555-9012" });
        contacts.Add(new Contact { Id = 4, Name = "Alice Williams", Email = "alice@example.com", Phone = "555-3456" });
        contacts.Add(new Contact { Id = 5, Name = "Mike Brown", Email = "mike@example.com", Phone = "555-7890" });
    }
    
    private void HandleItemClick(Contact contact)
    {
        Console.WriteLine($"Clicked: {contact.Name}");
    }
    
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
```

This example shows a basic list view with a collection of contacts, displaying the contact name and allowing selection of a single item.

### Example 2: ListView with Custom Item Template

```razor
<ListView Items="@products"
         @bind-SelectedItem="selectedProduct"
         OnItemClick="HandleItemClick">
    <ItemTemplate>
        <div class="d-flex align-items-center p-2">
            <div class="product-icon me-3" style="background-color: @context.ColorCode">
                @context.Name.Substring(0, 1)
            </div>
            <div>
                <div class="fw-bold">@context.Name</div>
                <div class="text-muted small">@context.Category - $@context.Price.ToString("0.00")</div>
            </div>
            <div class="ms-auto">
                <span class="badge @(context.InStock ? "bg-success" : "bg-danger")">
                    @(context.InStock ? "In Stock" : "Out of Stock")
                </span>
            </div>
        </div>
    </ItemTemplate>
</ListView>

<style>
    .product-icon {
        width: 40px;
        height: 40px;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        font-weight: bold;
    }
</style>

@code {
    private List<Product> products = new List<Product>();
    private Product selectedProduct;
    
    protected override void OnInitialized()
    {
        // Create sample product data
        products.Add(new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99, InStock = true, ColorCode = "#1976d2" });
        products.Add(new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 699.99, InStock = true, ColorCode = "#388e3c" });
        products.Add(new Product { Id = 3, Name = "Headphones", Category = "Accessories", Price = 149.99, InStock = false, ColorCode = "#d32f2f" });
        products.Add(new Product { Id = 4, Name = "Monitor", Category = "Electronics", Price = 299.99, InStock = true, ColorCode = "#7b1fa2" });
        products.Add(new Product { Id = 5, Name = "Keyboard", Category = "Accessories", Price = 89.99, InStock = true, ColorCode = "#f57c00" });
    }
    
    private void HandleItemClick(Product product)
    {
        Console.WriteLine($"Selected: {product.Name}");
    }
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }
        public string ColorCode { get; set; }
    }
}
```

This example demonstrates a list view with a custom item template that displays product information with icons, categories, prices, and stock status.

### Example 3: ListView with Multiple Selection

```razor
<ListView Items="@tasks"
         TextField="Title"
         AllowMultipleSelection="true"
         @bind-SelectedItems="selectedTasks"
         OnSelectionChanged="HandleSelectionChanged">
</ListView>

<div class="mt-3">
    <h5>Selected Tasks:</h5>
    @if (selectedTasks != null && selectedTasks.Any())
    {
        <ul class="list-group">
            @foreach (var task in selectedTasks)
            {
                <li class="list-group-item d-flex justify-content-between align-items-center">
                    @task.Title
                    <span class="badge bg-@GetPriorityColor(task.Priority)">@task.Priority</span>
                </li>
            }
        </ul>
        
        <div class="mt-3">
            <Button Color="Color.Primary" OnClick="MarkAsCompleted">Mark Selected as Completed</Button>
        </div>
    }
    else
    {
        <p>No tasks selected</p>
    }
</div>

@code {
    private List<TaskItem> tasks = new List<TaskItem>();
    private List<TaskItem> selectedTasks = new List<TaskItem>();
    
    protected override void OnInitialized()
    {
        // Create sample task data
        tasks.Add(new TaskItem { Id = 1, Title = "Complete project proposal", Priority = "High", IsCompleted = false });
        tasks.Add(new TaskItem { Id = 2, Title = "Review code changes", Priority = "Medium", IsCompleted = false });
        tasks.Add(new TaskItem { Id = 3, Title = "Update documentation", Priority = "Low", IsCompleted = true });
        tasks.Add(new TaskItem { Id = 4, Title = "Fix reported bugs", Priority = "High", IsCompleted = false });
        tasks.Add(new TaskItem { Id = 5, Title = "Prepare for demo", Priority = "Medium", IsCompleted = false });
    }
    
    private void HandleSelectionChanged(IEnumerable<TaskItem> tasks)
    {
        Console.WriteLine($"Selected {tasks.Count()} tasks");
    }
    
    private string GetPriorityColor(string priority)
    {
        return priority switch
        {
            "High" => "danger",
            "Medium" => "warning",
            "Low" => "success",
            _ => "secondary"
        };
    }
    
    private void MarkAsCompleted()
    {
        foreach (var task in selectedTasks)
        {
            task.IsCompleted = true;
        }
        
        // Clear selection after action
        selectedTasks.Clear();
    }
    
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
```

This example shows a list view with multiple selection enabled, allowing users to select multiple tasks and perform batch operations on them.

### Example 4: Virtualized ListView for Large Datasets

```razor
<div class="virtualized-list-container" style="height: 400px;">
    <ListView Items="@largeDataset"
             TextField="Name"
             IsVirtualized="true"
             ItemHeight="50"
             @bind-SelectedItem="selectedItem"
             OnScroll="HandleScroll">
    </ListView>
</div>

<div class="mt-3">
    <p>Scroll position: @scrollPosition%</p>
    @if (selectedItem != null)
    {
        <p>Selected: <strong>@selectedItem.Name</strong> (ID: @selectedItem.Id)</p>
    }
</div>

@code {
    private List<DataItem> largeDataset = new List<DataItem>();
    private DataItem selectedItem;
    private int scrollPosition = 0;
    
    protected override void OnInitialized()
    {
        // Generate a large dataset
        for (int i = 1; i <= 1000; i++)
        {
            largeDataset.Add(new DataItem
            {
                Id = i,
                Name = $"Item {i}",
                Description = $"This is a description for item {i}"
            });
        }
    }
    
    private void HandleScroll(ScrollEventArgs args)
    {
        scrollPosition = (int)(args.ScrollTop / (args.ScrollHeight - args.ClientHeight) * 100);
    }
    
    public class DataItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
```

This example demonstrates a virtualized list view for efficiently rendering a large dataset, with scroll position tracking.

### Example 5: Grouped ListView

```razor
<ListView Items="@groupedContacts"
         TextField="Name"
         ShowHeader="true"
         HeaderText="Contacts by Department"
         @bind-SelectedItem="selectedContact">
    <GroupHeaderTemplate>
        <div class="group-header">
            <i class="fa fa-users me-2"></i>
            <span>@context.Key</span>
            <span class="badge bg-secondary ms-2">@context.Count()</span>
        </div>
    </GroupHeaderTemplate>
</ListView>

<style>
    .group-header {
        padding: 8px 16px;
        background-color: #f0f0f0;
        font-weight: bold;
        border-bottom: 1px solid #ddd;
    }
</style>

@code {
    private IEnumerable<IGrouping<string, Contact>> groupedContacts;
    private Contact selectedContact;
    
    protected override void OnInitialized()
    {
        // Create sample contact data
        var contacts = new List<Contact>
        {
            new Contact { Id = 1, Name = "John Smith", Department = "Engineering", Email = "john@example.com" },
            new Contact { Id = 2, Name = "Jane Doe", Department = "Marketing", Email = "jane@example.com" },
            new Contact { Id = 3, Name = "Bob Johnson", Department = "Engineering", Email = "bob@example.com" },
            new Contact { Id = 4, Name = "Alice Williams", Department = "HR", Email = "alice@example.com" },
            new Contact { Id = 5, Name = "Mike Brown", Department = "Marketing", Email = "mike@example.com" },
            new Contact { Id = 6, Name = "Sarah Davis", Department = "HR", Email = "sarah@example.com" },
            new Contact { Id = 7, Name = "Tom Wilson", Department = "Engineering", Email = "tom@example.com" }
        };
        
        // Group contacts by department
        groupedContacts = contacts.GroupBy(c => c.Department);
    }
    
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}
```

This example shows a list view with items grouped by department, with custom group header templates displaying the department name and count.

### Example 6: ListView with Lazy Loading

```razor
<ListView Items="@loadedItems"
         TextField="Title"
         @bind-SelectedItem="selectedItem"
         OnScrollEnd="LoadMoreItems">
</ListView>

<div class="mt-3">
    @if (isLoading)
    {
        <div class="d-flex justify-content-center">
            <Spinner Type="SpinnerType.Border" Color="Color.Primary" />
        </div>
    }
    else
    {
        <p>Loaded @loadedItems.Count of @totalItems items</p>
    }
</div>

@code {
    private List<NewsItem> allItems = new List<NewsItem>();
    private List<NewsItem> loadedItems = new List<NewsItem>();
    private NewsItem selectedItem;
    private bool isLoading = false;
    private int currentPage = 0;
    private int pageSize = 10;
    private int totalItems = 100;
    
    protected override void OnInitialized()
    {
        // Generate sample data
        for (int i = 1; i <= totalItems; i++)
        {
            allItems.Add(new NewsItem
            {
                Id = i,
                Title = $"News Article {i}",
                PublishDate = DateTime.Now.AddDays(-i),
                Category = i % 5 == 0 ? "Technology" : 
                          i % 4 == 0 ? "Business" : 
                          i % 3 == 0 ? "Sports" : 
                          i % 2 == 0 ? "Entertainment" : "Politics"
            });
        }
        
        // Load initial items
        LoadMoreItems();
    }
    
    private async Task LoadMoreItems()
    {
        if (isLoading || loadedItems.Count >= totalItems) return;
        
        isLoading = true;
        
        // Simulate API delay
        await Task.Delay(1000);
        
        var nextItems = allItems
            .Skip(currentPage * pageSize)
            .Take(pageSize)
            .ToList();
        
        loadedItems.AddRange(nextItems);
        currentPage++;
        
        isLoading = false;
    }
    
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishDate { get; set; }
        public string Category { get; set; }
    }
}
```

This example demonstrates a list view with lazy loading, where additional items are loaded when the user scrolls to the end of the list.

### Example 7: ListView with Drag and Drop

```razor
<ListView Items="@todoItems"
         TextField="Text"
         AllowDragDrop="true"
         OnItemDrop="HandleItemDrop">
    <ItemTemplate>
        <div class="d-flex align-items-center p-2">
            <div class="me-3">
                <i class="@(context.IsCompleted ? "fa fa-check-circle text-success" : "fa fa-circle-o")" 
                   @onclick="() => ToggleItemStatus(context)"></i>
            </div>
            <div class="@(context.IsCompleted ? "text-decoration-line-through text-muted" : "")">
                @context.Text
            </div>
            <div class="ms-auto text-muted small">
                <i class="fa fa-grip-lines"></i>
            </div>
        </div>
    </ItemTemplate>
</ListView>

<div class="mt-3">
    <div class="input-group">
        <input type="text" class="form-control" @bind="newItemText" placeholder="Add new item..." />
        <Button Color="Color.Primary" OnClick="AddItem">Add</Button>
    </div>
</div>

@code {
    private List<TodoItem> todoItems = new List<TodoItem>();
    private string newItemText;
    
    protected override void OnInitialized()
    {
        // Create sample todo items
        todoItems.Add(new TodoItem { Id = 1, Text = "Complete project proposal", IsCompleted = true });
        todoItems.Add(new TodoItem { Id = 2, Text = "Review code changes", IsCompleted = false });
        todoItems.Add(new TodoItem { Id = 3, Text = "Update documentation", IsCompleted = false });
        todoItems.Add(new TodoItem { Id = 4, Text = "Fix reported bugs", IsCompleted = false });
        todoItems.Add(new TodoItem { Id = 5, Text = "Prepare for demo", IsCompleted = false });
    }
    
    private void HandleItemDrop(TodoItem source, TodoItem target, DropPosition position)
    {
        // Remove the source item from its current position
        todoItems.Remove(source);
        
        // Find the target index
        int targetIndex = todoItems.IndexOf(target);
        
        // Insert at the appropriate position
        if (position == DropPosition.After)
        {
            targetIndex++;
        }
        
        todoItems.Insert(targetIndex, source);
    }
    
    private void ToggleItemStatus(TodoItem item)
    {
        item.IsCompleted = !item.IsCompleted;
    }
    
    private void AddItem()
    {
        if (!string.IsNullOrWhiteSpace(newItemText))
        {
            todoItems.Add(new TodoItem
            {
                Id = todoItems.Count > 0 ? todoItems.Max(i => i.Id) + 1 : 1,
                Text = newItemText,
                IsCompleted = false
            });
            
            newItemText = string.Empty;
        }
    }
    
    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
    
    public enum DropPosition
    {
        Before,
        After
    }
}
```

This example shows a list view with drag and drop functionality for reordering todo items, along with the ability to toggle item completion status and add new items.

## Customization Notes

### CSS Variables

The ListView component can be customized using CSS variables:

```css
:root {
    --bb-listview-padding: 0.5rem;
    --bb-listview-margin: 0;
    --bb-listview-border: 1px solid rgba(0, 0, 0, 0.125);
    --bb-listview-border-radius: 0.25rem;
    --bb-listview-item-padding: 0.75rem 1.25rem;
    --bb-listview-item-margin: 0;
    --bb-listview-item-border: 1px solid rgba(0, 0, 0, 0.125);
    --bb-listview-item-active-color: #fff;
    --bb-listview-item-active-bg: var(--bs-primary);
    --bb-listview-item-hover-color: inherit;
    --bb-listview-item-hover-bg: rgba(0, 0, 0, 0.05);
    --bb-listview-header-padding: 0.75rem 1.25rem;
    --bb-listview-header-bg: rgba(0, 0, 0, 0.03);
    --bb-listview-header-border: 1px solid rgba(0, 0, 0, 0.125);
    --bb-listview-group-header-padding: 0.5rem 1rem;
    --bb-listview-group-header-bg: #f8f9fa;
    --bb-listview-group-header-border: 1px solid rgba(0, 0, 0, 0.125);
    --bb-listview-disabled-opacity: 0.65;
}
```

### Integration with Other Components

The ListView component works well with:

1. **ContextMenu**: For providing context-specific actions on list items
2. **Dialog/Modal**: For editing item properties or confirming deletions
3. **Tooltip**: For displaying additional information about items
4. **Search**: For filtering list items based on text input
5. **Pagination**: For navigating through pages of items
6. **Badge**: For displaying counts or status indicators
7. **Button**: For actions related to list items

### Accessibility Considerations

The ListView component includes several accessibility features:

1. **ARIA Attributes**: Proper ARIA roles and attributes for screen readers
2. **Keyboard Navigation**: Support for keyboard shortcuts to navigate the list
3. **Focus Management**: Visual indicators for focused items
4. **Screen Reader Announcements**: Descriptive text for list operations

### Performance Optimization

For large lists, consider these performance optimizations:

1. **Virtualization**: Enable the `IsVirtualized` property to render only visible items
2. **Lazy Loading**: Implement the `OnScrollEnd` event to load data incrementally
3. **Pagination**: For very large datasets, consider paginating the list
4. **Memoization**: Use memoization techniques to prevent unnecessary re-renders