# Row Component

## Overview

The Row component in BootstrapBlazor provides a flexible container for organizing content in horizontal groups. It is part of the grid system and works in conjunction with Column components to create responsive layouts. The Row component is based on Bootstrap's row class and implements flexbox functionality, allowing for easy alignment, distribution, and ordering of content across different screen sizes.

## Features

- **Responsive Grid System**: Works with Bootstrap's 12-column grid system for responsive layouts
- **Flexbox Integration**: Built-in flexbox capabilities for advanced content alignment and distribution
- **Gutters Control**: Configurable spacing between columns (gutters)
- **Vertical Alignment**: Options for aligning content vertically within the row
- **Horizontal Alignment**: Options for distributing columns horizontally within the row
- **Column Wrapping**: Control over how columns wrap when they exceed the row width
- **Responsive Behaviors**: Different layouts across various screen sizes

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ChildContent` | `RenderFragment` | `null` | The content to be rendered within the row, typically Column components. |
| `Gutters` | `int` | `0` | The size of the gutters (spacing between columns) from 0-5, corresponding to Bootstrap's gutter sizes. |
| `NoGutters` | `bool` | `false` | When true, removes the gutters between columns. This is a shorthand for Gutters="0". |
| `Align` | `Alignment` | `Alignment.None` | Vertical alignment of items within the row. Options include Start, Center, End, Stretch, and Baseline. |
| `Justify` | `Justify` | `Justify.None` | Horizontal distribution of columns within the row. Options include Start, Center, End, Between, Around, and Evenly. |
| `Wrap` | `Wrap` | `Wrap.Wrap` | Controls how columns wrap when they exceed the row width. Options include Wrap, NoWrap, and WrapReverse. |
| `Class` | `string` | `""` | Additional CSS class(es) to apply to the row element. |
| `Style` | `string` | `""` | Additional inline styles to apply to the row element. |
| `Height` | `string` | `""` | Sets the height of the row. Can be a CSS value like "100px" or "100%". |
| `MinHeight` | `string` | `""` | Sets the minimum height of the row. Can be a CSS value like "100px" or "100%". |
| `MaxHeight` | `string` | `""` | Sets the maximum height of the row. Can be a CSS value like "100px" or "100%". |

## Events

The Row component does not expose specific events beyond the standard Blazor component lifecycle events.

## Usage Examples

### Example 1: Basic Row with Columns

A simple row with three equal-width columns:

```razor
<Row>
    <Column Span="4">
        <div class="bg-primary text-white p-3">Column 1</div>
    </Column>
    <Column Span="4">
        <div class="bg-success text-white p-3">Column 2</div>
    </Column>
    <Column Span="4">
        <div class="bg-info text-white p-3">Column 3</div>
    </Column>
</Row>
```

### Example 2: Row with Different Column Widths

A row with columns of varying widths:

```razor
<Row>
    <Column Span="2">
        <div class="bg-primary text-white p-3">Span 2</div>
    </Column>
    <Column Span="6">
        <div class="bg-success text-white p-3">Span 6</div>
    </Column>
    <Column Span="4">
        <div class="bg-info text-white p-3">Span 4</div>
    </Column>
</Row>
```

### Example 3: Responsive Row Layout

A row with columns that have different widths on different screen sizes:

```razor
<Row>
    <Column Span="12" SpanSm="6" SpanMd="4" SpanLg="3">
        <div class="bg-primary text-white p-3 mb-3">
            <p>Column 1</p>
            <p>xs: 12, sm: 6, md: 4, lg: 3</p>
        </div>
    </Column>
    <Column Span="12" SpanSm="6" SpanMd="4" SpanLg="3">
        <div class="bg-success text-white p-3 mb-3">
            <p>Column 2</p>
            <p>xs: 12, sm: 6, md: 4, lg: 3</p>
        </div>
    </Column>
    <Column Span="12" SpanSm="6" SpanMd="4" SpanLg="3">
        <div class="bg-info text-white p-3 mb-3">
            <p>Column 3</p>
            <p>xs: 12, sm: 6, md: 4, lg: 3</p>
        </div>
    </Column>
    <Column Span="12" SpanSm="6" SpanMd="12" SpanLg="3">
        <div class="bg-warning text-white p-3 mb-3">
            <p>Column 4</p>
            <p>xs: 12, sm: 6, md: 12, lg: 3</p>
        </div>
    </Column>
</Row>
```

### Example 4: Row with Alignment Options

A row demonstrating vertical and horizontal alignment options:

```razor
<div class="mb-4">
    <h5>Vertical Alignment (Align="Alignment.Center")</h5>
    <Row Align="Alignment.Center" Style="height: 150px; background-color: #f8f9fa;">
        <Column Span="4">
            <div class="bg-primary text-white p-3">Short Content</div>
        </Column>
        <Column Span="4">
            <div class="bg-success text-white p-3" style="height: 100px;">Taller Content</div>
        </Column>
        <Column Span="4">
            <div class="bg-info text-white p-3">Short Content</div>
        </Column>
    </Row>
</div>

<div class="mb-4">
    <h5>Horizontal Alignment (Justify="Justify.Between")</h5>
    <Row Justify="Justify.Between">
        <Column Span="3">
            <div class="bg-primary text-white p-3">Left</div>
        </Column>
        <Column Span="3">
            <div class="bg-success text-white p-3">Middle</div>
        </Column>
        <Column Span="3">
            <div class="bg-info text-white p-3">Right</div>
        </Column>
    </Row>
</div>

<div class="mb-4">
    <h5>Combined Alignments</h5>
    <Row Align="Alignment.Center" Justify="Justify.Around" Style="height: 150px; background-color: #f8f9fa;">
        <Column Span="3">
            <div class="bg-primary text-white p-3">Item 1</div>
        </Column>
        <Column Span="3">
            <div class="bg-success text-white p-3" style="height: 80px;">Item 2</div>
        </Column>
        <Column Span="3">
            <div class="bg-info text-white p-3">Item 3</div>
        </Column>
    </Row>
</div>
```

### Example 5: Row with Gutters Control

Demonstrating different gutter sizes between columns:

```razor
<div class="mb-4">
    <h5>No Gutters</h5>
    <Row NoGutters="true">
        <Column Span="6">
            <div class="bg-primary text-white p-3">Column 1</div>
        </Column>
        <Column Span="6">
            <div class="bg-success text-white p-3">Column 2</div>
        </Column>
    </Row>
</div>

<div class="mb-4">
    <h5>Small Gutters (Gutters="1")</h5>
    <Row Gutters="1">
        <Column Span="6">
            <div class="bg-primary text-white p-3">Column 1</div>
        </Column>
        <Column Span="6">
            <div class="bg-success text-white p-3">Column 2</div>
        </Column>
    </Row>
</div>

<div class="mb-4">
    <h5>Large Gutters (Gutters="5")</h5>
    <Row Gutters="5">
        <Column Span="6">
            <div class="bg-primary text-white p-3">Column 1</div>
        </Column>
        <Column Span="6">
            <div class="bg-success text-white p-3">Column 2</div>
        </Column>
    </Row>
</div>
```

### Example 6: Nested Rows and Columns

Creating complex layouts with nested rows and columns:

```razor
<Row>
    <Column Span="8">
        <div class="bg-light p-3 mb-3">
            <h5>Main Content</h5>
            <p>This is the main content area spanning 8 columns.</p>
            
            <Row>
                <Column Span="6">
                    <div class="bg-primary text-white p-3">Nested Column 1</div>
                </Column>
                <Column Span="6">
                    <div class="bg-info text-white p-3">Nested Column 2</div>
                </Column>
            </Row>
        </div>
    </Column>
    <Column Span="4">
        <div class="bg-secondary text-white p-3 mb-3">
            <h5>Sidebar</h5>
            <p>This is the sidebar area spanning 4 columns.</p>
            
            <Row>
                <Column Span="12">
                    <div class="bg-dark text-white p-3 mb-3">Sidebar Item 1</div>
                </Column>
                <Column Span="12">
                    <div class="bg-dark text-white p-3">Sidebar Item 2</div>
                </Column>
            </Row>
        </div>
    </Column>
</Row>
```

### Example 7: Row with Column Ordering

Changing the visual order of columns using the Order property:

```razor
<div class="mb-4">
    <h5>Default Order</h5>
    <Row>
        <Column Span="4">
            <div class="bg-primary text-white p-3">First in DOM</div>
        </Column>
        <Column Span="4">
            <div class="bg-success text-white p-3">Second in DOM</div>
        </Column>
        <Column Span="4">
            <div class="bg-info text-white p-3">Third in DOM</div>
        </Column>
    </Row>
</div>

<div class="mb-4">
    <h5>Custom Order</h5>
    <Row>
        <Column Span="4" Order="3">
            <div class="bg-primary text-white p-3">First in DOM, Third visually</div>
        </Column>
        <Column Span="4" Order="1">
            <div class="bg-success text-white p-3">Second in DOM, First visually</div>
        </Column>
        <Column Span="4" Order="2">
            <div class="bg-info text-white p-3">Third in DOM, Second visually</div>
        </Column>
    </Row>
</div>

<div class="mb-4">
    <h5>Responsive Order</h5>
    <Row>
        <Column Span="4" Order="1" OrderMd="3">
            <div class="bg-primary text-white p-3">
                <p>First on mobile</p>
                <p>Third on desktop</p>
            </div>
        </Column>
        <Column Span="4" Order="2" OrderMd="2">
            <div class="bg-success text-white p-3">
                <p>Second on mobile</p>
                <p>Second on desktop</p>
            </div>
        </Column>
        <Column Span="4" Order="3" OrderMd="1">
            <div class="bg-info text-white p-3">
                <p>Third on mobile</p>
                <p>First on desktop</p>
            </div>
        </Column>
    </Row>
</div>
```

## CSS Customization

The Row component generates HTML that uses Bootstrap's grid system classes. You can customize the appearance of rows through:

1. **Class Property**: Add custom CSS classes to the row element
2. **Style Property**: Apply inline styles directly to the row element
3. **Custom CSS**: Define your own CSS rules targeting the row and its child columns

The Row component uses the following CSS classes from Bootstrap:

- `.row`: The base class for the row container
- `.g-*`: Classes for controlling gutter size (e.g., `.g-0`, `.g-1`, etc.)
- `.align-items-*`: Classes for vertical alignment (e.g., `.align-items-center`)
- `.justify-content-*`: Classes for horizontal distribution (e.g., `.justify-content-between`)
- `.flex-*`: Classes for controlling flex behavior (e.g., `.flex-wrap`, `.flex-nowrap`)

## JavaScript Interop

The Row component is primarily a layout component and doesn't require JavaScript interop for its core functionality. It operates entirely through CSS classes and HTML structure.

## Accessibility Considerations

When using the Row component, consider the following accessibility best practices:

1. Maintain a logical source order in the HTML, even when using ordering properties
2. Ensure that the visual layout makes sense when navigating with a keyboard
3. Use appropriate semantic HTML elements within columns
4. Test layouts at different zoom levels to ensure content remains accessible
5. Consider how the layout will be interpreted by screen readers

## Browser Compatibility

The Row component is compatible with all modern browsers that support Flexbox, which includes:

- Chrome (recent versions)
- Firefox (recent versions)
- Safari (recent versions)
- Edge (recent versions)

For older browsers with limited Flexbox support, the component will fall back to basic layout behavior, but some advanced alignment features may not work as expected.

## Integration with Other Components

The Row component works primarily with:

- **Column Component**: For defining the width and behavior of content within the row
- **Container Component**: For wrapping rows in a centered, padded container
- **Card Component**: For organizing content within rows and columns
- **Form Components**: For creating responsive form layouts
- **Table Component**: For responsive table layouts

## Best Practices

1. Always use Column components within Rows for proper grid behavior
2. Remember that the grid system is based on 12 columns, so column spans should add up to 12 for a full row
3. Use responsive column spans (SpanSm, SpanMd, etc.) to create layouts that adapt to different screen sizes
4. Consider using the Container component to wrap rows for proper padding and centering
5. Use the Align and Justify properties for more precise control over content positioning
6. Avoid deeply nested rows and columns when possible, as they can become difficult to maintain
7. Use the NoGutters property when you need columns to touch with no spacing between them