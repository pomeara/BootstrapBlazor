# Calendar Component

## Overview
The Calendar component in BootstrapBlazor provides a visual representation of dates in a monthly or weekly format. It allows users to view and interact with dates in a traditional calendar layout, making it ideal for date selection, event scheduling, appointment management, and any application that requires date visualization. The component offers customizable styling, navigation controls, and event handling to create an intuitive and user-friendly calendar interface.

## Features
- **Month View**: Traditional monthly calendar display with days organized in a grid
- **Week View**: Weekly calendar display for focused date ranges
- **Date Navigation**: Controls for moving between months and years
- **Today Highlighting**: Visual indication of the current date
- **Date Selection**: Support for selecting individual dates
- **Custom Date Styling**: Ability to customize the appearance of specific dates
- **Header Customization**: Configurable calendar header with title and navigation buttons
- **Localization Support**: Adaptable to different languages and date formats
- **Responsive Design**: Adjusts to different screen sizes
- **Accessibility Support**: ARIA attributes for screen readers
- **Event Integration**: Ability to display and manage events on the calendar
- **Range Selection**: Support for selecting date ranges
- **Week Numbers**: Optional display of week numbers
- **Custom Cell Content**: Ability to customize the content of calendar cells

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `DateTime?` | `null` | Gets or sets the selected date |
| `ValueChanged` | `EventCallback<DateTime?>` | - | Callback when the selected date changes |
| `ViewMode` | `CalendarViewMode` | `CalendarViewMode.Month` | Sets the calendar view mode (Month or Week) |
| `FirstDayOfWeek` | `DayOfWeek` | `DayOfWeek.Sunday` | Sets the first day of the week |
| `ShowHeader` | `bool` | `true` | Whether to show the calendar header with title and navigation buttons |
| `ShowFooter` | `bool` | `false` | Whether to show the calendar footer |
| `ShowWeekNumbers` | `bool` | `false` | Whether to show week numbers |
| `ShowToday` | `bool` | `true` | Whether to highlight the current date |
| `AllowSelection` | `bool` | `true` | Whether date selection is enabled |
| `MinValue` | `DateTime?` | `null` | Minimum selectable date |
| `MaxValue` | `DateTime?` | `null` | Maximum selectable date |
| `DisabledDates` | `IEnumerable<DateTime>` | `null` | Collection of dates that cannot be selected |
| `SpecialDates` | `IEnumerable<SpecialDate>` | `null` | Collection of dates with special styling or content |
| `TitleFormat` | `string` | `"MMMM yyyy"` | Format string for the calendar title |
| `DayHeaderFormat` | `string` | `"ddd"` | Format string for day headers |
| `CellTemplate` | `RenderFragment<CalendarCellContext>` | `null` | Custom template for calendar cells |
| `HeaderTemplate` | `RenderFragment` | `null` | Custom template for the calendar header |
| `FooterTemplate` | `RenderFragment` | `null` | Custom template for the calendar footer |

## Events

| Event | Description |
| --- | --- |
| `OnDateClick` | Triggered when a date is clicked |
| `OnDateDoubleClick` | Triggered when a date is double-clicked |
| `OnViewModeChanged` | Triggered when the view mode changes |
| `OnPreviousClick` | Triggered when the previous button is clicked |
| `OnNextClick` | Triggered when the next button is clicked |
| `OnTodayClick` | Triggered when the today button is clicked |

## Usage Examples

### Example 1: Basic Calendar

```razor
<Calendar @bind-Value="@selectedDate" />

<div class="mt-3">
    @if (selectedDate.HasValue)
    {
        <p>Selected date: @selectedDate.Value.ToString("MMMM d, yyyy")</p>
    }
    else
    {
        <p>No date selected</p>
    }
</div>

@code {
    private DateTime? selectedDate = DateTime.Today;
}
```

This example shows a basic calendar with date selection, displaying the selected date below the calendar.

### Example 2: Calendar with Week Numbers and Custom First Day

```razor
<Calendar @bind-Value="@selectedDate"
          FirstDayOfWeek="DayOfWeek.Monday"
          ShowWeekNumbers="true" />

@code {
    private DateTime? selectedDate = DateTime.Today;
}
```

This example demonstrates a calendar with Monday as the first day of the week and week numbers displayed.

### Example 3: Calendar with Date Range Constraints

```razor
<Calendar @bind-Value="@appointmentDate"
          MinValue="@minDate"
          MaxValue="@maxDate"
          DisabledDates="@holidays" />

<div class="mt-3">
    <p>Please select an appointment date (excluding holidays).</p>
    @if (appointmentDate.HasValue)
    {
        <p>Your appointment is scheduled for @appointmentDate.Value.ToString("MMMM d, yyyy").</p>
    }
</div>

@code {
    private DateTime? appointmentDate;
    private DateTime minDate = DateTime.Today;
    private DateTime maxDate = DateTime.Today.AddMonths(3);
    private List<DateTime> holidays = new List<DateTime>
    {
        new DateTime(2023, 12, 25), // Christmas
        new DateTime(2024, 1, 1),   // New Year's Day
        new DateTime(2024, 1, 15)   // MLK Day
    };
}
```

This example shows a calendar with date constraints, including minimum and maximum selectable dates, as well as specific disabled dates for holidays.

### Example 4: Calendar with Special Dates

```razor
<Calendar @bind-Value="@selectedDate"
          SpecialDates="@specialDates" />

@code {
    private DateTime? selectedDate;
    private List<SpecialDate> specialDates = new List<SpecialDate>
    {
        new SpecialDate
        {
            Date = DateTime.Today.AddDays(2),
            CssClass = "important-date",
            ToolTip = "Important meeting"
        },
        new SpecialDate
        {
            Date = DateTime.Today.AddDays(5),
            CssClass = "deadline-date",
            ToolTip = "Project deadline"
        },
        new SpecialDate
        {
            Date = DateTime.Today.AddDays(10),
            CssClass = "holiday-date",
            ToolTip = "Holiday"
        }
    };
}

<style>
    .important-date {
        background-color: rgba(0, 123, 255, 0.2);
        font-weight: bold;
    }
    
    .deadline-date {
        background-color: rgba(220, 53, 69, 0.2);
        font-weight: bold;
    }
    
    .holiday-date {
        background-color: rgba(40, 167, 69, 0.2);
        font-weight: bold;
    }
</style>
```

This example demonstrates a calendar with special dates that have custom styling and tooltips.

### Example 5: Calendar with Custom Cell Template

```razor
<Calendar @bind-Value="@selectedDate">
    <CellTemplate>
        <div class="d-flex flex-column align-items-center">
            <span>@context.Date.Day</span>
            @if (events.Any(e => e.Date.Date == context.Date.Date))
            {
                <div class="event-indicator mt-1"></div>
                <small class="text-muted">@events.Count(e => e.Date.Date == context.Date.Date) events</small>
            }
        </div>
    </CellTemplate>
</Calendar>

@code {
    private DateTime? selectedDate;
    private List<EventItem> events = new List<EventItem>
    {
        new EventItem { Title = "Team Meeting", Date = DateTime.Today },
        new EventItem { Title = "Doctor Appointment", Date = DateTime.Today },
        new EventItem { Title = "Project Deadline", Date = DateTime.Today.AddDays(3) },
        new EventItem { Title = "Conference Call", Date = DateTime.Today.AddDays(5) },
        new EventItem { Title = "Birthday Party", Date = DateTime.Today.AddDays(10) }
    };
    
    public class EventItem
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
    }
}

<style>
    .event-indicator {
        width: 6px;
        height: 6px;
        border-radius: 50%;
        background-color: var(--bs-primary);
    }
</style>
```

This example shows a calendar with a custom cell template that displays event indicators for dates with events.

### Example 6: Week View Calendar

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="() => viewMode = CalendarViewMode.Month">Month View</Button>
    <Button Color="Color.Secondary" OnClick="() => viewMode = CalendarViewMode.Week">Week View</Button>
</div>

<Calendar @bind-Value="@selectedDate"
          ViewMode="@viewMode"
          OnViewModeChanged="HandleViewModeChanged" />

@code {
    private DateTime? selectedDate = DateTime.Today;
    private CalendarViewMode viewMode = CalendarViewMode.Month;
    
    private void HandleViewModeChanged(CalendarViewMode newMode)
    {
        viewMode = newMode;
        Console.WriteLine($"View mode changed to {newMode}");
    }
}
```

This example demonstrates a calendar that can switch between month and week views using buttons.

### Example 7: Calendar with Event Handling

```razor
<Calendar @bind-Value="@selectedDate"
          OnDateClick="HandleDateClick"
          OnDateDoubleClick="HandleDateDoubleClick"
          OnPreviousClick="HandlePreviousClick"
          OnNextClick="HandleNextClick"
          OnTodayClick="HandleTodayClick" />

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
    private DateTime? selectedDate;
    private List<string> eventLogs = new List<string>();
    
    private void HandleDateClick(DateTime date)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Date clicked: {date:MMMM d, yyyy}");
    }
    
    private void HandleDateDoubleClick(DateTime date)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Date double-clicked: {date:MMMM d, yyyy}");
    }
    
    private void HandlePreviousClick()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Previous button clicked");
    }
    
    private void HandleNextClick()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Next button clicked");
    }
    
    private void HandleTodayClick()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Today button clicked");
    }
    
    private void ClearLogs()
    {
        eventLogs.Clear();
    }
}
```

This example shows a calendar with event handling for various interactions, displaying an event log that records user actions.

## Customization Notes

### CSS Variables

The Calendar component can be customized using CSS variables:

```css
:root {
    --bb-calendar-padding: 0.5rem;
    --bb-calendar-header-border-bottom: 1px solid var(--bs-border-color);
    --bb-calendar-title-color: var(--bs-body-color);
    --bb-calendar-title-font-size: 1rem;
    --bb-calendar-toolbar-border: 1px solid transparent;
    --bb-calendar-toolbar-font-size: 0.875rem;
    --bb-calendar-toolbar-padding: 0.375rem 0.75rem;
    --bb-calendar-toolbar-hover-bg: var(--bs-light);
    --bb-calendar-toolbar-hover-color: var(--bs-primary);
    --bb-calendar-toolbar-hover-border-color: var(--bs-border-color);
    --bb-calendar-toolbar-focus-bg: var(--bs-light);
    --bb-calendar-toolbar-focus-color: var(--bs-primary);
    --bb-calendar-toolbar-focus-border-color: var(--bs-primary);
    --bb-calendar-toolbar-active-color: var(--bs-primary);
    --bb-calendar-toolbar-active-border-color: var(--bs-primary);
    --bb-calendar-cell-padding: 0.25rem;
    --bb-calendar-cell-height: 5rem;
    --bb-calendar-cell-hover-bg: rgba(var(--bs-primary-rgb), 0.1);
    --bb-calendar-header-padding: 0.5rem;
    --bb-calendar-today-color: var(--bs-primary);
    --bb-calendar-selected-color: #fff;
    --bb-calendar-selected-bg: var(--bs-primary);
    --bb-calendar-week-header-border-bottom: 1px solid var(--bs-border-color);
    --bb-calendar-week-header-min-width: 2rem;
    --bb-calendar-week-header-padding: 0.25rem;
    --bb-calendar-week-today-color: var(--bs-primary);
    --bb-calendar-week-today-border-color: var(--bs-primary);
    --bb-calendar-week-cell-padding: 0.5rem;
}
```

### View Modes

The Calendar component supports two view modes:

1. **Month View**: The default view showing a full month of dates
2. **Week View**: A focused view showing a single week of dates

### Date Customization

Calendar dates can be customized in several ways:

1. **Special Dates**: Use the `SpecialDates` property to apply custom styling and tooltips to specific dates
2. **Disabled Dates**: Use the `DisabledDates` property to prevent selection of specific dates
3. **Min/Max Values**: Use the `MinValue` and `MaxValue` properties to set date range constraints

### Integration with Other Components

The Calendar component works well with:

1. **DateTimePicker**: For more advanced date and time selection
2. **DateTimeRange**: For selecting date ranges
3. **Form Components**: For collecting date information in forms
4. **Modal/Dialog**: For displaying the calendar in a popup
5. **Tooltip**: For providing additional information about dates

### Accessibility Considerations

The Calendar component includes several accessibility features:

1. **ARIA Attributes**: Proper ARIA roles and attributes for screen readers
2. **Keyboard Navigation**: Support for keyboard shortcuts to navigate the calendar
3. **Focus Management**: Visual indicators for focused elements
4. **Screen Reader Announcements**: Descriptive text for calendar operations