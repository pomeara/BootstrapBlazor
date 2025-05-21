# Title Component

## Overview
The Title component in BootstrapBlazor provides a standardized way to display headings and titles throughout an application. It offers consistent styling, customization options, and additional features beyond standard HTML heading elements. The Title component helps maintain visual hierarchy and improves accessibility while ensuring design consistency across the application.

## Features
- **Hierarchical Levels**: Supports multiple heading levels (h1-h6)
- **Customizable Appearance**: Options for color, size, alignment, and style
- **Icon Support**: Can include icons alongside text
- **Subtitle Support**: Optional subtitle display
- **Divider Integration**: Optional divider below the title
- **Responsive Behavior**: Adapts to different screen sizes
- **Accessibility Features**: Proper semantic structure for screen readers
- **Animation Effects**: Optional entrance and hover animations
- **Event Handling**: Click and hover event support

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Text` | string | null | The main text content of the title |
| `Level` | TitleLevel | TitleLevel.H3 | The heading level (H1-H6) |
| `Subtitle` | string | null | Optional subtitle text |
| `Color` | Color | Color.None | The color of the title text |
| `Alignment` | Alignment | Alignment.Left | Text alignment (Left, Center, Right) |
| `ShowDivider` | bool | false | When true, shows a divider below the title |
| `DividerColor` | Color | Color.Primary | The color of the divider |
| `Icon` | string | null | Icon to display alongside the title |
| `IconColor` | Color | Color.None | The color of the icon |
| `Size` | Size | Size.Medium | Size of the title (Small, Medium, Large) |
| `IsItalic` | bool | false | When true, applies italic styling |
| `IsBold` | bool | true | When true, applies bold styling |
| `IsUnderline` | bool | false | When true, applies underline styling |
| `IsAnimated` | bool | false | When true, applies entrance animation |
| `AnimationType` | AnimationType | AnimationType.Fade | Type of animation to apply |
| `MarginTop` | int | 0 | Top margin in pixels |
| `MarginBottom` | int | 16 | Bottom margin in pixels |

## Events

| Event | Description |
|-------|-------------|
| `OnClick` | Triggered when the title is clicked |
| `OnMouseOver` | Triggered when the mouse pointer moves over the title |
| `OnMouseOut` | Triggered when the mouse pointer moves out of the title |

## Usage Examples

### Example 1: Basic Title with Different Levels

```html
<Title Text="Main Heading" Level="TitleLevel.H1" />

<Title Text="Section Heading" Level="TitleLevel.H2" />

<Title Text="Subsection Heading" Level="TitleLevel.H3" />

<Title Text="Minor Heading" Level="TitleLevel.H4" />

<Title Text="Small Heading" Level="TitleLevel.H5" />

<Title Text="Smallest Heading" Level="TitleLevel.H6" />
```

### Example 2: Title with Subtitle and Divider

```html
<Title Text="Product Features"
       Level="TitleLevel.H2"
       Subtitle="Everything you need to know about our product"
       ShowDivider="true"
       DividerColor="Color.Primary"
       Alignment="Alignment.Center" />
```

### Example 3: Styled Title with Icon

```html
<Title Text="User Dashboard"
       Level="TitleLevel.H2"
       Icon="fa-solid fa-tachometer-alt"
       IconColor="Color.Primary"
       Color="Color.Dark"
       IsBold="true"
       Size="Size.Large" />
```

### Example 4: Animated Title

```html
<Title Text="Welcome to Our Application"
       Level="TitleLevel.H1"
       IsAnimated="true"
       AnimationType="AnimationType.SlideInDown"
       Alignment="Alignment.Center"
       Color="Color.Primary" />
```

### Example 5: Interactive Title with Events

```html
<Title @ref="interactiveTitle"
       Text="Click Me for More Information"
       Level="TitleLevel.H3"
       OnClick="HandleTitleClick"
       OnMouseOver="HandleMouseOver"
       OnMouseOut="HandleMouseOut"
       Color="@currentColor" />

<div class="mt-3">
    <p>@statusMessage</p>
</div>
```

```csharp
@code {
    private Title interactiveTitle;
    private string statusMessage = "Hover over or click the title";
    private Color currentColor = Color.Primary;
    
    private void HandleTitleClick()
    {
        statusMessage = "Title was clicked!";
        currentColor = Color.Success;
    }
    
    private void HandleMouseOver()
    {
        statusMessage = "Mouse is over the title";
        currentColor = Color.Info;
    }
    
    private void HandleMouseOut()
    {
        statusMessage = "Mouse left the title";
        currentColor = Color.Primary;
    }
}
```

### Example 6: Responsive Title System

```html
<div class="responsive-titles">
    <Title Text="Responsive Title System"
           Level="TitleLevel.H1"
           Alignment="Alignment.Center"
           MarginBottom="32" />
    
    <div class="row">
        <div class="col-md-6">
            <Title Text="Left Column Heading"
                   Level="TitleLevel.H2"
                   ShowDivider="true" />
            
            <p>Content for the left column goes here...</p>
            
            <Title Text="Subsection"
                   Level="TitleLevel.H3"
                   MarginTop="24" />
            
            <p>Subsection content goes here...</p>
        </div>
        
        <div class="col-md-6">
            <Title Text="Right Column Heading"
                   Level="TitleLevel.H2"
                   ShowDivider="true" />
            
            <p>Content for the right column goes here...</p>
            
            <Title Text="Another Subsection"
                   Level="TitleLevel.H3"
                   MarginTop="24" />
            
            <p>More subsection content goes here...</p>
        </div>
    </div>
</div>
```

### Example 7: Title with Custom Styling

```html
<div class="custom-title-container p-4 bg-light">
    <Title Text="Custom Styled Title"
           Level="TitleLevel.H2"
           Class="custom-title"
           Style="letter-spacing: 2px; text-transform: uppercase;"
           ShowDivider="true"
           DividerColor="Color.Danger" />
    
    <p>This example demonstrates how to apply custom CSS classes and inline styles to the Title component.</p>
</div>

<style>
    .custom-title {
        font-family: 'Georgia', serif;
        text-shadow: 1px 1px 3px rgba(0,0,0,0.2);
    }
</style>
```

## CSS Customization

The Title component can be customized using CSS variables and classes:

```css
/* Custom styles for Title component */
.bb-title {
    /* Component container */
    margin-bottom: var(--bb-title-margin-bottom, 1rem);
}

.bb-title-h1 {
    /* Styles for h1 level */
    font-size: var(--bb-title-h1-font-size, 2.5rem);
}

.bb-title-h2 {
    /* Styles for h2 level */
    font-size: var(--bb-title-h2-font-size, 2rem);
}

.bb-title-h3 {
    /* Styles for h3 level */
    font-size: var(--bb-title-h3-font-size, 1.75rem);
}

.bb-title-h4 {
    /* Styles for h4 level */
    font-size: var(--bb-title-h4-font-size, 1.5rem);
}

.bb-title-h5 {
    /* Styles for h5 level */
    font-size: var(--bb-title-h5-font-size, 1.25rem);
}

.bb-title-h6 {
    /* Styles for h6 level */
    font-size: var(--bb-title-h6-font-size, 1rem);
}

.bb-title-subtitle {
    /* Subtitle styles */
    font-size: var(--bb-title-subtitle-font-size, 0.875rem);
    color: var(--bb-title-subtitle-color, #6c757d);
    margin-top: var(--bb-title-subtitle-margin-top, 0.25rem);
}

.bb-title-divider {
    /* Divider styles */
    height: var(--bb-title-divider-height, 2px);
    margin-top: var(--bb-title-divider-margin-top, 0.5rem);
    width: var(--bb-title-divider-width, 50px);
}

.bb-title-icon {
    /* Icon styles */
    margin-right: var(--bb-title-icon-margin-right, 0.5rem);
}

/* Animation classes */
.bb-title-animated {
    animation-duration: var(--bb-title-animation-duration, 1s);
    animation-fill-mode: both;
}

.bb-title-fade {
    animation-name: fadeIn;
}

.bb-title-slide-down {
    animation-name: slideInDown;
}

@keyframes fadeIn {
    from { opacity: 0; }
    to { opacity: 1; }
}

@keyframes slideInDown {
    from {
        transform: translate3d(0, -100%, 0);
        visibility: visible;
    }
    to {
        transform: translate3d(0, 0, 0);
    }
}

/* Responsive adjustments */
@media (max-width: 768px) {
    .bb-title-h1 { font-size: calc(var(--bb-title-h1-font-size, 2.5rem) * 0.8); }
    .bb-title-h2 { font-size: calc(var(--bb-title-h2-font-size, 2rem) * 0.8); }
    .bb-title-h3 { font-size: calc(var(--bb-title-h3-font-size, 1.75rem) * 0.8); }
}
```

## JavaScript Interop

The Title component primarily operates without JavaScript, but for animations and advanced interactions, it uses JavaScript interop. You can extend its functionality by using the following methods:

```csharp
// Trigger animation programmatically
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.title.animate", elementRef, animationType);

// Update title text programmatically
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.title.setText", elementRef, newText);

// Toggle visibility
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.title.toggle", elementRef);
```

## Accessibility

The Title component is designed with accessibility in mind:

- Uses proper heading elements (h1-h6) for semantic structure
- Maintains correct heading hierarchy for screen readers
- Provides sufficient color contrast for readability
- Includes ARIA attributes when necessary
- Supports keyboard navigation for interactive titles

## Browser Compatibility

The Title component is compatible with all modern browsers:

- Chrome
- Firefox
- Edge
- Safari
- Opera

For older browsers, the component includes fallback styling to ensure basic functionality and appearance.

## Integration with Other Components

The Title component can be integrated with various other BootstrapBlazor components:

- Use with Card components for card headers
- Combine with Collapse or Accordion for section headers
- Integrate with Tab components for tab titles
- Pair with Modal or Dialog for dialog headers
- Use with Table for table captions or section headers