# Marquee Component

## Overview
The Marquee component in BootstrapBlazor provides a scrolling text display that moves content horizontally or vertically across the screen. It's ideal for creating attention-grabbing announcements, news tickers, promotional banners, or any content that needs to be displayed in a dynamic, scrolling format. This component offers a modern implementation of the classic HTML marquee element with enhanced customization options and directional controls.

## Features
- **Multiple Scroll Directions**: Support for left-to-right, right-to-left, top-to-bottom, and bottom-to-top scrolling
- **Customizable Speed**: Adjustable animation duration to control scrolling speed
- **Text Styling**: Options for font size, color, and background customization
- **Smooth Animation**: CSS-based animations for smooth scrolling performance
- **Responsive Design**: Adapts to container width and works across different screen sizes
- **Vertical Text Support**: Ability to display text in vertical orientation
- **Continuous Looping**: Infinite animation iteration for continuous display
- **Accessibility Support**: Proper semantic structure for screen readers
- **Custom Styling**: Extensive CSS customization options
- **Integration Flexibility**: Easy to integrate with other components and layouts

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Text` | string? | null | The text content to be displayed in the marquee. |
| `Color` | string | "#000" | The color of the text. Supports hexadecimal values and color names. |
| `BackgroundColor` | string | "#fff" | The background color of the marquee. Supports hexadecimal values and color names. |
| `FontSize` | int | 72 | The font size of the text in pixels. |
| `Duration` | int | 14 | The animation duration in seconds. Lower values result in faster scrolling. |
| `Direction` | MarqueeDirection | LeftToRight | The scrolling direction of the text. Options include LeftToRight, RightToLeft, TopToBottom, and BottomToTop. |

## Events

The Marquee component doesn't expose specific events, as it's primarily a display component. However, it supports the standard component lifecycle events and can be controlled through state changes in the parent component.

## Usage Examples

### Example 1: Basic Marquee with Default Settings

```razor
<Marquee Text="Welcome to BootstrapBlazor - A powerful UI component library for Blazor applications" />
```

This example creates a basic marquee with default settings, scrolling text from left to right.

### Example 2: Right-to-Left Scrolling Announcement

```razor
<Marquee Text="Important announcement: System maintenance scheduled for this weekend"
         Direction="MarqueeDirection.RightToLeft"
         Color="#ff0000"
         BackgroundColor="#ffffcc"
         FontSize="24"
         Duration="10" />
```

This example creates an attention-grabbing announcement with right-to-left scrolling, red text on a light yellow background, smaller font size, and faster scrolling speed.

### Example 3: Vertical Scrolling Credits

```razor
<div style="height: 300px; width: 100px;">
    <Marquee Text="Director: John Smith | Producer: Jane Doe | Writer: Bob Johnson | Music: Sarah Williams"
             Direction="MarqueeDirection.BottomToTop"
             Color="#ffffff"
             BackgroundColor="#000000"
             FontSize="18"
             Duration="20" />
</div>
```

This example creates a vertical scrolling credits display with white text on a black background, contained within a fixed-height container.

### Example 4: News Ticker with Dynamic Content

```razor
@code {
    private string newsText = "Breaking News: BootstrapBlazor releases new Marquee component | Weather: Sunny with a chance of updates | Sports: Code team wins development championship";
    
    protected override async Task OnInitializedAsync()
    {
        // Simulate updating news ticker content periodically
        _ = UpdateNewsTickerAsync();
        await base.OnInitializedAsync();
    }
    
    private async Task UpdateNewsTickerAsync()
    {
        while (true)
        {
            await Task.Delay(30000); // Update every 30 seconds
            newsText = $"Updated News: {DateTime.Now.ToShortTimeString()} | " + newsText;
            StateHasChanged();
        }
    }
}

<div class="news-ticker">
    <Marquee Text="@newsText"
             Direction="MarqueeDirection.LeftToRight"
             Color="#333333"
             BackgroundColor="#f8f9fa"
             FontSize="16"
             Duration="25" />
</div>

<style>
    .news-ticker {
        border: 1px solid #dee2e6;
        border-radius: 4px;
        padding: 8px 0;
    }
</style>
```

This example creates a news ticker with dynamically updated content, styled to look like a traditional news banner.

### Example 5: Promotional Banner with Custom Styling

```razor
<div class="promo-banner">
    <Marquee Text="🎉 Special Offer: 50% off all products this weekend! Use code BLAZOR50 at checkout 🎉"
             Direction="MarqueeDirection.LeftToRight"
             Color="#ffffff"
             BackgroundColor="linear-gradient(90deg, #4b6cb7 0%, #182848 100%)"
             FontSize="20"
             Duration="12" />
</div>

<style>
    .promo-banner {
        margin: 20px 0;
        border-radius: 8px;
        overflow: hidden;
        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
    }
    
    .promo-banner .marquee {
        padding: 10px 0;
    }
</style>
```

This example creates an eye-catching promotional banner with gradient background and custom styling.

### Example 6: Multiple Direction Marquee Showcase

```razor
<div class="marquee-showcase">
    <div class="row mb-4">
        <div class="col-12">
            <h5>Left to Right</h5>
            <Marquee Text="This text scrolls from left to right"
                     Direction="MarqueeDirection.LeftToRight"
                     FontSize="18"
                     Duration="8" />
        </div>
    </div>
    
    <div class="row mb-4">
        <div class="col-12">
            <h5>Right to Left</h5>
            <Marquee Text="This text scrolls from right to left"
                     Direction="MarqueeDirection.RightToLeft"
                     FontSize="18"
                     Duration="8" />
        </div>
    </div>
    
    <div class="row">
        <div class="col-6">
            <h5>Top to Bottom</h5>
            <div style="height: 200px;">
                <Marquee Text="This text scrolls from top to bottom"
                         Direction="MarqueeDirection.TopToBottom"
                         FontSize="18"
                         Duration="8" />
            </div>
        </div>
        
        <div class="col-6">
            <h5>Bottom to Top</h5>
            <div style="height: 200px;">
                <Marquee Text="This text scrolls from bottom to top"
                         Direction="MarqueeDirection.BottomToTop"
                         FontSize="18"
                         Duration="8" />
            </div>
        </div>
    </div>
</div>
```

This example showcases all four scrolling directions in a single view for comparison.

### Example 7: Responsive Marquee with Media Queries

```razor
<div class="responsive-marquee">
    <Marquee Text="This marquee adapts to different screen sizes with responsive font sizing and speed"
             Direction="MarqueeDirection.LeftToRight"
             FontSize="@GetFontSize()"
             Duration="@GetDuration()" />
</div>

@code {
    private int GetFontSize()
    {
        // This would typically use JavaScript interop to get actual window width
        // Simplified example for demonstration
        return IsSmallScreen() ? 16 : 32;
    }
    
    private int GetDuration()
    {
        // Adjust speed based on screen size
        return IsSmallScreen() ? 8 : 14;
    }
    
    private bool IsSmallScreen()
    {
        // This would typically use JavaScript interop to determine screen size
        // Simplified example for demonstration
        return false;
    }
}

<style>
    @media (max-width: 768px) {
        .responsive-marquee .marquee {
            font-size: 16px !important;
            animation-duration: 8s !important;
        }
    }
    
    @media (min-width: 769px) {
        .responsive-marquee .marquee {
            font-size: 32px !important;
            animation-duration: 14s !important;
        }
    }
</style>
```

This example creates a responsive marquee that adjusts font size and scrolling speed based on screen size using media queries.

## Customization Notes

### CSS Variables

The Marquee component uses CSS classes and styles that can be customized:

```css
.marquee {
    overflow: hidden;
    white-space: nowrap;
    position: relative;
}

.marquee-vertical {
    writing-mode: vertical-rl;
    text-orientation: upright;
}

.marquee-text {
    display: inline-block;
    animation-timing-function: linear;
    animation-iteration-count: infinite;
}

.marquee-text-left {
    padding-left: 100%;
}

.marquee-text-top {
    padding-top: 100%;
}
```

### Animation Keyframes

The component uses the following keyframes for animations:

```css
@keyframes RightToLeft {
    from { transform: translateX(0); }
    to { transform: translateX(-100%); }
}

@keyframes LeftToRight {
    from { transform: translateX(-100%); }
    to { transform: translateX(0); }
}

@keyframes TopToBottom {
    from { transform: translateY(-100%); }
    to { transform: translateY(0); }
}

@keyframes BottomToTop {
    from { transform: translateY(0); }
    to { transform: translateY(-100%); }
}
```

### Integration with Other Components

The Marquee component works well with:

- **Card**: For creating boxed marquee displays
- **Alert**: To enhance notification messages
- **Modal**: For attention-grabbing announcements in dialogs
- **Layout Components**: To integrate scrolling text in headers, footers, or sidebars

### Accessibility Considerations

- For important information, consider providing an alternative static display for users who may find moving text difficult to read
- Use appropriate color contrast ratios between text and background
- Consider using the `aria-live="polite"` attribute for screen readers
- Avoid extremely fast scrolling speeds that may cause readability issues

### Performance Optimization

- For very long text, consider breaking it into smaller segments
- Use appropriate duration values to ensure smooth scrolling
- For complex layouts with multiple marquees, be mindful of performance impact
- Consider pausing animations when the component is not in the viewport