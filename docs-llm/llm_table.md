# Table Component Documentation

## Overview
The Table component in BootstrapBlazor is a powerful and feature-rich data grid that allows for displaying, sorting, filtering, and editing tabular data. It supports various data operations, customization options, and advanced features like virtualization and tree structures.

## Features
- Data binding with various sources (List, IEnumerable, etc.)
- Column sorting (single and multi-column)
- Filtering and searching
- Pagination
- Row selection (single and multiple)
- Inline editing and form editing
- Custom column templates
- Fixed headers and columns
- Row detail expansion
- Grouping and aggregation
- Export functionality (Excel, CSV, PDF)
- Virtualization for large datasets
- Tree table structure
- Responsive design
- Customizable styling
- Context menu support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Items | IEnumerable<TItem> | null | Data source for the table |
| RenderMode | TableRenderMode | Auto | Rendering mode (Table, CardView, Auto) |
| PageItems | int | 20 | Number of items per page when pagination is enabled |
| IsStriped | bool | false | Whether to show striped rows |
| IsBordered | bool | false | Whether to show borders |
| IsMultipleSelect | bool | false | Whether multiple row selection is allowed |
| ShowToolbar | bool | false | Whether to show the toolbar |
| ShowSearch | bool | false | Whether to show the search box |
| ShowRefresh | bool | false | Whether to show the refresh button |
| ShowColumnList | bool | false | Whether to show the column visibility toggle |
| ShowExtendButtons | bool | false | Whether to show the extend buttons |
| ShowDefaultButtons | bool | false | Whether to show the default buttons |
| ShowLineNo | bool | false | Whether to show line numbers |
| ShowCheckboxText | bool | false | Whether to show text next to checkboxes |
| ShowFooter | bool | false | Whether to show the footer |
| ShowPagination | bool | false | Whether to show pagination |
| ShowSkeleton | bool | false | Whether to show skeleton loading |
| IsFixedHeader | bool | false | Whether to fix the header |
| ScrollingHeight | int | 0 | Height of the scrollable area when fixed header is enabled |
| OnQueryAsync | Func<QueryPageOptions, Task<QueryData<TItem>>> | null | Callback for querying data asynchronously |
| OnSaveAsync | Func<TItem, Task<bool>> | null | Callback for saving edited data |
| OnDeleteAsync | Func<IEnumerable<TItem>, Task<bool>> | null | Callback for deleting data |

## Events

| Event | Description |
| --- | --- |
| OnSort | Triggered when a column is sorted |
| OnFilter | Triggered when a column is filtered |
| OnSearch | Triggered when the search function is used |
| OnRowClick | Triggered when a row is clicked |
| OnRowDoubleClick | Triggered when a row is double-clicked |
| OnSelectedRowsChanged | Triggered when selected rows change |
| OnEditRow | Triggered when a row enters edit mode |
| OnSaveRow | Triggered when a row is saved after editing |
| OnDeleteRow | Triggered when a row is deleted |

## Usage Examples

### Example 1: Basic Table

```razor
<Table TItem="Person" Items="@people" IsBordered="true" IsStriped="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Age" />
        <TableColumn @bind-Field="@context.Address" />
        <TableColumn @bind-Field="@context.Birthday" FormatString="yyyy-MM-dd" />
    </TableColumns>
</Table>

@code {
    private List<Person> people = new List<Person>();

    protected override void OnInitialized()
    {
        for (int i = 0; i < 10; i++)
        {
            people.Add(new Person
            {
                Id = i + 1,
                Name = $"Person {i + 1}",
                Age = 20 + i,
                Address = $"Address {i + 1}",
                Birthday = DateTime.Now.AddYears(-20 - i).AddDays(i)
            });
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Age { get; set; }
        public string Address { get; set; } = "";
        public DateTime Birthday { get; set; }
    }
}
```

### Example 2: Table with Pagination, Search, and Toolbar

```razor
<Table TItem="Employee" 
       Items="@employees"
       IsBordered="true"
       IsStriped="true"
       ShowToolbar="true"
       ShowSearch="true"
       ShowPagination="true"
       PageItems="5"
       OnSearch="@OnSearch">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" Searchable="true" />
        <TableColumn @bind-Field="@context.Department" Searchable="true" />
        <TableColumn @bind-Field="@context.Salary" FormatString="{0:C}" />
        <TableColumn @bind-Field="@context.HireDate" FormatString="yyyy-MM-dd" />
    </TableColumns>
</Table>

@code {
    private List<Employee> employees = new List<Employee>();
    private List<Employee> allEmployees = new List<Employee>();

    protected override void OnInitialized()
    {
        // Generate sample data
        string[] departments = new[] { "HR", "Engineering", "Sales", "Marketing", "Finance" };
        Random random = new Random();

        for (int i = 0; i < 20; i++)
        {
            allEmployees.Add(new Employee
            {
                Id = i + 1,
                Name = $"Employee {i + 1}",
                Department = departments[random.Next(departments.Length)],
                Salary = 50000 + random.Next(50000),
                HireDate = DateTime.Now.AddYears(-random.Next(1, 10)).AddDays(-random.Next(1, 365))
            });
        }

        employees = allEmployees;
    }

    private Task<QueryData<Employee>> OnSearch(QueryPageOptions options)
    {
        // Filter data based on search criteria
        IEnumerable<Employee> items = allEmployees;

        // Apply search
        if (!string.IsNullOrEmpty(options.SearchText))
        {
            items = items.Where(i => i.Name.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ||
                                    i.Department.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase));
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(options.SortName))
        {
            items = options.SortOrder == SortOrder.Asc ?
                items.OrderBy(i => typeof(Employee).GetProperty(options.SortName)?.GetValue(i, null)) :
                items.OrderByDescending(i => typeof(Employee).GetProperty(options.SortName)?.GetValue(i, null));
        }

        // Get total count
        var total = items.Count();

        // Apply pagination
        items = items.Skip(options.PageIndex * options.PageItems).Take(options.PageItems);

        return Task.FromResult(new QueryData<Employee>()
        {
            Items = items,
            TotalCount = total
        });
    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Department { get; set; } = "";
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}
```

### Example 3: Editable Table

```razor
<Table TItem="Product"
       Items="@products"
       IsBordered="true"
       IsStriped="true"
       ShowToolbar="true"
       ShowSearch="true"
       ShowPagination="true"
       ShowExtendButtons="true"
       EditMode="EditMode.EditForm"
       OnSaveAsync="@OnSaveAsync"
       OnDeleteAsync="@OnDeleteAsync">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" IsReadOnly="true" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Category" />
        <TableColumn @bind-Field="@context.Price" FormatString="{0:C}" />
        <TableColumn @bind-Field="@context.Stock" />
        <TableColumn @bind-Field="@context.IsAvailable" />
    </TableColumns>
</Table>

@code {
    private List<Product> products = new List<Product>();

    protected override void OnInitialized()
    {
        // Generate sample data
        string[] categories = new[] { "Electronics", "Clothing", "Books", "Home & Kitchen", "Toys" };
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            products.Add(new Product
            {
                Id = i + 1,
                Name = $"Product {i + 1}",
                Category = categories[random.Next(categories.Length)],
                Price = (decimal)(10 + random.Next(1, 100) + random.NextDouble()),
                Stock = random.Next(0, 100),
                IsAvailable = random.Next(0, 10) > 2
            });
        }
    }

    private Task<bool> OnSaveAsync(Product product)
    {
        // In a real application, you would save to a database here
        if (product.Id == 0)
        {
            // New product
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
        }
        else
        {
            // Update existing product
            var existingProduct = products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Category = product.Category;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                existingProduct.IsAvailable = product.IsAvailable;
            }
        }

        return Task.FromResult(true);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<Product> products)
    {
        // In a real application, you would delete from a database here
        foreach (var product in products)
        {
            this.products.Remove(product);
        }

        return Task.FromResult(true);
    }

    public class Product
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";
        
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = "";
        
        [Range(0.01, 10000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }
        
        [Range(0, 1000, ErrorMessage = "Stock must be between 0 and 1000")]
        public int Stock { get; set; }
        
        public bool IsAvailable { get; set; }
    }
}
```

### Example 4: Table with Custom Templates

```razor
<Table TItem="Customer"
       Items="@customers"
       IsBordered="true"
       IsStriped="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Email" />
        <TableColumn @bind-Field="@context.Status">
            <Template Context="value">
                @if (value.ToString() == "Active")
                {
                    <span class="badge bg-success">@value</span>
                }
                else if (value.ToString() == "Inactive")
                {
                    <span class="badge bg-danger">@value</span>
                }
                else
                {
                    <span class="badge bg-warning">@value</span>
                }
            </Template>
        </TableColumn>
        <TableColumn @bind-Field="@context.LastLogin" FormatString="yyyy-MM-dd HH:mm" />
        <TableColumn Text="Actions" Width="200">
            <Template Context="customer">
                <Button Color="Color.Primary" Size="Size.Small" OnClick="() => ViewCustomer(customer)">View</Button>
                <Button Color="Color.Success" Size="Size.Small" OnClick="() => EditCustomer(customer)">Edit</Button>
                <Button Color="Color.Danger" Size="Size.Small" OnClick="() => DeleteCustomer(customer)">Delete</Button>
            </Template>
        </TableColumn>
    </TableColumns>
</Table>

@code {
    private List<Customer> customers = new List<Customer>();

    protected override void OnInitialized()
    {
        // Generate sample data
        string[] statuses = new[] { "Active", "Inactive", "Pending" };
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            customers.Add(new Customer
            {
                Id = i + 1,
                Name = $"Customer {i + 1}",
                Email = $"customer{i+1}@example.com",
                Status = statuses[random.Next(statuses.Length)],
                LastLogin = DateTime.Now.AddDays(-random.Next(1, 30))
            });
        }
    }

    private void ViewCustomer(Customer customer)
    {
        // View customer logic
        Console.WriteLine($"Viewing customer: {customer.Name}");
    }

    private void EditCustomer(Customer customer)
    {
        // Edit customer logic
        Console.WriteLine($"Editing customer: {customer.Name}");
    }

    private void DeleteCustomer(Customer customer)
    {
        // Delete customer logic
        Console.WriteLine($"Deleting customer: {customer.Name}");
        customers.Remove(customer);
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime LastLogin { get; set; }
    }
}
```

### Example 5: Table with Row Selection and Events

```razor
<div class="mb-3">
    <h5>Selected Users:</h5>
    @if (selectedUsers.Any())
    {
        <ul>
            @foreach (var user in selectedUsers)
            {
                <li>@user.Name (@user.Username)</li>
            }
        </ul>
    }
    else
    {
        <p>No users selected</p>
    }
</div>

<Table TItem="User"
       Items="@users"
       IsBordered="true"
       IsStriped="true"
       IsMultipleSelect="true"
       OnRowClick="@OnRowClick"
       OnSelectedRowsChanged="@OnSelectedRowsChanged">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Username" />
        <TableColumn @bind-Field="@context.Email" />
        <TableColumn @bind-Field="@context.IsAdmin" />
        <TableColumn @bind-Field="@context.RegisterDate" FormatString="yyyy-MM-dd" />
    </TableColumns>
</Table>

@code {
    private List<User> users = new List<User>();
    private List<User> selectedUsers = new List<User>();

    protected override void OnInitialized()
    {
        // Generate sample data
        Random random = new Random();

        for (int i = 0; i < 10; i++)
        {
            var name = $"User {i + 1}";
            var username = $"user{i+1}";
            
            users.Add(new User
            {
                Id = i + 1,
                Name = name,
                Username = username,
                Email = $"{username}@example.com",
                IsAdmin = i < 2, // First two users are admins
                RegisterDate = DateTime.Now.AddMonths(-random.Next(1, 24))
            });
        }
    }

    private Task OnRowClick(User user)
    {
        Console.WriteLine($"Clicked on user: {user.Name}");
        return Task.CompletedTask;
    }

    private Task OnSelectedRowsChanged(IEnumerable<User> users)
    {
        selectedUsers = users.ToList();
        return Task.CompletedTask;
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Email { get; set; } = "";
        public bool IsAdmin { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
```

### Example 6: Table with Server-Side Processing

```razor
<Table TItem="Order"
       IsBordered="true"
       IsStriped="true"
       ShowToolbar="true"
       ShowSearch="true"
       ShowPagination="true"
       PageItems="10"
       OnQueryAsync="@OnQueryAsync">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.CustomerName" Sortable="true" Searchable="true" />
        <TableColumn @bind-Field="@context.OrderDate" FormatString="yyyy-MM-dd" Sortable="true" />
        <TableColumn @bind-Field="@context.TotalAmount" FormatString="{0:C}" Sortable="true" />
        <TableColumn @bind-Field="@context.Status" Sortable="true" Filterable="true" />
    </TableColumns>
</Table>

@code {
    private List<Order> allOrders = new List<Order>();

    protected override void OnInitialized()
    {
        // Generate sample data
        string[] statuses = new[] { "Pending", "Processing", "Shipped", "Delivered", "Cancelled" };
        Random random = new Random();

        for (int i = 0; i < 100; i++)
        {
            allOrders.Add(new Order
            {
                Id = i + 1,
                CustomerName = $"Customer {random.Next(1, 20)}",
                OrderDate = DateTime.Now.AddDays(-random.Next(1, 365)),
                TotalAmount = (decimal)(10 + random.Next(1, 500) + random.NextDouble()),
                Status = statuses[random.Next(statuses.Length)]
            });
        }
    }

    private Task<QueryData<Order>> OnQueryAsync(QueryPageOptions options)
    {
        // Start with all orders
        IEnumerable<Order> items = allOrders;

        // Apply search
        if (!string.IsNullOrEmpty(options.SearchText))
        {
            items = items.Where(i => i.CustomerName.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase));
        }

        // Apply filtering
        var filters = options.Filters;
        if (filters.Any())
        {
            items = items.Where(i => 
            {
                var ret = true;
                foreach (var filter in filters)
                {
                    if (filter.FieldKey == nameof(Order.Status))
                    {
                        ret = ret && i.Status == filter.FieldValue;
                    }
                }
                return ret;
            });
        }

        // Apply sorting
        if (!string.IsNullOrEmpty(options.SortName))
        {
            var invoker = SortLambdaBuilder.Build<Order>(options.SortName, options.SortOrder);
            items = invoker(items);
        }
        else
        {
            // Default sort by Id
            items = items.OrderBy(i => i.Id);
        }

        // Get total count before pagination
        var total = items.Count();

        // Apply pagination
        items = items.Skip(options.PageIndex * options.PageItems).Take(options.PageItems);

        return Task.FromResult(new QueryData<Order>()
        {
            Items = items,
            TotalCount = total,
            IsFiltered = filters.Any(),
            IsSorted = !string.IsNullOrEmpty(options.SortName),
            IsSearch = !string.IsNullOrEmpty(options.SearchText)
        });
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = "";
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "";
    }

    // Helper class for building sort expressions
    public static class SortLambdaBuilder
    {
        public static Func<IEnumerable<TItem>, IEnumerable<TItem>> Build<TItem>(string sortName, SortOrder sortOrder)
        {
            return items =>
            {
                var property = typeof(TItem).GetProperty(sortName);
                if (property == null) return items;

                if (sortOrder == SortOrder.Asc)
                {
                    return items.OrderBy(i => property.GetValue(i, null));
                }
                else
                {
                    return items.OrderByDescending(i => property.GetValue(i, null));
                }
            };
        }
    }
}
```

### Example 7: Tree Table

```razor
<Table TItem="TreeNode"
       Items="@rootNodes"
       IsBordered="true"
       IsTree="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Type" />
        <TableColumn @bind-Field="@context.Size" />
        <TableColumn @bind-Field="@context.LastModified" FormatString="yyyy-MM-dd HH:mm" />
    </TableColumns>
</Table>

@code {
    private List<TreeNode> rootNodes = new List<TreeNode>();

    protected override void OnInitialized()
    {
        // Create a sample file system structure
        var documents = new TreeNode
        {
            Id = 1,
            Name = "Documents",
            Type = "Folder",
            Size = "-",
            LastModified = DateTime.Now.AddDays(-5),
            Children = new List<TreeNode>()
        };

        documents.Children.Add(new TreeNode
        {
            Id = 2,
            Name = "Reports",
            Type = "Folder",
            Size = "-",
            LastModified = DateTime.Now.AddDays(-4),
            Children = new List<TreeNode>
            {
                new TreeNode
                {
                    Id = 3,
                    Name = "Annual Report.docx",
                    Type = "Word Document",
                    Size = "2.4 MB",
                    LastModified = DateTime.Now.AddDays(-2)
                },
                new TreeNode
                {
                    Id = 4,
                    Name = "Q4 Results.xlsx",
                    Type = "Excel Spreadsheet",
                    Size = "1.8 MB",
                    LastModified = DateTime.Now.AddDays(-1)
                }
            }
        });

        documents.Children.Add(new TreeNode
        {
            Id = 5,
            Name = "Presentations",
            Type = "Folder",
            Size = "-",
            LastModified = DateTime.Now.AddDays(-3),
            Children = new List<TreeNode>
            {
                new TreeNode
                {
                    Id = 6,
                    Name = "Company Overview.pptx",
                    Type = "PowerPoint Presentation",
                    Size = "5.7 MB",
                    LastModified = DateTime.Now.AddHours(-12)
                }
            }
        });

        var pictures = new TreeNode
        {
            Id = 7,
            Name = "Pictures",
            Type = "Folder",
            Size = "-",
            LastModified = DateTime.Now.AddDays(-10),
            Children = new List<TreeNode>
            {
                new TreeNode
                {
                    Id = 8,
                    Name = "Vacation.jpg",
                    Type = "JPEG Image",
                    Size = "3.2 MB",
                    LastModified = DateTime.Now.AddDays(-8)
                },
                new TreeNode
                {
                    Id = 9,
                    Name = "Family.png",
                    Type = "PNG Image",
                    Size = "2.9 MB",
                    LastModified = DateTime.Now.AddDays(-7)
                }
            }
        };

        rootNodes.Add(documents);
        rootNodes.Add(pictures);
    }

    public class TreeNode
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public string Size { get; set; } = "";
        public DateTime LastModified { get; set; }
        public List<TreeNode>? Children { get; set; }
    }
}
```

## Notes

- The Table component is one of the most complex components in BootstrapBlazor, with many features and customization options.
- For large datasets, consider using server-side processing with the `OnQueryAsync` callback to improve performance.
- When using the tree table feature, make sure your data model includes a `Children` property of the same type as the model itself.
- The Table component supports both automatic column generation based on model properties and explicit column definitions using `TableColumn` components.
- For complex scenarios, you can combine multiple features like filtering, sorting, pagination, and custom templates to create powerful data grids.
- The component provides built-in support for common operations like adding, editing, and deleting rows, but you can also implement custom logic for these operations.
- For responsive design, the Table component can automatically switch between table view and card view based on screen size when `RenderMode="TableRenderMode.Auto"`.