# Skeleton Component

## Overview
The Skeleton component in BootstrapBlazor provides a placeholder preview for content that is still loading. It creates a low-fidelity representation of UI elements before they're fully loaded, improving perceived performance and reducing layout shifts during loading states.

## Features
- Multiple shape options (rectangle, circle, square)
- Customizable dimensions (width, height)
- Animation effects (pulse, wave, none)
- Configurable loading state
- Support for custom templates
- Ability to create complex skeleton layouts
- Automatic or manual control of loading state

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Active` | `bool` | `true` | Controls whether the skeleton is in loading state |
| `Width` | `string` | `"100%"` | Sets the width of the skeleton element |
| `Height` | `string` | `null` | Sets the height of the skeleton element |
| `Shape` | `SkeletonShape` | `SkeletonShape.Rectangle` | Sets the shape of the skeleton (Rectangle, Circle, Square) |
| `Animation` | `SkeletonAnimation` | `SkeletonAnimation.Pulse` | Sets the animation type (Pulse, Wave, None) |
| `Count` | `int` | `1` | Number of skeleton items to render when using the repeat template |
| `Template` | `RenderFragment` | `null` | Custom template for complex skeleton layouts |
| `ChildContent` | `RenderFragment` | `null` | Content to display when not in loading state |

## Events

| Event | Description |
| --- | --- |
| `OnActiveChanged` | Triggered when the active state changes |

## Usage Examples

### Example 1: Basic Skeleton
```html
<Skeleton Width="200px" Height="20px" />
```
This example shows a basic rectangular skeleton with a width of 200px and height of 20px, using the default pulse animation.

### Example 2: Circle Skeleton
```html
<Skeleton Shape="SkeletonShape.Circle" Width="50px" Height="50px" />
```
This example demonstrates a circular skeleton with equal width and height of 50px, useful for avatar or icon placeholders.

### Example 3: Multiple Paragraph Lines
```html
<div>
    <Skeleton Width="100%" Height="24px" />
    <Skeleton Width="90%" Height="16px" style="margin-top: 12px;" />
    <Skeleton Width="80%" Height="16px" style="margin-top: 12px;" />
    <Skeleton Width="70%" Height="16px" style="margin-top: 12px;" />
</div>
```
This example creates a paragraph-like skeleton with multiple lines of decreasing width, simulating a text block.

### Example 4: Card Skeleton with Different Animation
```html
<div style="width: 300px; padding: 20px; border: 1px solid #eee; border-radius: 4px;">
    <Skeleton Shape="SkeletonShape.Square" Width="100%" Height="160px" Animation="SkeletonAnimation.Wave" />
    <Skeleton Width="60%" Height="24px" style="margin-top: 16px;" Animation="SkeletonAnimation.Wave" />
    <Skeleton Width="100%" Height="16px" style="margin-top: 12px;" Animation="SkeletonAnimation.Wave" />
    <Skeleton Width="100%" Height="16px" style="margin-top: 8px;" Animation="SkeletonAnimation.Wave" />
</div>
```
This example shows a card-like skeleton with a square image placeholder and text lines, using the wave animation effect.

### Example 5: Conditional Content Display
```html
<Skeleton Active="@isLoading">
    <ChildContent>
        <div class="content-loaded">
            <h3>Content Title</h3>
            <p>This content is displayed when loading is complete.</p>
        </div>
    </ChildContent>
</Skeleton>

@code {
    private bool isLoading = true;
    
    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(2000); // Simulate loading
        isLoading = false;
    }
}
```
This example demonstrates how to conditionally display content after loading is complete, showing the skeleton during the loading phase and the actual content afterward.

### Example 6: Custom Template with Repeat
```html
<Skeleton Count="3">
    <Template>
        <div style="display: flex; margin-bottom: 16px;">
            <Skeleton Shape="SkeletonShape.Circle" Width="40px" Height="40px" />
            <div style="margin-left: 16px; flex: 1;">
                <Skeleton Width="200px" Height="16px" />
                <Skeleton Width="100px" Height="12px" style="margin-top: 8px;" />
            </div>
        </div>
    </Template>
</Skeleton>
```
This example shows how to create a custom template for a list of items (like a comment or message list) and repeat it multiple times using the Count property.

### Example 7: No Animation Skeleton
```html
<Skeleton Width="100%" Height="200px" Animation="SkeletonAnimation.None" />
```
This example demonstrates a skeleton without any animation effect, which can be useful in certain UI contexts or for users who prefer reduced motion.

## CSS Customization

The Skeleton component can be customized using the following CSS variables:

```css
--bb-skeleton-bg: rgba(190, 190, 190, 0.2);
--bb-skeleton-highlight-bg: rgba(255, 255, 255, 0.5);
--bb-skeleton-border-radius: 4px;
--bb-skeleton-animation-duration: 1.5s;
--bb-skeleton-animation-timing-function: ease-in-out;
```

## Notes

1. **Accessibility**: When using skeletons, consider adding `aria-busy="true"` to the parent container and `aria-hidden="true"` to the skeleton elements themselves for better accessibility.

2. **Performance**: For complex layouts with many skeleton elements, consider using the Template and Count properties instead of repeating individual skeleton components to improve rendering performance.

3. **Dimensions**: While you can set explicit dimensions for skeletons, it's often better to use relative units (%, em, rem) to ensure they adapt to different screen sizes and font settings.

4. **Animation Preferences**: Some users may prefer reduced motion. Consider respecting the `prefers-reduced-motion` media query by conditionally setting the Animation property to None.

5. **Layout Stability**: Try to match the skeleton dimensions closely to the expected content dimensions to minimize layout shifts when the actual content loads.