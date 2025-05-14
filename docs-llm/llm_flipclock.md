# FlipClock Component

## Overview

The FlipClock component in BootstrapBlazor is a visually appealing animated clock or timer that displays time with a flip animation effect, similar to traditional mechanical flip clocks. It provides a nostalgic yet modern way to display time or countdown values in web applications. The component can be used for displaying current time, countdowns for events, timers for activities, or as an engaging visual element in dashboards and interfaces.

## Features

- **Multiple Display Modes**: Supports both clock mode (showing current time) and countdown mode (counting down to zero from a specified time).
- **Customizable Time Units**: Flexible configuration to show or hide hours, minutes, and seconds based on requirements.
- **Animated Transitions**: Smooth flip animations when digits change, creating an engaging visual effect.
- **Event Callbacks**: Provides completion callback for countdown operations.
- **Highly Customizable**: Extensive styling options including size, colors, backgrounds, and spacing.
- **Responsive Design**: Adapts well to different screen sizes and container widths.
- **Accessibility Support**: Designed with accessibility considerations for better user experience.

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `ShowHour` | bool | true | Controls the visibility of the hour display. |
| `ShowMinute` | bool | true | Controls the visibility of the minute display. |
| `ShowSecond` | bool | true | Controls the visibility of the second display. |
| `OnCompletedAsync` | Func<Task>? | null | Callback function that executes when countdown completes. |
| `ViewMode` | FlipClockViewMode | DateTime | Sets the display mode (DateTime for current time, CountDown for countdown). |
| `StartValue` | TimeSpan? | null | The starting time value for countdown mode. |
| `Height` | string? | null | Sets the component height (default is 200px if not specified). |
| `BackgroundColor` | string? | null | Sets the component background color. |
| `FontSize` | string? | null | Sets the font size for digits (default is 80px if not specified). |
| `CardWidth` | string? | null | Sets the width of each flip card (default is 60px if not specified). |
| `CardHeight` | string? | null | Sets the height of each flip card (default is 90px if not specified). |
| `CardColor` | string? | null | Sets the text color of the cards (default is #ccc if not specified). |
| `CardBackgroundColor` | string? | null | Sets the background color of the cards (default is #333 if not specified). |
| `CardDividerHeight` | string? | null | Sets the height of the divider line in cards (default is 1px if not specified). |
| `CardDividerColor` | string? | null | Sets the color of the divider line (default is rgba(0, 0, 0, .4) if not specified). |
| `CardMargin` | string? | null | Sets the margin between individual cards. |
| `CardGroupMargin` | string? | null | Sets the margin between card groups (hours, minutes, seconds). |

## Events

| Event | Description |
| --- | --- |
| `OnCompletedAsync` | Triggered when a countdown reaches zero. This event can be used to execute custom logic when a timer completes. |

## Usage Examples

### Example 1: Basic Clock Display

```razor
<FlipClock />
```

This example creates a basic flip clock that displays the current time with hours, minutes, and seconds, using default styling.

### Example 2: Countdown Timer

```razor
<FlipClock ViewMode="FlipClockViewMode.CountDown"
          StartValue="TimeSpan.FromMinutes(5)"
          OnCompletedAsync="@TimerCompletedHandler" />

@code {
    private async Task TimerCompletedHandler()
    {
        await Task.Delay(100);
        // Show notification or trigger next action
        await MessageService.Show(new MessageOption()
        {
            Content = "Countdown completed!",
            Icon = "fa-solid fa-bell"
        });
    }
}
```

This example creates a 5-minute countdown timer that triggers a notification when the countdown reaches zero.

### Example 3: Custom Styled Clock

```razor
<FlipClock Height="300px"
          BackgroundColor="linear-gradient(135deg, #667eea 0%, #764ba2 100%)"
          FontSize="100px"
          CardWidth="80px"
          CardHeight="120px"
          CardColor="#ffffff"
          CardBackgroundColor="rgba(0, 0, 0, 0.5)"
          CardDividerColor="rgba(255, 255, 255, 0.2)"
          CardGroupMargin="30px" />
```

This example creates a larger clock with custom colors, gradients, and spacing for a more dramatic visual effect.

### Example 4: Time Units Selection

```razor
<FlipClock ShowHour="true"
          ShowMinute="true"
          ShowSecond="false" />
```

This example creates a clock that only displays hours and minutes, hiding the seconds display.

### Example 5: Countdown with Hours Only

```razor
<FlipClock ViewMode="FlipClockViewMode.CountDown"
          StartValue="TimeSpan.FromHours(24)"
          ShowMinute="false"
          ShowSecond="false"
          OnCompletedAsync="@(() => InvokeAsync(() => StateHasChanged()))" />
```

This example creates a countdown that only shows hours counting down from 24 hours, suitable for day-long events.

### Example 6: Responsive Clock

```razor
<div class="responsive-clock-container">
    <FlipClock Height="calc(15vw)"
              FontSize="calc(6vw)"
              CardWidth="calc(4.5vw)"
              CardHeight="calc(7vw)"
              CardMargin="calc(0.3vw)"
              CardGroupMargin="calc(1.5vw)" />
</div>

<style>
    .responsive-clock-container {
        width: 100%;
        max-width: 600px;
        margin: 0 auto;
    }
</style>
```

This example creates a responsive clock that scales based on the viewport width, making it suitable for both desktop and mobile views.

### Example 7: Integration with Card Component

```razor
<Card Title="Event Countdown">
    <Body>
        <div class="text-center mb-3">
            <h5>Time remaining until product launch:</h5>
        </div>
        <FlipClock ViewMode="FlipClockViewMode.CountDown"
                  StartValue="TimeSpan.FromDays(3).Add(TimeSpan.FromHours(12))"
                  BackgroundColor="transparent"
                  CardBackgroundColor="var(--bs-primary)"
                  CardColor="white"
                  OnCompletedAsync="@LaunchProductHandler" />
    </Body>
</Card>

@code {
    private async Task LaunchProductHandler()
    {
        await Task.Delay(100);
        // Product launch logic
    }
}
```

This example integrates the FlipClock within a Card component to create a product launch countdown display with custom styling that matches the application theme.

## Customization Notes

### CSS Variables

The FlipClock component uses CSS variables for styling, which can be overridden for customization:

```css
.bb-flip-clock {
    --bb-flip-clock-height: 200px;
    --bb-flip-clock-bg: radial-gradient(ellipse at center, rgba(150, 150, 150, 1) 0%, rgba(89, 89, 89, 1) 100%);
    --bb-flip-clock-font-size: 80px;
    --bb-flip-clock-text-shadow: 0 1px 2px #000;
    --bb-flip-clock-justify-content: center;
    --bb-flip-clock-list-margin-right: 20px;
    --bb-flip-clock-item-margin: 0 5px;
    --bb-flip-clock-item-width: 60px;
    --bb-flip-clock-item-height: 90px;
    --bb-flip-clock-item-box-shadow: 0 2px 5px rgba(0, 0, 0, 0.7);
    --bb-flip-clock-number-color: #ccc;
    --bb-flip-clock-number-bg: #333;
    --bb-flip-clock-number-line-bg: rgba(0, 0, 0, 0.4);
    --bb-flip-clock-number-line-height: 1px;
}
```

### Integration with Other Components

The FlipClock component works well with:

- **Card**: For creating boxed timer displays
- **Modal**: For countdown popups or timers in modal dialogs
- **Alert**: To combine with notifications when countdowns complete
- **Button**: To add start/pause/reset controls for the timer

### Accessibility Considerations

- Ensure sufficient color contrast between the card background and text for readability
- Consider adding aria labels or descriptions for screen readers
- For interactive timers, provide alternative text notifications when countdowns complete

### Performance Optimization

- For long-running countdowns, consider using server time synchronization
- For multiple clocks on a single page, be mindful of performance impact
- Use the ViewMode property appropriately to avoid unnecessary animations

### Mobile Considerations

- Use responsive sizing with viewport units for better mobile display
- Consider reducing the number of displayed units on small screens
- Test touch interactions if combining with interactive elements