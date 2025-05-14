# DragDrap Component

## Overview
The DragDrap component in BootstrapBlazor provides drag-and-drop functionality for elements within a web application. It allows users to intuitively move, reorder, and organize content through direct manipulation, enhancing user experience and interaction with the interface.

## Key Features
- Drag and drop functionality for UI elements
- Support for both horizontal and vertical dragging
- Customizable drag handles
- Visual feedback during drag operations
- Event callbacks for drag start, drag, and drop events
- Support for touch devices
- Sortable list functionality
- Drag between different containers
- Customizable appearance during drag operations
- Animation effects for smooth transitions

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of items to be draggable |
| `IsVertical` | `bool` | `true` | Determines if dragging is vertical (true) or horizontal (false) |
| `HandleCssClass` | `string` | `null` | CSS class for the drag handle element |
| `ItemTemplate` | `RenderFragment<TItem>` | `null` | Template for rendering each draggable item |
| `PlaceholderTemplate` | `RenderFragment` | `null` | Template for the placeholder shown during dragging |
| `DragItemClass` | `string` | `null` | CSS class applied to the item being dragged |
| `DragItemStyle` | `string` | `null` | CSS style applied to the item being dragged |
| `DisabledDragItems` | `IEnumerable<TItem>` | `null` | Collection of items that should not be draggable |
| `ItemsChanged` | `EventCallback<IEnumerable<TItem>>` | `null` | Callback invoked when the order of items changes |
| `OnDragStart` | `EventCallback<DragStartEventArgs<TItem>>` | `null` | Callback invoked when dragging starts |
| `OnDrag` | `EventCallback<DragEventArgs<TItem>>` | `null` | Callback invoked during dragging |
| `OnDragEnd` | `EventCallback<DragEndEventArgs<TItem>>` | `null` | Callback invoked when dragging ends |
| `OnDragEnter` | `EventCallback<DragEnterEventArgs<TItem>>` | `null` | Callback invoked when dragging enters a drop target |
| `OnDragLeave` | `EventCallback<DragLeaveEventArgs<TItem>>` | `null` | Callback invoked when dragging leaves a drop target |
| `OnDrop` | `EventCallback<DropEventArgs<TItem>>` | `null` | Callback invoked when an item is dropped |
| `AllowDrop` | `bool` | `true` | Determines if dropping is allowed |
| `DragEnabled` | `bool` | `true` | Enables or disables dragging functionality |
| `AnimationDuration` | `int` | `150` | Duration of animation effects in milliseconds |

## Events

| Event | Description |
| --- | --- |
| `OnItemsChanged` | Triggered when the order of items changes after a drag operation |
| `OnDragStarted` | Triggered when a drag operation begins |
| `OnDragging` | Triggered continuously during a drag operation |
| `OnDragEnded` | Triggered when a drag operation ends |
| `OnItemDropped` | Triggered when an item is dropped |

## Usage Examples

### Example 1: Basic Sortable List

```razor
@page "/drag-drop-demo"

<DragDrap Items="@items" ItemsChanged="@OnItemsChanged">
    <ItemTemplate Context="item">
        <div class="drag-item p-2 mb-2 bg-light border rounded">
            @item
        </div>
    </ItemTemplate>
</DragDrap>

<div class="mt-3">
    <h5>Current Order:</h5>
    <ul>
        @foreach (var item in items)
        {
            <li>@item</li>
        }
    </ul>
</div>

@code {
    private List<string> items = new List<string>
    {
        "Item 1",
        "Item 2",
        "Item 3",
        "Item 4",
        "Item 5"
    };

    private Task OnItemsChanged(IEnumerable<string> newItems)
    {
        items = newItems.ToList();
        return Task.CompletedTask;
    }
}
```

### Example 2: Custom Drag Handle

```razor
@page "/custom-handle-demo"

<DragDrap Items="@tasks" 
         ItemsChanged="@OnTasksChanged"
         HandleCssClass="drag-handle">
    <ItemTemplate Context="task">
        <div class="task-item p-2 mb-2 bg-light border rounded d-flex align-items-center">
            <div class="drag-handle me-2">
                <i class="fa fa-grip-vertical"></i>
            </div>
            <div class="flex-grow-1">
                <div class="fw-bold">@task.Title</div>
                <div class="small text-muted">@task.Description</div>
            </div>
            <div>
                <Badge Color="@GetPriorityColor(task.Priority)">
                    @task.Priority
                </Badge>
            </div>
        </div>
    </ItemTemplate>
</DragDrap>

<style>
    .drag-handle {
        cursor: grab;
        color: #6c757d;
    }
    
    .drag-handle:active {
        cursor: grabbing;
    }
</style>

@code {
    private List<TaskItem> tasks = new List<TaskItem>
    {
        new TaskItem { Id = 1, Title = "Complete project proposal", Description = "Draft and finalize the project proposal document", Priority = "High" },
        new TaskItem { Id = 2, Title = "Schedule team meeting", Description = "Coordinate with team members for weekly sync", Priority = "Medium" },
        new TaskItem { Id = 3, Title = "Research new technologies", Description = "Investigate potential technologies for upcoming project", Priority = "Low" },
        new TaskItem { Id = 4, Title = "Review pull requests", Description = "Review and merge pending pull requests", Priority = "High" },
        new TaskItem { Id = 5, Title = "Update documentation", Description = "Update project documentation with recent changes", Priority = "Medium" }
    };

    private Task OnTasksChanged(IEnumerable<TaskItem> newTasks)
    {
        tasks = newTasks.ToList();
        return Task.CompletedTask;
    }

    private Color GetPriorityColor(string priority)
    {
        return priority switch
        {
            "High" => Color.Danger,
            "Medium" => Color.Warning,
            "Low" => Color.Success,
            _ => Color.Secondary
        };
    }

    private class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }
}
```

### Example 3: Horizontal Dragging

```razor
@page "/horizontal-drag-demo"

<DragDrap Items="@colorItems" 
         ItemsChanged="@OnColorsChanged"
         IsVertical="false">
    <ItemTemplate Context="color">
        <div class="color-item mx-2" style="background-color: @color; width: 100px; height: 100px; border-radius: 8px;">
            <div class="d-flex justify-content-center align-items-center h-100 text-white">
                @color
            </div>
        </div>
    </ItemTemplate>
</DragDrap>

@code {
    private List<string> colorItems = new List<string>
    {
        "#007bff", // Blue
        "#28a745", // Green
        "#dc3545", // Red
        "#ffc107", // Yellow
        "#6f42c1"  // Purple
    };

    private Task OnColorsChanged(IEnumerable<string> newColors)
    {
        colorItems = newColors.ToList();
        return Task.CompletedTask;
    }
}
```

### Example 4: Drag Between Containers

```razor
@page "/multi-container-demo"

<div class="row">
    <div class="col-md-6">
        <Card Title="Available Items">
            <Body>
                <DragDrap Items="@availableItems" 
                         ItemsChanged="@OnAvailableItemsChanged"
                         OnDrop="@OnItemDropped"
                         ContainerId="available-container">
                    <ItemTemplate Context="item">
                        <div class="drag-item p-2 mb-2 bg-light border rounded">
                            @item.Name
                        </div>
                    </ItemTemplate>
                </DragDrap>
            </Body>
        </Card>
    </div>
    <div class="col-md-6">
        <Card Title="Selected Items">
            <Body>
                <DragDrap Items="@selectedItems" 
                         ItemsChanged="@OnSelectedItemsChanged"
                         OnDrop="@OnItemDropped"
                         ContainerId="selected-container">
                    <ItemTemplate Context="item">
                        <div class="drag-item p-2 mb-2 bg-primary text-white border rounded">
                            @item.Name
                        </div>
                    </ItemTemplate>
                </DragDrap>
            </Body>
        </Card>
    </div>
</div>

@code {
    private List<DragItem> availableItems = new List<DragItem>();
    private List<DragItem> selectedItems = new List<DragItem>();

    protected override void OnInitialized()
    {
        // Initialize available items
        for (int i = 1; i <= 5; i++)
        {
            availableItems.Add(new DragItem { Id = i, Name = $"Item {i}", Container = "available-container" });
        }
    }

    private Task OnAvailableItemsChanged(IEnumerable<DragItem> items)
    {
        availableItems = items.ToList();
        return Task.CompletedTask;
    }

    private Task OnSelectedItemsChanged(IEnumerable<DragItem> items)
    {
        selectedItems = items.ToList();
        return Task.CompletedTask;
    }

    private Task OnItemDropped(DropEventArgs<DragItem> args)
    {
        var item = args.Item;
        var sourceContainer = item.Container;
        var targetContainer = args.TargetContainerId;

        if (sourceContainer != targetContainer)
        {
            // Update the item's container
            item.Container = targetContainer;

            // Move item between containers
            if (targetContainer == "available-container")
            {
                if (!availableItems.Any(i => i.Id == item.Id))
                {
                    availableItems.Add(item);
                    selectedItems.RemoveAll(i => i.Id == item.Id);
                }
            }
            else if (targetContainer == "selected-container")
            {
                if (!selectedItems.Any(i => i.Id == item.Id))
                {
                    selectedItems.Add(item);
                    availableItems.RemoveAll(i => i.Id == item.Id);
                }
            }
        }

        return Task.CompletedTask;
    }

    private class DragItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Container { get; set; }
    }
}
```

### Example 5: Kanban Board

```razor
@page "/kanban-board"

<div class="kanban-board d-flex">
    @foreach (var column in kanbanColumns)
    {
        <div class="kanban-column me-3" style="min-width: 300px;">
            <Card Title="@column.Name">
                <HeaderTemplate>
                    <div class="d-flex justify-content-between align-items-center">
                        <span>@column.Name</span>
                        <Badge Color="Color.Secondary">@GetTasksForColumn(column.Id).Count()</Badge>
                    </div>
                </HeaderTemplate>
                <Body>
                    <DragDrap Items="@GetTasksForColumn(column.Id)" 
                             ItemsChanged="@(items => OnColumnTasksChanged(items, column.Id))"
                             OnDrop="@OnKanbanTaskDropped"
                             ContainerId="@($"column-{column.Id}")">
                        <ItemTemplate Context="task">
                            <div class="kanban-task p-2 mb-2 bg-light border rounded">
                                <div class="fw-bold">@task.Title</div>
                                <div class="small text-muted">@task.Description</div>
                                <div class="mt-2 d-flex justify-content-between">
                                    <Badge Color="@GetPriorityColor(task.Priority)">
                                        @task.Priority
                                    </Badge>
                                    <small class="text-muted">ID: @task.Id</small>
                                </div>
                            </div>
                        </ItemTemplate>
                    </DragDrap>
                </Body>
            </Card>
        </div>
    }
</div>

@code {
    private List<KanbanColumn> kanbanColumns = new List<KanbanColumn>
    {
        new KanbanColumn { Id = 1, Name = "To Do" },
        new KanbanColumn { Id = 2, Name = "In Progress" },
        new KanbanColumn { Id = 3, Name = "Review" },
        new KanbanColumn { Id = 4, Name = "Done" }
    };

    private List<KanbanTask> kanbanTasks = new List<KanbanTask>();

    protected override void OnInitialized()
    {
        // Initialize tasks
        kanbanTasks = new List<KanbanTask>
        {
            new KanbanTask { Id = 1, Title = "Research requirements", Description = "Gather project requirements", ColumnId = 1, Priority = "Medium" },
            new KanbanTask { Id = 2, Title = "Create wireframes", Description = "Design initial wireframes", ColumnId = 1, Priority = "High" },
            new KanbanTask { Id = 3, Title = "Setup project", Description = "Initialize project structure", ColumnId = 2, Priority = "High" },
            new KanbanTask { Id = 4, Title = "Implement authentication", Description = "Add user authentication", ColumnId = 2, Priority = "Medium" },
            new KanbanTask { Id = 5, Title = "Code review", Description = "Review pull request #42", ColumnId = 3, Priority = "Low" },
            new KanbanTask { Id = 6, Title = "Fix bugs", Description = "Address reported issues", ColumnId = 3, Priority = "High" },
            new KanbanTask { Id = 7, Title = "Documentation", Description = "Update API documentation", ColumnId = 4, Priority = "Medium" },
            new KanbanTask { Id = 8, Title = "Release v1.0", Description = "Prepare for initial release", ColumnId = 4, Priority = "High" }
        };
    }

    private IEnumerable<KanbanTask> GetTasksForColumn(int columnId)
    {
        return kanbanTasks.Where(t => t.ColumnId == columnId);
    }

    private Task OnColumnTasksChanged(IEnumerable<KanbanTask> tasks, int columnId)
    {
        // Update task order within the same column
        var updatedTasks = tasks.ToList();
        var otherTasks = kanbanTasks.Where(t => t.ColumnId != columnId).ToList();
        
        kanbanTasks = otherTasks.Concat(updatedTasks).ToList();
        
        return Task.CompletedTask;
    }

    private Task OnKanbanTaskDropped(DropEventArgs<KanbanTask> args)
    {
        var task = args.Item;
        var targetContainerId = args.TargetContainerId;
        
        // Extract column ID from container ID (format: "column-{columnId}")
        if (int.TryParse(targetContainerId.Replace("column-", ""), out int columnId))
        {
            // Update the task's column
            task.ColumnId = columnId;
        }
        
        return Task.CompletedTask;
    }

    private Color GetPriorityColor(string priority)
    {
        return priority switch
        {
            "High" => Color.Danger,
            "Medium" => Color.Warning,
            "Low" => Color.Success,
            _ => Color.Secondary
        };
    }

    private class KanbanColumn
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    private class KanbanTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ColumnId { get; set; }
        public string Priority { get; set; }
    }
}
```

### Example 6: Drag with Animation and Custom Placeholder

```razor
@page "/animated-drag-demo"

<DragDrap Items="@animatedItems" 
         ItemsChanged="@OnAnimatedItemsChanged"
         AnimationDuration="300"
         DragItemClass="dragging-item">
    <ItemTemplate Context="item">
        <div class="animated-item p-3 mb-3 rounded shadow-sm">
            <h5>@item.Title</h5>
            <p>@item.Content</p>
        </div>
    </ItemTemplate>
    <PlaceholderTemplate>
        <div class="placeholder-item p-3 mb-3 rounded border border-dashed border-primary bg-light">
            <div class="text-center text-primary">
                <i class="fa fa-arrow-down fa-2x mb-2"></i>
                <p>Drop here</p>
            </div>
        </div>
    </PlaceholderTemplate>
</DragDrap>

<style>
    .animated-item {
        background-color: #f8f9fa;
        border-left: 4px solid #007bff;
        transition: all 0.3s ease;
    }
    
    .animated-item:hover {
        transform: translateX(5px);
        background-color: #e9ecef;
    }
    
    .dragging-item {
        transform: rotate(1deg) scale(1.02);
        box-shadow: 0 10px 20px rgba(0,0,0,0.1);
        opacity: 0.8;
    }
    
    .border-dashed {
        border-style: dashed !important;
    }
</style>

@code {
    private List<ContentItem> animatedItems = new List<ContentItem>();

    protected override void OnInitialized()
    {
        // Initialize items
        animatedItems = new List<ContentItem>
        {
            new ContentItem { Id = 1, Title = "Introduction", Content = "This is the introduction section of our document." },
            new ContentItem { Id = 2, Title = "Background", Content = "Here we provide some background information." },
            new ContentItem { Id = 3, Title = "Methodology", Content = "Our approach to solving the problem." },
            new ContentItem { Id = 4, Title = "Results", Content = "The outcomes of our research and implementation." },
            new ContentItem { Id = 5, Title = "Conclusion", Content = "Final thoughts and future directions." }
        };
    }

    private Task OnAnimatedItemsChanged(IEnumerable<ContentItem> items)
    {
        animatedItems = items.ToList();
        return Task.CompletedTask;
    }

    private class ContentItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
```

### Example 7: Drag with Disabled Items

```razor
@page "/disabled-items-demo"

<DragDrap Items="@userItems" 
         ItemsChanged="@OnUserItemsChanged"
         DisabledDragItems="@disabledItems">
    <ItemTemplate Context="user">
        <div class="user-item p-2 mb-2 border rounded d-flex align-items-center @(IsDisabled(user) ? "disabled-item" : "")">
            <div class="me-3">
                <Avatar Icon="@(user.IsAdmin ? "fa fa-user-shield" : "fa fa-user")" 
                        Color="@(user.IsAdmin ? Color.Danger : Color.Primary)" />
            </div>
            <div>
                <div class="fw-bold">@user.Name</div>
                <div class="small text-muted">@user.Email</div>
                <div>
                    <Badge Color="@(user.IsAdmin ? Color.Danger : Color.Success)">
                        @(user.IsAdmin ? "Admin" : "User")
                    </Badge>
                    @if (IsDisabled(user))
                    {
                        <Badge Color="Color.Secondary">Locked</Badge>
                    }
                </div>
            </div>
            <div class="ms-auto">
                <Button Size="Size.Small" 
                        Color="@(IsDisabled(user) ? Color.Success : Color.Secondary)"
                        OnClick="() => ToggleDisabled(user)">
                    @(IsDisabled(user) ? "Enable" : "Disable")
                </Button>
            </div>
        </div>
    </ItemTemplate>
</DragDrap>

<style>
    .disabled-item {
        opacity: 0.6;
        background-color: #f8f9fa;
    }
</style>

@code {
    private List<UserItem> userItems = new List<UserItem>();
    private List<UserItem> disabledItems = new List<UserItem>();

    protected override void OnInitialized()
    {
        // Initialize user items
        userItems = new List<UserItem>
        {
            new UserItem { Id = 1, Name = "John Doe", Email = "john@example.com", IsAdmin = true },
            new UserItem { Id = 2, Name = "Jane Smith", Email = "jane@example.com", IsAdmin = false },
            new UserItem { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", IsAdmin = false },
            new UserItem { Id = 4, Name = "Alice Williams", Email = "alice@example.com", IsAdmin = true },
            new UserItem { Id = 5, Name = "Charlie Brown", Email = "charlie@example.com", IsAdmin = false }
        };

        // Initially disable admin users
        disabledItems = userItems.Where(u => u.IsAdmin).ToList();
    }

    private Task OnUserItemsChanged(IEnumerable<UserItem> items)
    {
        userItems = items.ToList();
        return Task.CompletedTask;
    }

    private bool IsDisabled(UserItem user)
    {
        return disabledItems.Any(u => u.Id == user.Id);
    }

    private void ToggleDisabled(UserItem user)
    {
        if (IsDisabled(user))
        {
            disabledItems.RemoveAll(u => u.Id == user.Id);
        }
        else
        {
            disabledItems.Add(user);
        }
    }

    private class UserItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
```

## CSS Customization

The DragDrap component can be customized using CSS variables and classes:

```css
/* Custom DragDrap styling */
.bb-dragdrap {
    --bb-dragdrap-item-bg: #ffffff;
    --bb-dragdrap-item-border: 1px solid #dee2e6;
    --bb-dragdrap-item-border-radius: 0.25rem;
    --bb-dragdrap-item-padding: 0.75rem;
    --bb-dragdrap-item-margin: 0 0 0.5rem 0;
    --bb-dragdrap-item-shadow: none;
    --bb-dragdrap-item-transition: all 0.2s ease;
    
    --bb-dragdrap-item-hover-bg: #f8f9fa;
    --bb-dragdrap-item-hover-border: 1px solid #ced4da;
    --bb-dragdrap-item-hover-shadow: 0 2px 5px rgba(0, 0, 0, 0.05);
    
    --bb-dragdrap-item-dragging-bg: #ffffff;
    --bb-dragdrap-item-dragging-border: 1px solid #007bff;
    --bb-dragdrap-item-dragging-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
    --bb-dragdrap-item-dragging-transform: rotate(1deg);
    --bb-dragdrap-item-dragging-z-index: 1000;
    
    --bb-dragdrap-placeholder-bg: #f8f9fa;
    --bb-dragdrap-placeholder-border: 1px dashed #007bff;
    --bb-dragdrap-placeholder-opacity: 0.6;
}
```

## JavaScript Interop

The DragDrap component uses JavaScript interop for the following operations:
- Handling drag and drop events
- Calculating positions during drag operations
- Applying animations and visual effects
- Managing touch events for mobile support

## Accessibility

The DragDrap component includes the following accessibility features:
- Keyboard navigation support (using arrow keys)
- ARIA roles and attributes for screen readers
- Focus management during drag operations
- High contrast visual indicators

## Browser Compatibility

The DragDrap component is compatible with all modern browsers including:
- Chrome
- Firefox
- Safari
- Edge

## Performance Considerations

- For large lists, consider implementing virtualization
- Use the `AnimationDuration` property to adjust animation performance
- Avoid complex templates for draggable items to maintain smooth performance
- Consider disabling animations on mobile devices for better performance

## Integration with Other Components

The DragDrap component works well with:
- List components for sortable lists
- Card components for drag-and-drop cards
- Table components for reorderable rows
- Tree components for drag-and-drop tree nodes

## Best Practices

1. Provide clear visual feedback during drag operations
2. Use custom drag handles for larger items
3. Implement appropriate event handlers for drag operations
4. Consider disabling certain items that should not be reordered
5. Use animations judiciously to enhance user experience without affecting performance
6. Provide alternative methods for reordering items for accessibility
7. Save the new order to persistent storage after drag operations
8. Consider the mobile experience when implementing drag and drop functionality