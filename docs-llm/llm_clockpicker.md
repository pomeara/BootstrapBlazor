# ClockPicker Component Documentation

## Overview
The ClockPicker component in BootstrapBlazor provides an intuitive analog clock interface for selecting time values. Unlike the standard TimePicker which uses spinners, the ClockPicker offers a visual clock face where users can select hours, minutes, and seconds by clicking or dragging the clock hands. This component is ideal for touch interfaces and applications where a more visual time selection experience is preferred, making time input more intuitive and user-friendly.

## Features
- **Visual Clock Interface**: Analog clock face for intuitive time selection
- **Hour, Minute, and Second Selection**: Complete time selection capabilities
- **12/24 Hour Format Support**: Toggle between 12-hour and 24-hour time formats
- **AM/PM Selection**: Period selection for 12-hour format
- **Drag and Click Interaction**: Select time by clicking numbers or dragging clock hands
- **Keyboard Navigation**: Accessibility support with keyboard controls
- **Animation Effects**: Smooth transitions between selection modes
- **Min/Max Time Constraints**: Limit selectable time ranges
- **Form Integration**: Seamless integration with form validation
- **Customizable Appearance**: Adjustable colors, sizes, and styles
- **Responsive Design**: Adapts to different screen sizes
- **Localization Support**: Multi-language support for time formats

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | TimeSpan? | null | Gets or sets the selected time value |
| ValueChanged | EventCallback<TimeSpan?> | - | Callback when the selected value changes |
| Format | string | "HH:mm:ss" | Format string for displaying the time |
| Use12Hours | bool | false | Whether to use 12-hour format with AM/PM |
| ShowSeconds | bool | true | Whether to show seconds selection |
| MinValue | TimeSpan? | null | Minimum selectable time |
| MaxValue | TimeSpan? | null | Maximum selectable time |
| IsDisabled | bool | false | Whether the picker is disabled |
| IsReadOnly | bool | false | Whether the picker is read-only |
| Placeholder | string | "" | Placeholder text when no value is selected |
| AutoClose | bool | true | Whether to automatically close the picker after selection |
| AllowClear | bool | true | Whether to show the clear button |
| Size | Size | Medium | Size of the component (Small, Medium, Large) |
| ClassName | string | "" | Additional CSS class for the component |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the selected time value changes |
| OnVisibleChanged | Triggered when the clock picker visibility changes |
| OnClear | Triggered when the time value is cleared |
| OnModeChanged | Triggered when the selection mode changes (hour, minute, second) |
| OnDragStart | Triggered when the user starts dragging a clock hand |
| OnDragEnd | Triggered when the user stops dragging a clock hand |

## Usage Examples

### Example 1: Basic ClockPicker

```razor
<ClockPicker @bind-Value="@selectedTime" />

<div class="mt-3">
    Selected time: @(selectedTime?.ToString("hh\\:mm\\:ss") ?? "None")
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
}
```

### Example 2: 12-Hour Format with AM/PM

```razor
<ClockPicker @bind-Value="@selectedTime"
             Use12Hours="true"
             Format="hh:mm tt"
             Placeholder="Select time" />

<div class="mt-3">
    Selected time: @(selectedTime?.ToString("hh\\:mm tt") ?? "None")
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
}
```

### Example 3: ClockPicker without Seconds

```razor
<ClockPicker @bind-Value="@selectedTime"
             ShowSeconds="false"
             Format="HH:mm"
             Placeholder="Select hours and minutes" />

<div class="mt-3">
    Selected time: @(selectedTime?.ToString("HH\\:mm") ?? "None")
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
}
```

### Example 4: ClockPicker with Min/Max Constraints

```razor
<ClockPicker @bind-Value="@selectedTime"
             MinValue="@minTime"
             MaxValue="@maxTime"
             Placeholder="Select a time within range" />

<div class="mt-3">
    <p>Selected time: @(selectedTime?.ToString("hh\\:mm\\:ss") ?? "None")</p>
    <p>Valid range: @minTime.ToString("hh\\:mm\\:ss") to @maxTime.ToString("hh\\:mm\\:ss")</p>
</div>

@code {
    private TimeSpan? selectedTime;
    private TimeSpan minTime = new TimeSpan(9, 0, 0); // 9:00 AM
    private TimeSpan maxTime = new TimeSpan(17, 0, 0); // 5:00 PM
}
```

### Example 5: ClockPicker with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.AppointmentName" placeholder="Appointment name" />
        <ValidationMessage For="@(() => model.AppointmentName)" />
    </div>
    
    <div class="mb-3">
        <label>Appointment Time</label>
        <ClockPicker @bind-Value="@model.AppointmentTime" />
        <ValidationMessage For="@(() => model.AppointmentTime)" />
    </div>
    
    <Button Type="ButtonType.Submit">Save Appointment</Button>
</ValidateForm>

@code {
    private AppointmentModel model = new AppointmentModel
    {
        AppointmentName = "",
        AppointmentTime = new TimeSpan(9, 0, 0)
    };
    
    private void HandleValidSubmit()
    {
        // Save the appointment
        Console.WriteLine($"Appointment saved: {model.AppointmentName}");
        Console.WriteLine($"Time: {model.AppointmentTime}");
    }
    
    public class AppointmentModel
    {
        [Required(ErrorMessage = "Appointment name is required")]
        public string AppointmentName { get; set; }
        
        [Required(ErrorMessage = "Appointment time is required")]
        [Range(typeof(TimeSpan), "08:00:00", "18:00:00", ErrorMessage = "Appointment must be between 8:00 AM and 6:00 PM")]
        public TimeSpan? AppointmentTime { get; set; }
    }
}
```

### Example 6: ClockPicker with Event Handling

```razor
<ClockPicker @bind-Value="@selectedTime"
             OnValueChanged="@HandleTimeChanged"
             OnModeChanged="@HandleModeChanged"
             OnDragStart="@HandleDragStart"
             OnDragEnd="@HandleDragEnd" />

<div class="mt-3">
    <h5>Event Log</h5>
    <div class="border p-3 bg-light" style="max-height: 200px; overflow-y: auto;">
        @foreach (var log in eventLogs.AsEnumerable().Reverse())
        {
            <div class="mb-1">@log</div>
        }
    </div>
</div>

<div class="mt-3">
    <Button Color="Color.Secondary" OnClick="ClearLogs">Clear Logs</Button>
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
    private List<string> eventLogs = new();
    
    private void HandleTimeChanged(TimeSpan? time)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Time changed: {time?.ToString("hh\\:mm\\:ss") ?? "None"}");
    }
    
    private void HandleModeChanged(string mode)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Mode changed: {mode}");
    }
    
    private void HandleDragStart()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Drag started");
    }
    
    private void HandleDragEnd()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Drag ended");
    }
    
    private void ClearLogs()
    {
        eventLogs.Clear();
    }
}
```

### Example 7: Multiple ClockPickers for Time Range

```razor
<div class="row">
    <div class="col-md-6">
        <div class="mb-3">
            <label>Start Time</label>
            <ClockPicker @bind-Value="@startTime"
                         OnValueChanged="@UpdateEndTimeMin"
                         Placeholder="Select start time" />
        </div>
    </div>
    <div class="col-md-6">
        <div class="mb-3">
            <label>End Time</label>
            <ClockPicker @bind-Value="@endTime"
                         MinValue="@endTimeMin"
                         Placeholder="Select end time" />
        </div>
    </div>
</div>

<div class="mt-3">
    @if (startTime.HasValue && endTime.HasValue)
    {
        <p>Selected time range: @startTime.Value.ToString("hh\\:mm\\:ss tt") to @endTime.Value.ToString("hh\\:mm\\:ss tt")</p>
        
        var duration = endTime.Value - startTime.Value;
        if (duration.TotalHours < 0)
        {
            duration = duration.Add(new TimeSpan(24, 0, 0));
        }
        
        <p>Duration: @duration.ToString("hh\\:mm\\:ss")</p>
    }
    else
    {
        <p>Please select both start and end times</p>
    }
</div>

@code {
    private TimeSpan? startTime;
    private TimeSpan? endTime;
    private TimeSpan? endTimeMin;
    
    private void UpdateEndTimeMin(TimeSpan? time)
    {
        if (time.HasValue)
        {
            // Set minimum end time to 30 minutes after start time
            endTimeMin = time.Value.Add(new TimeSpan(0, 30, 0));
            
            // If current end time is earlier than new minimum, reset it
            if (endTime.HasValue && endTime.Value < endTimeMin.Value)
            {
                endTime = endTimeMin;
            }
        }
        else
        {
            endTimeMin = null;
        }
    }
}
```

## Customization Notes

The ClockPicker component can be customized using the following CSS variables:

```css
:root {
    --bb-time-text-color: #409eff;
    --bb-time-body-width: 264px;
    --bb-time-body-height: 264px;
    --bb-time-clock-number-width: 22px;
    --bb-time-clock-number-height: 22px;
    --bb-time-clock-hour-margin-top: 6px;
    --bb-time-clock-minute-margin-top: 9px;
    --bb-time-clock-second-margin-top: 12px;
    --bb-time-clock-point-bg-color: var(--bs-primary);
    --bb-time-clock-point-bar-bg-color: var(--bs-primary);
    --bb-time-footer-btn-border: 1px solid #dcdfe6;
    --bb-time-footer-btn-padding: 3px 12px;
    --bb-time-footer-btn-font-size: .75rem;
    --bb-time-footer-btn-color: #606266;
    --bb-time-footer-btn-hover-color: #fff;
    --bb-time-footer-btn-hover-border-color: #409eff;
    --bb-time-footer-btn-hover-bg-color: #409eff;
    --bb-time-footer-btn-active-color: #409eff;
    --bb-time-footer-btn-active-border-color: #409eff;
}
```

Additionally, you can customize the appearance and behavior of the ClockPicker component by:

1. Using the `Format` property to change the time format
2. Using the `Use12Hours` property to switch between 12-hour and 24-hour formats
3. Using the `ShowSeconds` property to show or hide seconds selection
4. Using the `Size` property to adjust the component size
5. Using the `MinValue` and `MaxValue` properties to set time constraints
6. Using the `AutoClose` property to control whether the picker closes after selection
7. Applying custom CSS classes to the component using the `ClassName` property