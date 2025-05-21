# Card Component

## Overview

The Card component in BootstrapBlazor provides a flexible and extensible content container with multiple variants and options. It includes options for headers, footers, content, and a wide variety of colors. Cards are built with as little markup and styles as possible but still deliver control and customization. They're powered by flexbox, so they offer easy alignment and mix well with other Bootstrap components.

## Features

- **Flexible Structure**: Header, body, and footer sections that can be customized
- **Content Templates**: Support for custom content in each section
- **Collapsible Cards**: Option to make cards expandable and collapsible
- **Color Variants**: Multiple color options for visual distinction
- **Shadow Effects**: Optional shadow styling for depth
- **Text Alignment**: Center alignment option for card content
- **Custom Icons**: Configurable collapse/expand icons
- **Event Handling**: Events for collapse state changes
- **Responsive Design**: Adapts to different screen sizes
- **Accessibility Support**: ARIA attributes for better accessibility

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `HeaderText` | `string` | `null` | Text to display in the card header. |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the card header. |
| `BodyTemplate` | `RenderFragment` | `null` | Custom template for the card body. |
| `FooterTemplate` | `RenderFragment` | `null` | Custom template for the card footer. |
| `Color` | `Color` | `Color.None` | Sets the color theme of the card. Options include Primary, Secondary, Success, Danger, Warning, Info, Dark, and Light. |
| `IsCenter` | `bool` | `false` | When true, centers the text content within the card. |
| `IsCollapsible` | `bool` | `false` | When true, makes the card collapsible with a toggle button in the header. |
| `Collapsed` | `bool` | `false` | When true, the card is initially collapsed. |
| `IsShadow` | `bool` | `false` | When true, adds a shadow effect to the card. |
| `CollapseIcon` | `string` | `fa-solid fa-circle-chevron-right` | Icon used for the collapse/expand button. |

## Events

| Event | Type | Description |
|-------|------|-------------|
| `CollapsedChanged` | `EventCallback<bool>` | Triggered when the collapse state of the card changes. The parameter indicates whether the card is collapsed (true) or expanded (false). |

## Usage Examples

### Example 1: Basic Card

A simple card with header, body, and footer:

```razor
<Card>
    <HeaderTemplate>
        <span class="card-title">Simple Card</span>
    </HeaderTemplate>
    <BodyTemplate>
        <p>This is a basic card example with custom content in the body.</p>
        <p>Cards are built with flexbox, so they offer easy alignment and mix well with other Bootstrap components.</p>
    </BodyTemplate>
    <FooterTemplate>
        <small class="text-muted">Last updated 3 mins ago</small>
    </FooterTemplate>
</Card>
```

### Example 2: Card with Color Variants

Cards with different color themes:

```razor
<div class="row">
    <div class="col-md-4 mb-3">
        <Card Color="Color.Primary">
            <HeaderTemplate>
                <span class="card-title">Primary Card</span>
            </HeaderTemplate>
            <BodyTemplate>
                <p>A card with primary color theme.</p>
            </BodyTemplate>
        </Card>
    </div>
    <div class="col-md-4 mb-3">
        <Card Color="Color.Success">
            <HeaderTemplate>
                <span class="card-title">Success Card</span>
            </HeaderTemplate>
            <BodyTemplate>
                <p>A card with success color theme.</p>
            </BodyTemplate>
        </Card>
    </div>
    <div class="col-md-4 mb-3">
        <Card Color="Color.Danger">
            <HeaderTemplate>
                <span class="card-title">Danger Card</span>
            </HeaderTemplate>
            <BodyTemplate>
                <p>A card with danger color theme.</p>
            </BodyTemplate>
        </Card>
    </div>
</div>
```

### Example 3: Collapsible Card

A card that can be expanded and collapsed:

```razor
@code {
    private bool _collapsed = false;
    
    private void OnCollapsedChanged(bool collapsed)
    {
        _collapsed = collapsed;
    }
}

<Card IsCollapsible="true" Collapsed="@_collapsed" CollapsedChanged="OnCollapsedChanged">
    <HeaderTemplate>
        <span class="card-title">Collapsible Card</span>
    </HeaderTemplate>
    <BodyTemplate>
        <p>This content can be hidden by clicking the header.</p>
        <p>The current state is: @(_collapsed ? "Collapsed" : "Expanded")</p>
    </BodyTemplate>
</Card>
```

### Example 4: Card with Shadow and Centered Text

A card with shadow effect and centered content:

```razor
<Card IsShadow="true" IsCenter="true">
    <HeaderTemplate>
        <span class="card-title">Centered Content with Shadow</span>
    </HeaderTemplate>
    <BodyTemplate>
        <p>This card has a shadow effect for depth.</p>
        <p>The text is centered within the card.</p>
        <Button Color="Color.Primary">Action Button</Button>
    </BodyTemplate>
    <FooterTemplate>
        <small class="text-muted">Card Footer</small>
    </FooterTemplate>
</Card>
```

### Example 5: Card with Image

A card that includes an image:

```razor
<Card>
    <BodyTemplate>
        <img src="images/sample.jpg" class="card-img-top" alt="Sample Image">
        <h5 class="card-title mt-3">Card with Image</h5>
        <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
        <Button Color="Color.Primary">Go somewhere</Button>
    </BodyTemplate>
</Card>
```

### Example 6: Card with Custom Header Icons

A card with custom icons in the header:

```razor
<Card>
    <HeaderTemplate>
        <span class="card-title"><i class="fa-solid fa-star me-2"></i>Featured</span>
        <div class="ms-auto">
            <Button Color="Color.None" Icon="fa-solid fa-ellipsis-vertical" />
        </div>
    </HeaderTemplate>
    <BodyTemplate>
        <h5 class="card-title">Special title treatment</h5>
        <p class="card-text">With supporting text below as a natural lead-in to additional content.</p>
        <Button Color="Color.Primary">Go somewhere</Button>
    </BodyTemplate>
</Card>
```

### Example 7: Card Group for Dashboard

A group of cards used in a dashboard layout:

```razor
<div class="row">
    <div class="col-md-3 mb-3">
        <Card IsShadow="true" IsCenter="true" Color="Color.Primary">
            <BodyTemplate>
                <i class="fa-solid fa-users fa-3x mb-3"></i>
                <h3>1,234</h3>
                <p>Total Users</p>
            </BodyTemplate>
        </Card>
    </div>
    <div class="col-md-3 mb-3">
        <Card IsShadow="true" IsCenter="true" Color="Color.Success">
            <BodyTemplate>
                <i class="fa-solid fa-chart-line fa-3x mb-3"></i>
                <h3>$56,789</h3>
                <p>Total Revenue</p>
            </BodyTemplate>
        </Card>
    </div>
    <div class="col-md-3 mb-3">
        <Card IsShadow="true" IsCenter="true" Color="Color.Info">
            <BodyTemplate>
                <i class="fa-solid fa-shopping-cart fa-3x mb-3"></i>
                <h3>567</h3>
                <p>New Orders</p>
            </BodyTemplate>
        </Card>
    </div>
    <div class="col-md-3 mb-3">
        <Card IsShadow="true" IsCenter="true" Color="Color.Warning">
            <BodyTemplate>
                <i class="fa-solid fa-comments fa-3x mb-3"></i>
                <h3>89</h3>
                <p>New Comments</p>
            </BodyTemplate>
        </Card>
    </div>
</div>
```

## Customization Notes

### CSS Variables

The Card component uses CSS variables for styling, which can be customized to match your application's theme:

```css
.card {
    --bb-card-shadow: 0 2px 5px 0 rgba(0, 0, 0, 0.16), 0 2px 10px 0 rgba(0, 0, 0, 0.12);
    --bb-card-hover-shadow: 0 5px 11px 0 rgba(0, 0, 0, 0.18), 0 4px 15px 0 rgba(0, 0, 0, 0.15);
    --bb-card-title-margin-left: 0.5rem;
    --bb-card-header-tag-height: 1.5rem;
    --bs-card-title-spacer-y: 0.5rem;
}
```

### Integration with Other Components

The Card component works well with other BootstrapBlazor components:

- **Tabs**: Place Tabs inside a Card to create tabbed content panels
- **Table**: Embed a Table in a Card for data presentation
- **Form**: Use a Card to contain forms for better visual organization
- **Button**: Add buttons to card headers or footers for actions
- **Progress**: Include progress bars in cards to show completion status
- **List**: Combine with List components for list-style content in cards

### Best Practices

1. **Use Cards for Related Content**: Group related information within a single card
2. **Consistent Styling**: Maintain consistent card styling throughout your application
3. **Responsive Considerations**: Use the grid system to ensure cards display well on all devices
4. **Appropriate Spacing**: Add proper margins between cards when displaying multiple cards
5. **Semantic Headers**: Use meaningful headers that clearly describe the card content
6. **Collapsible for Dense UIs**: Use the collapsible feature when displaying many cards in a dense UI
7. **Shadows for Hierarchy**: Use shadows to create visual hierarchy between cards
8. **Color for Meaning**: Use color variants to convey meaning (success, warning, etc.)
9. **Accessible Content**: Ensure card content follows accessibility best practices