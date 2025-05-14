# AnchorLink Component

## Overview
The AnchorLink component in BootstrapBlazor provides a simple way to create links that navigate to specific sections within a page. Unlike the Anchor component which creates a navigation menu, AnchorLink is a standalone link that, when clicked, smoothly scrolls to a target element on the page. This component is useful for creating table of contents, back-to-top buttons, and other in-page navigation elements.

## Features
- Smooth scrolling to target elements
- Customizable text and icon
- Support for custom content
- Configurable offset to account for fixed headers
- Customizable scroll behavior
- Event handling for navigation
- Accessibility support
- Integration with Anchor component

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Target` | `string` | `null` | CSS selector or ID of the target element to scroll to |
| `Text` | `string` | `null` | Display text for the link |
| `Icon` | `string` | `null` | Icon class for the link |
| `Offset` | `int` | `0` | Offset value in pixels to adjust scroll position (useful for fixed headers) |
| `SmoothScroll` | `bool` | `true` | Whether to use smooth scrolling animation |
| `IsBlock` | `bool` | `false` | Whether the link should be displayed as a block element |
| `ChildContent` | `RenderFragment` | `null` | Custom content for the link |
| `AfterNavigate` | `Func<string, Task>` | `null` | Callback function that executes after navigation to the target |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the link is clicked |

## Usage Examples

### Example 1: Basic AnchorLink
```csharp
<div>
    <h2 id="section1">Section 1</h2>
    <p>This is the content for section 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="section2">Section 2</h2>
    <p>This is the content for section 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="section3">Section 3</h2>
    <p>This is the content for section 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <div class="mt-4">
        <h3>Table of Contents</h3>
        <ul>
            <li><AnchorLink Target="#section1" Text="Go to Section 1" /></li>
            <li><AnchorLink Target="#section2" Text="Go to Section 2" /></li>
            <li><AnchorLink Target="#section3" Text="Go to Section 3" /></li>
        </ul>
    </div>
</div>
```
This example shows basic usage of AnchorLink components to create a table of contents that links to different sections on the page.

### Example 2: AnchorLink with Icons
```csharp
<div>
    <h2 id="overview">Overview</h2>
    <p>This is the overview section. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="features">Features</h2>
    <p>This is the features section. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="installation">Installation</h2>
    <p>This is the installation section. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="usage">Usage</h2>
    <p>This is the usage section. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <div class="mt-4">
        <div class="d-flex flex-wrap gap-3">
            <AnchorLink Target="#overview" Text="Overview" Icon="fa fa-info-circle" />
            <AnchorLink Target="#features" Text="Features" Icon="fa fa-list" />
            <AnchorLink Target="#installation" Text="Installation" Icon="fa fa-download" />
            <AnchorLink Target="#usage" Text="Usage" Icon="fa fa-code" />
        </div>
    </div>
</div>
```
This example demonstrates AnchorLink components with icons, displayed in a horizontal layout.

### Example 3: Back to Top Button
```csharp
<div>
    <h2 id="section1">Section 1</h2>
    <p>This is the content for section 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="section2">Section 2</h2>
    <p>This is the content for section 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="section3">Section 3</h2>
    <p>This is the content for section 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <div class="back-to-top">
        <AnchorLink Target="body" Icon="fa fa-arrow-up">
            <span>Back to Top</span>
        </AnchorLink>
    </div>
</div>

<style>
    .back-to-top {
        position: fixed;
        bottom: 20px;
        right: 20px;
        z-index: 100;
    }
    
    .back-to-top a {
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        width: 50px;
        height: 50px;
        background-color: #007bff;
        color: white;
        border-radius: 50%;
        text-decoration: none;
        box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
        transition: background-color 0.3s;
    }
    
    .back-to-top a:hover {
        background-color: #0056b3;
    }
    
    .back-to-top span {
        font-size: 0.7rem;
        margin-top: 2px;
    }
</style>
```
This example shows how to create a fixed "Back to Top" button using the AnchorLink component, which scrolls the page back to the top when clicked.

### Example 4: AnchorLink with Offset for Fixed Header
```csharp
<div class="fixed-header">Fixed Header (60px height)</div>

<div class="content-container">
    <div class="toc">
        <h3>Table of Contents</h3>
        <ul>
            <li><AnchorLink Target="#heading1" Text="Heading 1" Offset="70" /></li>
            <li><AnchorLink Target="#heading2" Text="Heading 2" Offset="70" /></li>
            <li><AnchorLink Target="#heading3" Text="Heading 3" Offset="70" /></li>
        </ul>
    </div>
    
    <div class="content">
        <h2 id="heading1">Heading 1</h2>
        <p>This is the content for heading 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
        <p>Additional paragraph with more content to make the section longer.</p>
        
        <h2 id="heading2">Heading 2</h2>
        <p>This is the content for heading 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
        <p>Additional paragraph with more content to make the section longer.</p>
        
        <h2 id="heading3">Heading 3</h2>
        <p>This is the content for heading 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
        <p>Additional paragraph with more content to make the section longer.</p>
    </div>
</div>

<style>
    .fixed-header {
        position: fixed;
        top: 0;
        left: 0;
        right: 0;
        height: 60px;
        background-color: #343a40;
        color: white;
        display: flex;
        align-items: center;
        justify-content: center;
        z-index: 1000;
    }
    
    .content-container {
        display: flex;
        margin-top: 70px;
    }
    
    .toc {
        width: 200px;
        padding: 10px;
        position: sticky;
        top: 70px;
        align-self: flex-start;
    }
    
    .content {
        flex: 1;
        padding: 10px;
    }
</style>
```
This example demonstrates how to use the Offset property to account for a fixed header, ensuring that sections are properly scrolled into view without being hidden behind the header.

### Example 5: AnchorLink with Custom Content
```csharp
<div>
    <h2 id="product1">Product 1</h2>
    <p>This is the description for Product 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="product2">Product 2</h2>
    <p>This is the description for Product 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="product3">Product 3</h2>
    <p>This is the description for Product 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <div class="product-navigation">
        <AnchorLink Target="#product1" IsBlock="true">
            <div class="product-card">
                <img src="/images/product1.jpg" alt="Product 1" />
                <div class="product-info">
                    <h4>Product 1</h4>
                    <p>Premium quality widget</p>
                </div>
            </div>
        </AnchorLink>
        
        <AnchorLink Target="#product2" IsBlock="true">
            <div class="product-card">
                <img src="/images/product2.jpg" alt="Product 2" />
                <div class="product-info">
                    <h4>Product 2</h4>
                    <p>Professional grade gadget</p>
                </div>
            </div>
        </AnchorLink>
        
        <AnchorLink Target="#product3" IsBlock="true">
            <div class="product-card">
                <img src="/images/product3.jpg" alt="Product 3" />
                <div class="product-info">
                    <h4>Product 3</h4>
                    <p>Ultimate solution package</p>
                </div>
            </div>
        </AnchorLink>
    </div>
</div>

<style>
    .product-navigation {
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
        margin: 30px 0;
    }
    
    .product-card {
        width: 200px;
        border: 1px solid #dee2e6;
        border-radius: 4px;
        overflow: hidden;
        transition: transform 0.3s, box-shadow 0.3s;
    }
    
    .product-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 5px 15px rgba(0, 0, 0, 0.1);
    }
    
    .product-card img {
        width: 100%;
        height: 120px;
        object-fit: cover;
    }
    
    .product-info {
        padding: 10px;
    }
    
    .product-info h4 {
        margin: 0 0 5px 0;
    }
    
    .product-info p {
        margin: 0;
        font-size: 0.875rem;
        color: #6c757d;
    }
</style>
```
This example shows how to use AnchorLink with custom content to create product cards that link to product sections on the page.

### Example 6: AnchorLink with Event Handling
```csharp
<div>
    <h2 id="section1">Section 1</h2>
    <p>This is the content for section 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="section2">Section 2</h2>
    <p>This is the content for section 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <h2 id="section3">Section 3</h2>
    <p>This is the content for section 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
    <p>Additional paragraph with more content to make the section longer.</p>
    
    <div class="mt-4">
        <h3>Navigation</h3>
        <div class="d-flex flex-wrap gap-3">
            <AnchorLink Target="#section1" Text="Section 1" OnClick="() => LogNavigation('section1')" AfterNavigate="OnAfterNavigate" />
            <AnchorLink Target="#section2" Text="Section 2" OnClick="() => LogNavigation('section2')" AfterNavigate="OnAfterNavigate" />
            <AnchorLink Target="#section3" Text="Section 3" OnClick="() => LogNavigation('section3')" AfterNavigate="OnAfterNavigate" />
        </div>
    </div>
</div>

<Toast @ref="toast" />

@code {
    private Toast toast;
    
    private void LogNavigation(string section)
    {
        Console.WriteLine($"Navigating to {section}");
        // You could track analytics here
    }
    
    private async Task OnAfterNavigate(string target)
    {
        string sectionName = target.TrimStart('#');
        await toast.Show("Navigation", $"You've navigated to {sectionName}");
    }
}
```
This example demonstrates how to handle events with AnchorLink, including the OnClick event and the AfterNavigate callback, which can be used for analytics tracking or showing notifications.

### Example 7: AnchorLink Integration with Anchor Component
```csharp
<div class="row">
    <div class="col-md-3">
        <Anchor Target="#integratedContainer">
            <Items>
                <AnchorItem Text="Introduction" Href="#intro" />
                <AnchorItem Text="Getting Started" Href="#getting-started" />
                <AnchorItem Text="Features" Href="#features-section" />
                <AnchorItem Text="Examples" Href="#examples" />
            </Items>
        </Anchor>
    </div>
    <div class="col-md-9">
        <div id="integratedContainer">
            <h2 id="intro">Introduction</h2>
            <p>This is the introduction section. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            <AnchorLink Target="#getting-started" Text="Skip to Getting Started" Icon="fa fa-arrow-down" />
            
            <h2 id="getting-started">Getting Started</h2>
            <p>This is the getting started section. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            <AnchorLink Target="#features-section" Text="Continue to Features" Icon="fa fa-arrow-down" />
            
            <h2 id="features-section">Features</h2>
            <p>This is the features section. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            <AnchorLink Target="#examples" Text="Skip to Examples" Icon="fa fa-arrow-down" />
            
            <h2 id="examples">Examples</h2>
            <p>This is the examples section. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            <AnchorLink Target="#intro" Text="Back to Introduction" Icon="fa fa-arrow-up" />
        </div>
    </div>
</div>
```
This example shows how to integrate AnchorLink with the Anchor component, creating a comprehensive navigation system with both a sidebar navigation menu and inline links for moving between sections.

## CSS Customization

The AnchorLink component can be customized using the following CSS variables:

```css
--bb-anchor-link-color: #007bff;
--bb-anchor-link-hover-color: #0056b3;
--bb-anchor-link-active-color: #0056b3;
--bb-anchor-link-text-decoration: none;
--bb-anchor-link-hover-text-decoration: underline;
--bb-anchor-link-font-weight: normal;
--bb-anchor-link-hover-font-weight: normal;
--bb-anchor-link-transition: color 0.15s ease-in-out;
--bb-anchor-link-icon-margin: 0 0.25rem 0 0;
--bb-anchor-link-block-padding: 0.5rem 1rem;
--bb-anchor-link-block-margin: 0;
--bb-anchor-link-block-border-radius: 0.25rem;
--bb-anchor-link-block-background: transparent;
--bb-anchor-link-block-hover-background: rgba(0, 123, 255, 0.1);
```

## Service Integration

The AnchorLink component can be integrated with the `AnchorService` for more advanced scenarios:

```csharp
@inject AnchorService AnchorService

<AnchorLink Target="#section1" Text="Go to Section 1" OnClick="() => TrackNavigation('section1')" />

@code {
    protected override void OnInitialized()
    {
        // Subscribe to anchor events
        AnchorService.OnAnchorChanged += HandleAnchorChanged;
    }
    
    private void HandleAnchorChanged(string href)
    {
        // Respond to anchor changes
        Console.WriteLine($"Active anchor changed to: {href}");
    }
    
    private void TrackNavigation(string section)
    {
        // Notify the service about navigation
        AnchorService.SetActiveAnchor($"#{section}");
    }
    
    public void Dispose()
    {
        // Unsubscribe from events
        AnchorService.OnAnchorChanged -= HandleAnchorChanged;
    }
}
```

To use the `AnchorService`, you need to register it in your application's service collection:

```csharp
builder.Services.AddBootstrapBlazor();
// or specifically
builder.Services.AddSingleton<AnchorService>();
```

## Notes

1. **Accessibility**: The AnchorLink component includes ARIA attributes for better accessibility. It uses appropriate roles and attributes to ensure screen readers can properly interpret the links.

2. **URL Synchronization**: Consider synchronizing the anchor navigation with the URL hash to allow direct linking to specific sections and to support browser history navigation.

3. **Mobile Considerations**: On mobile devices, ensure that anchor links are large enough to be easily tapped, and consider adding additional spacing between links to prevent accidental taps.

4. **Performance**: For pages with many AnchorLink components, consider using event delegation to handle click events more efficiently.

5. **Integration with Routing**: When using AnchorLink in a Blazor application with routing, be careful to distinguish between in-page navigation (using AnchorLink) and page-to-page navigation (using NavLink or other routing components).