# Progress Component

## Overview
The Progress component in BootstrapBlazor is used to display the completion status of an operation or task. It provides visual feedback about the progress of a workflow or action, helping users understand how much of a task has been completed and how much remains.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Different progress types (line, circle)
- Customizable progress percentage
- Striped and animated variants
- Text display options (percentage, custom text)
- Size variations
- Indeterminate state for unknown progress duration

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Color` | `Color` | `Color.Primary` | Sets the color theme of the progress bar |
| `Height` | `int` | `null` | Sets the height of the progress bar in pixels |
| `IsAnimated` | `bool` | `false` | Enables animation effect on the progress bar |
| `IsStriped` | `bool` | `false` | Enables striped pattern on the progress bar |
| `IsIndeterminate` | `bool` | `false` | Sets the progress bar to indeterminate state (continuous animation) |
| `ShowLabel` | `bool` | `true` | Controls whether to display the progress text |
| `Value` | `double` | `0` | The current progress value (0-100) |
| `Type` | `ProgressType` | `ProgressType.Line` | Sets the progress type (Line or Circle) |
| `Width` | `int` | `null` | Sets the width of the progress component |
| `Format` | `Func<double, string>` | `null` | Custom formatter for the progress text |
| `StrokeWidth` | `int` | `6` | Sets the stroke width for circle progress type |

## Events

| Event | Description |
| --- | --- |
| `OnValueChanged` | Triggered when the progress value changes |

## Usage Examples

### Example 1: Basic Progress Bar
```html
<Progress Value="50" />
```
This example shows a basic progress bar with 50% completion. It uses the default primary color and displays the percentage text.

### Example 2: Colored Progress Bars
```html
<Progress Value="20" Color="Color.Success" />
<Progress Value="40" Color="Color.Info" />
<Progress Value="60" Color="Color.Warning" />
<Progress Value="80" Color="Color.Danger" />
```
This example demonstrates progress bars with different color themes, each showing a different completion percentage.

### Example 3: Striped and Animated Progress Bar
```html
<Progress Value="75" IsStriped="true" IsAnimated="true" Color="Color.Info" />
```
This example shows a striped progress bar with animation effect, displaying 75% completion in the info color theme.

### Example 4: Progress Bar with Custom Height
```html
<Progress Value="65" Height="30" />
```
This example demonstrates a progress bar with a custom height of 30 pixels and 65% completion.

### Example 5: Circle Progress
```html
<Progress Type="ProgressType.Circle" Value="88" />
```
This example shows a circular progress indicator with 88% completion.

### Example 6: Custom Text Format
```html
<Progress Value="42" Format="@(v => $"{v:F1}% Complete")" />
```
This example demonstrates a progress bar with custom text formatting, displaying the percentage with one decimal place followed by "Complete".

### Example 7: Indeterminate Progress
```html
<Progress IsIndeterminate="true" />
```
This example shows an indeterminate progress bar, useful for operations where the completion time is unknown.

## CSS Customization

The Progress component can be customized using the following CSS variables:

```css
--bb-progress-height: 1rem;
--bb-progress-font-size: 0.75rem;
--bb-progress-bg: #e9ecef;
--bb-progress-border-radius: 0.25rem;
--bb-progress-box-shadow: inset 0 1px 2px rgba(0, 0, 0, 0.075);
--bb-progress-bar-color: #fff;
--bb-progress-bar-bg: #0d6efd;
--bb-progress-bar-transition: width 0.6s ease;
```

## Notes

1. **Accessibility**: The Progress component includes ARIA attributes for better accessibility. It uses `role="progressbar"`, `aria-valuenow`, `aria-valuemin`, and `aria-valuemax` attributes.

2. **Indeterminate vs. Animated**: The `IsIndeterminate` property creates a continuously animated progress bar without a specific value, while `IsAnimated` applies animation to a progress bar with a specific value.

3. **Circle Progress Sizing**: When using the Circle progress type, you can control the size using the `Width` property. The circle will maintain an aspect ratio of 1:1.

4. **Performance**: For frequently updating progress bars (like during file uploads), consider throttling updates to avoid performance issues.

5. **Browser Compatibility**: The striped and animated effects use CSS gradients and animations, which are supported in all modern browsers but may have slight visual differences across browsers.