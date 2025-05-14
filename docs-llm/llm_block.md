# Block Component

## Overview
The Block component in BootstrapBlazor provides a container with loading state management. It's designed to wrap content sections that may require loading states, such as during data fetching or processing operations. The component can display a loading indicator, apply a mask overlay, and prevent user interaction with the contained content while in a loading state. This enhances user experience by providing clear visual feedback during asynchronous operations.

## Key Features
- **Loading State Management**: Toggle between loading and normal states
- **Customizable Loading Indicator**: Support for different spinner types and sizes
- **Content Masking**: Optional overlay mask during loading state
- **Conditional Rendering**: Option to show/hide content during loading
- **Timeout Handling**: Configurable timeout for loading states
- **Loading Text**: Customizable loading message
- **Animation Effects**: Smooth transitions between states
- **Backdrop Options**: Configurable backdrop opacity and color
- **Responsive Behavior**: Adapts to container size
- **Accessibility Support**: ARIA attributes for screen readers

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `IsLoading` | `bool` | `false` | Determines whether the block is in loading state |
| `ShowMask` | `bool` | `true` | Whether to show a mask overlay during loading |
| `ShowIndicator` | `bool` | `true` | Whether to show a loading indicator |
| `ShowLoadingText` | `bool` | `false` | Whether to show loading text |
| `LoadingText` | `string` | `"Loading..."` | Text to display during loading |
| `Timeout` | `int` | `0` | Timeout in milliseconds (0 means no timeout) |
| `ChildContent` | `RenderFragment` | `null` | Content to be displayed within the block |
| `LoadingTemplate` | `RenderFragment` | `null` | Custom template for the loading indicator |
| `SpinnerType` | `SpinnerType` | `SpinnerType.Border` | Type of spinner to display (Border, Grow) |
| `SpinnerSize` | `Size` | `Size.Medium` | Size of the spinner (Small, Medium, Large) |
| `Color` | `Color` | `Color.Primary` | Color of the spinner |
| `BackdropColor` | `string` | `"rgba(255, 255, 255, 0.6)"` | Color of the backdrop overlay |
| `MinHeight` | `int` | `0` | Minimum height of the block in pixels |

## Events

| Event | Description |
| --- | --- |
| `OnLoadingChanged` | Triggered when the loading state changes |
| `OnTimeout` | Triggered when the loading timeout is reached |

## Usage Examples

### Example 1: Basic Block with Loading State
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="ToggleLoading">Toggle Loading</Button>
</div>

<Block IsLoading="@isLoading">
    <div class="p-3 border rounded">
        <h4>Content Section</h4>
        <p>This content will be masked during loading state.</p>
        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam euismod, nisl eget aliquam ultricies, nunc nisl aliquet nunc, quis aliquam nisl nunc quis nisl.</p>
    </div>
</Block>

@code {
    private bool isLoading = false;
    
    private void ToggleLoading()
    {
        isLoading = !isLoading;
        
        if (isLoading)
        {
            // Simulate an operation that takes time
            Task.Delay(3000).ContinueWith(_ => {
                isLoading = false;
                StateHasChanged();
            });
        }
    }
}
```

### Example 2: Block with Custom Loading Template
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="StartLoading">Load Data</Button>
</div>

<Block IsLoading="@isLoading" ShowLoadingText="true" LoadingText="Fetching data...">
    <LoadingTemplate>
        <div class="custom-loader">
            <div class="spinner-grow text-primary" role="status"></div>
            <div class="spinner-grow text-secondary" role="status"></div>
            <div class="spinner-grow text-success" role="status"></div>
            <div class="mt-2">@LoadingText</div>
        </div>
    </LoadingTemplate>
    <div class="p-3 border rounded">
        <h4>Data Display</h4>
        @if (items.Count > 0)
        {
            <ul class="list-group">
                @foreach (var item in items)
                {
                    <li class="list-group-item">@item</li>
                }
            </ul>
        }
        else
        {
            <p>No data to display. Click the button to load data.</p>
        }
    </div>
</Block>

<style>
    .custom-loader {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        height: 100%;
    }
    
    .custom-loader .spinner-grow {
        margin: 0 0.25rem;
    }
</style>

@code {
    private bool isLoading = false;
    private List<string> items = new List<string>();
    private string LoadingText = "Fetching data...";
    
    private async Task StartLoading()
    {
        isLoading = true;
        items.Clear();
        
        // Simulate data fetching
        await Task.Delay(2000);
        
        // Add sample data
        items.AddRange(new[] {
            "Item 1 - Data loaded successfully",
            "Item 2 - More information here",
            "Item 3 - Additional details",
            "Item 4 - Extra information",
            "Item 5 - Final item"
        });
        
        isLoading = false;
    }
}
```

### Example 3: Block with Timeout Handling
```razor
@using BootstrapBlazor.Components
@inject ToastService ToastService

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="StartOperation">Start Operation</Button>
</div>

<Block @ref="blockRef"
       IsLoading="@isLoading"
       Timeout="5000"
       OnTimeout="HandleTimeout"
       ShowLoadingText="true"
       LoadingText="Operation in progress...">
    <div class="p-3 border rounded">
        <h4>Operation Status</h4>
        <p>Status: <strong>@status</strong></p>
        <p>Last operation result: <strong>@result</strong></p>
    </div>
</Block>

@code {
    private Block? blockRef;
    private bool isLoading = false;
    private string status = "Ready";
    private string result = "None";
    
    private async Task StartOperation()
    {
        isLoading = true;
        status = "Processing";
        result = "Pending";
        
        try
        {
            // Simulate a long-running operation that might time out
            // In this example, we'll use a random delay that might exceed the timeout
            Random random = new Random();
            int delay = random.Next(3000, 7000); // Between 3 and 7 seconds
            
            await Task.Delay(delay);
            
            // If we get here, the operation completed before timeout
            status = "Completed";
            result = $"Operation completed in {delay}ms";
        }
        catch (Exception ex)
        {
            status = "Error";
            result = $"Error: {ex.Message}";
        }
        finally
        {
            // Only set isLoading to false if we haven't timed out
            // The timeout handler will set it to false if timeout occurs
            if (status != "Timeout")
            {
                isLoading = false;
            }
        }
    }
    
    private Task HandleTimeout()
    {
        status = "Timeout";
        result = "Operation timed out after 5 seconds";
        isLoading = false;
        
        ToastService.Error("The operation timed out. Please try again.");
        
        return Task.CompletedTask;
    }
}
```

### Example 4: Block with Different Spinner Types and Sizes
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <h5>Spinner Type</h5>
    <div class="d-flex gap-2">
        <Radio TValue="SpinnerType" Value="SpinnerType.Border" @bind-Value="spinnerType" DisplayText="Border" />
        <Radio TValue="SpinnerType" Value="SpinnerType.Grow" @bind-Value="spinnerType" DisplayText="Grow" />
    </div>
</div>

<div class="mb-3">
    <h5>Spinner Size</h5>
    <div class="d-flex gap-2">
        <Radio TValue="Size" Value="Size.Small" @bind-Value="spinnerSize" DisplayText="Small" />
        <Radio TValue="Size" Value="Size.Medium" @bind-Value="spinnerSize" DisplayText="Medium" />
        <Radio TValue="Size" Value="Size.Large" @bind-Value="spinnerSize" DisplayText="Large" />
    </div>
</div>

<div class="mb-3">
    <h5>Spinner Color</h5>
    <div class="d-flex gap-2">
        <Radio TValue="Color" Value="Color.Primary" @bind-Value="spinnerColor" DisplayText="Primary" />
        <Radio TValue="Color" Value="Color.Secondary" @bind-Value="spinnerColor" DisplayText="Secondary" />
        <Radio TValue="Color" Value="Color.Success" @bind-Value="spinnerColor" DisplayText="Success" />
        <Radio TValue="Color" Value="Color.Danger" @bind-Value="spinnerColor" DisplayText="Danger" />
        <Radio TValue="Color" Value="Color.Warning" @bind-Value="spinnerColor" DisplayText="Warning" />
        <Radio TValue="Color" Value="Color.Info" @bind-Value="spinnerColor" DisplayText="Info" />
    </div>
</div>

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="ToggleLoading">Toggle Loading</Button>
</div>

<Block IsLoading="@isLoading"
       SpinnerType="@spinnerType"
       SpinnerSize="@spinnerSize"
       Color="@spinnerColor"
       ShowLoadingText="true"
       LoadingText="Loading with custom spinner...">
    <div class="p-3 border rounded" style="min-height: 200px;">
        <h4>Content Area</h4>
        <p>This block demonstrates different spinner types, sizes, and colors.</p>
    </div>
</Block>

@code {
    private bool isLoading = false;
    private SpinnerType spinnerType = SpinnerType.Border;
    private Size spinnerSize = Size.Medium;
    private Color spinnerColor = Color.Primary;
    
    private void ToggleLoading()
    {
        isLoading = !isLoading;
        
        if (isLoading)
        {
            // Automatically turn off loading after 3 seconds
            Task.Delay(3000).ContinueWith(_ => {
                isLoading = false;
                StateHasChanged();
            });
        }
    }
}
```

### Example 5: Block with Conditional Content Rendering
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="LoadData">Load Data</Button>
</div>

<Block @ref="blockRef" IsLoading="@isLoading" MinHeight="300">
    @if (isDataLoaded)
    {
        <div class="p-3 border rounded">
            <h4>Data Visualization</h4>
            <div class="row">
                @foreach (var item in chartData)
                {
                    <div class="col-md-4 mb-3">
                        <div class="card">
                            <div class="card-body">
                                <h5 class="card-title">@item.Category</h5>
                                <div class="progress">
                                    <div class="progress-bar" role="progressbar" style="width: @(item.Value)%;" aria-valuenow="@item.Value" aria-valuemin="0" aria-valuemax="100">@item.Value%</div>
                                </div>
                                <p class="mt-2 mb-0">@item.Description</p>
                            </div>
                        </div>
                    </div>
                }
            </div>
        </div>
    }
    else
    {
        <div class="p-3 border rounded d-flex align-items-center justify-content-center" style="height: 300px;">
            <div class="text-center">
                <h4>No Data Available</h4>
                <p>Click the "Load Data" button to fetch data.</p>
            </div>
        </div>
    }
</Block>

@code {
    private Block? blockRef;
    private bool isLoading = false;
    private bool isDataLoaded = false;
    private List<ChartDataItem> chartData = new List<ChartDataItem>();
    
    private async Task LoadData()
    {
        isLoading = true;
        
        // Clear existing data
        chartData.Clear();
        isDataLoaded = false;
        
        // Simulate data loading delay
        await Task.Delay(2000);
        
        // Generate sample data
        chartData = new List<ChartDataItem>
        {
            new ChartDataItem { Category = "Category A", Value = 75, Description = "Primary business segment" },
            new ChartDataItem { Category = "Category B", Value = 45, Description = "Secondary business segment" },
            new ChartDataItem { Category = "Category C", Value = 90, Description = "Tertiary business segment" },
            new ChartDataItem { Category = "Category D", Value = 30, Description = "Emerging business segment" },
            new ChartDataItem { Category = "Category E", Value = 60, Description = "Support business segment" },
            new ChartDataItem { Category = "Category F", Value = 85, Description = "Experimental business segment" }
        };
        
        isDataLoaded = true;
        isLoading = false;
    }
    
    private class ChartDataItem
    {
        public string Category { get; set; } = "";
        public int Value { get; set; }
        public string Description { get; set; } = "";
    }
}
```

### Example 6: Block with Custom Backdrop
```razor
@using BootstrapBlazor.Components

<div class="mb-3">
    <h5>Backdrop Color</h5>
    <div class="d-flex gap-2 align-items-center">
        <div class="color-preview" style="background-color: @backdropColor;"></div>
        <input type="color" value="@backdropColorHex" @onchange="OnColorChange" />
        <div class="ms-2">Opacity: @backdropOpacity.ToString("P0")</div>
        <input type="range" class="form-range" min="0" max="1" step="0.1" value="@backdropOpacity" @onchange="OnOpacityChange" style="width: 200px;" />
    </div>
</div>

<div class="mb-3">
    <Button Color="Color.Primary" OnClick="ToggleLoading">Toggle Loading</Button>
</div>

<Block IsLoading="@isLoading"
       BackdropColor="@backdropColor"
       ShowLoadingText="true"
       LoadingText="Custom backdrop demonstration">
    <div class="p-3 border rounded" style="min-height: 200px;">
        <h4>Content with Custom Backdrop</h4>
        <p>This example demonstrates how to customize the backdrop color and opacity.</p>
        <p>Try different colors and opacity levels to see how they affect the loading overlay.</p>
    </div>
</Block>

<style>
    .color-preview {
        width: 30px;
        height: 30px;
        border-radius: 4px;
        border: 1px solid #ccc;
    }
</style>

@code {
    private bool isLoading = false;
    private double backdropOpacity = 0.6;
    private string backdropColorHex = "#007bff";
    private string backdropColor => $"rgba({GetRgbComponents(backdropColorHex)}, {backdropOpacity})";
    
    private void ToggleLoading()
    {
        isLoading = !isLoading;
        
        if (isLoading)
        {
            // Automatically turn off loading after 3 seconds
            Task.Delay(3000).ContinueWith(_ => {
                isLoading = false;
                StateHasChanged();
            });
        }
    }
    
    private void OnColorChange(ChangeEventArgs args)
    {
        if (args.Value is string colorValue)
        {
            backdropColorHex = colorValue;
        }
    }
    
    private void OnOpacityChange(ChangeEventArgs args)
    {
        if (args.Value is string opacityValue && double.TryParse(opacityValue, out double opacity))
        {
            backdropOpacity = opacity;
        }
    }
    
    private string GetRgbComponents(string hex)
    {
        // Remove # if present
        hex = hex.TrimStart('#');
        
        // Parse the hex color
        int r = Convert.ToInt32(hex.Substring(0, 2), 16);
        int g = Convert.ToInt32(hex.Substring(2, 2), 16);
        int b = Convert.ToInt32(hex.Substring(4, 2), 16);
        
        return $"{r}, {g}, {b}";
    }
}
```

### Example 7: Block with Loading State Management in a Form
```razor
@using BootstrapBlazor.Components
@using System.ComponentModel.DataAnnotations

<Block @ref="formBlock" IsLoading="@isSubmitting">
    <ValidateForm Model="@model" OnValidSubmit="HandleValidSubmit">
        <div class="row">
            <div class="col-md-6 mb-3">
                <BootstrapInput @bind-Value="@model.FirstName" DisplayText="First Name" placeholder="Enter first name" />
            </div>
            <div class="col-md-6 mb-3">
                <BootstrapInput @bind-Value="@model.LastName" DisplayText="Last Name" placeholder="Enter last name" />
            </div>
        </div>
        
        <div class="row">
            <div class="col-md-6 mb-3">
                <BootstrapInput @bind-Value="@model.Email" DisplayText="Email" placeholder="Enter email address" />
            </div>
            <div class="col-md-6 mb-3">
                <BootstrapInput @bind-Value="@model.Phone" DisplayText="Phone" placeholder="Enter phone number" />
            </div>
        </div>
        
        <div class="row">
            <div class="col-12 mb-3">
                <Textarea @bind-Value="@model.Message" DisplayText="Message" placeholder="Enter your message" rows="5" />
            </div>
        </div>
        
        <div class="row">
            <div class="col-12">
                <div class="d-flex gap-2">
                    <Button ButtonType="ButtonType.Submit" Color="Color.Primary">Submit</Button>
                    <Button ButtonType="ButtonType.Reset" Color="Color.Secondary">Reset</Button>
                </div>
            </div>
        </div>
    </ValidateForm>
</Block>

@if (isSubmitSuccess)
{
    <Alert Color="Color.Success" ShowDismiss="true" class="mt-3">
        <h4>Form Submitted Successfully</h4>
        <p>Thank you for your submission, @model.FirstName!</p>
        <p>We will contact you shortly at @model.Email.</p>
    </Alert>
}

@code {
    private Block? formBlock;
    private bool isSubmitting = false;
    private bool isSubmitSuccess = false;
    private ContactFormModel model = new();
    
    private async Task HandleValidSubmit()
    {
        isSubmitting = true;
        isSubmitSuccess = false;
        
        // Simulate form submission delay
        await Task.Delay(2000);
        
        // Simulate successful submission
        isSubmitSuccess = true;
        isSubmitting = false;
    }
    
    private class ContactFormModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; } = "";
        
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public string LastName { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; } = "";
        
        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
        public string Message { get; set; } = "";
    }
}
```

## CSS Customization

The Block component can be customized using CSS variables:

```css
/* Block component custom styling */
.bb-block {
  --bb-block-min-height: 100px;
  --bb-block-position: relative;
  --bb-block-transition: all 0.3s ease-in-out;
  
  /* Backdrop customization */
  --bb-block-backdrop-bg: rgba(255, 255, 255, 0.6);
  --bb-block-backdrop-z-index: 1;
  --bb-block-backdrop-transition: opacity 0.3s ease-in-out;
  
  /* Loading indicator customization */
  --bb-block-loading-z-index: 2;
  --bb-block-loading-color: var(--bs-primary);
  --bb-block-loading-text-color: var(--bs-body-color);
  --bb-block-loading-text-font-size: 0.875rem;
  --bb-block-loading-text-margin-top: 0.5rem;
}

/* Custom styling for the loading container */
.bb-block-loading {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  z-index: var(--bb-block-loading-z-index);
}

/* Custom styling for the backdrop */
.bb-block-backdrop {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: var(--bb-block-backdrop-bg);
  z-index: var(--bb-block-backdrop-z-index);
  transition: var(--bb-block-backdrop-transition);
}

/* Custom styling for the loading text */
.bb-block-loading-text {
  margin-top: var(--bb-block-loading-text-margin-top);
  font-size: var(--bb-block-loading-text-font-size);
  color: var(--bb-block-loading-text-color);
}
```

## Notes

### Integration with Other Components
- The Block component works well with other BootstrapBlazor components like Forms, Tables, and Cards.
- It can be used to wrap any content that requires loading state management.

### Performance Considerations
- The Block component uses CSS transitions for smooth animations, which may impact performance on older browsers.
- For large content areas, consider using a more specific selector for the loading indicator to improve performance.

### Accessibility
- The Block component includes ARIA attributes to improve accessibility for screen readers.
- When using custom loading templates, ensure they include appropriate ARIA attributes.
- Consider using appropriate color contrast for the loading text and backdrop to ensure visibility for all users.

### Best Practices
- Use the Block component to provide clear visual feedback during asynchronous operations.
- Set appropriate timeout values to prevent indefinite loading states.
- Consider using the `MinHeight` property to ensure the loading indicator is visible even when the content is empty.
- Use the `OnLoadingChanged` event to perform additional actions when the loading state changes.
- For forms, wrap the entire form in a Block component to prevent user interaction during submission.
- Use custom loading templates for more complex loading indicators or to match your application's design language.