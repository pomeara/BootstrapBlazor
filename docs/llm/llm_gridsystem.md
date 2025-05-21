# Grid System Component

## Overview

The Grid System in BootstrapBlazor provides a powerful and flexible layout framework for creating responsive web designs. Based on Bootstrap's 12-column grid system, it allows developers to create complex layouts that automatically adjust to different screen sizes. The Grid System consists primarily of Row and Column components that work together to create structured, responsive layouts with minimal effort.

## Features

- **Responsive Design**: Automatically adapts layouts to different screen sizes (mobile, tablet, desktop)
- **12-Column System**: Divides the screen width into 12 equal columns for precise layout control
- **Flexbox Integration**: Built on CSS flexbox for advanced alignment and distribution capabilities
- **Breakpoint Support**: Different layout configurations at various screen width breakpoints (xs, sm, md, lg, xl, xxl)
- **Nested Grids**: Support for grid structures within grid columns for complex layouts
- **Auto-Layout Columns**: Option for equal-width columns without specifying exact column sizes
- **Column Ordering**: Control over the visual order of columns independent of their order in the markup
- **Column Offsets**: Ability to create space between columns
- **Gutters Control**: Adjustable spacing between columns
- **Alignment Options**: Vertical and horizontal alignment controls for content within rows and columns

## Components

### Row Component

The Row component serves as a container for columns and provides horizontal grouping of content.

#### Row Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ChildContent` | `RenderFragment` | `null` | The content to be rendered within the row, typically Column components. |
| `ItemsPerRow` | `ItemsPerRow` | `ItemsPerRow.Default` | Sets the number of items to display per row. |
| `RowType` | `RowType` | `RowType.Default` | Sets the layout format of the row. |
| `ColSpan` | `int?` | `null` | Sets the number of columns that a child Row spans across its parent Row. |

### Column Component

The Column component defines the width, positioning, and behavior of content within the grid system.

#### Column Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `ChildContent` | `RenderFragment` | `null` | The content to be rendered within the column. |
| `Span` | `int` | `0` | The number of columns to span (1-12) on all screen sizes. |
| `SpanSm` | `int` | `0` | The number of columns to span on small screens (≥576px). |
| `SpanMd` | `int` | `0` | The number of columns to span on medium screens (≥768px). |
| `SpanLg` | `int` | `0` | The number of columns to span on large screens (≥992px). |
| `SpanXl` | `int` | `0` | The number of columns to span on extra-large screens (≥1200px). |
| `SpanXxl` | `int` | `0` | The number of columns to span on extra-extra-large screens (≥1400px). |
| `Offset` | `int` | `0` | The number of columns to offset on all screen sizes. |
| `OffsetSm` | `int` | `0` | The number of columns to offset on small screens. |
| `OffsetMd` | `int` | `0` | The number of columns to offset on medium screens. |
| `OffsetLg` | `int` | `0` | The number of columns to offset on large screens. |
| `OffsetXl` | `int` | `0` | The number of columns to offset on extra-large screens. |
| `OffsetXxl` | `int` | `0` | The number of columns to offset on extra-extra-large screens. |
| `Order` | `int` | `0` | The order in which the column appears on all screen sizes. |
| `OrderSm` | `int` | `0` | The order in which the column appears on small screens. |
| `OrderMd` | `int` | `0` | The order in which the column appears on medium screens. |
| `OrderLg` | `int` | `0` | The order in which the column appears on large screens. |
| `OrderXl` | `int` | `0` | The order in which the column appears on extra-large screens. |
| `OrderXxl` | `int` | `0` | The order in which the column appears on extra-extra-large screens. |

## Usage Examples

### Example 1: Basic Grid Layout

```razor
<div class="container">
    <Row>
        <Column Span="4">
            <div class="bg-primary text-white p-3">Column 1 (Span 4)</div>
        </Column>
        <Column Span="4">
            <div class="bg-success text-white p-3">Column 2 (Span 4)</div>
        </Column>
        <Column Span="4">
            <div class="bg-info text-white p-3">Column 3 (Span 4)</div>
        </Column>
    </Row>
</div>
```

This example creates a simple row with three equal-width columns that each span 4 of the 12 available columns.

### Example 2: Responsive Grid Layout

```razor
<div class="container">
    <Row>
        <Column Span="12" SpanMd="6" SpanLg="4">
            <div class="bg-primary text-white p-3 mb-3">
                <p>Column 1</p>
                <p>xs: Full width, md: Half width, lg: One-third width</p>
            </div>
        </Column>
        <Column Span="12" SpanMd="6" SpanLg="4">
            <div class="bg-success text-white p-3 mb-3">
                <p>Column 2</p>
                <p>xs: Full width, md: Half width, lg: One-third width</p>
            </div>
        </Column>
        <Column Span="12" SpanMd="12" SpanLg="4">
            <div class="bg-info text-white p-3 mb-3">
                <p>Column 3</p>
                <p>xs: Full width, md: Full width, lg: One-third width</p>
            </div>
        </Column>
    </Row>
</div>
```

This example demonstrates responsive behavior where columns stack vertically on small screens, form two columns on medium screens, and three columns on large screens.

### Example 3: Column Offsets

```razor
<div class="container">
    <Row>
        <Column Span="4">
            <div class="bg-primary text-white p-3">Span 4</div>
        </Column>
        <Column Span="4" Offset="4">
            <div class="bg-success text-white p-3">Span 4, Offset 4</div>
        </Column>
    </Row>
    <Row class="mt-3">
        <Column Span="3" Offset="3">
            <div class="bg-info text-white p-3">Span 3, Offset 3</div>
        </Column>
        <Column Span="3" Offset="3">
            <div class="bg-warning text-white p-3">Span 3, Offset 3</div>
        </Column>
    </Row>
</div>
```

This example shows how to use column offsets to create space between columns or indent columns from the left.

### Example 4: Nested Grids

```razor
<div class="container">
    <Row>
        <Column Span="6">
            <div class="bg-light p-3 border">
                <h5>Outer Column 1</h5>
                <Row>
                    <Column Span="6">
                        <div class="bg-primary text-white p-3">Nested Column 1</div>
                    </Column>
                    <Column Span="6">
                        <div class="bg-success text-white p-3">Nested Column 2</div>
                    </Column>
                </Row>
            </div>
        </Column>
        <Column Span="6">
            <div class="bg-light p-3 border">
                <h5>Outer Column 2</h5>
                <Row>
                    <Column Span="4">
                        <div class="bg-info text-white p-3">Nested Column 3</div>
                    </Column>
                    <Column Span="4">
                        <div class="bg-warning text-white p-3">Nested Column 4</div>
                    </Column>
                    <Column Span="4">
                        <div class="bg-danger text-white p-3">Nested Column 5</div>
                    </Column>
                </Row>
            </div>
        </Column>
    </Row>
</div>
```

This example demonstrates how to nest grid rows and columns within columns to create more complex layouts.

### Example 5: Column Ordering

```razor
<div class="container">
    <Row>
        <Column Span="4" Order="3">
            <div class="bg-primary text-white p-3">First in code, third in display (Order 3)</div>
        </Column>
        <Column Span="4" Order="1">
            <div class="bg-success text-white p-3">Second in code, first in display (Order 1)</div>
        </Column>
        <Column Span="4" Order="2">
            <div class="bg-info text-white p-3">Third in code, second in display (Order 2)</div>
        </Column>
    </Row>
</div>
```

This example shows how to control the visual order of columns independently of their order in the markup using the Order property.

### Example 6: Responsive Column Ordering

```razor
<div class="container">
    <Row>
        <Column Span="4" Order="1" OrderMd="3">
            <div class="bg-primary text-white p-3">
                <p>First on mobile, third on desktop</p>
                <p>Order="1" OrderMd="3"</p>
            </div>
        </Column>
        <Column Span="4" Order="2" OrderMd="1">
            <div class="bg-success text-white p-3">
                <p>Second on mobile, first on desktop</p>
                <p>Order="2" OrderMd="1"</p>
            </div>
        </Column>
        <Column Span="4" Order="3" OrderMd="2">
            <div class="bg-info text-white p-3">
                <p>Third on mobile, second on desktop</p>
                <p>Order="3" OrderMd="2"</p>
            </div>
        </Column>
    </Row>
</div>
```

This example demonstrates responsive column ordering where the visual order changes between mobile and desktop viewports.

### Example 7: Auto-Layout Columns

```razor
<div class="container">
    <h5>Equal-Width Columns</h5>
    <Row>
        <Column>
            <div class="bg-primary text-white p-3">Column 1 (auto width)</div>
        </Column>
        <Column>
            <div class="bg-success text-white p-3">Column 2 (auto width)</div>
        </Column>
        <Column>
            <div class="bg-info text-white p-3">Column 3 (auto width)</div>
        </Column>
    </Row>
    
    <h5 class="mt-4">Mixed Column Widths</h5>
    <Row>
        <Column Span="6">
            <div class="bg-primary text-white p-3">Column 1 (Span 6)</div>
        </Column>
        <Column>
            <div class="bg-success text-white p-3">Column 2 (auto width)</div>
        </Column>
        <Column>
            <div class="bg-info text-white p-3">Column 3 (auto width)</div>
        </Column>
    </Row>
</div>
```

This example shows how to use auto-layout columns that automatically share the available width, as well as mixing fixed-width and auto-width columns.

## Customization Notes

### CSS Variables

The Grid System uses Bootstrap's CSS variables for customization. Some key variables include:

```css
:root {
    /* Grid breakpoints */
    --bs-breakpoint-xs: 0;
    --bs-breakpoint-sm: 576px;
    --bs-breakpoint-md: 768px;
    --bs-breakpoint-lg: 992px;
    --bs-breakpoint-xl: 1200px;
    --bs-breakpoint-xxl: 1400px;
    
    /* Grid containers */
    --bs-container-padding-x: 0.75rem;
    --bs-container-sm: 540px;
    --bs-container-md: 720px;
    --bs-container-lg: 960px;
    --bs-container-xl: 1140px;
    --bs-container-xxl: 1320px;
    
    /* Grid columns */
    --bs-gutter-x: 1.5rem;
    --bs-gutter-y: 0;
}
```

### Container Classes

The Grid System works best within container elements. Bootstrap provides several container classes:

- `.container`: Fixed-width container that changes width at each breakpoint
- `.container-fluid`: Full-width container that spans the entire viewport
- `.container-{breakpoint}`: Width: 100% until the specified breakpoint, then fixed-width

### Responsive Utilities

Bootstrap includes responsive utility classes that can be used with the Grid System:

- `.d-none`, `.d-{breakpoint}-none`: Hide elements at specific breakpoints
- `.d-block`, `.d-{breakpoint}-block`: Show elements as block at specific breakpoints
- `.d-flex`, `.d-{breakpoint}-flex`: Apply flexbox at specific breakpoints

### Integration with Other Components

The Grid System works well with other BootstrapBlazor components:

- **Card**: Place cards within columns for responsive card layouts
- **Form**: Create responsive form layouts using the grid system
- **Table**: Wrap tables in appropriate grid containers for responsive behavior
- **Modal**: Structure modal content using rows and columns
- **Tabs**: Organize tab content using the grid system

### Best Practices

1. Always start with mobile-first design, then add larger breakpoint classes as needed
2. Use the smallest number of columns necessary to achieve your layout
3. Avoid deeply nested grids when possible to maintain performance
4. Use the appropriate container class based on your layout needs
5. Test your layouts across different screen sizes to ensure proper responsiveness
6. Use the browser's developer tools to debug grid layouts
7. Consider accessibility when designing grid layouts