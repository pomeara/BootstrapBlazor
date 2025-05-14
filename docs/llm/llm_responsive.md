# Responsive Component

## Overview

The Responsive component in BootstrapBlazor provides a way to conditionally render content based on the current viewport size or device characteristics. It allows developers to create adaptive user interfaces that respond to different screen sizes without writing JavaScript or complex CSS media queries. This component is particularly useful for creating mobile-friendly applications that need different layouts or components across various device types.

## Features

- **Viewport-Based Rendering**: Conditionally display content based on screen size
- **Device Type Detection**: Render different content for mobile, tablet, and desktop devices
- **Orientation Support**: Adapt UI based on device orientation (portrait or landscape)
- **Breakpoint System**: Align with Bootstrap's responsive breakpoint system (xs, sm, md, lg, xl, xxl)
- **Content Adaptation**: Show, hide, or transform content based on viewport characteristics
- **Performance Optimization**: Efficient rendering with minimal overhead
- **Server-Side Compatibility**: Works in both Blazor WebAssembly and Blazor Server

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Breakpoint` | `Breakpoint` | `Breakpoint.None` | The breakpoint at which the component should trigger. Options include None, ExtraSmall, Small, Medium, Large, ExtraLarge, and ExtraExtraLarge. |
| `Mode` | `ResponsiveMode` | `ResponsiveMode.Auto` | The mode of operation. Auto adjusts based on breakpoint, Show displays content at or above the breakpoint, and Hide displays content below the breakpoint. |
| `DeviceType` | `DeviceType` | `DeviceType.None` | The device type to target. Options include None, Mobile, Tablet, and Desktop. |
| `Orientation` | `Orientation` | `Orientation.None` | The device orientation to target. Options include None, Portrait, and Landscape. |
| `ChildContent` | `RenderFragment` | `null` | The content to be conditionally rendered based on the responsive criteria. |
| `FallbackContent` | `RenderFragment` | `null` | Alternative content to display when the responsive criteria are not met. |
| `EnableClientCheck` | `bool` | `true` | When true, enables client-side checking of viewport size for more accurate responsiveness. |
| `RefreshOnResize` | `bool` | `true` | When true, the component will re-evaluate and potentially re-render when the window is resized. |
| `ResizeDebounceInterval` | `int` | `250` | The debounce interval in milliseconds for handling window resize events. |

## Events

| Event | Description |
|-------|-------------|
| `OnBreakpointChanged` | Triggered when the active breakpoint changes. Provides the new breakpoint value. |
| `OnOrientationChanged` | Triggered when the device orientation changes. Provides the new orientation value. |
| `OnDeviceTypeChanged` | Triggered when the detected device type changes. Provides the new device type value. |
| `OnVisibilityChanged` | Triggered when the visibility of the component's content changes. Provides a boolean indicating if the content is visible. |

## Usage Examples

### Example 1: Basic Responsive Content

Showing different content based on screen size:

```razor
<div class="container">
    <h3>Responsive Content Example</h3>
    
    <Responsive Breakpoint="Breakpoint.Large" Mode="ResponsiveMode.Show">
        <div class="alert alert-success">
            <h4>Desktop View</h4>
            <p>This content is only visible on large screens (≥992px).</p>
        </div>
    </Responsive>
    
    <Responsive Breakpoint="Breakpoint.Medium" Mode="ResponsiveMode.Show" 
               FallbackContent="@FallbackTemplate">
        <div class="alert alert-info">
            <h4>Tablet View</h4>
            <p>This content is visible on medium screens (≥768px).</p>
        </div>
    </Responsive>
    
    <Responsive Breakpoint="Breakpoint.Small" Mode="ResponsiveMode.Hide">
        <div class="alert alert-warning">
            <h4>Mobile View</h4>
            <p>This content is hidden on small screens (<576px).</p>
        </div>
    </Responsive>
</div>

@code {
    private RenderFragment FallbackTemplate => @<div class="alert alert-secondary">
        <p>The main content is not available at this screen size.</p>
    </div>;
}
```

### Example 2: Responsive Navigation Menu

Implementing a responsive navigation menu that adapts to different screen sizes:

```razor
<header class="navbar navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
        <a class="navbar-brand" href="#">My App</a>
        
        <!-- Hamburger menu for mobile -->
        <Responsive Breakpoint="Breakpoint.Large" Mode="ResponsiveMode.Hide">
            <Button Color="Color.Dark" 
                    OnClick="ToggleMobileMenu" 
                    class="navbar-toggler">
                <i class="fa-solid fa-bars"></i>
            </Button>
        </Responsive>
        
        <!-- Desktop navigation -->
        <Responsive Breakpoint="Breakpoint.Large" Mode="ResponsiveMode.Show">
            <div class="collapse navbar-collapse">
                <ul class="navbar-nav me-auto">
                    <li class="nav-item"><a class="nav-link" href="#">Home</a></li>
                    <li class="nav-item"><a class="nav-link" href="#">Products</a></li>
                    <li class="nav-item"><a class="nav-link" href="#">Services</a></li>
                    <li class="nav-item"><a class="nav-link" href="#">About</a></li>
                    <li class="nav-item"><a class="nav-link" href="#">Contact</a></li>
                </ul>
                <form class="d-flex">
                    <input class="form-control me-2" type="search" placeholder="Search" />
                    <Button Color="Color.Primary">Search</Button>
                </form>
            </div>
        </Responsive>
    </div>
</header>

<!-- Mobile menu drawer -->
<Drawer Placement="Placement.Left" 
        IsOpen="@_isMobileMenuOpen" 
        OnClose="@(() => _isMobileMenuOpen = false)">
    <div class="p-3">
        <h5 class="mb-3">Menu</h5>
        <ul class="nav flex-column">
            <li class="nav-item"><a class="nav-link" href="#">Home</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Products</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Services</a></li>
            <li class="nav-item"><a class="nav-link" href="#">About</a></li>
            <li class="nav-item"><a class="nav-link" href="#">Contact</a></li>
        </ul>
        <hr />
        <form>
            <div class="input-group mb-3">
                <input type="text" class="form-control" placeholder="Search" />
                <Button Color="Color.Primary">Go</Button>
            </div>
        </form>
    </div>
</Drawer>

@code {
    private bool _isMobileMenuOpen = false;
    
    private void ToggleMobileMenu()
    {
        _isMobileMenuOpen = !_isMobileMenuOpen;
    }
}
```

### Example 3: Device-Specific Content

Displaying different content based on device type:

```razor
<div class="container my-4">
    <h3>Device-Specific Content</h3>
    
    <Responsive DeviceType="DeviceType.Mobile">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Mobile Experience</h5>
                <p class="card-text">Optimized for touch interaction on small screens.</p>
                <div class="d-grid">
                    <Button Color="Color.Primary">Tap to Continue</Button>
                </div>
            </div>
        </div>
    </Responsive>
    
    <Responsive DeviceType="DeviceType.Tablet">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Tablet Experience</h5>
                <p class="card-text">Designed for medium-sized touch screens with enhanced features.</p>
                <div class="d-flex justify-content-between">
                    <Button Color="Color.Secondary">Back</Button>
                    <Button Color="Color.Primary">Continue</Button>
                </div>
            </div>
        </div>
    </Responsive>
    
    <Responsive DeviceType="DeviceType.Desktop">
        <div class="card">
            <div class="card-body">
                <h5 class="card-title">Desktop Experience</h5>
                <p class="card-text">Full-featured interface optimized for mouse and keyboard.</p>
                <div class="row">
                    <div class="col-md-8">
                        <input type="text" class="form-control" placeholder="Enter details..." />
                    </div>
                    <div class="col-md-4">
                        <div class="d-flex justify-content-end">
                            <Button Color="Color.Secondary" class="me-2">Cancel</Button>
                            <Button Color="Color.Primary">Submit</Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </Responsive>
</div>
```

### Example 4: Orientation-Based Layout

Adapting layout based on device orientation:

```razor
<div class="container my-4">
    <h3>Orientation-Based Layout</h3>
    
    <Responsive Orientation="Orientation.Portrait">
        <div class="card mb-3">
            <div class="card-header bg-info text-white">
                <h5 class="mb-0">Portrait Mode</h5>
            </div>
            <div class="card-body">
                <div class="d-flex flex-column">
                    <img src="/images/sample-portrait.jpg" class="img-fluid mb-3" alt="Portrait image" />
                    <div>
                        <h5>Vertical Layout</h5>
                        <p>This layout is optimized for portrait orientation, with content stacked vertically for easier scrolling.</p>
                    </div>
                </div>
            </div>
        </div>
    </Responsive>
    
    <Responsive Orientation="Orientation.Landscape">
        <div class="card mb-3">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">Landscape Mode</h5>
            </div>
            <div class="card-body">
                <div class="d-flex flex-row">
                    <img src="/images/sample-landscape.jpg" class="img-fluid me-3" style="max-width: 50%;" alt="Landscape image" />
                    <div>
                        <h5>Horizontal Layout</h5>
                        <p>This layout takes advantage of the wider screen in landscape orientation, placing content side by side for better space utilization.</p>
                    </div>
                </div>
            </div>
        </div>
    </Responsive>
    
    <div class="alert alert-info">
        <i class="fa-solid fa-info-circle me-2"></i>
        <span>Try rotating your device to see the layout change.</span>
    </div>
</div>
```

### Example 5: Responsive Data Visualization

Adapting data visualization components based on screen size:

```razor
@page "/dashboard"

<div class="container-fluid my-4">
    <h3>Sales Dashboard</h3>
    
    <div class="row">
        <div class="col-md-12 mb-4">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Sales Overview</h5>
                    
                    <!-- Different chart types based on screen size -->
                    <Responsive Breakpoint="Breakpoint.Large" Mode="ResponsiveMode.Show">
                        <!-- Complex interactive chart for large screens -->
                        <Chart Type="ChartType.Line"
                               Options="@_desktopChartOptions"
                               Data="@_salesData"
                               Height="400" />
                    </Responsive>
                    
                    <Responsive Breakpoint="Breakpoint.Small" Mode="ResponsiveMode.Show"
                               Breakpoint2="Breakpoint.Large" Mode2="ResponsiveMode.Hide">
                        <!-- Simplified chart for medium screens -->
                        <Chart Type="ChartType.Bar"
                               Options="@_tabletChartOptions"
                               Data="@_salesData"
                               Height="300" />
                    </Responsive>
                    
                    <Responsive Breakpoint="Breakpoint.Small" Mode="ResponsiveMode.Hide">
                        <!-- Simple data display for small screens -->
                        <div class="table-responsive">
                            <Table Items="@_salesSummary"
                                   IsBordered="true"
                                   IsStriped="true">
                                <TableColumns>
                                    <TableColumn @bind-Field="@context.Month" />
                                    <TableColumn @bind-Field="@context.Sales" FormatString="{0:C0}" />
                                </TableColumns>
                            </Table>
                        </div>
                    </Responsive>
                </div>
            </div>
        </div>
    </div>
    
    <div class="row">
        <!-- Responsive grid layout for dashboard widgets -->
        <Responsive Breakpoint="Breakpoint.Large" Mode="ResponsiveMode.Show">
            <!-- 3-column layout for large screens -->
            <div class="row">
                <div class="col-md-4 mb-4">
                    <DashboardWidget Title="Revenue" Value="$45,289" Icon="fa-dollar-sign" Color="#28a745" />
                </div>
                <div class="col-md-4 mb-4">
                    <DashboardWidget Title="Orders" Value="1,205" Icon="fa-shopping-cart" Color="#007bff" />
                </div>
                <div class="col-md-4 mb-4">
                    <DashboardWidget Title="Customers" Value="3,842" Icon="fa-users" Color="#fd7e14" />
                </div>
            </div>
        </Responsive>
        
        <Responsive Breakpoint="Breakpoint.Medium" Mode="ResponsiveMode.Show"
                   Breakpoint2="Breakpoint.Large" Mode2="ResponsiveMode.Hide">
            <!-- 2-column layout for medium screens -->
            <div class="row">
                <div class="col-md-6 mb-4">
                    <DashboardWidget Title="Revenue" Value="$45,289" Icon="fa-dollar-sign" Color="#28a745" />
                </div>
                <div class="col-md-6 mb-4">
                    <DashboardWidget Title="Orders" Value="1,205" Icon="fa-shopping-cart" Color="#007bff" />
                </div>
                <div class="col-md-6 mb-4">
                    <DashboardWidget Title="Customers" Value="3,842" Icon="fa-users" Color="#fd7e14" />
                </div>
            </div>
        </Responsive>
        
        <Responsive Breakpoint="Breakpoint.Medium" Mode="ResponsiveMode.Hide">
            <!-- 1-column layout for small screens -->
            <div class="row">
                <div class="col-12 mb-4">
                    <DashboardWidget Title="Revenue" Value="$45,289" Icon="fa-dollar-sign" Color="#28a745" />
                </div>
                <div class="col-12 mb-4">
                    <DashboardWidget Title="Orders" Value="1,205" Icon="fa-shopping-cart" Color="#007bff" />
                </div>
                <div class="col-12 mb-4">
                    <DashboardWidget Title="Customers" Value="3,842" Icon="fa-users" Color="#fd7e14" />
                </div>
            </div>
        </Responsive>
    </div>
</div>

@code {
    private object _desktopChartOptions = new { /* Complex chart options */ };
    private object _tabletChartOptions = new { /* Simplified chart options */ };
    private object _salesData = new { /* Chart data */ };
    
    private List<SalesSummary> _salesSummary = new()
    {
        new SalesSummary { Month = "January", Sales = 12500 },
        new SalesSummary { Month = "February", Sales = 15200 },
        new SalesSummary { Month = "March", Sales = 18900 }
        // Additional months...
    };
    
    public class SalesSummary
    {
        public string Month { get; set; }
        public decimal Sales { get; set; }
    }
}

@* Dashboard Widget Component *@
@code {
    public class DashboardWidgetBase : ComponentBase
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Value { get; set; }
        [Parameter] public string Icon { get; set; }
        [Parameter] public string Color { get; set; }
    }
}
```

### Example 6: Responsive Form Layout

Adapting form layout based on screen size:

```razor
<div class="container my-4">
    <h3>Responsive Form</h3>
    
    <div class="card">
        <div class="card-body">
            <h5 class="card-title mb-4">User Registration</h5>
            
            <Form Model="@_formModel">
                <!-- Desktop layout: 2-column form -->
                <Responsive Breakpoint="Breakpoint.Medium" Mode="ResponsiveMode.Show">
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <FormItem Label="First Name" IsRequired="true">
                                <Input @bind-Value="@_formModel.FirstName" PlaceHolder="Enter first name" />
                            </FormItem>
                        </div>
                        <div class="col-md-6 mb-3">
                            <FormItem Label="Last Name" IsRequired="true">
                                <Input @bind-Value="@_formModel.LastName" PlaceHolder="Enter last name" />
                            </FormItem>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <FormItem Label="Email" IsRequired="true">
                                <Input @bind-Value="@_formModel.Email" PlaceHolder="Enter email address" />
                            </FormItem>
                        </div>
                        <div class="col-md-6 mb-3">
                            <FormItem Label="Phone Number">
                                <Input @bind-Value="@_formModel.Phone" PlaceHolder="Enter phone number" />
                            </FormItem>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12 mb-3">
                            <FormItem Label="Address">
                                <Textarea @bind-Value="@_formModel.Address" PlaceHolder="Enter address" Rows="3" />
                            </FormItem>
                        </div>
                    </div>
                    
                    <div class="d-flex justify-content-end">
                        <Button Color="Color.Secondary" class="me-2">Cancel</Button>
                        <Button Color="Color.Primary" Type="ButtonType.Submit">Register</Button>
                    </div>
                </Responsive>
                
                <!-- Mobile layout: 1-column stacked form -->
                <Responsive Breakpoint="Breakpoint.Medium" Mode="ResponsiveMode.Hide">
                    <FormItem Label="First Name" IsRequired="true">
                        <Input @bind-Value="@_formModel.FirstName" PlaceHolder="Enter first name" />
                    </FormItem>
                    
                    <FormItem Label="Last Name" IsRequired="true">
                        <Input @bind-Value="@_formModel.LastName" PlaceHolder="Enter last name" />
                    </FormItem>
                    
                    <FormItem Label="Email" IsRequired="true">
                        <Input @bind-Value="@_formModel.Email" PlaceHolder="Enter email address" />
                    </FormItem>
                    
                    <FormItem Label="Phone Number">
                        <Input @bind-Value="@_formModel.Phone" PlaceHolder="Enter phone number" />
                    </FormItem>
                    
                    <FormItem Label="Address">
                        <Textarea @bind-Value="@_formModel.Address" PlaceHolder="Enter address" Rows="3" />
                    </FormItem>
                    
                    <div class="d-grid gap-2">
                        <Button Color="Color.Primary" Type="ButtonType.Submit">Register</Button>
                        <Button Color="Color.Secondary">Cancel</Button>
                    </div>
                </Responsive>
            </Form>
        </div>
    </div>
</div>

@code {
    private UserRegistration _formModel = new();
    
    public class UserRegistration
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
```

### Example 7: Responsive Image Gallery

Creating a responsive image gallery that adapts to different screen sizes:

```razor
<div class="container my-4">
    <h3>Responsive Image Gallery</h3>
    
    <!-- Desktop: 4 images per row -->
    <Responsive Breakpoint="Breakpoint.Large" Mode="ResponsiveMode.Show">
        <div class="row">
            @foreach (var image in _images)
            {
                <div class="col-md-3 mb-4">
                    <GalleryItem Image="@image" OnClick="@(() => ShowImageDetails(image))" />
                </div>
            }
        </div>
    </Responsive>
    
    <!-- Tablet: 3 images per row -->
    <Responsive Breakpoint="Breakpoint.Medium" Mode="ResponsiveMode.Show"
               Breakpoint2="Breakpoint.Large" Mode2="ResponsiveMode.Hide">
        <div class="row">
            @foreach (var image in _images)
            {
                <div class="col-md-4 mb-4">
                    <GalleryItem Image="@image" OnClick="@(() => ShowImageDetails(image))" />
                </div>
            }
        </div>
    </Responsive>
    
    <!-- Mobile: 2 images per row -->
    <Responsive Breakpoint="Breakpoint.Small" Mode="ResponsiveMode.Show"
               Breakpoint2="Breakpoint.Medium" Mode2="ResponsiveMode.Hide">
        <div class="row">
            @foreach (var image in _images)
            {
                <div class="col-6 mb-4">
                    <GalleryItem Image="@image" OnClick="@(() => ShowImageDetails(image))" />
                </div>
            }
        </div>
    </Responsive>
    
    <!-- Extra Small: 1 image per row -->
    <Responsive Breakpoint="Breakpoint.Small" Mode="ResponsiveMode.Hide">
        <div class="row">
            @foreach (var image in _images)
            {
                <div class="col-12 mb-4">
                    <GalleryItem Image="@image" OnClick="@(() => ShowImageDetails(image))" IsCompact="true" />
                </div>
            }
        </div>
    </Responsive>
    
    <!-- Image Detail Modal -->
    <Modal @ref="_imageModal" Title="@_selectedImage?.Title" Size="Size.Large">
        <BodyTemplate>
            <div class="text-center">
                <img src="@_selectedImage?.Url" class="img-fluid" alt="@_selectedImage?.Title" />
            </div>
            <div class="mt-3">
                <p>@_selectedImage?.Description</p>
                <div class="d-flex justify-content-between">
                    <span><i class="fa-solid fa-calendar me-2"></i>@_selectedImage?.Date.ToString("MMMM d, yyyy")</span>
                    <span><i class="fa-solid fa-camera me-2"></i>@_selectedImage?.Author</span>
                </div>
            </div>
        </BodyTemplate>
        <FooterTemplate>
            <Button Color="Color.Secondary" OnClick="@(() => _imageModal.Close())">Close</Button>
        </FooterTemplate>
    </Modal>
</div>

@code {
    private List<GalleryImage> _images = new();
    private Modal _imageModal;
    private GalleryImage _selectedImage;
    
    protected override void OnInitialized()
    {
        // Initialize with sample images
        _images = new List<GalleryImage>
        {
            new GalleryImage
            {
                Id = 1,
                Title = "Mountain Landscape",
                Description = "Beautiful mountain landscape at sunset.",
                Url = "/images/gallery/landscape1.jpg",
                Thumbnail = "/images/gallery/thumbnails/landscape1.jpg",
                Date = new DateTime(2023, 5, 15),
                Author = "John Smith"
            },
            // Add more images...
        };
    }
    
    private void ShowImageDetails(GalleryImage image)
    {
        _selectedImage = image;
        _imageModal.Show();
    }
    
    public class GalleryImage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}

@* Gallery Item Component *@
@code {
    public class GalleryItemBase : ComponentBase
    {
        [Parameter] public GalleryImage Image { get; set; }
        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }
        [Parameter] public bool IsCompact { get; set; }
    }
}
```

## CSS Customization

The Responsive component itself doesn't apply specific styling to the rendered content, allowing for complete customization through your own CSS. However, you can leverage Bootstrap's responsive utility classes in conjunction with this component:

- Use Bootstrap's grid system (`col-*`, `col-sm-*`, etc.) within responsive templates
- Apply responsive spacing utilities (`m-*`, `p-*`, etc.) for consistent spacing
- Utilize responsive display utilities (`d-none`, `d-md-block`, etc.) for additional control
- Combine with responsive text utilities (`text-*-center`, etc.) for adaptive typography

## JavaScript Interop

The Responsive component uses JavaScript interop to detect viewport size, device characteristics, and orientation changes. It interacts with the browser's window resize events and media query API to determine when to show or hide content. The component handles the following JavaScript operations:

- Window resize event listening and debouncing
- Media query evaluation for breakpoint detection
- Device orientation change detection
- User agent parsing for device type inference (when applicable)

## Accessibility Considerations

When using the Responsive component, consider the following accessibility best practices:

1. Ensure that all content is accessible regardless of which responsive variant is shown
2. Avoid hiding critical functionality or content on certain screen sizes
3. Maintain proper heading hierarchy across different responsive layouts
4. Test keyboard navigation in all responsive variants
5. Consider using ARIA attributes to improve screen reader announcements for dynamic content

## Browser Compatibility

The Responsive component is compatible with all modern browsers that support Blazor WebAssembly or Blazor Server. The component relies on standard browser APIs for detecting viewport size and device characteristics, which are well-supported across modern browsers.

For older browsers with limited support for modern CSS features, consider implementing fallback styles or simplified layouts.

## Integration with Other Components

The Responsive component works well with:

- **Layout Components**: For creating adaptive page layouts
- **Navigation Components**: For responsive menus and navigation bars
- **Table/Grid Components**: For adapting data displays to different screen sizes
- **Form Components**: For creating responsive form layouts
- **Media Components**: For responsive image and video handling

## Best Practices

1. Design for mobile-first, then enhance for larger screens
2. Use the appropriate breakpoints that align with your design system
3. Test thoroughly on various devices and screen sizes
4. Consider performance implications when using complex responsive layouts
5. Use the DeviceType property judiciously, as device detection is not always reliable
6. Combine with CSS Grid or Flexbox for more sophisticated responsive layouts
7. Set appropriate debounce intervals to balance responsiveness and performance