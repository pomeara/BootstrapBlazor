# Divider Component

## Overview

The Divider component in BootstrapBlazor provides a simple yet effective way to separate content sections in your application. It creates a horizontal or vertical line that helps establish visual hierarchy and improve content readability. The component can be customized with text, icons, or custom content positioned at different alignments, making it versatile for various design needs.

## Features

- **Orientation Options**: Support for both horizontal and vertical dividers
- **Text Integration**: Ability to add descriptive text within the divider
- **Icon Support**: Option to include icons for enhanced visual cues
- **Alignment Control**: Multiple alignment options for text and icons (left, center, right)
- **Custom Content**: Support for custom content through child components
- **Styling Flexibility**: Customizable appearance through CSS variables
- **Responsive Design**: Adapts to container width in horizontal mode
- **Semantic Separation**: Creates meaningful visual separation between content sections
- **Accessibility Support**: Proper semantic structure for screen readers

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `IsVertical` | `bool` | `false` | When true, displays the divider vertically instead of horizontally. |
| `Alignment` | `Alignment` | `Alignment.Center` | Sets the alignment of text/icon/content within the divider. Options are Left, Center, and Right. |
| `Text` | `string` | `null` | Text to display within the divider. |
| `Icon` | `string` | `null` | CSS class for an icon to display within the divider (e.g., "fa-solid fa-star"). |
| `ChildContent` | `RenderFragment` | `null` | Custom content to render within the divider. |

## Usage Examples

### Example 1: Basic Horizontal Divider

A simple horizontal divider to separate content sections:

```razor
<p>This is the content above the divider.</p>

<Divider />

<p>This is the content below the divider.</p>
```

### Example 2: Divider with Text

A horizontal divider with descriptive text:

```razor
<Divider Text="Section Break" />
```

### Example 3: Divider with Icon and Text

A divider with both an icon and text:

```razor
<Divider Icon="fa-solid fa-star" Text="Featured Content" />
```

### Example 4: Divider with Different Alignments

Dividers with left, center, and right alignment:

```razor
<Divider Text="Left Aligned" Alignment="Alignment.Left" />

<div class="my-4"></div>

<Divider Text="Center Aligned" Alignment="Alignment.Center" />

<div class="my-4"></div>

<Divider Text="Right Aligned" Alignment="Alignment.Right" />
```

### Example 5: Vertical Divider

A vertical divider used between inline content:

```razor
<div style="display: flex; align-items: center; height: 100px;">
    <span>Left Content</span>
    <Divider IsVertical="true" />
    <span>Right Content</span>
</div>
```

### Example 6: Divider with Custom Content

A divider with custom content using the ChildContent property:

```razor
<Divider>
    <Badge Color="Color.Danger">New</Badge>
</Divider>
```

### Example 7: Styled Divider in a Card

A divider used within a card component with custom styling:

```razor
<Card>
    <HeaderTemplate>
        <span class="card-title">Card Title</span>
    </HeaderTemplate>
    <BodyTemplate>
        <p>This is the first section of content in the card.</p>
        
        <Divider Text="Details" Icon="fa-solid fa-info-circle" />
        
        <p>This is the second section with more detailed information.</p>
    </BodyTemplate>
</Card>
```

## Customization Notes

### CSS Variables

The Divider component uses CSS variables for styling, which can be customized to match your application's theme:

```css
.divider {
    --bb-divider-margin: 1.5rem 0;         /* Margin around the divider */
    --bb-divider-text-padding: 0 20px;       /* Padding around the text */
}
```

For vertical dividers, additional customization is available:

```css
.divider.divider-vertical {
    margin: 0 1rem;                          /* Horizontal margin */
    min-height: 21px;                        /* Minimum height */
    
    .divider-text {
        --bb-divider-text-padding: 20px 0;   /* Padding for vertical divider text */
    }
}
```

### Integration with Other Components

The Divider component works well with other BootstrapBlazor components:

- **Card**: Use dividers to separate sections within a card
- **Form**: Create logical groupings in forms
- **List**: Separate list items or groups
- **Layout**: Define sections in page layouts
- **Tabs**: Add dividers between tab content sections

### Best Practices

1. **Semantic Usage**: Use dividers to create meaningful separation between content sections
2. **Text Clarity**: Keep divider text short and descriptive
3. **Consistent Spacing**: Maintain consistent margins around dividers
4. **Appropriate Orientation**: Use horizontal dividers for vertical content flow and vertical dividers for horizontal content flow
5. **Accessibility**: Ensure divider text provides meaningful context for screen readers
6. **Visual Hierarchy**: Use dividers to establish clear visual hierarchy in complex layouts
7. **Responsive Considerations**: For vertical dividers, ensure they adapt properly on small screens