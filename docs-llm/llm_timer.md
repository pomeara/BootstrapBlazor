# Timer Component

## Overview
The Timer component in BootstrapBlazor provides a flexible and customizable way to implement timing functionality in applications. It enables precise time tracking, countdown operations, and scheduled actions, making it ideal for applications that require time-based features such as countdowns, stopwatches, periodic task execution, or real-time updates.

## Features
- **Countdown Timer**: Counts down from a specified time to zero
- **Stopwatch**: Measures elapsed time from a starting point
- **Interval Timer**: Executes actions at regular intervals
- **Customizable Display**: Flexible formatting options for time display
- **Pause/Resume Control**: Ability to pause and resume timing operations
- **Reset Functionality**: Option to reset timer to initial state
- **Event Callbacks**: Triggers for timer start, tick, pause, resume, and completion
- **Auto-start Option**: Can automatically start on component initialization
- **Server and Client Timing**: Works in both server-side and client-side Blazor

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Mode` | TimerMode | TimerMode.Countdown | Sets the timer mode (Countdown, Stopwatch, or Interval) |
| `Duration` | TimeSpan | 60 seconds | The total duration for countdown mode |
| `Interval` | TimeSpan | 1 second | The interval between timer ticks |
| `Format` | string | "mm\:ss" | Format string for time display |
| `AutoStart` | bool | false | When true, timer starts automatically on initialization |
| `IsRunning` | bool | false | Indicates whether the timer is currently running |
| `IsPaused` | bool | false | Indicates whether the timer is currently paused |
| `RemainingTime` | TimeSpan | Duration | The remaining time in countdown mode |
| `ElapsedTime` | TimeSpan | 0 | The elapsed time in stopwatch mode |
| `ShowControls` | bool | true | When true, displays built-in control buttons |
| `ShowDisplay` | bool | true | When true, displays the timer value |
| `RepeatCount` | int | 0 | Number of times to repeat the timer (0 = no repeat, -1 = infinite) |
| `CompletedText` | string | "Completed" | Text to display when timer completes |

## Events

| Event | Description |
|-------|-------------|
| `OnStart` | Triggered when the timer starts |
| `OnTick` | Triggered at each timer interval |
| `OnPause` | Triggered when the timer is paused |
| `OnResume` | Triggered when the timer is resumed |
| `OnReset` | Triggered when the timer is reset |
| `OnComplete` | Triggered when the timer completes (reaches zero in countdown mode) |
| `OnRepeat` | Triggered when the timer repeats after completion |

## Usage Examples

### Example 1: Basic Countdown Timer

```html
<Timer Mode="TimerMode.Countdown"
       Duration="TimeSpan.FromMinutes(5)"
       Format="mm\:ss"
       AutoStart="true"
       OnComplete="HandleTimerComplete" />

<div class="mt-3">
    <p>Status: @timerStatus</p>
</div>
```

```csharp
@code {
    private string timerStatus = "Timer running...";
    
    private void HandleTimerComplete()
    {
        timerStatus = "Timer completed!";
        StateHasChanged();
    }
}
```

### Example 2: Stopwatch with Controls

```html
<Timer @ref="stopwatch"
       Mode="TimerMode.Stopwatch"
       Format="hh\:mm\:ss\.ff"
       ShowControls="false" />

<div class="btn-group mt-3">
    <Button Text="Start" OnClick="StartStopwatch" />
    <Button Text="Pause" OnClick="PauseStopwatch" />
    <Button Text="Resume" OnClick="ResumeStopwatch" />
    <Button Text="Reset" OnClick="ResetStopwatch" />
    <Button Text="Lap" OnClick="RecordLap" />
</div>

<div class="mt-3">
    <h5>Lap Times:</h5>
    <ul class="list-group">
        @foreach (var lap in lapTimes)
        {
            <li class="list-group-item">@lap</li>
        }
    </ul>
</div>
```

```csharp
@code {
    private Timer stopwatch;
    private List<string> lapTimes = new List<string>();
    
    private void StartStopwatch()
    {
        stopwatch.Start();
    }
    
    private void PauseStopwatch()
    {
        stopwatch.Pause();
    }
    
    private void ResumeStopwatch()
    {
        stopwatch.Resume();
    }
    
    private void ResetStopwatch()
    {
        stopwatch.Reset();
        lapTimes.Clear();
    }
    
    private void RecordLap()
    {
        if (stopwatch.IsRunning)
        {
            lapTimes.Add(stopwatch.ElapsedTime.ToString(@"hh\:mm\:ss\.ff"));
        }
    }
}
```

### Example 3: Interval Timer for Periodic Tasks

```html
<Timer @ref="intervalTimer"
       Mode="TimerMode.Interval"
       Interval="TimeSpan.FromSeconds(5)"
       OnTick="ExecutePeriodicTask"
       AutoStart="true" />

<div class="mt-3">
    <h5>Task Execution Log:</h5>
    <div class="border p-3" style="max-height: 200px; overflow-y: auto;">
        @foreach (var log in executionLogs)
        {
            <p>@log</p>
        }
    </div>
</div>

<Button Text="Toggle Timer" OnClick="ToggleTimer" />
```

```csharp
@code {
    private Timer intervalTimer;
    private List<string> executionLogs = new List<string>();
    
    protected override void OnInitialized()
    {
        executionLogs.Add($"{DateTime.Now:HH:mm:ss} - Timer initialized");
    }
    
    private void ExecutePeriodicTask()
    {
        executionLogs.Add($"{DateTime.Now:HH:mm:ss} - Task executed");
        
        // Keep only the last 10 logs
        if (executionLogs.Count > 10)
        {
            executionLogs.RemoveAt(0);
        }
        
        StateHasChanged();
    }
    
    private void ToggleTimer()
    {
        if (intervalTimer.IsRunning)
        {
            intervalTimer.Pause();
            executionLogs.Add($"{DateTime.Now:HH:mm:ss} - Timer paused");
        }
        else
        {
            intervalTimer.Resume();
            executionLogs.Add($"{DateTime.Now:HH:mm:ss} - Timer resumed");
        }
    }
}
```

### Example 4: Pomodoro Timer

```html
<div class="pomodoro-timer">
    <h3>Pomodoro Timer</h3>
    
    <Timer @ref="pomodoroTimer"
           Mode="TimerMode.Countdown"
           Duration="@currentDuration"
           Format="mm\:ss"
           OnComplete="HandlePomodoroComplete"
           ShowControls="false" />
    
    <div class="mt-3">
        <p>Current Mode: <strong>@currentMode</strong></p>
        <p>Completed Pomodoros: <strong>@completedPomodoros</strong></p>
    </div>
    
    <div class="btn-group mt-3">
        <Button Text="Start" OnClick="StartPomodoro" />
        <Button Text="Pause/Resume" OnClick="TogglePomodoro" />
        <Button Text="Reset" OnClick="ResetPomodoro" />
        <Button Text="Skip" OnClick="SkipToNext" />
    </div>
</div>
```

```csharp
@code {
    private Timer pomodoroTimer;
    private string currentMode = "Work";
    private int completedPomodoros = 0;
    private TimeSpan currentDuration = TimeSpan.FromMinutes(25);
    
    private readonly TimeSpan workDuration = TimeSpan.FromMinutes(25);
    private readonly TimeSpan shortBreakDuration = TimeSpan.FromMinutes(5);
    private readonly TimeSpan longBreakDuration = TimeSpan.FromMinutes(15);
    
    private void StartPomodoro()
    {
        pomodoroTimer.Start();
    }
    
    private void TogglePomodoro()
    {
        if (pomodoroTimer.IsRunning && !pomodoroTimer.IsPaused)
        {
            pomodoroTimer.Pause();
        }
        else
        {
            pomodoroTimer.Resume();
        }
    }
    
    private void ResetPomodoro()
    {
        pomodoroTimer.Reset();
        currentMode = "Work";
        currentDuration = workDuration;
        completedPomodoros = 0;
    }
    
    private void SkipToNext()
    {
        HandlePomodoroComplete();
    }
    
    private void HandlePomodoroComplete()
    {
        if (currentMode == "Work")
        {
            completedPomodoros++;
            
            if (completedPomodoros % 4 == 0)
            {
                // After every 4 pomodoros, take a long break
                currentMode = "Long Break";
                currentDuration = longBreakDuration;
            }
            else
            {
                currentMode = "Short Break";
                currentDuration = shortBreakDuration;
            }
        }
        else
        {
            // After a break, go back to work
            currentMode = "Work";
            currentDuration = workDuration;
        }
        
        pomodoroTimer.Reset();
        pomodoroTimer.Start();
    }
}
```

### Example 5: Countdown with Progress Bar

```html
<div class="countdown-progress">
    <Timer @ref="progressTimer"
           Mode="TimerMode.Countdown"
           Duration="TimeSpan.FromSeconds(30)"
           OnTick="UpdateProgress"
           OnComplete="HandleComplete"
           Format="ss\.f" />
    
    <div class="mt-3">
        <Progress Value="@progressValue" Color="@progressColor" ShowLabel="true" />
    </div>
    
    <div class="btn-group mt-3">
        <Button Text="Start" OnClick="StartTimer" />
        <Button Text="Reset" OnClick="ResetTimer" />
    </div>
</div>
```

```csharp
@code {
    private Timer progressTimer;
    private double progressValue = 100;
    private Color progressColor = Color.Success;
    
    private void StartTimer()
    {
        progressTimer.Start();
    }
    
    private void ResetTimer()
    {
        progressTimer.Reset();
        progressValue = 100;
        progressColor = Color.Success;
    }
    
    private void UpdateProgress()
    {
        var totalSeconds = progressTimer.Duration.TotalSeconds;
        var remainingSeconds = progressTimer.RemainingTime.TotalSeconds;
        
        progressValue = (remainingSeconds / totalSeconds) * 100;
        
        if (progressValue > 60)
        {
            progressColor = Color.Success;
        }
        else if (progressValue > 30)
        {
            progressColor = Color.Warning;
        }
        else
        {
            progressColor = Color.Danger;
        }
        
        StateHasChanged();
    }
    
    private void HandleComplete()
    {
        progressValue = 0;
    }
}
```

### Example 6: Multiple Synchronized Timers

```html
<div class="synchronized-timers">
    <div class="row">
        <div class="col-md-4">
            <div class="card">
                <div class="card-header">Timer 1 (Master)</div>
                <div class="card-body">
                    <Timer @ref="masterTimer"
                           Mode="TimerMode.Countdown"
                           Duration="TimeSpan.FromMinutes(2)"
                           OnTick="SyncTimers"
                           OnComplete="HandleMasterComplete"
                           Format="mm\:ss" />
                </div>
            </div>
        </div>
        
        <div class="col-md-4">
            <div class="card">
                <div class="card-header">Timer 2 (Slave)</div>
                <div class="card-body">
                    <Timer @ref="slaveTimer1"
                           Mode="TimerMode.Countdown"
                           Duration="TimeSpan.FromMinutes(2)"
                           Format="mm\:ss"
                           ShowControls="false" />
                </div>
            </div>
        </div>
        
        <div class="col-md-4">
            <div class="card">
                <div class="card-header">Timer 3 (Slave)</div>
                <div class="card-body">
                    <Timer @ref="slaveTimer2"
                           Mode="TimerMode.Countdown"
                           Duration="TimeSpan.FromMinutes(2)"
                           Format="mm\:ss"
                           ShowControls="false" />
                </div>
            </div>
        </div>
    </div>
    
    <div class="mt-3">
        <Button Text="Start All" OnClick="StartAllTimers" />
        <Button Text="Reset All" OnClick="ResetAllTimers" />
    </div>
</div>
```

```csharp
@code {
    private Timer masterTimer;
    private Timer slaveTimer1;
    private Timer slaveTimer2;
    
    private void StartAllTimers()
    {
        masterTimer.Start();
        slaveTimer1.Start();
        slaveTimer2.Start();
    }
    
    private void ResetAllTimers()
    {
        masterTimer.Reset();
        slaveTimer1.Reset();
        slaveTimer2.Reset();
    }
    
    private void SyncTimers()
    {
        // Ensure all timers are synchronized with the master
        slaveTimer1.RemainingTime = masterTimer.RemainingTime;
        slaveTimer2.RemainingTime = masterTimer.RemainingTime;
    }
    
    private void HandleMasterComplete()
    {
        // Ensure all timers complete together
        slaveTimer1.Reset();
        slaveTimer2.Reset();
    }
}
```

### Example 7: Timer with Custom Display

```html
<div class="custom-timer-display">
    <Timer @ref="customTimer"
           Mode="TimerMode.Countdown"
           Duration="TimeSpan.FromMinutes(10)"
           OnTick="UpdateCustomDisplay"
           ShowDisplay="false" />
    
    <div class="timer-display">
        <div class="time-unit">
            <div class="time-value">@minutes</div>
            <div class="time-label">Minutes</div>
        </div>
        <div class="time-separator">:</div>
        <div class="time-unit">
            <div class="time-value">@seconds</div>
            <div class="time-label">Seconds</div>
        </div>
    </div>
    
    <div class="timer-progress mt-3">
        <div class="progress-bar" style="width: @progressWidth%"></div>
    </div>
    
    <div class="btn-group mt-3">
        <Button Text="Start" OnClick="StartCustomTimer" />
        <Button Text="Pause" OnClick="PauseCustomTimer" />
        <Button Text="Reset" OnClick="ResetCustomTimer" />
    </div>
</div>
```

```csharp
@code {
    private Timer customTimer;
    private string minutes = "10";
    private string seconds = "00";
    private double progressWidth = 100;
    
    private void StartCustomTimer()
    {
        customTimer.Start();
    }
    
    private void PauseCustomTimer()
    {
        if (customTimer.IsRunning && !customTimer.IsPaused)
        {
            customTimer.Pause();
        }
        else
        {
            customTimer.Resume();
        }
    }
    
    private void ResetCustomTimer()
    {
        customTimer.Reset();
        minutes = "10";
        seconds = "00";
        progressWidth = 100;
    }
    
    private void UpdateCustomDisplay()
    {
        var remaining = customTimer.RemainingTime;
        minutes = remaining.Minutes.ToString("D2");
        seconds = remaining.Seconds.ToString("D2");
        
        var totalSeconds = customTimer.Duration.TotalSeconds;
        var remainingSeconds = remaining.TotalSeconds;
        progressWidth = (remainingSeconds / totalSeconds) * 100;
        
        StateHasChanged();
    }
}
```

## CSS Customization

The Timer component can be customized using CSS variables and classes:

```css
/* Custom styles for Timer component */
.bb-timer {
    /* Component container */
    font-family: 'Roboto Mono', monospace;
    padding: 1rem;
    border-radius: 0.5rem;
    background-color: var(--bs-light);
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.bb-timer-display {
    /* Timer display */
    font-size: 2.5rem;
    font-weight: bold;
    text-align: center;
    color: var(--bs-primary);
}

.bb-timer-controls {
    /* Control buttons container */
    display: flex;
    justify-content: center;
    gap: 0.5rem;
    margin-top: 1rem;
}

.bb-timer-completed {
    /* Styles when timer completes */
    color: var(--bs-success);
    animation: pulse 1s infinite;
}

@keyframes pulse {
    0% { opacity: 1; }
    50% { opacity: 0.5; }
    100% { opacity: 1; }
}

/* Custom timer display example */
.custom-timer-display {
    text-align: center;
}

.timer-display {
    display: flex;
    justify-content: center;
    align-items: center;
    font-family: 'Roboto Mono', monospace;
}

.time-unit {
    display: flex;
    flex-direction: column;
    align-items: center;
}

.time-value {
    font-size: 3rem;
    font-weight: bold;
    color: var(--bs-primary);
}

.time-label {
    font-size: 0.8rem;
    text-transform: uppercase;
    color: var(--bs-secondary);
}

.time-separator {
    font-size: 3rem;
    margin: 0 0.5rem;
    color: var(--bs-primary);
}

.timer-progress {
    height: 0.5rem;
    background-color: var(--bs-light);
    border-radius: 0.25rem;
    overflow: hidden;
}

.progress-bar {
    height: 100%;
    background-color: var(--bs-primary);
    transition: width 1s linear;
}
```

## JavaScript Interop

The Timer component uses JavaScript interop for precise timing. You can extend its functionality by using the following methods:

```csharp
// Start the timer
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.timer.start", elementRef);

// Pause the timer
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.timer.pause", elementRef);

// Resume the timer
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.timer.resume", elementRef);

// Reset the timer
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.timer.reset", elementRef);

// Get current timer state
var state = await JSRuntime.InvokeAsync<TimerState>("bootstrapBlazor.timer.getState", elementRef);

// Set timer options
await JSRuntime.InvokeVoidAsync("bootstrapBlazor.timer.setOptions", elementRef, options);
```

## Accessibility

The Timer component is designed with accessibility in mind:

- Provides ARIA attributes for screen reader compatibility
- Supports keyboard navigation for control buttons
- Includes high-contrast visual indicators
- Offers audible notifications for timer completion

## Browser Compatibility

The Timer component is compatible with all modern browsers:

- Chrome
- Firefox
- Edge
- Safari
- Opera

For older browsers, the component includes fallback mechanisms to ensure basic functionality.

## Integration with Other Components

The Timer component can be integrated with various other BootstrapBlazor components:

- Use with Progress components for visual time tracking
- Combine with Modal or Dialog for timed popups
- Integrate with Form components for timed form submissions
- Pair with Alert or Toast components for timer notifications
- Use with Table for timed data refreshes