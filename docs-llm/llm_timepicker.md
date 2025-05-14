# TimePicker Component Documentation

## Overview
The TimePicker component in BootstrapBlazor provides a specialized interface for selecting time values without dates. It offers an intuitive dropdown with hour, minute, and second selection, making it ideal for applications that require time input such as scheduling, appointment booking, time tracking, and more. The component provides a user-friendly interface with spinner controls for precise time selection.

## Features
- Time-only selection with hour, minute, and second precision
- 12-hour and 24-hour format support
- Customizable time format
- Spinner interface for intuitive time selection
- Keyboard navigation support
- Min/max time constraints
- Clear button for resetting selection
- Customizable dropdown placement
- Form validation integration
- Disabled and read-only states
- Mobile-friendly design
- Localization support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | TimeSpan? | null | Gets or sets the selected time value |
| ValueChanged | EventCallback<TimeSpan?> | - | Callback when the selected value changes |
| Format | string | "HH:mm:ss" | Format string for displaying the time |
| Use12Hours | bool | false | Whether to use 12-hour format with AM/PM |
| ShowClear | bool | true | Whether to show the clear button |
| ShowSeconds | bool | true | Whether to show seconds selection |
| MinValue | TimeSpan? | null | Minimum selectable time |
| MaxValue | TimeSpan? | null | Maximum selectable time |
| Placement | Placement | Auto | The placement of the picker dropdown |
| IsDisabled | bool | false | Whether the picker is disabled |
| IsReadOnly | bool | false | Whether the picker is read-only |
| Placeholder | string | "" | Placeholder text when no value is selected |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the selected value changes |
| OnVisibleChanged | Triggered when the dropdown visibility changes |
| OnClear | Triggered when the value is cleared |

## Usage Examples

### Example 1: Basic TimePicker

```razor
<TimePicker @bind-Value="@selectedTime" />

<div class="mt-3">
    Selected time: @(selectedTime?.ToString("hh\\:mm\\:ss") ?? "None")
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
}
```

### Example 2: 12-Hour Format with AM/PM

```razor
<TimePicker @bind-Value="@selectedTime"
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

### Example 3: Minutes-Only TimePicker

```razor
<TimePicker @bind-Value="@selectedTime"
            ShowSeconds="false"
            Format="HH:mm"
            Placeholder="Select hours and minutes" />

<div class="mt-3">
    Selected time: @(selectedTime?.ToString("hh\\:mm") ?? "None")
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
}
```

### Example 4: TimePicker with Min/Max Constraints

```razor
<TimePicker @bind-Value="@selectedTime"
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

### Example 5: TimePicker with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.AppointmentName" placeholder="Appointment name" />
        <ValidationMessage For="@(() => model.AppointmentName)" />
    </div>
    
    <div class="mb-3">
        <label>Appointment Time</label>
        <TimePicker @bind-Value="@model.AppointmentTime" />
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

### Example 6: TimePicker with Different Placements

```razor
<div class="mb-3">
    <RadioGroup @bind-Value="@selectedPlacement" TValue="Placement">
        <Radio TValue="Placement" Value="Placement.Top">Top</Radio>
        <Radio TValue="Placement" Value="Placement.Bottom">Bottom</Radio>
        <Radio TValue="Placement" Value="Placement.Left">Left</Radio>
        <Radio TValue="Placement" Value="Placement.Right">Right</Radio>
    </RadioGroup>
</div>

<div style="margin: 100px;">
    <TimePicker @bind-Value="@selectedTime"
                Placement="@selectedPlacement" />
</div>

@code {
    private TimeSpan? selectedTime = new TimeSpan(14, 30, 0);
    private Placement selectedPlacement = Placement.Bottom;
}
```

### Example 7: TimePicker in a Work Schedule Application

```razor
<div class="mb-3">
    <h4>Work Schedule</h4>
    <div class="row">
        <div class="col-md-6">
            <div class="mb-2">
                <label>Start Time</label>
                <TimePicker @bind-Value="@startTime"
                            MinValue="@new TimeSpan(8, 0, 0)"
                            MaxValue="@new TimeSpan(17, 0, 0)"
                            OnValueChanged="@UpdateDuration" />
            </div>
        </div>
        <div class="col-md-6">
            <div class="mb-2">
                <label>End Time</label>
                <TimePicker @bind-Value="@endTime"
                            MinValue="@(startTime.HasValue ? startTime.Value.Add(new TimeSpan(0, 30, 0)) : new TimeSpan(8, 30, 0))"
                            MaxValue="@new TimeSpan(18, 0, 0)"
                            OnValueChanged="@UpdateDuration" />
            </div>
        </div>
    </div>
    
    <div class="mt-3">
        <p>Work Duration: @(workDuration?.ToString("h\\:mm") ?? "Not set")</p>
    </div>
</div>

@code {
    private TimeSpan? startTime = new TimeSpan(9, 0, 0);
    private TimeSpan? endTime = new TimeSpan(17, 0, 0);
    private TimeSpan? workDuration;
    
    protected override void OnInitialized()
    {
        UpdateDuration();
    }
    
    private void UpdateDuration()
    {
        if (startTime.HasValue && endTime.HasValue)
        {
            workDuration = endTime.Value - startTime.Value;
            if (workDuration.Value.TotalMinutes < 0)
            {
                // Handle case where end time is earlier than start time (next day)
                workDuration = new TimeSpan(24, 0, 0) + workDuration.Value;
            }
        }
        else
        {
            workDuration = null;
        }
    }
}
```

## Customization Notes

The TimePicker component can be customized using the following CSS variables:

```css
:root {
    --bb-time-picker-width: 180px;
    --bb-time-picker-shadow: 0 2px 12px 0 rgba(0, 0, 0, .1);
    --bb-time-picker-footer-padding: 4px;
    --bb-time-picker-footer-btn-font-size: 0.75rem;
    --bb-time-picker-footer-btn-padding: 4px 12px;
    --bb-time-picker-footer-btn-transition: border-color .3s linear, color .3s linear;
    --bb-time-picker-spinner-item-height: 32px;
    --bb-time-picker-spinner-item-font-size: 14px;
    --bb-time-picker-spinner-item-active-font-size: 16px;
    --bb-time-picker-spinner-item-active-color: var(--primary);
}
```

Additionally, you can customize the appearance and behavior of the TimePicker component by:

1. Using the `Format` property to change the time format
2. Using the `Use12Hours` property to switch between 12-hour and 24-hour formats
3. Using the `ShowSeconds` property to show or hide seconds selection
4. Using the `Placement` property to change the dropdown placement
5. Using the `MinValue` and `MaxValue` properties to set time constraints
6. Applying custom CSS classes to the component using the `ClassName` property