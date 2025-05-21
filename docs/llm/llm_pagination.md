# Pagination Component

## Overview
The Pagination component in BootstrapBlazor provides a user interface for navigating through multiple pages of content. It displays a series of page numbers and navigation controls that allow users to move between pages in a dataset. This component is essential for applications that display large datasets that need to be broken into manageable chunks.

## Features
- Multiple display modes (default, simple, with total count)
- Customizable page size options
- First/previous/next/last page navigation
- Page number input for direct navigation
- Responsive design
- Customizable styling
- Event handling for page changes
- Support for server-side and client-side pagination
- Accessibility support
- Localization support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `PageIndex` | `int` | `1` | Current page number (1-based) |
| `PageItems` | `IEnumerable<int>` | `new int[] { 10, 20, 50, 100, 200, 500, 1000 }` | Available page size options |
| `PageItemsSource` | `IEnumerable<SelectedItem>` | `null` | Custom source for page size options |
| `PageSize` | `int` | `20` | Number of items per page |
| `Total` | `int` | `0` | Total number of items |
| `ShowPageInfo` | `bool` | `true` | Whether to show page information |
| `ShowNavigationButton` | `bool` | `true` | Whether to show navigation buttons (first/prev/next/last) |
| `ShowPageInput` | `bool` | `false` | Whether to show direct page input |
| `ShowTotalCount` | `bool` | `true` | Whether to show total count |
| `ShowPageSizeSelector` | `bool` | `true` | Whether to show page size selector |
| `IsDisabled` | `bool` | `false` | Whether the pagination is disabled |
| `ShowFirstLastButtons` | `bool` | `true` | Whether to show first and last page buttons |
| `ShowMoreButtons` | `bool` | `true` | Whether to show ellipsis buttons for more pages |
| `MaxPageLinkCount` | `int` | `7` | Maximum number of page links to display |
| `AlignCenter` | `bool` | `false` | Whether to center-align the pagination |
| `AlignRight` | `bool` | `false` | Whether to right-align the pagination |
| `Size` | `Size` | `Size.None` | Size of the pagination (Small, Medium, Large) |

## Events

| Event | Description |
| --- | --- |
| `OnPageIndexChanged` | Triggered when the page index changes |
| `OnPageSizeChanged` | Triggered when the page size changes |
| `OnPageChanged` | Triggered when either page index or page size changes |

## Usage Examples

### Example 1: Basic Pagination
```csharp
<Pagination PageIndex="1" PageSize="10" Total="100" OnPageIndexChanged="OnPageChanged" />

@code {
    private Task OnPageChanged(int pageIndex)
    {
        // Handle page change
        return Task.CompletedTask;
    }
}
```
This example shows a basic pagination component with 10 items per page and a total of 100 items. The `OnPageChanged` method is called when the user navigates to a different page.

### Example 2: Pagination with Page Size Selector
```csharp
<Pagination PageIndex="@pageIndex" 
           PageSize="@pageSize" 
           Total="@total" 
           OnPageIndexChanged="OnPageIndexChanged"
           OnPageSizeChanged="OnPageSizeChanged" />

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 235;
    private List<Person> allData = new();
    private List<Person> displayData = new();
    
    protected override void OnInitialized()
    {
        // Generate sample data
        allData = Enumerable.Range(1, total).Select(i => new Person
        {
            Id = i,
            Name = $"Person {i}",
            Age = Random.Shared.Next(18, 70)
        }).ToList();
        
        // Initialize display data
        UpdateDisplayData();
    }
    
    private Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        UpdateDisplayData();
        return Task.CompletedTask;
    }
    
    private Task OnPageSizeChanged(int pageSize)
    {
        this.pageSize = pageSize;
        this.pageIndex = 1; // Reset to first page when changing page size
        UpdateDisplayData();
        return Task.CompletedTask;
    }
    
    private void UpdateDisplayData()
    {
        // Calculate start and end indices
        int startIndex = (pageIndex - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize, total);
        
        // Update display data
        displayData = allData.Skip(startIndex).Take(pageSize).ToList();
    }
    
    private class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
```
This example demonstrates pagination with a page size selector, allowing users to change the number of items displayed per page. The component maintains both page index and page size state.

### Example 3: Simple Pagination Mode
```csharp
<Pagination PageIndex="@pageIndex" 
           PageSize="@pageSize" 
           Total="@total" 
           ShowPageInfo="false"
           ShowPageSizeSelector="false"
           ShowTotalCount="false"
           OnPageIndexChanged="OnPageIndexChanged" />

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 50;
    
    private Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        return Task.CompletedTask;
    }
}
```
This example shows a simplified pagination component that only displays page numbers and navigation buttons, without page size selector or total count information.

### Example 4: Pagination with Direct Page Input
```csharp
<Pagination PageIndex="@pageIndex" 
           PageSize="@pageSize" 
           Total="@total" 
           ShowPageInput="true"
           OnPageIndexChanged="OnPageIndexChanged" />

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 500;
    
    private Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        return Task.CompletedTask;
    }
}
```
This example demonstrates pagination with a direct page input field, allowing users to jump directly to a specific page by entering the page number.

### Example 5: Pagination with Table Integration
```csharp
<Table TItem="Person"
       Items="@displayData"
       PageItems="new int[] { 5, 10, 20 }"
       PageSize="@pageSize"
       PageIndex="@pageIndex"
       Total="@total"
       OnPageSizeChanged="OnPageSizeChanged"
       OnPageIndexChanged="OnPageIndexChanged"
       IsPagination="true">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Age" />
    </TableColumns>
</Table>

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 100;
    private List<Person> allData = new();
    private List<Person> displayData = new();
    
    protected override void OnInitialized()
    {
        // Generate sample data
        allData = Enumerable.Range(1, total).Select(i => new Person
        {
            Id = i,
            Name = $"Person {i}",
            Age = Random.Shared.Next(18, 70)
        }).ToList();
        
        // Initialize display data
        UpdateDisplayData();
    }
    
    private Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        UpdateDisplayData();
        return Task.CompletedTask;
    }
    
    private Task OnPageSizeChanged(int pageSize)
    {
        this.pageSize = pageSize;
        this.pageIndex = 1; // Reset to first page when changing page size
        UpdateDisplayData();
        return Task.CompletedTask;
    }
    
    private void UpdateDisplayData()
    {
        // Calculate start and end indices
        int startIndex = (pageIndex - 1) * pageSize;
        
        // Update display data
        displayData = allData.Skip(startIndex).Take(pageSize).ToList();
    }
    
    private class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
```
This example shows how to integrate the Pagination component with a Table component for displaying paginated data in a tabular format.

### Example 6: Server-Side Pagination
```csharp
@inject HttpClient Http

<Pagination PageIndex="@pageIndex" 
           PageSize="@pageSize" 
           Total="@total" 
           OnPageIndexChanged="OnPageIndexChanged"
           OnPageSizeChanged="OnPageSizeChanged" />

<Table TItem="User"
       Items="@users"
       IsLoading="@isLoading">
    <TableColumns>
        <TableColumn @bind-Field="@context.Id" Width="80" />
        <TableColumn @bind-Field="@context.Name" />
        <TableColumn @bind-Field="@context.Email" />
    </TableColumns>
</Table>

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 0;
    private List<User> users = new();
    private bool isLoading = false;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadData();
    }
    
    private async Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        await LoadData();
    }
    
    private async Task OnPageSizeChanged(int pageSize)
    {
        this.pageSize = pageSize;
        this.pageIndex = 1; // Reset to first page when changing page size
        await LoadData();
    }
    
    private async Task LoadData()
    {
        try
        {
            isLoading = true;
            
            // Make API call to get paginated data
            var response = await Http.GetFromJsonAsync<PaginationResponse<User>>(
                $"api/users?page={pageIndex}&pageSize={pageSize}");
            
            if (response != null)
            {
                users = response.Items;
                total = response.Total;
            }
        }
        catch (Exception ex)
        {
            // Handle error
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
    
    private class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
    
    private class PaginationResponse<T>
    {
        public List<T> Items { get; set; } = new();
        public int Total { get; set; }
    }
}
```
This example demonstrates server-side pagination, where data is fetched from an API with pagination parameters. The server returns both the paginated data and the total count.

### Example 7: Custom Pagination Styling
```csharp
<div class="custom-pagination-container">
    <Pagination PageIndex="@pageIndex" 
               PageSize="@pageSize" 
               Total="@total" 
               OnPageIndexChanged="OnPageIndexChanged"
               Size="Size.Small"
               AlignCenter="true"
               ShowFirstLastButtons="false" />
</div>

<style>
    .custom-pagination-container :deep(.pagination) {
        --bb-pagination-color: #6c757d;
        --bb-pagination-bg: #f8f9fa;
        --bb-pagination-border-color: #dee2e6;
        --bb-pagination-hover-color: #fff;
        --bb-pagination-hover-bg: #0d6efd;
        --bb-pagination-hover-border-color: #0d6efd;
        --bb-pagination-active-color: #fff;
        --bb-pagination-active-bg: #0d6efd;
        --bb-pagination-active-border-color: #0d6efd;
        --bb-pagination-disabled-color: #6c757d;
        --bb-pagination-disabled-bg: #fff;
        --bb-pagination-disabled-border-color: #dee2e6;
    }
</style>

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 100;
    
    private Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        return Task.CompletedTask;
    }
}
```
This example shows how to customize the styling of the Pagination component using CSS variables, as well as component properties like `Size`, `AlignCenter`, and `ShowFirstLastButtons`.

## CSS Customization

The Pagination component can be customized using the following CSS variables:

```css
--bb-pagination-padding-y: 0.375rem;
--bb-pagination-padding-x: 0.75rem;
--bb-pagination-font-size: 1rem;
--bb-pagination-color: #007bff;
--bb-pagination-bg: #fff;
--bb-pagination-border-width: 1px;
--bb-pagination-border-color: #dee2e6;
--bb-pagination-border-radius: 0.25rem;
--bb-pagination-hover-color: #0056b3;
--bb-pagination-hover-bg: #e9ecef;
--bb-pagination-hover-border-color: #dee2e6;
--bb-pagination-active-color: #fff;
--bb-pagination-active-bg: #007bff;
--bb-pagination-active-border-color: #007bff;
--bb-pagination-disabled-color: #6c757d;
--bb-pagination-disabled-bg: #fff;
--bb-pagination-disabled-border-color: #dee2e6;
```

## Service Integration

The Pagination component can be integrated with the `PaginationService` for more advanced scenarios:

```csharp
@inject PaginationService PaginationService

<Pagination PageIndex="@pageIndex" 
           PageSize="@pageSize" 
           Total="@total" 
           OnPageIndexChanged="OnPageIndexChanged"
           OnPageSizeChanged="OnPageSizeChanged" />

@code {
    private int pageIndex = 1;
    private int pageSize = 10;
    private int total = 0;
    
    protected override void OnInitialized()
    {
        // Subscribe to pagination events
        PaginationService.OnPageChanged += HandlePageChanged;
    }
    
    private void HandlePageChanged(int pageIndex, int pageSize)
    {
        this.pageIndex = pageIndex;
        this.pageSize = pageSize;
        StateHasChanged();
    }
    
    private Task OnPageIndexChanged(int pageIndex)
    {
        this.pageIndex = pageIndex;
        // Notify other components about page change
        PaginationService.ChangePage(pageIndex, pageSize);
        return Task.CompletedTask;
    }
    
    private Task OnPageSizeChanged(int pageSize)
    {
        this.pageSize = pageSize;
        this.pageIndex = 1; // Reset to first page when changing page size
        // Notify other components about page size change
        PaginationService.ChangePage(pageIndex, pageSize);
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        // Unsubscribe from events
        PaginationService.OnPageChanged -= HandlePageChanged;
    }
}
```

To use the `PaginationService`, you need to register it in your application's service collection:

```csharp
builder.Services.AddBootstrapBlazor();
// or specifically
builder.Services.AddSingleton<PaginationService>();
```

## Notes

1. **Accessibility**: The Pagination component includes ARIA attributes for better accessibility. It uses `aria-label` and `aria-current` attributes to indicate the current page.

2. **Performance**: For large datasets, consider using server-side pagination to avoid loading all data at once, which can improve performance and reduce memory usage.

3. **Responsive Design**: The Pagination component automatically adjusts its layout on smaller screens. On mobile devices, it may hide some page numbers and show only essential navigation controls.

4. **Localization**: The Pagination component supports localization for text like "Page", "of", "items per page", etc. You can customize these texts by configuring the localization service.

5. **Integration with Other Components**: The Pagination component is designed to work seamlessly with other data display components like Table, List, and DataGrid, providing a consistent pagination experience across your application.