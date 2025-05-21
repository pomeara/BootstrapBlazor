# Empty Component

## Overview
The Empty component in BootstrapBlazor provides a standardized way to display placeholder content when there is no data to show. It creates a visually consistent empty state that can include an image, descriptive text, and optional actions. This component is particularly useful for lists, tables, search results, or any data container that might be empty, helping to guide users and provide context rather than showing a blank space.

## Features
- **Visual Feedback**: Provides clear visual indication of empty state
- **Customizable Image**: Support for default or custom empty state illustrations
- **Descriptive Text**: Configurable message to explain the empty state
- **Action Support**: Optional buttons or links for user actions
- **Custom Templates**: Flexible templating for complete customization
- **Responsive Design**: Adapts to different container sizes
- **Consistent Styling**: Maintains design consistency across the application
- **Accessibility Support**: Screen reader friendly with proper ARIA attributes
- **Animation Options**: Optional animations for empty state appearance
- **Integration**: Seamless integration with other data display components

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Image` | `string` | `null` | URL or path to the image displayed in the empty state |
| `ImageTemplate` | `RenderFragment` | `null` | Custom template for the empty state image |
| `Description` | `string` | `"No Data"` | Text description explaining the empty state |
| `DescriptionTemplate` | `RenderFragment` | `null` | Custom template for the description content |
| `ActionTemplate` | `RenderFragment` | `null` | Template for action buttons or links |
| `ShowImage` | `bool` | `true` | Whether to display the empty state image |
| `ShowDescription` | `bool` | `true` | Whether to display the description text |
| `ImageWidth` | `int` | `100` | Width of the empty state image in pixels |
| `ImageHeight` | `int` | `100` | Height of the empty state image in pixels |
| `Class` | `string` | `""` | Additional CSS class for the component |
| `ChildContent` | `RenderFragment` | `null` | Custom content to display in the empty state |

## Events

| Event | Description |
| --- | --- |
| `OnActionClick` | Triggered when the default action button is clicked |

## Usage Examples

### Example 1: Basic Empty State

```razor
<Empty Description="No data available" />
```

This example shows a basic empty state with the default image and a custom description message.

### Example 2: Custom Image and Description

```razor
<Empty Image="/images/custom-empty.svg"
       Description="No search results found"
       ImageWidth="150"
       ImageHeight="150" />
```

This example demonstrates an empty state with a custom image and description, with specified image dimensions.

### Example 3: Empty State with Action

```razor
<Empty Description="Your cart is empty">
    <ActionTemplate>
        <Button Color="Color.Primary" OnClick="NavigateToProducts">Browse Products</Button>
    </ActionTemplate>
</Empty>

@code {
    private void NavigateToProducts()
    {
        // Navigation logic here
        Console.WriteLine("Navigating to products page");
    }
}
```

This example shows an empty state for a shopping cart with an action button that navigates to the products page.

### Example 4: Fully Customized Empty State

```razor
<Empty>
    <ImageTemplate>
        <div class="custom-empty-image">
            <i class="fa fa-file-alt fa-4x text-muted"></i>
        </div>
    </ImageTemplate>
    
    <DescriptionTemplate>
        <h4>No Documents Found</h4>
        <p class="text-muted">There are no documents matching your search criteria.</p>
    </DescriptionTemplate>
    
    <ActionTemplate>
        <div class="d-flex gap-2">
            <Button Color="Color.Secondary" OnClick="ClearFilters">Clear Filters</Button>
            <Button Color="Color.Primary" OnClick="CreateDocument">Create New Document</Button>
        </div>
    </ActionTemplate>
</Empty>

@code {
    private void ClearFilters()
    {
        // Clear filter logic
        Console.WriteLine("Filters cleared");
    }
    
    private void CreateDocument()
    {
        // Document creation logic
        Console.WriteLine("Creating new document");
    }
}
```

This example demonstrates a fully customized empty state with a custom icon, detailed description, and multiple action buttons.

### Example 5: Empty State in a Table

```razor
<Table TItem="User" Items="@users">
    <EmptyTemplate>
        <Empty Description="No users found" Image="/images/no-users.svg">
            <ActionTemplate>
                <Button Color="Color.Primary" OnClick="AddUser">Add User</Button>
            </ActionTemplate>
        </Empty>
    </EmptyTemplate>
    <Columns>
        <TableColumn @bind-Field="@context.Id" Text="ID" />
        <TableColumn @bind-Field="@context.Name" Text="Name" />
        <TableColumn @bind-Field="@context.Email" Text="Email" />
    </Columns>
</Table>

@code {
    private List<User> users = new List<User>();
    
    private void AddUser()
    {
        // Add user logic
        Console.WriteLine("Adding new user");
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
```

This example shows how to use the Empty component within a Table component's EmptyTemplate to display a custom empty state when there are no items to display.

### Example 6: Empty State with Animation

```razor
<div class="position-relative">
    <Empty Description="No notifications">
        <ImageTemplate>
            <div class="animated-empty-image">
                <i class="fa fa-bell fa-3x text-muted"></i>
                <div class="notification-badge"></div>
            </div>
        </ImageTemplate>
    </Empty>
</div>

<style>
    .animated-empty-image {
        position: relative;
        padding: 20px;
    }
    
    .notification-badge {
        position: absolute;
        top: 15px;
        right: 15px;
        width: 10px;
        height: 10px;
        background-color: #dc3545;
        border-radius: 50%;
        animation: pulse 2s infinite;
    }
    
    @keyframes pulse {
        0% {
            transform: scale(0.95);
            box-shadow: 0 0 0 0 rgba(220, 53, 69, 0.7);
        }
        70% {
            transform: scale(1);
            box-shadow: 0 0 0 10px rgba(220, 53, 69, 0);
        }
        100% {
            transform: scale(0.95);
            box-shadow: 0 0 0 0 rgba(220, 53, 69, 0);
        }
    }
</style>
```

This example demonstrates an empty state for a notifications panel with an animated icon to draw attention.

### Example 7: Conditional Empty State

```razor
<div class="search-results">
    @if (isSearching)
    {
        <div class="d-flex justify-content-center p-5">
            <Spinner />
        </div>
    }
    else if (searchResults.Count == 0)
    {
        <Empty>
            <ImageTemplate>
                <img src="@(string.IsNullOrEmpty(searchTerm) ? "/images/empty-search.svg" : "/images/no-results.svg")" 
                     width="120" height="120" alt="Empty state" />
            </ImageTemplate>
            
            <DescriptionTemplate>
                @if (string.IsNullOrEmpty(searchTerm))
                {
                    <p>Enter a search term to begin</p>
                }
                else
                {
                    <div>
                        <h5>No results found for "@searchTerm"</h5>
                        <p class="text-muted">Try adjusting your search terms or filters</p>
                    </div>
                }
            </DescriptionTemplate>
        </Empty>
    }
    else
    {
        <div class="result-list">
            @foreach (var result in searchResults)
            {
                <div class="result-item">@result</div>
            }
        </div>
    }
</div>

@code {
    private bool isSearching = false;
    private string searchTerm = "";
    private List<string> searchResults = new List<string>();
    
    private async Task Search(string term)
    {
        searchTerm = term;
        isSearching = true;
        searchResults.Clear();
        
        await Task.Delay(1000); // Simulate search delay
        
        if (!string.IsNullOrEmpty(term))
        {
            // Simulate search results
            if (term.Contains("found", StringComparison.OrdinalIgnoreCase))
            {
                searchResults.Add($"Result 1 for {term}");
                searchResults.Add($"Result 2 for {term}");
                searchResults.Add($"Result 3 for {term}");
            }
        }
        
        isSearching = false;
    }
}
```

This example shows a conditional empty state that displays different content based on whether the user has performed a search and whether any results were found.

## Customization

The Empty component can be customized using CSS variables:

```css
.empty {
    /* Image margin */
    --bb-empty-image-margin: 1rem 0;
    
    /* Template margin */
    --bb-empty-template-margin: 1rem 0;
}
```

You can override these variables in your CSS to customize the appearance of the Empty component according to your design requirements.

Additionally, you can customize the Empty component by:

1. Using the `ImageTemplate` property to provide a custom image or icon
2. Using the `DescriptionTemplate` property to create a custom description
3. Using the `ActionTemplate` property to add custom action buttons or links
4. Using the `ImageWidth` and `ImageHeight` properties to adjust the image size
5. Using the `ShowImage` and `ShowDescription` properties to control visibility
6. Applying custom CSS classes to the component using the `Class` property