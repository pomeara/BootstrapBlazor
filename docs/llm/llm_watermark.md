# Watermark Component

## Overview
The Watermark component in BootstrapBlazor provides a way to add semi-transparent text or image overlays to content areas. It's commonly used to indicate document status (like "Draft" or "Confidential"), ownership, or to prevent unauthorized copying of content while maintaining readability.

## Features
- **Text Watermarks**: Add text-based watermarks with customizable content
- **Image Watermarks**: Support for image-based watermarks
- **Customizable Appearance**: Control opacity, color, size, and rotation
- **Positioning Options**: Place watermarks in different positions or tile them across content
- **Responsive Design**: Automatically adapts to container size
- **Z-index Control**: Configure layering to ensure proper display with other content
- **Multiple Watermarks**: Apply multiple watermarks simultaneously

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Content` | `string` | `null` | Text content for the watermark |
| `ImageUrl` | `string` | `null` | URL to an image to use as watermark |
| `Width` | `int?` | `null` | Width of the watermark in pixels |
| `Height` | `int?` | `null` | Height of the watermark in pixels |
| `Opacity` | `double` | `0.15` | Opacity of the watermark (0.0 to 1.0) |
| `Rotation` | `int` | `-22` | Rotation angle in degrees |
| `FontSize` | `int` | `16` | Font size for text watermarks |
| `FontColor` | `string` | `#000000` | Font color for text watermarks |
| `FontFamily` | `string` | `inherit` | Font family for text watermarks |
| `Gap` | `int` | `100` | Gap between repeated watermarks when using tiled mode |
| `Offset` | `int[]` | `[0, 0]` | X and Y offset for positioning |
| `Position` | `WatermarkPosition` | `WatermarkPosition.Center` | Position of the watermark |
| `ZIndex` | `int` | `9` | Z-index value for layering |
| `IsTiled` | `bool` | `false` | Whether to tile the watermark across the container |
| `IsFullScreen` | `bool` | `false` | Whether to apply the watermark to the full screen |
| `TargetSelector` | `string` | `null` | CSS selector for target element(s) to apply watermark to |

## Events

| Event | Description |
| --- | --- |
| `OnWatermarkRendered` | Triggered after the watermark has been rendered |
| `OnWatermarkClicked` | Triggered when the watermark is clicked |
| `OnWatermarkVisibilityChanged` | Triggered when the watermark visibility changes |

## Usage Examples

### Example 1: Basic Text Watermark

```razor
<div class="content-container" style="position: relative; min-height: 300px;">
    <Watermark Content="DRAFT" />
    
    <div class="document-content">
        <h2>Project Proposal</h2>
        <p>This is a draft document outlining the proposed project scope and timeline.</p>
        <p>The content is still under review and subject to changes.</p>
        <!-- More content here -->
    </div>
</div>
```

### Example 2: Customized Text Watermark

```razor
<div class="content-area" style="position: relative; min-height: 400px;">
    <Watermark 
        Content="CONFIDENTIAL" 
        FontSize="24" 
        FontColor="#FF0000" 
        Opacity="0.1" 
        Rotation="-30" />
    
    <div class="sensitive-content">
        <h3>Financial Report - Q2 2023</h3>
        <p>This document contains sensitive financial information.</p>
        <table class="financial-data">
            <!-- Financial data table -->
        </table>
    </div>
</div>
```

### Example 3: Image Watermark

```razor
<div class="image-gallery" style="position: relative;">
    <Watermark 
        ImageUrl="/images/company-logo.png" 
        Width="150" 
        Height="80" 
        Opacity="0.2" 
        Rotation="0" />
    
    <div class="gallery-items">
        <img src="/images/product1.jpg" alt="Product 1" />
        <img src="/images/product2.jpg" alt="Product 2" />
        <img src="/images/product3.jpg" alt="Product 3" />
    </div>
</div>
```

### Example 4: Tiled Watermark Pattern

```razor
<div class="document-viewer" style="position: relative; min-height: 500px;">
    <Watermark 
        Content="COMPANY NAME" 
        IsTiled="true" 
        Gap="150" 
        Opacity="0.08" 
        FontSize="18" />
    
    <div class="document-content">
        <!-- Document content that spans multiple pages -->
        <h2>Annual Report</h2>
        <p>This report contains information about the company's performance over the past year.</p>
        <!-- More content here -->
    </div>
</div>
```

### Example 5: Full Screen Watermark

```razor
@page "/protected-document"

<Watermark 
    Content="INTERNAL USE ONLY" 
    IsFullScreen="true" 
    Opacity="0.1" 
    Rotation="-20" 
    ZIndex="1000" />

<div class="protected-page-content">
    <h1>Internal Documentation</h1>
    <p>This page contains internal documentation that should not be shared outside the organization.</p>
    <!-- Page content here -->
</div>
```

### Example 6: Multiple Watermarks with Different Positions

```razor
<div class="legal-document" style="position: relative; min-height: 600px;">
    <Watermark 
        Content="DRAFT" 
        Position="WatermarkPosition.Center" 
        Opacity="0.15" 
        FontSize="32" 
        Rotation="-30" />
        
    <Watermark 
        Content="Page @currentPage of @totalPages" 
        Position="WatermarkPosition.BottomRight" 
        Opacity="0.5" 
        FontSize="12" 
        Rotation="0" 
        FontColor="#666666" />
    
    <Watermark 
        ImageUrl="/images/company-logo-small.png" 
        Position="WatermarkPosition.TopLeft" 
        Width="80" 
        Height="40" 
        Opacity="0.3" 
        Rotation="0" />
    
    <div class="document-content">
        <!-- Legal document content -->
    </div>
</div>

@code {
    private int currentPage = 1;
    private int totalPages = 10;
}
```

### Example 7: Dynamic Watermark with User Information

```razor
@page "/secure-document/{DocumentId}"
@inject AuthenticationStateProvider AuthStateProvider

<div class="secure-document-container" style="position: relative; min-height: 800px;">
    <Watermark 
        Content="@watermarkText" 
        Opacity="0.12" 
        Rotation="-25" 
        FontSize="20" />
    
    <div class="document-content">
        <h2>@documentTitle</h2>
        <div class="document-metadata">
            <span>Accessed by: @userName</span>
            <span>Access Time: @accessTime</span>
            <span>Document ID: @DocumentId</span>
        </div>
        
        <!-- Document content here -->
    </div>
</div>

@code {
    [Parameter]
    public string DocumentId { get; set; }
    
    private string watermarkText;
    private string userName;
    private string documentTitle;
    private string accessTime;
    
    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        
        if (user.Identity.IsAuthenticated)
        {
            userName = user.Identity.Name;
            accessTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            
            // Fetch document details based on DocumentId
            await FetchDocumentDetails();
            
            // Create watermark with user info and timestamp
            watermarkText = $"{userName}\n{accessTime}";
        }
    }
    
    private async Task FetchDocumentDetails()
    {
        // Simulate API call to fetch document details
        await Task.Delay(500);
        documentTitle = $"Secure Document #{DocumentId}";
    }
}
```

## CSS Customization

The Watermark component can be customized using CSS variables and classes:

```css
/* Custom styling for Watermark component */
.bb-watermark {
    --bb-watermark-opacity: 0.12;
    --bb-watermark-color: rgba(0, 0, 0, var(--bb-watermark-opacity));
    --bb-watermark-font-family: 'Arial', sans-serif;
    --bb-watermark-z-index: 10;
}

.bb-watermark-text {
    font-weight: bold;
    text-transform: uppercase;
    pointer-events: none;
}

.bb-watermark-image {
    filter: grayscale(100%);
}

/* Custom watermark for specific content types */
.financial-report .bb-watermark {
    --bb-watermark-color: rgba(255, 0, 0, 0.1);
    --bb-watermark-font-family: 'Courier New', monospace;
}
```

## JavaScript Interop

The Watermark component uses JavaScript interop for advanced functionality:

```javascript
// Example of custom JS function for Watermark component
window.watermarkFunctions = {
    // Apply watermark to dynamically loaded content
    applyToNewContent: function(selector, watermarkOptions) {
        const container = document.querySelector(selector);
        if (container) {
            // Implementation details for applying watermark
        }
    },
    
    // Toggle watermark visibility
    toggleWatermark: function(visible) {
        const watermarks = document.querySelectorAll('.bb-watermark');
        watermarks.forEach(watermark => {
            watermark.style.display = visible ? 'block' : 'none';
        });
    },
    
    // Update watermark content dynamically
    updateWatermarkContent: function(selector, newContent) {
        const watermark = document.querySelector(selector + ' .bb-watermark-text');
        if (watermark) {
            watermark.textContent = newContent;
        }
    }
};
```

## Accessibility

The Watermark component is designed with accessibility in mind:

- Watermarks are non-interactive by default (pointer-events: none) to avoid interfering with content interaction
- Screen readers typically ignore watermarks as they're presentational
- Sufficient contrast is maintained between content and watermark for readability
- Watermarks can be disabled for users with specific accessibility needs

## Browser Compatibility

The Watermark component is compatible with modern browsers:

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Opera (latest)

## Integration with Other Components

The Watermark component can be integrated with other BootstrapBlazor components:

- Use with `Print` component to ensure watermarks appear in printed documents
- Combine with `Modal` or `Drawer` to add watermarks to modal content
- Integrate with `Table` to add watermarks to data tables
- Use with `Form` to mark form content as draft or pending
- Combine with `ThemeProvider` for theme-specific watermark styling