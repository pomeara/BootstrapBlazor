# Anchor Component

## Overview
The Anchor component in BootstrapBlazor provides a navigation mechanism that allows users to quickly jump to specific sections within a page. It creates a fixed navigation menu that highlights the current section as the user scrolls through the content. This component is particularly useful for long-form content, documentation pages, and single-page applications where in-page navigation enhances user experience.

## Features
- Automatic generation of navigation links from page content
- Smooth scrolling to target sections
- Active state indication for current section
- Customizable positioning (left, right)
- Sticky navigation that remains visible while scrolling
- Support for nested navigation items
- Custom templates for anchor items
- Offset configuration to account for fixed headers
- Responsive design
- Accessibility support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Target` | `string` | `null` | CSS selector for the container with sections to generate anchors from |
| `Items` | `IEnumerable<AnchorItem>` | `null` | Collection of anchor items to display |
| `Placement` | `Placement` | `Placement.Right` | Position of the anchor navigation (Left, Right) |
| `Boundary` | `int` | `5` | Boundary value in pixels to determine when an item becomes active |
| `Offset` | `int` | `0` | Offset value in pixels to adjust scroll position (useful for fixed headers) |
| `SmoothScroll` | `bool` | `true` | Whether to use smooth scrolling animation |
| `ShowInk` | `bool` | `true` | Whether to show the ink indicator for the active item |
| `AfterNavigate` | `Func<string, Task>` | `null` | Callback function that executes after navigation to a section |
| `ItemTemplate` | `RenderFragment<AnchorItem>` | `null` | Custom template for rendering anchor items |

## AnchorItem Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Text` | `string` | `null` | Display text for the anchor item |
| `Href` | `string` | `null` | Target ID or URL for the anchor link |
| `Icon` | `string` | `null` | Icon class for the anchor item |
| `Children` | `List<AnchorItem>` | `null` | Child anchor items for nested navigation |
| `IsActive` | `bool` | `false` | Whether the anchor item is currently active |

## Events

| Event | Description |
| --- | --- |
| `OnAnchorClick` | Triggered when an anchor item is clicked |
| `OnActiveItemChanged` | Triggered when the active anchor item changes due to scrolling |

## Usage Examples

### Example 1: Basic Anchor Navigation
```csharp
<div class="row">
    <div class="col-md-9">
        <div id="anchorContainer">
            <h2 id="section1">Section 1</h2>
            <p>This is the content for section 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="section2">Section 2</h2>
            <p>This is the content for section 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="section3">Section 3</h2>
            <p>This is the content for section 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="section4">Section 4</h2>
            <p>This is the content for section 4. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
        </div>
    </div>
    <div class="col-md-3">
        <Anchor Target="#anchorContainer">
            <Items>
                <AnchorItem Text="Section 1" Href="#section1" />
                <AnchorItem Text="Section 2" Href="#section2" />
                <AnchorItem Text="Section 3" Href="#section3" />
                <AnchorItem Text="Section 4" Href="#section4" />
            </Items>
        </Anchor>
    </div>
</div>
```
This example shows a basic anchor navigation setup with four sections. The Anchor component is placed in a sidebar and links to each section in the main content area.

### Example 2: Auto-Generated Anchors
```csharp
<div class="row">
    <div class="col-md-9">
        <div id="autoAnchorContainer">
            <h2 id="introduction">Introduction</h2>
            <p>This is the introduction section. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="getting-started">Getting Started</h2>
            <p>This is the getting started section. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="features">Features</h2>
            <p>This is the features section. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="api-reference">API Reference</h2>
            <p>This is the API reference section. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="examples">Examples</h2>
            <p>This is the examples section. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
        </div>
    </div>
    <div class="col-md-3">
        <Anchor Target="#autoAnchorContainer" />
    </div>
</div>
```
This example demonstrates auto-generated anchors. The Anchor component automatically scans the target container for heading elements and creates navigation links based on them.

### Example 3: Nested Anchor Navigation
```csharp
<div class="row">
    <div class="col-md-9">
        <div id="nestedAnchorContainer">
            <h2 id="chapter1">Chapter 1: Introduction</h2>
            <p>This is the introduction chapter. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            
            <h3 id="chapter1-1">1.1 Background</h3>
            <p>Background information for Chapter 1. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            
            <h3 id="chapter1-2">1.2 Objectives</h3>
            <p>Objectives for Chapter 1. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            
            <h2 id="chapter2">Chapter 2: Methodology</h2>
            <p>This is the methodology chapter. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            
            <h3 id="chapter2-1">2.1 Research Design</h3>
            <p>Research design for Chapter 2. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            
            <h3 id="chapter2-2">2.2 Data Collection</h3>
            <p>Data collection methods for Chapter 2. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            
            <h2 id="chapter3">Chapter 3: Results</h2>
            <p>This is the results chapter. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            
            <h3 id="chapter3-1">3.1 Findings</h3>
            <p>Findings for Chapter 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            
            <h3 id="chapter3-2">3.2 Analysis</h3>
            <p>Analysis for Chapter 3. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
        </div>
    </div>
    <div class="col-md-3">
        <Anchor Target="#nestedAnchorContainer">
            <Items>
                <AnchorItem Text="Chapter 1: Introduction" Href="#chapter1">
                    <Children>
                        <AnchorItem Text="1.1 Background" Href="#chapter1-1" />
                        <AnchorItem Text="1.2 Objectives" Href="#chapter1-2" />
                    </Children>
                </AnchorItem>
                <AnchorItem Text="Chapter 2: Methodology" Href="#chapter2">
                    <Children>
                        <AnchorItem Text="2.1 Research Design" Href="#chapter2-1" />
                        <AnchorItem Text="2.2 Data Collection" Href="#chapter2-2" />
                    </Children>
                </AnchorItem>
                <AnchorItem Text="Chapter 3: Results" Href="#chapter3">
                    <Children>
                        <AnchorItem Text="3.1 Findings" Href="#chapter3-1" />
                        <AnchorItem Text="3.2 Analysis" Href="#chapter3-2" />
                    </Children>
                </AnchorItem>
            </Items>
        </Anchor>
    </div>
</div>
```
This example shows nested anchor navigation with parent and child items, suitable for hierarchical content like documentation or book chapters.

### Example 4: Left-Positioned Anchor with Icons
```csharp
<div class="row">
    <div class="col-md-3">
        <Anchor Target="#iconAnchorContainer" Placement="Placement.Left">
            <Items>
                <AnchorItem Text="Overview" Href="#overview" Icon="fa fa-info-circle" />
                <AnchorItem Text="Installation" Href="#installation" Icon="fa fa-download" />
                <AnchorItem Text="Configuration" Href="#configuration" Icon="fa fa-cog" />
                <AnchorItem Text="Usage" Href="#usage" Icon="fa fa-code" />
                <AnchorItem Text="Troubleshooting" Href="#troubleshooting" Icon="fa fa-wrench" />
            </Items>
        </Anchor>
    </div>
    <div class="col-md-9">
        <div id="iconAnchorContainer">
            <h2 id="overview">Overview</h2>
            <p>This is the overview section. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="installation">Installation</h2>
            <p>This is the installation section. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="configuration">Configuration</h2>
            <p>This is the configuration section. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="usage">Usage</h2>
            <p>This is the usage section. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="troubleshooting">Troubleshooting</h2>
            <p>This is the troubleshooting section. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
        </div>
    </div>
</div>
```
This example demonstrates a left-positioned anchor navigation with icons for each item, providing visual cues for different sections.

### Example 5: Anchor with Custom Offset for Fixed Header
```csharp
<div class="fixed-header">Fixed Header (60px height)</div>

<div class="row mt-5">
    <div class="col-md-9">
        <div id="offsetAnchorContainer">
            <h2 id="section-a">Section A</h2>
            <p>This is Section A. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="section-b">Section B</h2>
            <p>This is Section B. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="section-c">Section C</h2>
            <p>This is Section C. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="section-d">Section D</h2>
            <p>This is Section D. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
        </div>
    </div>
    <div class="col-md-3">
        <Anchor Target="#offsetAnchorContainer" Offset="60">
            <Items>
                <AnchorItem Text="Section A" Href="#section-a" />
                <AnchorItem Text="Section B" Href="#section-b" />
                <AnchorItem Text="Section C" Href="#section-c" />
                <AnchorItem Text="Section D" Href="#section-d" />
            </Items>
        </Anchor>
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
    
    .mt-5 {
        margin-top: 5rem;
    }
</style>
```
This example shows how to use the Offset property to account for a fixed header, ensuring that sections are properly scrolled into view without being hidden behind the header.

### Example 6: Anchor with Custom Item Template
```csharp
<div class="row">
    <div class="col-md-9">
        <div id="customTemplateContainer">
            <h2 id="product1">Product 1</h2>
            <p>This is the description for Product 1. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="product2">Product 2</h2>
            <p>This is the description for Product 2. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="product3">Product 3</h2>
            <p>This is the description for Product 3. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="product4">Product 4</h2>
            <p>This is the description for Product 4. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
        </div>
    </div>
    <div class="col-md-3">
        <Anchor Target="#customTemplateContainer">
            <ItemTemplate Context="item">
                <div class="custom-anchor-item @(item.IsActive ? "active" : "")">
                    <div class="item-number">@(item.Index + 1)</div>
                    <div class="item-content">
                        <div class="item-title">@item.Text</div>
                        <div class="item-description">Click to view details</div>
                    </div>
                </div>
            </ItemTemplate>
            <Items>
                <AnchorItem Text="Product 1" Href="#product1" />
                <AnchorItem Text="Product 2" Href="#product2" />
                <AnchorItem Text="Product 3" Href="#product3" />
                <AnchorItem Text="Product 4" Href="#product4" />
            </Items>
        </Anchor>
    </div>
</div>

<style>
    .custom-anchor-item {
        display: flex;
        align-items: center;
        padding: 10px;
        margin-bottom: 8px;
        border-radius: 4px;
        transition: all 0.3s;
        cursor: pointer;
    }
    
    .custom-anchor-item:hover {
        background-color: rgba(0, 123, 255, 0.1);
    }
    
    .custom-anchor-item.active {
        background-color: rgba(0, 123, 255, 0.2);
    }
    
    .item-number {
        width: 24px;
        height: 24px;
        border-radius: 50%;
        background-color: #e9ecef;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-right: 12px;
        font-weight: bold;
    }
    
    .custom-anchor-item.active .item-number {
        background-color: #007bff;
        color: white;
    }
    
    .item-title {
        font-weight: bold;
    }
    
    .item-description {
        font-size: 0.75rem;
        color: #6c757d;
    }
</style>
```
This example demonstrates how to use a custom template for anchor items, creating a more elaborate design with item numbers and descriptions.

### Example 7: Programmatic Control with AfterNavigate
```csharp
<div class="row">
    <div class="col-md-3">
        <div class="mb-3">
            <Button Color="Color.Primary" OnClick="() => NavigateToSection('quick-start')">Go to Quick Start</Button>
        </div>
        <div class="mb-3">
            <Button Color="Color.Secondary" OnClick="() => NavigateToSection('components')">Go to Components</Button>
        </div>
        <Anchor @ref="anchorComponent" Target="#programmaticContainer" AfterNavigate="OnAfterNavigate">
            <Items>
                <AnchorItem Text="Introduction" Href="#introduction-section" />
                <AnchorItem Text="Quick Start" Href="#quick-start" />
                <AnchorItem Text="Components" Href="#components" />
                <AnchorItem Text="Customization" Href="#customization" />
                <AnchorItem Text="FAQ" Href="#faq" />
            </Items>
        </Anchor>
    </div>
    <div class="col-md-9">
        <div id="programmaticContainer">
            <h2 id="introduction-section">Introduction</h2>
            <p>This is the introduction section. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="quick-start">Quick Start</h2>
            <p>This is the quick start section. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="components">Components</h2>
            <p>This is the components section. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="customization">Customization</h2>
            <p>This is the customization section. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
            
            <h2 id="faq">FAQ</h2>
            <p>This is the FAQ section. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.</p>
            <p>Additional paragraph with more content to make the section longer.</p>
        </div>
    </div>
</div>

<Toast @ref="toast" />

@code {
    private Anchor anchorComponent;
    private Toast toast;
    
    private void NavigateToSection(string sectionId)
    {
        // Programmatically navigate to a section
        anchorComponent.NavigateTo($"#{sectionId}");
    }
    
    private async Task OnAfterNavigate(string href)
    {
        // Show a toast notification after navigation
        string sectionName = href.TrimStart('#');
        await toast.Show($"Navigated to {sectionName}", $"You are now viewing the {sectionName} section.");
    }
}
```
This example shows how to programmatically control the Anchor component, including navigating to specific sections via buttons and responding to navigation events with the AfterNavigate callback.

## CSS Customization

The Anchor component can be customized using the following CSS variables:

```css
--bb-anchor-ink-ball-width: 8px;
--bb-anchor-ink-ball-height: 8px;
--bb-anchor-ink-ball-color: #1890ff;
--bb-anchor-link-top: 8px;
--bb-anchor-link-left: 16px;
--bb-anchor-link-padding: 4px 0 4px 16px;
--bb-anchor-link-margin: 4px 0;
--bb-anchor-link-color: #595959;
--bb-anchor-link-hover-color: #1890ff;
--bb-anchor-link-active-color: #1890ff;
--bb-anchor-link-font-size: 14px;
--bb-anchor-link-line-height: 1.5;
--bb-anchor-link-border-radius: 4px;
--bb-anchor-link-hover-bg: rgba(0, 0, 0, 0.06);
--bb-anchor-link-active-bg: rgba(0, 0, 0, 0.06);
--bb-anchor-ink-top: 10px;
--bb-anchor-ink-left: 0;
--bb-anchor-ink-height: 100%;
--bb-anchor-ink-width: 2px;
--bb-anchor-ink-color: #e8e8e8;
```

## Service Integration

The Anchor component can be integrated with the `AnchorService` for more advanced scenarios:

```csharp
@inject AnchorService AnchorService

<div class="row">
    <div class="col-md-9">
        <div id="serviceAnchorContainer">
            <!-- Content sections -->
        </div>
    </div>
    <div class="col-md-3">
        <Anchor Target="#serviceAnchorContainer" />
    </div>
</div>

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
        
        // You could update other components or state based on the active anchor
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

1. **Accessibility**: The Anchor component includes ARIA attributes for better accessibility. It uses appropriate roles and attributes to ensure screen readers can properly interpret the navigation.

2. **Performance**: For pages with many sections, consider using virtualization or lazy loading techniques to improve performance, especially when auto-generating anchors.

3. **URL Synchronization**: Consider synchronizing the anchor navigation with the URL hash to allow direct linking to specific sections and to support browser history navigation.

4. **Mobile Considerations**: On mobile devices, the anchor navigation may need to be collapsed or displayed differently to conserve screen space. Consider implementing a responsive design that adapts to different screen sizes.

5. **Integration with Scrollspy**: The Anchor component essentially implements a scrollspy behavior, automatically updating the active item based on scroll position. This can be integrated with other scrolling libraries or components for enhanced functionality.