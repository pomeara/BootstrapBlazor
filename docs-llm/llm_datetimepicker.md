# DateTimePicker Component Documentation

## Overview
The DateTimePicker component in BootstrapBlazor provides an intuitive interface for users to select dates and times. It offers a calendar-style date picker and time selection controls, making it easy for users to input date and time values accurately. This component is essential for forms that require date and time inputs such as scheduling, booking systems, event management, and more.

## Features
- Date and time selection in a single component
- Configurable date and time formats
- Range selection support
- Min/max date constraints
- Today button for quick navigation
- Clear button for resetting selection
- Localization support
- Keyboard navigation
- Customizable placement of the dropdown
- Mobile-friendly design
- Form validation integration

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | DateTime? | null | Gets or sets the selected date and time value |
| ValueChanged | EventCallback<DateTime?> | - | Callback when the selected value changes |
| Format | string | "yyyy-MM-dd HH:mm:ss" | Format string for displaying the date and time |
| ShowToday | bool | true | Whether to show the today button |
| ShowClear | bool | true | Whether to show the clear button |
| ShowTime | bool | true | Whether to show time selection controls |
| ShowWeekNumber | bool | false | Whether to show week numbers in the calendar |
| AutoClose | bool | true | Whether to automatically close the picker after selection |
| AllowNull | bool | true | Whether to allow null (empty) values |
| MinValue | DateTime? | null | Minimum selectable date |
| MaxValue | DateTime? | null | Maximum selectable date |
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
| OnToday | Triggered when the today button is clicked |

## Usage Examples

### Example 1: Basic DateTimePicker

```razor
<DateTimePicker @bind-Value="@selectedDateTime" />

<div class="mt-3">
    Selected date and time: @(selectedDateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "None")
</div>

@code {
    private DateTime? selectedDateTime = DateTime.Now;
}
```

### Example 2: Date-Only Picker

```razor
<DateTimePicker @bind-Value="@selectedDate"
                ShowTime="false"
                Format="yyyy-MM-dd"
                Placeholder="Select a date" />

<div class="mt-3">
    Selected date: @(selectedDate?.ToString("yyyy-MM-dd") ?? "None")
</div>

@code {
    private DateTime? selectedDate = DateTime.Today;
}
```

### Example 3: Time-Only Picker

```razor
<DateTimePicker @bind-Value="@selectedTime"
                ShowTime="true"
                Format="HH:mm:ss"
                Placeholder="Select a time" />

<div class="mt-3">
    Selected time: @(selectedTime?.ToString("HH:mm:ss") ?? "None")
</div>

@code {
    private DateTime? selectedTime = DateTime.Now;
}
```

### Example 4: DateTimePicker with Min/Max Constraints

```razor
<DateTimePicker @bind-Value="@selectedDateTime"
                MinValue="@minDate"
                MaxValue="@maxDate"
                Placeholder="Select a date within range" />

<div class="mt-3">
    <p>Selected date: @(selectedDateTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "None")</p>
    <p>Valid range: @minDate.ToString("yyyy-MM-dd") to @maxDate.ToString("yyyy-MM-dd")</p>
</div>

@code {
    private DateTime? selectedDateTime;
    private DateTime minDate = DateTime.Today.AddDays(-7);
    private DateTime maxDate = DateTime.Today.AddDays(30);
}
```

### Example 5: DateTimePicker with Custom Format

```razor
<div class="mb-3">
    <RadioGroup @bind-Value="@selectedFormat" TValue="string">
        <Radio TValue="string" Value="yyyy-MM-dd HH:mm:ss">Default (yyyy-MM-dd HH:mm:ss)</Radio>
        <Radio TValue="string" Value="MM/dd/yyyy h:mm tt">US Format (MM/dd/yyyy h:mm tt)</Radio>
        <Radio TValue="string" Value="dd.MM.yyyy HH:mm">European Format (dd.MM.yyyy HH:mm)</Radio>
        <Radio TValue="string" Value="yyyy年MM月dd日 HH時mm分">Japanese Format (yyyy年MM月dd日 HH時mm分)</Radio>
    </RadioGroup>
</div>

<DateTimePicker @bind-Value="@selectedDateTime"
                Format="@selectedFormat" />

<div class="mt-3">
    Selected date and time: @(selectedDateTime?.ToString(selectedFormat) ?? "None")
</div>

@code {
    private DateTime? selectedDateTime = DateTime.Now;
    private string selectedFormat = "yyyy-MM-dd HH:mm:ss";
}
```

### Example 6: DateTimePicker with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.EventName" placeholder="Event name" />
        <ValidationMessage For="@(() => model.EventName)" />
    </div>
    
    <div class="mb-3">
        <label>Start Date and Time</label>
        <DateTimePicker @bind-Value="@model.StartDateTime" />
        <ValidationMessage For="@(() => model.StartDateTime)" />
    </div>
    
    <div class="mb-3">
        <label>End Date and Time</label>
        <DateTimePicker @bind-Value="@model.EndDateTime"
                        MinValue="@model.StartDateTime" />
        <ValidationMessage For="@(() => model.EndDateTime)" />
    </div>
    
    <Button Type="ButtonType.Submit">Save Event</Button>
</ValidateForm>

@code {
    private EventModel model = new EventModel
    {
        EventName = "",
        StartDateTime = DateTime.Now.AddHours(1),
        EndDateTime = DateTime.Now.AddHours(2)
    };
    
    private void HandleValidSubmit()
    {
        // Save the event
        Console.WriteLine($"Event saved: {model.EventName}");
        Console.WriteLine($"Start: {model.StartDateTime}");
        Console.WriteLine($"End: {model.EndDateTime}");
    }
    
    public class EventModel
    {
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }
        
        [Required(ErrorMessage = "Start date and time is required")]
        public DateTime? StartDateTime { get; set; }
        
        [Required(ErrorMessage = "End date and time is required")]
        [DateGreaterThan(nameof(StartDateTime), ErrorMessage = "End date must be after start date")]
        public DateTime? EndDateTime { get; set; }
    }
    
    // Custom validation attribute
    public class DateGreaterThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        
        public DateGreaterThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
            if (property == null)
                return new ValidationResult($"Unknown property: {_comparisonProperty}");
                
            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as DateTime?;
            var currentValue = value as DateTime?;
            
            if (currentValue.HasValue && comparisonValue.HasValue && currentValue.Value <= comparisonValue.Value)
                return new ValidationResult(ErrorMessage);
                
            return ValidationResult.Success;
        }
    }
}
```

### Example 7: DateTimePicker with Different Placements

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
    <DateTimePicker @bind-Value="@selectedDateTime"
                    Placement="@selectedPlacement" />
</div>

@code {
    private DateTime? selectedDateTime = DateTime.Now;
    private Placement selectedPlacement = Placement.Bottom;
}
```

## Customization Notes

The DateTimePicker component can be customized using the following CSS variables:

```css
:root {
    --bb-datetimepicker-width: 280px;
    --bb-datetimepicker-height: auto;
    --bb-datetimepicker-panel-background: #fff;
    --bb-datetimepicker-panel-border-color: rgba(0, 0, 0, 0.15);
    --bb-datetimepicker-panel-border-radius: 0.25rem;
    --bb-datetimepicker-panel-box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    --bb-datetimepicker-header-height: 40px;
    --bb-datetimepicker-cell-size: 30px;
    --bb-datetimepicker-cell-hover-background: rgba(0, 123, 255, 0.1);
    --bb-datetimepicker-cell-active-background: var(--primary);
    --bb-datetimepicker-cell-active-color: #fff;
    --bb-datetimepicker-cell-today-border-color: var(--primary);
    --bb-datetimepicker-cell-disabled-color: #ccc;
    --bb-datetimepicker-time-column-height: 224px;
    --bb-datetimepicker-time-cell-height: 28px;
}
```

Additionally, you can customize the appearance and behavior of the DateTimePicker component by:

1. Using the `Format` property to change the date and time format
2. Using the `ShowTime` property to show or hide time selection
3. Using the `ShowWeekNumber` property to show or hide week numbers
4. Using the `Placement` property to change the dropdown placement
5. Using the `MinValue` and `MaxValue` properties to set date constraints
6. Applying custom CSS classes to the component using the `ClassName` property