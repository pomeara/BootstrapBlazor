# TreeView Component

## Overview
The TreeView component in BootstrapBlazor provides a hierarchical display of data in a tree structure. It allows users to visualize and interact with nested data, making it ideal for representing file systems, organizational structures, category hierarchies, or any data that has parent-child relationships. The component supports features like expanding/collapsing nodes, selection, customization, and drag-and-drop operations, providing a flexible solution for displaying hierarchical data in a user-friendly manner.

## Features
- **Hierarchical Data Display**: Visualize nested data structures with parent-child relationships
- **Expandable/Collapsible Nodes**: Toggle visibility of child nodes
- **Node Selection**: Single or multiple node selection capabilities
- **Custom Node Templates**: Customize the appearance of tree nodes
- **Icons and Indicators**: Visual cues for node types and states
- **Lazy Loading**: Load child nodes on demand for improved performance
- **Drag and Drop**: Rearrange nodes through drag-and-drop operations
- **Checkboxes**: Enable node selection with checkboxes
- **Search Functionality**: Find nodes based on text content
- **Context Menu Integration**: Right-click actions for tree nodes
- **Keyboard Navigation**: Navigate the tree using keyboard shortcuts
- **Node Editing**: Add, rename, or delete nodes
- **State Persistence**: Maintain expanded/collapsed state
- **Accessibility Support**: ARIA attributes for screen readers

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<TItem>` | `null` | Collection of root-level items to display in the tree |
| `ChildrenProperty` | `string` | `"Children"` | Name of the property that contains child items |
| `TextField` | `string` | `null` | Property name to use as the display text for nodes |
| `ValueField` | `string` | `null` | Property name to use as the value for nodes |
| `IconField` | `string` | `null` | Property name to use for node icons |
| `SelectedItem` | `TItem` | `null` | Currently selected item in the tree |
| `SelectedItems` | `IEnumerable<TItem>` | `null` | Collection of selected items when multiple selection is enabled |
| `IsExpanded` | `bool` | `false` | Whether all nodes are expanded by default |
| `ShowBorder` | `bool` | `true` | Whether to show borders around the tree |
| `ShowCheckbox` | `bool` | `false` | Whether to show checkboxes for node selection |
| `ShowIcon` | `bool` | `true` | Whether to show icons for nodes |
| `ShowLine` | `bool` | `false` | Whether to show connecting lines between nodes |
| `AllowDragDrop` | `bool` | `false` | Whether drag and drop operations are allowed |
| `AllowMultipleSelection` | `bool` | `false` | Whether multiple nodes can be selected simultaneously |
| `IsDisabled` | `bool` | `false` | Whether the tree component is disabled |
| `NodeTemplate` | `RenderFragment<TItem>` | `null` | Custom template for rendering tree nodes |
| `ExpandedNodes` | `IEnumerable<TItem>` | `null` | Collection of nodes that are expanded |

## Events

| Event | Description |
| --- | --- |
| `OnNodeClick` | Triggered when a node is clicked |
| `OnNodeDoubleClick` | Triggered when a node is double-clicked |
| `OnNodeExpand` | Triggered when a node is expanded |
| `OnNodeCollapse` | Triggered when a node is collapsed |
| `OnNodeSelect` | Triggered when a node is selected |
| `OnNodeUnselect` | Triggered when a node is unselected |
| `OnNodeDragStart` | Triggered when a drag operation starts on a node |
| `OnNodeDragOver` | Triggered when a dragged node is over another node |
| `OnNodeDrop` | Triggered when a node is dropped onto another node |
| `OnNodeCheckChange` | Triggered when a node's checkbox state changes |
| `OnNodeContextMenu` | Triggered when a node is right-clicked |

## Usage Examples

### Example 1: Basic Tree

```razor
<Tree Items="@treeData">
</Tree>

@code {
    private List<TreeNode> treeData = new List<TreeNode>();
    
    protected override void OnInitialized()
    {
        // Create sample tree data
        var root1 = new TreeNode
        {
            Text = "Root Node 1",
            Children = new List<TreeNode>
            {
                new TreeNode { Text = "Child 1.1" },
                new TreeNode 
                { 
                    Text = "Child 1.2",
                    Children = new List<TreeNode>
                    {
                        new TreeNode { Text = "Grandchild 1.2.1" },
                        new TreeNode { Text = "Grandchild 1.2.2" }
                    }
                }
            }
        };
        
        var root2 = new TreeNode
        {
            Text = "Root Node 2",
            Children = new List<TreeNode>
            {
                new TreeNode { Text = "Child 2.1" },
                new TreeNode { Text = "Child 2.2" }
            }
        };
        
        treeData.Add(root1);
        treeData.Add(root2);
    }
    
    public class TreeNode
    {
        public string Text { get; set; }
        public List<TreeNode> Children { get; set; }
    }
}
```

This example shows a basic tree with two root nodes, each with their own child nodes. The first root node also has grandchild nodes, demonstrating the hierarchical structure.

### Example 2: Tree with Icons and Selection

```razor
<Tree Items="@fileSystem"
      TextField="Name"
      IconField="Icon"
      @bind-SelectedItem="selectedNode"
      OnNodeSelect="HandleNodeSelect"
      ShowIcon="true"
      ShowLine="true">
</Tree>

<div class="mt-3">
    @if (selectedNode != null)
    {
        <p>Selected: <strong>@selectedNode.Name</strong> (@selectedNode.Type)</p>
    }
</div>

@code {
    private List<FileItem> fileSystem = new List<FileItem>();
    private FileItem selectedNode;
    
    protected override void OnInitialized()
    {
        // Create sample file system data
        fileSystem.Add(new FileItem
        {
            Name = "Documents",
            Type = "Folder",
            Icon = "fa fa-folder",
            Children = new List<FileItem>
            {
                new FileItem { Name = "Report.docx", Type = "Word Document", Icon = "fa fa-file-word" },
                new FileItem { Name = "Budget.xlsx", Type = "Excel Spreadsheet", Icon = "fa fa-file-excel" }
            }
        });
        
        fileSystem.Add(new FileItem
        {
            Name = "Pictures",
            Type = "Folder",
            Icon = "fa fa-folder",
            Children = new List<FileItem>
            {
                new FileItem { Name = "Vacation.jpg", Type = "Image", Icon = "fa fa-file-image" },
                new FileItem { Name = "Family.png", Type = "Image", Icon = "fa fa-file-image" }
            }
        });
    }
    
    private void HandleNodeSelect(FileItem node)
    {
        Console.WriteLine($"Selected: {node.Name}");
    }
    
    public class FileItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public List<FileItem> Children { get; set; }
    }
}
```

This example shows a tree with custom icons for different file types and a selection mechanism that displays the selected node's details.

### Example 3: Tree with Checkboxes

```razor
<Tree Items="@categories"
      TextField="Name"
      ShowCheckbox="true"
      @bind-SelectedItems="selectedCategories"
      OnNodeCheckChange="HandleCheckChange">
</Tree>

<div class="mt-3">
    <h5>Selected Categories:</h5>
    @if (selectedCategories != null && selectedCategories.Any())
    {
        <ul class="list-group">
            @foreach (var category in selectedCategories)
            {
                <li class="list-group-item">@category.Name</li>
            }
        </ul>
    }
    else
    {
        <p>No categories selected</p>
    }
</div>

@code {
    private List<Category> categories = new List<Category>();
    private List<Category> selectedCategories = new List<Category>();
    
    protected override void OnInitialized()
    {
        // Create sample category data
        categories.Add(new Category
        {
            Name = "Electronics",
            Children = new List<Category>
            {
                new Category { Name = "Smartphones" },
                new Category { Name = "Laptops" },
                new Category { Name = "Accessories" }
            }
        });
        
        categories.Add(new Category
        {
            Name = "Clothing",
            Children = new List<Category>
            {
                new Category { Name = "Men's" },
                new Category { Name = "Women's" },
                new Category { Name = "Children's" }
            }
        });
    }
    
    private void HandleCheckChange(Category category, bool isChecked)
    {
        Console.WriteLine($"{category.Name} is {(isChecked ? "checked" : "unchecked")}");
    }
    
    public class Category
    {
        public string Name { get; set; }
        public List<Category> Children { get; set; }
    }
}
```

This example shows a tree with checkboxes that allow multiple selection of nodes, with the selected items displayed in a list below the tree.

### Example 4: Lazy Loading Tree

```razor
<Tree Items="@lazyLoadedNodes"
      TextField="Name"
      OnNodeExpand="LoadChildNodes">
</Tree>

@code {
    private List<LazyNode> lazyLoadedNodes = new List<LazyNode>();
    
    protected override void OnInitialized()
    {
        // Initialize with root nodes only
        lazyLoadedNodes.Add(new LazyNode { Id = 1, Name = "Node 1", HasChildren = true });
        lazyLoadedNodes.Add(new LazyNode { Id = 2, Name = "Node 2", HasChildren = true });
        lazyLoadedNodes.Add(new LazyNode { Id = 3, Name = "Node 3", HasChildren = true });
    }
    
    private async Task LoadChildNodes(LazyNode node)
    {
        // Simulate loading delay
        await Task.Delay(500);
        
        // Only load children if they haven't been loaded yet
        if (node.Children == null)
        {
            node.Children = new List<LazyNode>();
            
            // Simulate fetching child nodes from a data source
            for (int i = 1; i <= 3; i++)
            {
                var childId = (node.Id * 10) + i;
                var childNode = new LazyNode
                {
                    Id = childId,
                    Name = $"{node.Name}.{i}",
                    HasChildren = childId < 100 // Limit depth for this example
                };
                
                node.Children.Add(childNode);
            }
        }
    }
    
    public class LazyNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasChildren { get; set; }
        public List<LazyNode> Children { get; set; }
    }
}
```

This example demonstrates lazy loading of tree nodes, where child nodes are only loaded when a parent node is expanded, improving performance for large hierarchical datasets.

### Example 5: Tree with Custom Node Template

```razor
<Tree Items="@employees"
      TextField="Name">
    <NodeTemplate>
        <div class="d-flex align-items-center">
            <div class="employee-avatar @(context.IsManager ? "manager-avatar" : "")">
                @context.Name.Substring(0, 1)
            </div>
            <div class="ms-2">
                <div>@context.Name</div>
                <small class="text-muted">@context.Title</small>
            </div>
        </div>
    </NodeTemplate>
</Tree>

<style>
    .employee-avatar {
        width: 32px;
        height: 32px;
        border-radius: 50%;
        background-color: #e0e0e0;
        display: flex;
        align-items: center;
        justify-content: center;
        font-weight: bold;
    }
    
    .manager-avatar {
        background-color: #bbdefb;
    }
</style>

@code {
    private List<Employee> employees = new List<Employee>();
    
    protected override void OnInitialized()
    {
        // Create sample employee data
        var ceo = new Employee
        {
            Name = "John Smith",
            Title = "CEO",
            IsManager = true,
            Children = new List<Employee>()
        };
        
        var cto = new Employee
        {
            Name = "Jane Doe",
            Title = "CTO",
            IsManager = true,
            Children = new List<Employee>()
        };
        
        cto.Children.Add(new Employee
        {
            Name = "Bob Johnson",
            Title = "Senior Developer",
            IsManager = false
        });
        
        cto.Children.Add(new Employee
        {
            Name = "Alice Williams",
            Title = "UX Designer",
            IsManager = false
        });
        
        ceo.Children.Add(cto);
        ceo.Children.Add(new Employee
        {
            Name = "Mike Brown",
            Title = "CFO",
            IsManager = true
        });
        
        employees.Add(ceo);
    }
    
    public class Employee
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsManager { get; set; }
        public List<Employee> Children { get; set; }
    }
}
```

This example shows a tree with a custom node template that displays employee information with avatars, highlighting managers with a different avatar color.

### Example 6: Tree with Drag and Drop

```razor
<Tree Items="@projectTasks"
      TextField="Title"
      AllowDragDrop="true"
      OnNodeDrop="HandleNodeDrop">
</Tree>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="SaveTaskOrder">Save Task Order</Button>
</div>

@code {
    private List<TaskItem> projectTasks = new List<TaskItem>();
    
    protected override void OnInitialized()
    {
        // Create sample project tasks
        var planning = new TaskItem
        {
            Id = 1,
            Title = "Project Planning",
            Status = "Completed",
            Children = new List<TaskItem>
            {
                new TaskItem { Id = 2, Title = "Requirements Gathering", Status = "Completed" },
                new TaskItem { Id = 3, Title = "Resource Allocation", Status = "Completed" }
            }
        };
        
        var development = new TaskItem
        {
            Id = 4,
            Title = "Development",
            Status = "In Progress",
            Children = new List<TaskItem>
            {
                new TaskItem { Id = 5, Title = "Frontend Implementation", Status = "In Progress" },
                new TaskItem { Id = 6, Title = "Backend Implementation", Status = "Not Started" }
            }
        };
        
        var testing = new TaskItem
        {
            Id = 7,
            Title = "Testing",
            Status = "Not Started",
            Children = new List<TaskItem>
            {
                new TaskItem { Id = 8, Title = "Unit Testing", Status = "Not Started" },
                new TaskItem { Id = 9, Title = "Integration Testing", Status = "Not Started" }
            }
        };
        
        projectTasks.Add(planning);
        projectTasks.Add(development);
        projectTasks.Add(testing);
    }
    
    private void HandleNodeDrop(TaskItem source, TaskItem target, DropPosition position)
    {
        Console.WriteLine($"Dropped {source.Title} {position} {target.Title}");
        // Implement logic to reorder tasks based on drag and drop operation
    }
    
    private void SaveTaskOrder()
    {
        // Implement logic to save the new task order to a data source
        Console.WriteLine("Task order saved");
    }
    
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public List<TaskItem> Children { get; set; }
    }
    
    public enum DropPosition
    {
        Before,
        After,
        Inside
    }
}
```

This example demonstrates a tree with drag and drop functionality for reordering tasks in a project management context.

### Example 7: Tree with Search and Context Menu

```razor
<div class="mb-3">
    <Input @bind-Value="searchText" Placeholder="Search..." OnInput="HandleSearch" />
</div>

<Tree Items="@filteredFiles"
      TextField="Name"
      IconField="Icon"
      ShowIcon="true"
      OnNodeContextMenu="ShowContextMenu">
</Tree>

<ContextMenu @ref="contextMenu">
    <Items>
        <ContextMenuItem Text="Open" Icon="fa fa-folder-open" OnClick="() => HandleContextMenuAction('open')" />
        <ContextMenuItem Text="Download" Icon="fa fa-download" OnClick="() => HandleContextMenuAction('download')" />
        <ContextMenuItem Text="Rename" Icon="fa fa-edit" OnClick="() => HandleContextMenuAction('rename')" />
        <ContextMenuItem Text="Delete" Icon="fa fa-trash" OnClick="() => HandleContextMenuAction('delete')" />
    </Items>
</ContextMenu>

@code {
    private List<FileItem> allFiles = new List<FileItem>();
    private List<FileItem> filteredFiles = new List<FileItem>();
    private string searchText = "";
    private ContextMenu contextMenu;
    private FileItem selectedFile;
    
    protected override void OnInitialized()
    {
        // Create sample file data
        allFiles = CreateSampleFileSystem();
        filteredFiles = allFiles;
    }
    
    private void HandleSearch(ChangeEventArgs e)
    {
        searchText = e.Value.ToString();
        if (string.IsNullOrWhiteSpace(searchText))
        {
            filteredFiles = allFiles;
        }
        else
        {
            filteredFiles = FilterFiles(allFiles, searchText);
        }
    }
    
    private List<FileItem> FilterFiles(List<FileItem> files, string searchTerm)
    {
        var result = new List<FileItem>();
        
        foreach (var file in files)
        {
            if (file.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            {
                result.Add(file);
            }
            else if (file.Children != null && file.Children.Any())
            {
                var matchingChildren = FilterFiles(file.Children, searchTerm);
                if (matchingChildren.Any())
                {
                    var folderCopy = new FileItem
                    {
                        Name = file.Name,
                        Type = file.Type,
                        Icon = file.Icon,
                        Children = matchingChildren
                    };
                    result.Add(folderCopy);
                }
            }
        }
        
        return result;
    }
    
    private void ShowContextMenu(FileItem file)
    {
        selectedFile = file;
        contextMenu.Show();
    }
    
    private void HandleContextMenuAction(string action)
    {
        if (selectedFile == null) return;
        
        switch (action)
        {
            case "open":
                Console.WriteLine($"Opening {selectedFile.Name}");
                break;
            case "download":
                Console.WriteLine($"Downloading {selectedFile.Name}");
                break;
            case "rename":
                Console.WriteLine($"Renaming {selectedFile.Name}");
                break;
            case "delete":
                Console.WriteLine($"Deleting {selectedFile.Name}");
                break;
        }
    }
    
    public class FileItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public List<FileItem> Children { get; set; }
    }
}
```

This example shows a tree with search functionality and a context menu that appears when right-clicking on a node, providing various actions for the selected file or folder.

## Customization Notes

### CSS Variables

The TreeView component can be customized using CSS variables:

```css
:root {
    --bb-tree-padding: 0.5rem;
    --bb-tree-margin: 0;
    --bb-tree-padding-left: 1.25rem;
    --bb-tree-item-margin: 0.25rem 0;
    --bb-tree-icon-width: 1rem;
    --bb-tree-check-margin: 0.25rem;
    --bb-tree-node-padding: 0.25rem 0.5rem;
    --bb-tree-item-active-color: var(--bs-primary);
    --bb-tree-item-active-bg: rgba(var(--bs-primary-rgb), 0.1);
    --bb-tree-item-hover-color: var(--bs-primary);
    --bb-tree-item-hover-bg: rgba(var(--bs-primary-rgb), 0.05);
    --bb-tree-icon-margin-right: 0.5rem;
    --bb-tree-disabled-opacity: 0.65;
    --bb-tree-search-height: 2.25rem;
    --bb-tree-item-bg-radius: var(--bs-border-radius);
}
```

### Integration with Other Components

The TreeView component works well with:

1. **ContextMenu**: For providing context-specific actions on tree nodes
2. **Dialog/Modal**: For editing node properties or confirming deletions
3. **Tooltip**: For displaying additional information about nodes
4. **Search**: For filtering tree nodes based on text input
5. **DragDrop**: For rearranging nodes through drag and drop operations

### Accessibility Considerations

The TreeView component includes several accessibility features:

1. **ARIA Attributes**: Proper ARIA roles and attributes for screen readers
2. **Keyboard Navigation**: Support for keyboard shortcuts to navigate the tree
3. **Focus Management**: Visual indicators for focused nodes
4. **Screen Reader Announcements**: Descriptive text for tree operations

### Performance Optimization

For large trees, consider these performance optimizations:

1. **Lazy Loading**: Load child nodes only when a parent node is expanded
2. **Virtualization**: Render only the visible portion of the tree
3. **Pagination**: For very large datasets, consider paginating the tree
4. **Memoization**: Use memoization techniques to prevent unnecessary re-renders