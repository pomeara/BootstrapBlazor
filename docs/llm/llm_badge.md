# Badge Component

## Overview
The Badge component in BootstrapBlazor is a small count and labeling component used to highlight or indicate status, notifications, or values. Badges are typically used to display numerical values, status indicators, or short text labels that draw attention to specific elements in the user interface. They're commonly found in navigation menus, buttons, or near icons to indicate counts, statuses, or to highlight new or important items.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Customizable text content
- Pill styling option for rounded badges
- Positioning options (standalone, inline, or positioned relative to other elements)
- Size variations
- Dot style for simple indicators
- Counter functionality for numerical values
- Animation support for attention-grabbing effects
- Integration with other components (buttons, tabs, nav items, etc.)
- Accessibility support with proper ARIA attributes

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Color.Primary` | Sets the color theme of the badge (Primary, Secondary, Success, Danger, Warning, Info, Dark) |
| `Text` | `string` | `null` | The text content to display inside the badge |
| `IsPill` | `bool` | `false` | When true, applies a more rounded, pill-like appearance |
| `IsOutline` | `bool` | `false` | When true, displays the badge with an outline style instead of a filled background |
| `IsDot` | `bool` | `false` | When true, displays the badge as a small dot indicator |
| `Position` | `BadgePosition` | `BadgePosition.None` | Specifies the position of the badge when used as an overlay (TopRight, TopLeft, BottomRight, BottomLeft, None) |
| `Size` | `Size` | `Size.None` | Sets the size of the badge (XS, SM, None, LG, XL) |
| `ChildContent` | `RenderFragment` | `null` | Content to display inside the badge |
| `Target` | `string` | `null` | ID of the target element to position the badge relative to |

## Events

| Event | Description |
| --- | --- |
| `OnClick` | Triggered when the badge is clicked |

## Usage Examples

### Example 1: Basic Badges with Different Colors

```razor
<div class="d-flex flex-wrap gap-2">
    <Badge Color="Color.Primary">Primary</Badge>
    <Badge Color="Color.Secondary">Secondary</Badge>
    <Badge Color="Color.Success">Success</Badge>
    <Badge Color="Color.Danger">Danger</Badge>
    <Badge Color="Color.Warning">Warning</Badge>
    <Badge Color="Color.Info">Info</Badge>
    <Badge Color="Color.Dark">Dark</Badge>
</div>
```

This example shows basic badges with different color themes. Each badge displays text with a background color corresponding to its theme.

### Example 2: Pill Badges

```razor
<div class="d-flex flex-wrap gap-2">
    <Badge Color="Color.Primary" IsPill="true">Primary</Badge>
    <Badge Color="Color.Secondary" IsPill="true">Secondary</Badge>
    <Badge Color="Color.Success" IsPill="true">Success</Badge>
    <Badge Color="Color.Danger" IsPill="true">Danger</Badge>
    <Badge Color="Color.Warning" IsPill="true">Warning</Badge>
    <Badge Color="Color.Info" IsPill="true">Info</Badge>
    <Badge Color="Color.Dark" IsPill="true">Dark</Badge>
</div>
```

This example demonstrates pill-style badges, which have more rounded corners for a softer appearance.

### Example 3: Badges with Buttons

```razor
<div class="d-flex flex-wrap gap-2">
    <Button Color="Color.Primary">
        Notifications <Badge Color="Color.Light" Text="4" />
    </Button>
    
    <Button Color="Color.Success">
        Messages <Badge Color="Color.Light" Text="8" />
    </Button>
    
    <Button Color="Color.Danger">
        Alerts <Badge Color="Color.Light" Text="12" />
    </Button>
</div>
```

This example shows how to use badges inside buttons to indicate counts or notifications.

### Example 4: Positioned Badges

```razor
<div class="position-relative d-inline-block" style="width: 50px; height: 50px;">
    <i class="fa-solid fa-bell fa-2x"></i>
    <Badge Color="Color.Danger" Position="BadgePosition.TopRight" Text="5" />
</div>

<div class="position-relative d-inline-block ms-4" style="width: 50px; height: 50px;">
    <i class="fa-solid fa-envelope fa-2x"></i>
    <Badge Color="Color.Success" Position="BadgePosition.TopRight" IsDot="true" />
</div>
```

This example demonstrates positioned badges, where the badge is placed at a specific position relative to another element. The first example shows a numerical badge, while the second shows a dot indicator.

### Example 5: Outline Badges

```razor
<div class="d-flex flex-wrap gap-2">
    <Badge Color="Color.Primary" IsOutline="true">Primary</Badge>
    <Badge Color="Color.Secondary" IsOutline="true">Secondary</Badge>
    <Badge Color="Color.Success" IsOutline="true">Success</Badge>
    <Badge Color="Color.Danger" IsOutline="true">Danger</Badge>
    <Badge Color="Color.Warning" IsOutline="true">Warning</Badge>
    <Badge Color="Color.Info" IsOutline="true">Info</Badge>
    <Badge Color="Color.Dark" IsOutline="true">Dark</Badge>
</div>
```

This example shows badges with outline styling, which displays the badge with a colored border and text but without a filled background.

### Example 6: Badge Sizes

```razor
<div class="d-flex flex-wrap align-items-center gap-2">
    <Badge Color="Color.Primary" Size="Size.XS">XS</Badge>
    <Badge Color="Color.Primary" Size="Size.SM">SM</Badge>
    <Badge Color="Color.Primary">Default</Badge>
    <Badge Color="Color.Primary" Size="Size.LG">LG</Badge>
    <Badge Color="Color.Primary" Size="Size.XL">XL</Badge>
</div>
```

This example demonstrates badges with different sizes, from extra small to extra large.

### Example 7: Interactive Badge with Click Event

```razor
<div class="d-flex flex-column gap-2">
    <div>
        <Badge Color="Color.Primary" OnClick="@HandleBadgeClick" style="cursor: pointer;">Click me</Badge>
    </div>
    
    <div>
        <span>Click count: @clickCount</span>
    </div>
</div>

@code {
    private int clickCount = 0;
    
    private void HandleBadgeClick()
    {
        clickCount++;
    }
}
```

This example shows an interactive badge that responds to click events, incrementing a counter each time it's clicked.

## Customization

The Badge component can be customized using CSS variables:

```css
.badge {
    /* Badge color variables */
    --bs-badge-color: #fff;
    --bs-badge-bg: #0d6efd;
    
    /* Badge padding and border radius */
    --bs-badge-padding-x: 0.65em;
    --bs-badge-padding-y: 0.35em;
    --bs-badge-border-radius: 0.375rem;
    
    /* Pill badge border radius */
    --bs-badge-pill-border-radius: 50rem;
    
    /* Font size and weight */
    --bs-badge-font-size: 0.75em;
    --bs-badge-font-weight: 700;
}

/* Badge color overrides for light backgrounds */
.badge.bg-secondary, .badge.bg-light {
    --bs-badge-color: #212529;
}
```

You can override these variables in your CSS to customize the appearance of the Badge component according to your design requirements.

Additionally, you can customize the Badge component by:

1. Using the `Color` property to change the badge's color theme
2. Using the `IsPill` property to toggle between standard and pill-style badges
3. Using the `IsOutline` property to toggle between filled and outline styles
4. Using the `Size` property to adjust the badge's size
5. Using the `Position` property to position the badge relative to other elements
6. Using custom CSS classes and styles for more specific customization needs