# Spinner Component

## Overview
The Spinner component in BootstrapBlazor provides a visual indicator of loading or processing state. It's used to inform users that an action is in progress, helping to improve the perceived performance and user experience during asynchronous operations or page transitions.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Different spinner types (border, grow)
- Size variations (small, medium, large)
- Customizable text display
- Fullscreen overlay option
- Backdrop customization
- Programmatic control of visibility

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Color.Primary` | Sets the color theme of the spinner |
| `Type` | `SpinnerType` | `SpinnerType.Border` | Sets the spinner type (Border or Grow) |
| `Size` | `Size` | `Size.Medium` | Sets the size of the spinner (Small, Medium, Large) |
| `IsShow` | `bool` | `true` | Controls the visibility of the spinner |
| `IsFullScreen` | `bool` | `false` | When true, displays the spinner as a fullscreen overlay |
| `ShowBackdrop` | `bool` | `true` | Controls whether to show a backdrop behind the spinner |
| `BackdropColor` | `string` | `"rgba(0, 0, 0, 0.3)"` | Sets the color of the backdrop |
| `Text` | `string` | `null` | Text to display alongside the spinner |
| `Delay` | `int` | `0` | Delay in milliseconds before showing the spinner |

## Events

| Event | Description |
| --- | --- |
| `OnVisibleChanged` | Triggered when the spinner visibility changes |

## Usage Examples

### Example 1: Basic Spinner
```html
<Spinner />
```
This example shows a basic spinner with default settings (border type, primary color, medium size).

### Example 2: Different Spinner Types and Colors
```html
<div class="d-flex flex-wrap gap-3">
    <Spinner Type="SpinnerType.Border" Color="Color.Primary" />
    <Spinner Type="SpinnerType.Border" Color="Color.Secondary" />
    <Spinner Type="SpinnerType.Border" Color="Color.Success" />
    <Spinner Type="SpinnerType.Grow" Color="Color.Danger" />
    <Spinner Type="SpinnerType.Grow" Color="Color.Warning" />
    <Spinner Type="SpinnerType.Grow" Color="Color.Info" />
</div>
```
This example demonstrates different spinner types (border and grow) with various color themes.

### Example 3: Spinner Sizes
```html
<div class="d-flex align-items-center gap-3">
    <Spinner Size="Size.Small" />
    <Spinner Size="Size.Medium" />
    <Spinner Size="Size.Large" />
</div>
```
This example shows spinners in different sizes (small, medium, large).

### Example 4: Spinner with Text
```html
<div class="d-flex flex-column align-items-center">
    <Spinner Text="Loading..." />
</div>
```
This example demonstrates a spinner with accompanying text to provide more context about the loading state.

### Example 5: Fullscreen Spinner with Custom Backdrop
```html
<Button @onclick="ShowFullscreenSpinner">Show Fullscreen Spinner</Button>

<Spinner IsFullScreen="true" IsShow="@isLoading" BackdropColor="rgba(0, 0, 0, 0.5)" Text="Processing your request..." />

@code {
    private bool isLoading = false;
    
    private async Task ShowFullscreenSpinner()
    {
        isLoading = true;
        await Task.Delay(3000); // Simulate processing
        isLoading = false;
    }
}
```
This example shows how to create a fullscreen spinner overlay with a custom backdrop color and text, controlled programmatically.

### Example 6: Delayed Spinner
```html
<Button @onclick="StartLongOperation">Start Operation</Button>

<div style="position: relative; min-height: 100px;">
    <Spinner IsShow="@isProcessing" Delay="500" Text="This will appear after 500ms" />
</div>

@code {
    private bool isProcessing = false;
    
    private async Task StartLongOperation()
    {
        isProcessing = true;
        await Task.Delay(3000); // Simulate long operation
        isProcessing = false;
    }
}
```
This example demonstrates a spinner with a delay of 500ms, which helps avoid flickering for operations that might complete quickly.

### Example 7: Spinner in a Button
```html
<Button Color="Color.Primary" IsDisabled="@isSubmitting">
    @if (isSubmitting)
    {
        <Spinner Size="Size.Small" style="margin-right: 8px;" />
        <span>Submitting...</span>
    }
    else
    {
        <span>Submit</span>
    }
</Button>

@code {
    private bool isSubmitting = false;
    
    private async Task Submit()
    {
        isSubmitting = true;
        await Task.Delay(2000); // Simulate form submission
        isSubmitting = false;
    }
}
```
This example shows how to integrate a spinner within a button to indicate that an action is in progress after the button is clicked.

## CSS Customization

The Spinner component can be customized using the following CSS variables:

```css
--bb-spinner-width: 2rem;
--bb-spinner-height: 2rem;
--bb-spinner-border-width: 0.25em;
--bb-spinner-animation-speed: 0.75s;
--bb-spinner-backdrop-zindex: 1050;
--bb-spinner-zindex: 1051;
--bb-spinner-text-margin-top: 0.5rem;
```

## Notes

1. **Accessibility**: When using spinners, especially fullscreen ones, consider adding appropriate ARIA attributes like `aria-busy="true"` and `aria-live="polite"` to improve accessibility.

2. **Performance**: For long-running operations, consider using the `Delay` property to avoid showing the spinner for operations that complete quickly, which can reduce perceived flickering.

3. **Fullscreen Usage**: The fullscreen spinner is useful for page-level operations, but use it sparingly as it blocks all user interaction. Consider using localized spinners for component-level operations.

4. **Button Integration**: When integrating spinners in buttons or other interactive elements, remember to disable the element while the spinner is active to prevent multiple submissions.

5. **Backdrop Customization**: The backdrop color can be customized using any valid CSS color value, including rgba() for transparency control.