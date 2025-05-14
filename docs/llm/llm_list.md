# List Component

## Overview
The List component in BootstrapBlazor provides a flexible and efficient way to display collections of data in a structured format. It's designed for presenting multiple items with consistent styling and layout, making it ideal for displaying simple to complex data sets such as user lists, product catalogs, notification feeds, or any collection of similar items that need to be displayed in a uniform manner.

## Features
- **Multiple Layout Options**: Supports vertical list and grid layouts
- **Item Templates**: Customizable templates for rendering list items
- **Header and Footer**: Optional header and footer sections
- **Empty State Handling**: Configurable empty state display
- **Bordered and Borderless Styles**: Options for visual separation between items
- **Size Variants**: Small, medium, and large size options
- **Pagination Integration**: Built-in support for paginated lists
- **Virtualization**: Efficient rendering for large data sets
- **Selection Support**: Single and multiple item selection
- **Sorting Capability**: Customizable item sorting
- **Filtering Options**: Built-in filtering functionality
- **Responsive Design**: Adapts to different screen sizes
- **Hover Effects**: Configurable hover state styling
- **Action Buttons**: Support for item-specific actions

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of items to display in the list |
| `RenderMode` | `ListRenderMode` | `Auto` | Rendering mode (List, Grid, Auto) |
| `IsVertical` | `bool` | `true` | Whether to display the list vertically |
| `IsMultipleSelect` | `bool` | `false` | Whether multiple item selection is allowed |
| `ShowBorder` | `bool` | `false` | Whether to show borders between list items |
| `ShowHeader` | `bool` | `false` | Whether to show the list header |
| `ShowFooter` | `bool` | `false` | Whether to show the list footer |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the list header |
| `FooterTemplate` | `RenderFragment` | `null` | Custom template for the list footer |
| `EmptyTemplate` | `RenderFragment` | `null` | Custom template for empty state |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Custom template for list items |
| `Size` | `Size` | `Medium` | Size of the list (Small, Medium, Large) |
| `MaxHeight` | `string` | `null` | Maximum height of the list with scrolling |
| `OnItemClick` | `EventCallback<TItem>` | `null` | Callback when an item is clicked |

## Events

| Event | Description |
| --- | --- |
| `OnItemClick` | Triggered when a list item is clicked |
| `OnSelectionChanged` | Triggered when the selection state changes |
| `OnItemsChanged` | Triggered when the items collection changes |

## Usage Examples

### Example 1: Basic List

```razor
<List Items="@people">
    <ItemTemplate Context="person">
        <div class="d-flex align-items-center">
            <Avatar Src="@person.Avatar" Size="Size.Small" />
            <div class="ms-3">
                <h5 class="mb-0">@person.Name</h5>
                <small class="text-muted">@person.Title</small>
            </div>
        </div>
    </ItemTemplate>
</List>

@code {
    private List<Person> people = new List<Person>
    {
        new Person { Name = "John Doe", Title = "Developer", Avatar = "/images/avatar1.jpg" },
        new Person { Name = "Jane Smith", Title = "Designer", Avatar = "/images/avatar2.jpg" },
        new Person { Name = "Mike Johnson", Title = "Manager", Avatar = "/images/avatar3.jpg" }
    };

    public class Person
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
    }
}
```

### Example 2: Grid Layout with Selection

```razor
<List Items="@products" 
      RenderMode="ListRenderMode.Grid" 
      IsMultipleSelect="true"
      OnSelectionChanged="HandleSelectionChanged">
    <ItemTemplate Context="product">
        <div class="card h-100">
            <img src="@product.ImageUrl" class="card-img-top" alt="@product.Name">
            <div class="card-body">
                <h5 class="card-title">@product.Name</h5>
                <p class="card-text">@product.Description</p>
                <p class="card-text"><strong>$@product.Price.ToString("F2")</strong></p>
            </div>
            <div class="card-footer">
                <Button Color="Color.Primary" OnClick="() => AddToCart(product)">Add to Cart</Button>
            </div>
        </div>
    </ItemTemplate>
</List>

<div class="mt-3">
    <h5>Selected Items: @selectedCount</h5>
</div>

@code {
    private List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Laptop", Description = "High-performance laptop", Price = 999.99, ImageUrl = "/images/laptop.jpg" },
        new Product { Id = 2, Name = "Smartphone", Description = "Latest smartphone model", Price = 699.99, ImageUrl = "/images/phone.jpg" },
        new Product { Id = 3, Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99, ImageUrl = "/images/headphones.jpg" },
        new Product { Id = 4, Name = "Tablet", Description = "Lightweight tablet", Price = 349.99, ImageUrl = "/images/tablet.jpg" }
    };
    
    private int selectedCount = 0;
    
    private void HandleSelectionChanged(IEnumerable<Product> items)
    {
        selectedCount = items.Count();
    }
    
    private void AddToCart(Product product)
    {
        // Add to cart logic
    }
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
```

### Example 3: List with Header, Footer, and Empty State

```razor
<List Items="@tasks"
      ShowHeader="true"
      ShowFooter="true">
    <HeaderTemplate>
        <div class="d-flex justify-content-between align-items-center p-3 bg-light">
            <h4 class="mb-0">Task List</h4>
            <Button Color="Color.Success" OnClick="AddNewTask">Add Task</Button>
        </div>
    </HeaderTemplate>
    <ItemTemplate Context="task">
        <div class="d-flex justify-content-between align-items-center p-2 border-bottom">
            <div>
                <Checkbox Value="@task.IsCompleted" ValueChanged="v => UpdateTaskStatus(task, v)" />
                <span class="@(task.IsCompleted ? "text-decoration-line-through text-muted" : "")">
                    @task.Title
                </span>
            </div>
            <div>
                <Button Color="Color.Danger" Size="Size.Small" OnClick="() => RemoveTask(task)">
                    <i class="fa fa-trash"></i>
                </Button>
            </div>
        </div>
    </ItemTemplate>
    <FooterTemplate>
        <div class="p-3 bg-light">
            <small>Completed: @tasks.Count(t => t.IsCompleted) / @tasks.Count</small>
        </div>
    </FooterTemplate>
    <EmptyTemplate>
        <Empty Description="No tasks found" Image="images/empty-tasks.svg">
            <Button Color="Color.Primary" OnClick="AddNewTask">Create Your First Task</Button>
        </Empty>
    </EmptyTemplate>
</List>

@code {
    private List<TaskItem> tasks = new List<TaskItem>();
    
    protected override void OnInitialized()
    {
        // Sample data
        tasks.Add(new TaskItem { Id = 1, Title = "Complete project documentation", IsCompleted = false });
        tasks.Add(new TaskItem { Id = 2, Title = "Review pull requests", IsCompleted = true });
        tasks.Add(new TaskItem { Id = 3, Title = "Prepare for team meeting", IsCompleted = false });
    }
    
    private void AddNewTask()
    {
        var newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;
        tasks.Add(new TaskItem { Id = newId, Title = "New Task", IsCompleted = false });
        StateHasChanged();
    }
    
    private void RemoveTask(TaskItem task)
    {
        tasks.Remove(task);
        StateHasChanged();
    }
    
    private void UpdateTaskStatus(TaskItem task, bool isCompleted)
    {
        task.IsCompleted = isCompleted;
        StateHasChanged();
    }
    
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
```

### Example 4: Virtualized List for Large Datasets

```razor
<div style="height: 400px;">
    <List Items="@largeDataset"
          IsVirtualized="true"
          VirtualItemSize="50"
          MaxHeight="400px">
        <ItemTemplate Context="item">
            <div class="p-3 border-bottom">
                <h5>Item #@item.Id</h5>
                <p>@item.Description</p>
            </div>
        </ItemTemplate>
    </List>
</div>

@code {
    private List<DataItem> largeDataset = new List<DataItem>();
    
    protected override void OnInitialized()
    {
        // Generate large dataset
        for (int i = 1; i <= 1000; i++)
        {
            largeDataset.Add(new DataItem 
            { 
                Id = i, 
                Description = $"This is a description for item {i}. It contains enough text to demonstrate the virtualization capabilities."
            });
        }
    }
    
    public class DataItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
```

### Example 5: List with Filtering and Sorting

```razor
<div class="mb-3">
    <Input @bind-Value="searchTerm" 
           placeholder="Search contacts..." 
           OnEnterPressed="FilterContacts" />
</div>

<div class="mb-3">
    <Select TValue="string" 
            Items="sortOptions" 
            @bind-Value="currentSort"
            OnSelectedItemChanged="SortContacts" />
</div>

<List Items="@filteredContacts">
    <ItemTemplate Context="contact">
        <div class="d-flex align-items-center p-2">
            <Avatar Src="@contact.Avatar" Size="Size.Small" />
            <div class="ms-3">
                <h5 class="mb-0">@contact.Name</h5>
                <div>
                    <small><i class="fa fa-envelope me-1"></i>@contact.Email</small>
                </div>
                <div>
                    <small><i class="fa fa-phone me-1"></i>@contact.Phone</small>
                </div>
            </div>
        </div>
    </ItemTemplate>
</List>

@code {
    private List<Contact> allContacts = new List<Contact>
    {
        new Contact { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "555-1234", Avatar = "/images/avatar1.jpg" },
        new Contact { Id = 2, Name = "Jane Smith", Email = "jane@example.com", Phone = "555-5678", Avatar = "/images/avatar2.jpg" },
        new Contact { Id = 3, Name = "Alice Johnson", Email = "alice@example.com", Phone = "555-9012", Avatar = "/images/avatar3.jpg" },
        new Contact { Id = 4, Name = "Bob Brown", Email = "bob@example.com", Phone = "555-3456", Avatar = "/images/avatar4.jpg" }
    };
    
    private List<Contact> filteredContacts;
    private string searchTerm = "";
    private string currentSort = "name_asc";
    
    private List<SelectedItem> sortOptions = new List<SelectedItem>
    {
        new SelectedItem { Value = "name_asc", Text = "Name (A-Z)" },
        new SelectedItem { Value = "name_desc", Text = "Name (Z-A)" },
        new SelectedItem { Value = "email_asc", Text = "Email (A-Z)" },
        new SelectedItem { Value = "email_desc", Text = "Email (Z-A)" }
    };
    
    protected override void OnInitialized()
    {
        filteredContacts = new List<Contact>(allContacts);
    }
    
    private void FilterContacts()
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            filteredContacts = new List<Contact>(allContacts);
        }
        else
        {
            filteredContacts = allContacts
                .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) || 
                           c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                           c.Phone.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        
        SortContacts(currentSort);
    }
    
    private void SortContacts(string sortOption)
    {
        currentSort = sortOption;
        
        switch (sortOption)
        {
            case "name_asc":
                filteredContacts = filteredContacts.OrderBy(c => c.Name).ToList();
                break;
            case "name_desc":
                filteredContacts = filteredContacts.OrderByDescending(c => c.Name).ToList();
                break;
            case "email_asc":
                filteredContacts = filteredContacts.OrderBy(c => c.Email).ToList();
                break;
            case "email_desc":
                filteredContacts = filteredContacts.OrderByDescending(c => c.Email).ToList();
                break;
        }
    }
    
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
    }
}
```

### Example 6: Interactive List with Action Buttons

```razor
<List Items="@notifications" 
      ShowBorder="true"
      OnItemClick="ViewNotificationDetails">
    <ItemTemplate Context="notification">
        <div class="d-flex p-2 @(notification.IsRead ? "bg-light" : "")">
            <div class="flex-shrink-0">
                <div class="notification-icon @GetIconClass(notification.Type)">
                    <i class="@GetIconName(notification.Type)"></i>
                </div>
            </div>
            <div class="flex-grow-1 ms-3">
                <div class="d-flex justify-content-between">
                    <h6 class="mb-0 @(!notification.IsRead ? "fw-bold" : "")">@notification.Title</h6>
                    <small class="text-muted">@notification.Time.ToShortTimeString()</small>
                </div>
                <p class="mb-1 small">@notification.Message</p>
                <div class="d-flex gap-2">
                    <Button Size="Size.ExtraSmall" Color="Color.Link" OnClick="() => MarkAsRead(notification)">
                        @(notification.IsRead ? "Mark as unread" : "Mark as read")
                    </Button>
                    <Button Size="Size.ExtraSmall" Color="Color.Link" OnClick="() => DeleteNotification(notification)">
                        Delete
                    </Button>
                </div>
            </div>
        </div>
    </ItemTemplate>
</List>

<style>
    .notification-icon {
        width: 40px;
        height: 40px;
        border-radius: 50%;
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
    }
    
    .notification-info { background-color: var(--bs-info); }
    .notification-success { background-color: var(--bs-success); }
    .notification-warning { background-color: var(--bs-warning); }
    .notification-danger { background-color: var(--bs-danger); }
</style>

@code {
    private List<Notification> notifications = new List<Notification>
    {
        new Notification { 
            Id = 1, 
            Title = "New Message", 
            Message = "You have received a new message from Alice", 
            Time = DateTime.Now.AddMinutes(-5), 
            Type = NotificationType.Info, 
            IsRead = false 
        },
        new Notification { 
            Id = 2, 
            Title = "Task Completed", 
            Message = "Project X has been successfully completed", 
            Time = DateTime.Now.AddHours(-2), 
            Type = NotificationType.Success, 
            IsRead = true 
        },
        new Notification { 
            Id = 3, 
            Title = "Warning", 
            Message = "Your subscription will expire in 3 days", 
            Time = DateTime.Now.AddHours(-5), 
            Type = NotificationType.Warning, 
            IsRead = false 
        },
        new Notification { 
            Id = 4, 
            Title = "System Error", 
            Message = "An error occurred while processing your request", 
            Time = DateTime.Now.AddDays(-1), 
            Type = NotificationType.Danger, 
            IsRead = true 
        }
    };
    
    private void ViewNotificationDetails(Notification notification)
    {
        // View details logic
        if (!notification.IsRead)
        {
            MarkAsRead(notification);
        }
    }
    
    private void MarkAsRead(Notification notification)
    {
        notification.IsRead = !notification.IsRead;
        StateHasChanged();
    }
    
    private void DeleteNotification(Notification notification)
    {
        notifications.Remove(notification);
        StateHasChanged();
    }
    
    private string GetIconClass(NotificationType type)
    {
        return type switch
        {
            NotificationType.Info => "notification-info",
            NotificationType.Success => "notification-success",
            NotificationType.Warning => "notification-warning",
            NotificationType.Danger => "notification-danger",
            _ => "notification-info"
        };
    }
    
    private string GetIconName(NotificationType type)
    {
        return type switch
        {
            NotificationType.Info => "fa fa-info",
            NotificationType.Success => "fa fa-check",
            NotificationType.Warning => "fa fa-exclamation",
            NotificationType.Danger => "fa fa-times",
            _ => "fa fa-bell"
        };
    }
    
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
    }
    
    public enum NotificationType
    {
        Info,
        Success,
        Warning,
        Danger
    }
}
```

### Example 7: List with Drag and Drop Reordering

```razor
<List Items="@todoItems"
      ShowBorder="true"
      IsDraggable="true"
      OnItemsReordered="HandleReordering">
    <ItemTemplate Context="item">
        <div class="d-flex align-items-center p-2">
            <div class="drag-handle me-2">
                <i class="fa fa-grip-vertical text-muted"></i>
            </div>
            <div class="d-flex align-items-center flex-grow-1">
                <Checkbox Value="@item.IsCompleted" ValueChanged="v => UpdateItemStatus(item, v)" />
                <span class="ms-2 @(item.IsCompleted ? "text-decoration-line-through text-muted" : "")">
                    @item.Title
                </span>
            </div>
            <div>
                <Badge Color="@GetPriorityColor(item.Priority)">
                    @item.Priority.ToString()
                </Badge>
            </div>
        </div>
    </ItemTemplate>
</List>

<style>
    .drag-handle {
        cursor: grab;
    }
    
    .drag-handle:active {
        cursor: grabbing;
    }
</style>

@code {
    private List<TodoItem> todoItems = new List<TodoItem>
    {
        new TodoItem { Id = 1, Title = "Complete project proposal", IsCompleted = false, Priority = Priority.High },
        new TodoItem { Id = 2, Title = "Review code changes", IsCompleted = true, Priority = Priority.Medium },
        new TodoItem { Id = 3, Title = "Update documentation", IsCompleted = false, Priority = Priority.Low },
        new TodoItem { Id = 4, Title = "Schedule team meeting", IsCompleted = false, Priority = Priority.Medium },
        new TodoItem { Id = 5, Title = "Prepare presentation", IsCompleted = false, Priority = Priority.High }
    };
    
    private void HandleReordering(List<TodoItem> reorderedItems)
    {
        todoItems = reorderedItems;
    }
    
    private void UpdateItemStatus(TodoItem item, bool isCompleted)
    {
        item.IsCompleted = isCompleted;
        StateHasChanged();
    }
    
    private Color GetPriorityColor(Priority priority)
    {
        return priority switch
        {
            Priority.High => Color.Danger,
            Priority.Medium => Color.Warning,
            Priority.Low => Color.Info,
            _ => Color.Secondary
        };
    }
    
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public Priority Priority { get; set; }
    }
    
    public enum Priority
    {
        Low,
        Medium,
        High
    }
}
```

## Customization Notes

### CSS Variables

The List component can be customized using CSS variables:

```css
:root {
    --bb-list-border-color: #dee2e6;
    --bb-list-item-hover-bg: rgba(0, 0, 0, 0.03);
    --bb-list-item-active-bg: rgba(0, 0, 0, 0.05);
    --bb-list-item-padding: 0.75rem 1.25rem;
    --bb-list-header-bg: #f8f9fa;
    --bb-list-footer-bg: #f8f9fa;
    --bb-list-border-radius: 0.25rem;
    --bb-list-max-height: none;
    --bb-list-scrollbar-width: 6px;
    --bb-list-scrollbar-track-bg: #f1f1f1;
    --bb-list-scrollbar-thumb-bg: #888;
}
```

### Custom Templates

The List component provides several template parameters for customization:

1. `ItemTemplate`: Defines how each item is rendered
2. `HeaderTemplate`: Customizes the list header
3. `FooterTemplate`: Customizes the list footer
4. `EmptyTemplate`: Defines what to show when the list is empty

### Responsive Behavior

For grid layout mode, you can control the number of columns at different breakpoints:

```razor
<List Items="@items" 
      RenderMode="ListRenderMode.Grid"
      GridItemsXs="1"
      GridItemsSm="2"
      GridItemsMd="3"
      GridItemsLg="4"
      GridItemsXl="6">
    <!-- ItemTemplate content -->
</List>
```

This creates a responsive grid that adjusts the number of columns based on screen size.