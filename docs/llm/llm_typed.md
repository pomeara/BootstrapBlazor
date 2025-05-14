# Typed Component

## Overview
The Typed component in BootstrapBlazor provides a typewriter-like animation effect for displaying text. It simulates the appearance of text being typed in real-time, creating an engaging and dynamic way to present content. This component is ideal for creating attention-grabbing headlines, interactive introductions, command-line simulations, or any scenario where you want to add a dynamic typing effect to your text content.

## Features
- **Typing Animation**: Simulates text being typed character by character
- **Customizable Speed**: Adjustable typing and deletion speeds
- **Multiple Strings**: Support for cycling through multiple text strings
- **Cursor Customization**: Options for cursor style, blinking, and color
- **Loop Control**: Options for continuous looping or stopping after completion
- **Delay Settings**: Configurable delays before typing starts and between strings
- **Backspace Effect**: Option to delete text before typing the next string
- **Smart Backspace**: Ability to delete only the different characters when changing strings
- **HTML Support**: Option to parse and render HTML within typed content
- **Event Callbacks**: Events for typing start, complete, and loop complete
- **Pause/Resume Control**: Methods to control the animation programmatically
- **Accessibility Support**: Screen reader compatibility with proper ARIA attributes

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Strings` | `IEnumerable<string>` | `[]` | Collection of strings to be typed in sequence. |
| `TypeSpeed` | `int` | `30` | Typing speed in milliseconds per character. |
| `BackSpeed` | `int` | `50` | Backspacing speed in milliseconds per character. |
| `StartDelay` | `int` | `0` | Delay before typing starts in milliseconds. |
| `BackDelay` | `int` | `700` | Delay before backspacing in milliseconds. |
| `Loop` | `bool` | `false` | Whether to loop through the strings continuously. |
| `LoopCount` | `int?` | `null` | Number of times to loop through the strings (null for infinite). |
| `ShowCursor` | `bool` | `true` | Whether to show the cursor. |
| `CursorChar` | `string` | `"|"` | Character to use as the cursor. |
| `CursorBlinking` | `bool` | `true` | Whether the cursor should blink. |
| `SmartBackspace` | `bool` | `true` | Whether to only backspace what doesn't match the previous string. |
| `Shuffle` | `bool` | `false` | Whether to shuffle the strings randomly. |
| `ParseHtml` | `bool` | `false` | Whether to parse HTML in the strings. |
| `FadeOut` | `bool` | `false` | Whether to fade out the text instead of backspacing. |
| `FadeOutDelay` | `int` | `500` | Delay before fading out in milliseconds. |
| `FadeOutDuration` | `int` | `500` | Duration of the fade out effect in milliseconds. |
| `CursorColor` | `string` | `null` | Color of the cursor (CSS color value). |
| `TextColor` | `string` | `null` | Color of the typed text (CSS color value). |
| `FontSize` | `string` | `null` | Font size of the typed text (CSS font-size value). |
| `FontFamily` | `string` | `null` | Font family of the typed text (CSS font-family value). |

## Events

| Event | Description |
| --- | --- |
| `OnTypingStart` | Triggered when typing starts for a new string. |
| `OnTypingComplete` | Triggered when typing completes for a string. |
| `OnBackspaceStart` | Triggered when backspacing starts. |
| `OnBackspaceComplete` | Triggered when backspacing completes. |
| `OnComplete` | Triggered when the entire typing sequence completes (all strings). |
| `OnLoopComplete` | Triggered when a loop through all strings completes. |
| `OnStop` | Triggered when the typing animation is stopped. |
| `OnStart` | Triggered when the typing animation starts or resumes. |
| `OnReset` | Triggered when the typing animation is reset. |

## Usage Examples

### Example 1: Basic Typing Effect

```razor
<Typed Strings="@(new[] { "Welcome to BootstrapBlazor", "A powerful UI component library", "For Blazor applications" })" />
```

This example creates a basic typing effect that cycles through three different strings with default animation settings.

### Example 2: Custom Typing Speed and Cursor

```razor
<Typed Strings="@(new[] { "Fast typing", "Slow typing", "Custom cursor" })"
       TypeSpeed="10"
       BackSpeed="5"
       CursorChar="_"
       CursorBlinking="true"
       CursorColor="#ff0000"
       TextColor="#0066cc"
       FontSize="24px"
       Loop="true" />
```

This example customizes the typing speed, cursor appearance, and text styling while enabling continuous looping.

### Example 3: HTML Content with Smart Backspace

```razor
<Typed Strings="@(new[] { 
           "<strong>Bold text</strong> with formatting", 
           "<em>Italic text</em> with formatting", 
           "<span style='color: green;'>Colored text</span> with formatting" 
       })"
       ParseHtml="true"
       SmartBackspace="true"
       BackDelay="1000"
       StartDelay="500" />
```

This example demonstrates typing HTML-formatted content with smart backspacing, which only deletes characters that differ between strings.

### Example 4: Typing Effect with Event Handling

```razor
<Typed @ref="typedComponent"
       Strings="@typedStrings"
       Loop="true"
       LoopCount="3"
       OnTypingStart="HandleTypingStart"
       OnTypingComplete="HandleTypingComplete"
       OnLoopComplete="HandleLoopComplete"
       OnComplete="HandleComplete" />

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="StartTyping">Start</Button>
    <Button Color="Color.Secondary" OnClick="StopTyping">Stop</Button>
    <Button Color="Color.Success" OnClick="ResetTyping">Reset</Button>
</div>

<div class="mt-3">
    <h5>Event Log</h5>
    <div class="border p-3 bg-light" style="max-height: 200px; overflow-y: auto;">
        @foreach (var log in eventLogs.AsEnumerable().Reverse())
        {
            <div class="mb-1">@log</div>
        }
    </div>
</div>

@code {
    private Typed typedComponent;
    private string[] typedStrings = new[] { 
        "This is the first string", 
        "This is the second string", 
        "This is the third string" 
    };
    private List<string> eventLogs = new List<string>();
    
    private void HandleTypingStart(string text)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Started typing: {text}");
    }
    
    private void HandleTypingComplete(string text)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Completed typing: {text}");
    }
    
    private void HandleLoopComplete()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Loop completed");
    }
    
    private void HandleComplete()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] All typing completed");
    }
    
    private void StartTyping()
    {
        typedComponent.Start();
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Typing started manually");
    }
    
    private void StopTyping()
    {
        typedComponent.Stop();
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Typing stopped manually");
    }
    
    private void ResetTyping()
    {
        typedComponent.Reset();
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Typing reset manually");
    }
}
```

This example shows how to handle various typing events and control the animation programmatically using component methods.

### Example 5: Command Line Simulation

```razor
<div class="command-line-container p-3 bg-dark text-light">
    <div class="command-prompt mb-2">$ <Typed Strings="@(new[] { "npm install bootstrap-blazor", "dotnet add package BootstrapBlazor" })"
                         TypeSpeed="50"
                         BackSpeed="10"
                         Loop="true"
                         BackDelay="2000"
                         StartDelay="1000"
                         CursorChar="█"
                         CursorBlinking="true"
                         TextColor="#00ff00" /></div>
    
    <div class="command-output" style="font-family: monospace;">
        <div>Installing packages...</div>
        <div>+ bootstrap-blazor@7.11.0</div>
        <div>added 1 package, and audited 42 packages in 2.5s</div>
        <div class="text-success">✓ found 0 vulnerabilities</div>
    </div>
</div>

<style>
    .command-line-container {
        border-radius: 6px;
        font-family: monospace;
    }
    
    .command-prompt {
        display: flex;
        align-items: center;
    }
</style>
```

This example creates a command-line interface simulation with a typing effect for commands.

### Example 6: Animated Heading with Fade Effect

```razor
<div class="text-center">
    <h1>
        <span>We create </span>
        <Typed Strings="@(new[] { "websites", "applications", "experiences", "solutions" })"
               Loop="true"
               FadeOut="true"
               FadeOutDelay="800"
               FadeOutDuration="400"
               TypeSpeed="80"
               BackSpeed="0"
               ShowCursor="true"
               CursorChar="|"
               TextColor="var(--bs-primary)" />
    </h1>
</div>
```

This example creates an animated heading with a fade-out effect instead of backspacing, suitable for hero sections or landing pages.

### Example 7: Interactive Form with Typing Suggestions

```razor
<div class="search-container mb-4">
    <label for="search-input" class="form-label">What are you looking for?</label>
    <div class="input-group">
        <span class="input-group-text"><i class="fa-solid fa-search"></i></span>
        <input id="search-input" type="text" class="form-control" @bind="searchInput" @bind:event="oninput" placeholder="Type here..." />
    </div>
    
    @if (string.IsNullOrEmpty(searchInput))
    {
        <div class="form-text mt-2">
            Try searching for: 
            <Typed Strings="@searchSuggestions"
                   Loop="true"
                   TypeSpeed="40"
                   BackSpeed="20"
                   BackDelay="1000"
                   CursorChar="_"
                   ShowCursor="true" />
        </div>
    }
</div>

@code {
    private string searchInput = "";
    private string[] searchSuggestions = new[] {
        "components",
        "documentation",
        "examples",
        "tutorials",
        "templates"
    };
}
```

This example integrates the Typed component with a search input to provide animated search suggestions when the input is empty.

## Customization Notes

### CSS Variables

The Typed component can be customized using CSS:

```css
.typed-cursor {
    /* Cursor styling */
    opacity: 1;
    animation: typed-blink 0.7s infinite;
    -webkit-animation: typed-blink 0.7s infinite;
}

@keyframes typed-blink {
    0% { opacity: 1; }
    50% { opacity: 0; }
    100% { opacity: 1; }
}

@-webkit-keyframes typed-blink {
    0% { opacity: 1; }
    50% { opacity: 0; }
    100% { opacity: 1; }
}

.typed-fade-out {
    opacity: 0;
    animation: typed-fade-out 0.5s;
    -webkit-animation: typed-fade-out 0.5s;
}

@keyframes typed-fade-out {
    0% { opacity: 1; }
    100% { opacity: 0; }
}

@-webkit-keyframes typed-fade-out {
    0% { opacity: 1; }
    100% { opacity: 0; }
}
```

### Integration with Other Components

The Typed component works well with:

- **Card Component**: For creating animated card headers or content
- **Alert Component**: For attention-grabbing notifications
- **Modal Component**: For dynamic dialog content
- **Button Component**: For animated button labels
- **Navigation Components**: For dynamic menu items or breadcrumbs

### Accessibility Considerations

- For important content, provide an alternative static text for screen readers
- Use appropriate ARIA attributes to improve accessibility
- Consider disabling animations for users who prefer reduced motion
- Ensure sufficient contrast between text and background colors

### Performance Optimization

- Keep the number of strings and their length reasonable to avoid performance issues
- Use appropriate typing speeds to balance between visual appeal and usability
- Consider using the `SmartBackspace` option for more efficient animations
- For very long strings, consider breaking them into smaller segments