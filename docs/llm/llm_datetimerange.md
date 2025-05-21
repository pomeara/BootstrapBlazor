# DateTimeRange Component Documentation

## Overview
The DateTimeRange component in BootstrapBlazor provides a specialized interface for selecting a range of dates or date-times. It allows users to specify start and end dates/times in a single control, making it ideal for filtering data by date ranges, scheduling events with durations, or any scenario requiring a time period selection. The component offers a user-friendly calendar interface with two selectable dates and optional time selection.

## Features
- Single control for selecting both start and end dates/times
- Configurable date and time formats
- Min/max date constraints
- Customizable separator between start and end values
- Today button for quick navigation
- Clear button for resetting selection
- Localization support
- Keyboard navigation
- Customizable placement of the dropdown
- Mobile-friendly design
- Form validation integration
- Range validation (ensuring end date is after start date)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | DateTimeRangeValue | null | Gets or sets the selected date range value |
| ValueChanged | EventCallback<DateTimeRangeValue> | - | Callback when the selected value changes |
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
| Separator | string | " - " | Separator between start and end date/time values |
| StartPlaceholder | string | "Start date" | Placeholder for the start date input |
| EndPlaceholder | string | "End date" | Placeholder for the end date input |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the selected value changes |
| OnVisibleChanged | Triggered when the dropdown visibility changes |
| OnClear | Triggered when the value is cleared |
| OnToday | Triggered when the today button is clicked |

## Usage Examples

### Example 1: Basic DateTimeRange

```razor
<DateTimeRange @bind-Value="@dateRange" />

<div class="mt-3">
    @if (dateRange != null)
    {
        <p>Start: @(dateRange.Start?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Not set")</p>
        <p>End: @(dateRange.End?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Not set")</p>
    }
    else
    {
        <p>No date range selected</p>
    }
</div>

@code {
    private DateTimeRangeValue dateRange = new DateTimeRangeValue
    {
        Start = DateTime.Today,
        End = DateTime.Today.AddDays(7)
    };
}
```

### Example 2: Date-Only Range

```razor
<DateTimeRange @bind-Value="@dateRange"
               ShowTime="false"
               Format="yyyy-MM-dd"
               StartPlaceholder="Start date"
               EndPlaceholder="End date" />

<div class="mt-3">
    @if (dateRange != null && dateRange.Start.HasValue && dateRange.End.HasValue)
    {
        <p>Selected range: @dateRange.Start.Value.ToString("yyyy-MM-dd") to @dateRange.End.Value.ToString("yyyy-MM-dd")</p>
        <p>Duration: @((dateRange.End.Value - dateRange.Start.Value).TotalDays) days</p>
    }
    else
    {
        <p>No date range selected</p>
    }
</div>

@code {
    private DateTimeRangeValue dateRange = new DateTimeRangeValue
    {
        Start = DateTime.Today,
        End = DateTime.Today.AddDays(7)
    };
}
```

### Example 3: DateTimeRange with Min/Max Constraints

```razor
<DateTimeRange @bind-Value="@dateRange"
               MinValue="@minDate"
               MaxValue="@maxDate"
               StartPlaceholder="Start date (within range)"
               EndPlaceholder="End date (within range)" />

<div class="mt-3">
    <p>Selected range: 
        @(dateRange?.Start?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Not set") to 
        @(dateRange?.End?.ToString("yyyy-MM-dd HH:mm:ss") ?? "Not set")
    </p>
    <p>Valid range: @minDate.ToString("yyyy-MM-dd") to @maxDate.ToString("yyyy-MM-dd")</p>
</div>

@code {
    private DateTimeRangeValue dateRange = new DateTimeRangeValue();
    private DateTime minDate = DateTime.Today.AddDays(-30);
    private DateTime maxDate = DateTime.Today.AddDays(60);
    
    protected override void OnInitialized()
    {
        dateRange.Start = DateTime.Today;
        dateRange.End = DateTime.Today.AddDays(7);
    }
}
```

### Example 4: Custom Format and Separator

```razor
<DateTimeRange @bind-Value="@dateRange"
               Format="MM/dd/yyyy h:mm tt"
               Separator=" to "
               StartPlaceholder="From"
               EndPlaceholder="To" />

<div class="mt-3">
    @if (dateRange != null && dateRange.Start.HasValue && dateRange.End.HasValue)
    {
        <p>Selected range: @dateRange.Start.Value.ToString("MM/dd/yyyy h:mm tt") to @dateRange.End.Value.ToString("MM/dd/yyyy h:mm tt")</p>
    }
    else
    {
        <p>No date range selected</p>
    }
</div>

@code {
    private DateTimeRangeValue dateRange = new DateTimeRangeValue
    {
        Start = DateTime.Now,
        End = DateTime.Now.AddDays(3).AddHours(2)
    };
}
```

### Example 5: DateTimeRange with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.EventName" placeholder="Event name" />
        <ValidationMessage For="@(() => model.EventName)" />
    </div>
    
    <div class="mb-3">
        <label>Event Duration</label>
        <DateTimeRange @bind-Value="@model.EventDuration"
                       ShowTime="true" />
        <ValidationMessage For="@(() => model.EventDuration)" />
    </div>
    
    <Button Type="ButtonType.Submit">Schedule Event</Button>
</ValidateForm>

@code {
    private EventModel model = new EventModel
    {
        EventName = "",
        EventDuration = new DateTimeRangeValue
        {
            Start = DateTime.Now.AddHours(1),
            End = DateTime.Now.AddHours(3)
        }
    };
    
    private void HandleValidSubmit()
    {
        // Save the event
        Console.WriteLine($"Event scheduled: {model.EventName}");
        Console.WriteLine($"Start: {model.EventDuration.Start}");
        Console.WriteLine($"End: {model.EventDuration.End}");
    }
    
    public class EventModel
    {
        [Required(ErrorMessage = "Event name is required")]
        public string EventName { get; set; }
        
        [Required(ErrorMessage = "Event duration is required")]
        [ValidDateTimeRange(ErrorMessage = "Please select a valid date range")]
        public DateTimeRangeValue EventDuration { get; set; }
    }
    
    // Custom validation attribute
    public class ValidDateTimeRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTimeRangeValue range)
            {
                if (!range.Start.HasValue || !range.End.HasValue)
                    return new ValidationResult("Both start and end dates are required");
                    
                if (range.End.Value <= range.Start.Value)
                    return new ValidationResult("End date must be after start date");
                    
                return ValidationResult.Success;
            }
            
            return new ValidationResult("Invalid date range value");
        }
    }
}
```

### Example 6: DateTimeRange for Reporting

```razor
<div class="card">
    <div class="card-header">Sales Report</div>
    <div class="card-body">
        <div class="row mb-3">
            <div class="col-md-6">
                <label>Date Range</label>
                <DateTimeRange @bind-Value="@reportRange"
                               ShowTime="false"
                               Format="yyyy-MM-dd" />
            </div>
            <div class="col-md-6 d-flex align-items-end">
                <Button Color="Color.Primary" OnClick="@GenerateReport">Generate Report</Button>
            </div>
        </div>
        
        @if (showReport)
        {
            <div class="mt-4">
                <h5>Sales Report: @reportRange.Start?.ToString("yyyy-MM-dd") to @reportRange.End?.ToString("yyyy-MM-dd")</h5>
                <Table TItem="SalesData" Items="@salesData" IsStriped="true" IsBordered="true">
                    <TableColumns>
                        <TableColumn @bind-Field="@context.Date" Width="180" Text="Date" />
                        <TableColumn @bind-Field="@context.Product" Text="Product" />
                        <TableColumn @bind-Field="@context.Amount" Width="180" Text="Amount" FormatString="{0:C2}" />
                    </TableColumns>
                </Table>
                
                <div class="mt-3">
                    <h6>Total Sales: @salesData.Sum(s => s.Amount).ToString("C2")</h6>
                </div>
            </div>
        }
    </div>
</div>

@code {
    private DateTimeRangeValue reportRange = new DateTimeRangeValue
    {
        Start = DateTime.Today.AddDays(-30),
        End = DateTime.Today
    };
    
    private List<SalesData> salesData = new List<SalesData>();
    private bool showReport = false;
    
    private void GenerateReport()
    {
        if (reportRange.Start.HasValue && reportRange.End.HasValue)
        {
            // In a real application, this would fetch data from a service
            // Here we're generating sample data
            salesData = GenerateSampleData(reportRange.Start.Value, reportRange.End.Value);
            showReport = true;
        }
    }
    
    private List<SalesData> GenerateSampleData(DateTime start, DateTime end)
    {
        var random = new Random();
        var products = new[] { "Product A", "Product B", "Product C", "Product D" };
        var result = new List<SalesData>();
        
        for (var date = start; date <= end; date = date.AddDays(1))
        {
            foreach (var product in products)
            {
                if (random.Next(0, 3) > 0) // 2/3 chance to have a sale for this product on this day
                {
                    result.Add(new SalesData
                    {
                        Date = date,
                        Product = product,
                        Amount = Math.Round(random.NextDouble() * 1000, 2)
                    });
                }
            }
        }
        
        return result.OrderBy(s => s.Date).ThenBy(s => s.Product).ToList();
    }
    
    public class SalesData
    {
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
    }
}
```

### Example 7: Predefined Date Ranges

```razor
<div class="mb-3">
    <label>Quick Select:</label>
    <div class="btn-group">
        <Button OnClick="@(() => SetRange("today"))">Today</Button>
        <Button OnClick="@(() => SetRange("yesterday"))">Yesterday</Button>
        <Button OnClick="@(() => SetRange("thisWeek"))">This Week</Button>
        <Button OnClick="@(() => SetRange("lastWeek"))">Last Week</Button>
        <Button OnClick="@(() => SetRange("thisMonth"))">This Month</Button>
        <Button OnClick="@(() => SetRange("lastMonth"))">Last Month</Button>
        <Button OnClick="@(() => SetRange("last30Days"))">Last 30 Days</Button>
        <Button OnClick="@(() => SetRange("last90Days"))">Last 90 Days</Button>
    </div>
</div>

<DateTimeRange @bind-Value="@dateRange"
               ShowTime="false"
               Format="yyyy-MM-dd" />

<div class="mt-3">
    @if (dateRange != null && dateRange.Start.HasValue && dateRange.End.HasValue)
    {
        <p>Selected range: @dateRange.Start.Value.ToString("yyyy-MM-dd") to @dateRange.End.Value.ToString("yyyy-MM-dd")</p>
        <p>Duration: @((dateRange.End.Value - dateRange.Start.Value).TotalDays + 1) days</p>
    }
    else
    {
        <p>No date range selected</p>
    }
</div>

@code {
    private DateTimeRangeValue dateRange = new DateTimeRangeValue
    {
        Start = DateTime.Today,
        End = DateTime.Today
    };
    
    private void SetRange(string rangeType)
    {
        var today = DateTime.Today;
        
        switch (rangeType)
        {
            case "today":
                dateRange.Start = today;
                dateRange.End = today;
                break;
                
            case "yesterday":
                dateRange.Start = today.AddDays(-1);
                dateRange.End = today.AddDays(-1);
                break;
                
            case "thisWeek":
                var firstDayOfWeek = today.AddDays(-(int)today.DayOfWeek);
                dateRange.Start = firstDayOfWeek;
                dateRange.End = firstDayOfWeek.AddDays(6);
                break;
                
            case "lastWeek":
                var lastWeekStart = today.AddDays(-(int)today.DayOfWeek - 7);
                dateRange.Start = lastWeekStart;
                dateRange.End = lastWeekStart.AddDays(6);
                break;
                
            case "thisMonth":
                dateRange.Start = new DateTime(today.Year, today.Month, 1);
                dateRange.End = dateRange.Start.Value.AddMonths(1).AddDays(-1);
                break;
                
            case "lastMonth":
                var firstDayOfLastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);
                dateRange.Start = firstDayOfLastMonth;
                dateRange.End = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
                break;
                
            case "last30Days":
                dateRange.Start = today.AddDays(-29);
                dateRange.End = today;
                break;
                
            case "last90Days":
                dateRange.Start = today.AddDays(-89);
                dateRange.End = today;
                break;
        }
    }
}
```

## Customization Notes

The DateTimeRange component can be customized using the following CSS variables:

```css
:root {
    --bb-datetimerange-width: 280px;
    --bb-datetimerange-height: auto;
    --bb-datetimerange-panel-background: #fff;
    --bb-datetimerange-panel-border-color: rgba(0, 0, 0, 0.15);
    --bb-datetimerange-panel-border-radius: 0.25rem;
    --bb-datetimerange-panel-box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
    --bb-datetimerange-header-height: 40px;
    --bb-datetimerange-cell-size: 30px;
    --bb-datetimerange-cell-hover-background: rgba(0, 123, 255, 0.1);
    --bb-datetimerange-cell-active-background: var(--primary);
    --bb-datetimerange-cell-active-color: #fff;
    --bb-datetimerange-cell-in-range-background: rgba(0, 123, 255, 0.1);
    --bb-datetimerange-cell-today-border-color: var(--primary);
    --bb-datetimerange-cell-disabled-color: #ccc;
    --bb-datetimerange-time-column-height: 224px;
    --bb-datetimerange-time-cell-height: 28px;
}
```

Additionally, you can customize the appearance and behavior of the DateTimeRange component by:

1. Using the `Format` property to change the date and time format
2. Using the `ShowTime` property to show or hide time selection
3. Using the `ShowWeekNumber` property to show or hide week numbers
4. Using the `Placement` property to change the dropdown placement
5. Using the `MinValue` and `MaxValue` properties to set date constraints
6. Using the `Separator` property to customize the separator between start and end dates
7. Using the `StartPlaceholder` and `EndPlaceholder` properties to customize placeholder text
8. Applying custom CSS classes to the component using the `ClassName` property