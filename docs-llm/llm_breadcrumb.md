# Breadcrumb Component

## Overview
The Breadcrumb component in BootstrapBlazor provides a navigation aid that helps users understand their current location within a website or application hierarchy. It displays a trail of links showing the path from the home page to the current page, allowing users to easily navigate back to previous levels in the hierarchy.

## Features
- Automatic generation from route data
- Custom item templates
- Icon support
- Separator customization
- Active state indication
- Dynamic breadcrumb generation
- Responsive design
- Accessibility support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<BreadcrumbItem>` | `null` | Collection of breadcrumb items to display |
| `Separator` | `string` | `/` | Character or HTML used as separator between items |
| `SeparatorTemplate` | `RenderFragment` | `null` | Custom template for the separator |
| `ItemTemplate` | `RenderFragment<BreadcrumbItem>` | `null` | Custom template for rendering each breadcrumb item |
| `ActiveItemTemplate` | `RenderFragment<BreadcrumbItem>` | `null` | Custom template for rendering the active (current) breadcrumb item |
| `UseRouter` | `bool` | `false` | When true, automatically generates breadcrumbs from router data |
| `IgnoreAttributes` | `IEnumerable<string>` | `null` | Route attributes to ignore when generating breadcrumbs |
| `MaxCount` | `int` | `0` | Maximum number of items to display (0 means no limit) |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when a breadcrumb item is clicked |

## Usage Examples

### Example 1: Basic Breadcrumb
```html
<Breadcrumb>
    <Items>
        <BreadcrumbItem Text="Home" Url="/" />
        <BreadcrumbItem Text="Products" Url="/products" />
        <BreadcrumbItem Text="Laptops" Url="/products/laptops" />
        <BreadcrumbItem Text="Gaming Laptops" Active="true" />
    </Items>
</Breadcrumb>
```
This example shows a basic breadcrumb trail with four levels, where the last item is marked as active (current page).

### Example 2: Breadcrumb with Icons
```html
<Breadcrumb>
    <Items>
        <BreadcrumbItem Text="Home" Url="/" Icon="fa fa-home" />
        <BreadcrumbItem Text="Library" Url="/library" Icon="fa fa-book" />
        <BreadcrumbItem Text="Data" Active="true" Icon="fa fa-database" />
    </Items>
</Breadcrumb>
```
This example demonstrates a breadcrumb with Font Awesome icons for each item, enhancing visual recognition.

### Example 3: Custom Separator
```html
<Breadcrumb Separator=">">
    <Items>
        <BreadcrumbItem Text="Dashboard" Url="/dashboard" />
        <BreadcrumbItem Text="Reports" Url="/dashboard/reports" />
        <BreadcrumbItem Text="Annual" Active="true" />
    </Items>
</Breadcrumb>
```
This example shows a breadcrumb with a custom separator character (>) instead of the default slash (/).

### Example 4: Custom Separator Template
```html
<Breadcrumb>
    <SeparatorTemplate>
        <i class="fa fa-angle-right mx-2"></i>
    </SeparatorTemplate>
    <Items>
        <BreadcrumbItem Text="Home" Url="/" />
        <BreadcrumbItem Text="Products" Url="/products" />
        <BreadcrumbItem Text="Electronics" Active="true" />
    </Items>
</Breadcrumb>
```
This example demonstrates a breadcrumb with a custom separator template using a Font Awesome icon.

### Example 5: Custom Item Templates
```html
<Breadcrumb>
    <ItemTemplate Context="item">
        <a href="@item.Url" class="custom-breadcrumb-link">
            @if (!string.IsNullOrEmpty(item.Icon))
            {
                <i class="@item.Icon mr-1"></i>
            }
            <span>@item.Text</span>
        </a>
    </ItemTemplate>
    <ActiveItemTemplate Context="item">
        <span class="custom-breadcrumb-active">
            @if (!string.IsNullOrEmpty(item.Icon))
            {
                <i class="@item.Icon mr-1"></i>
            }
            <strong>@item.Text</strong>
        </span>
    </ActiveItemTemplate>
    <Items>
        <BreadcrumbItem Text="Home" Url="/" Icon="fa fa-home" />
        <BreadcrumbItem Text="Settings" Url="/settings" Icon="fa fa-cog" />
        <BreadcrumbItem Text="Profile" Active="true" Icon="fa fa-user" />
    </Items>
</Breadcrumb>
```
This example shows how to use custom templates for both regular and active breadcrumb items, allowing for complete control over the rendering.

### Example 6: Dynamically Generated Breadcrumb
```csharp
<Breadcrumb Items="@breadcrumbItems" />

@code {
    private List<BreadcrumbItem> breadcrumbItems;
    
    protected override void OnInitialized()
    {
        breadcrumbItems = new List<BreadcrumbItem>
        {
            new BreadcrumbItem { Text = "Home", Url = "/", Icon = "fa fa-home" },
            new BreadcrumbItem { Text = "Products", Url = "/products" }
        };
        
        // Add dynamic items based on some condition or data
        if (SomeCondition)
        {
            breadcrumbItems.Add(new BreadcrumbItem { Text = "Category", Url = $"/products/{CategoryId}" });
        }
        
        // Add the current page as active
        breadcrumbItems.Add(new BreadcrumbItem { Text = CurrentPageTitle, Active = true });
    }
}
```
This example demonstrates how to dynamically generate breadcrumb items based on application state or data.

### Example 7: Router-Based Breadcrumb
```html
<Router AppAssembly="@typeof(Program).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
    </Found>
    <NotFound>
        <LayoutView Layout="@typeof(MainLayout)">
            <p>Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>

<!-- In MainLayout.razor -->
<div class="main">
    <div class="top-row px-4">
        <Breadcrumb UseRouter="true" />
    </div>
    <div class="content px-4">
        @Body
    </div>
</div>
```
This example shows how to use the `UseRouter` property to automatically generate breadcrumbs based on the current route, which requires proper route attributes on your pages.

## CSS Customization

The Breadcrumb component can be customized using the following CSS variables:

```css
--bb-breadcrumb-padding-y: 0.75rem;
--bb-breadcrumb-padding-x: 1rem;
--bb-breadcrumb-item-padding: 0.5rem;
--bb-breadcrumb-margin-bottom: 1rem;
--bb-breadcrumb-bg: #e9ecef;
--bb-breadcrumb-border-radius: 0.25rem;
--bb-breadcrumb-divider-color: #6c757d;
--bb-breadcrumb-active-color: #6c757d;
--bb-breadcrumb-divider: "/";
```

## Notes

1. **Accessibility**: The Breadcrumb component includes ARIA attributes for better accessibility. It uses `nav` element with `aria-label="breadcrumb"` and the current page is marked with `aria-current="page"`.

2. **Router Integration**: When using `UseRouter="true"`, ensure your pages have appropriate route attributes and titles defined. You may need to use the `[BreadcrumbNavigation]` attribute on your pages.

3. **Responsive Design**: For mobile devices with limited screen space, consider using the `MaxCount` property to limit the number of visible breadcrumb items, or implement a responsive design that collapses earlier items into a dropdown on small screens.

4. **SEO Considerations**: Breadcrumbs can improve SEO by providing structured data about your site hierarchy. Consider adding appropriate schema.org markup for breadcrumbs.

5. **Navigation Logic**: When a user clicks a breadcrumb item, they expect to navigate to that level in the hierarchy. Ensure your navigation logic preserves any necessary state or context when moving between levels.