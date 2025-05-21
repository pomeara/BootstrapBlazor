# Textarea Component Documentation

## Overview
The Textarea component in BootstrapBlazor provides an enhanced multi-line text input field for collecting longer text content from users. It extends the standard HTML textarea element with additional features and styling consistent with the BootstrapBlazor design system. This component is ideal for comment sections, description fields, feedback forms, and any scenario requiring multi-line text input. It supports various customization options, validation integration, and responsive design to meet different application needs.

## Features
- **Multi-line Text Input**: Capture longer text content with line breaks
- **Two-way Data Binding**: Seamless integration with Blazor's binding system
- **Form Validation**: Integration with BootstrapBlazor's form validation
- **Auto-resize**: Optional automatic height adjustment based on content
- **Character Count**: Display and limit the number of characters
- **Placeholder Support**: Informative placeholder text when empty
- **Readonly and Disabled States**: Control user interaction capabilities
- **Size Variants**: Small, medium, and large size options
- **Custom Styling**: Flexible appearance customization
- **Event Callbacks**: Rich set of events for various user interactions
- **Localization Support**: Multi-language compatibility
- **Accessibility Features**: Screen reader compatibility and keyboard navigation

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | string | "" | Gets or sets the textarea value |
| ValueChanged | EventCallback<string> | - | Callback when the textarea value changes |
| Rows | int | 3 | Number of visible text lines |
| Cols | int | null | Number of visible characters per line |
| MaxLength | int? | null | Maximum number of characters allowed |
| MinLength | int? | null | Minimum number of characters required |
| Placeholder | string | "" | Placeholder text when the textarea is empty |
| IsDisabled | bool | false | Whether the textarea is disabled |
| IsReadOnly | bool | false | Whether the textarea is read-only |
| AutoFocus | bool | false | Whether to automatically focus the textarea on load |
| AutoSize | bool | false | Whether to automatically adjust height based on content |
| MinRows | int | 2 | Minimum number of rows when AutoSize is true |
| MaxRows | int? | null | Maximum number of rows when AutoSize is true |
| ShowCount | bool | false | Whether to display character count |
| Size | Size | Medium | Size of the component (Small, Medium, Large) |
| ClassName | string | "" | Additional CSS class for the component |
| ShowLabel | bool | false | Whether to show a label |
| DisplayText | string | null | Text to display in the label |
| ShowRequiredMark | bool | false | Whether to show a required mark in the label |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the textarea value changes |
| OnFocus | Triggered when the textarea receives focus |
| OnBlur | Triggered when the textarea loses focus |
| OnKeyUp | Triggered when a key is released while the textarea has focus |
| OnKeyDown | Triggered when a key is pressed while the textarea has focus |
| OnKeyPress | Triggered when a key is pressed and released while the textarea has focus |
| OnInput | Triggered when the textarea value changes by user input |
| OnResize | Triggered when the textarea is resized (when AutoSize is true) |

## Usage Examples

### Example 1: Basic Textarea

```razor
<Textarea @bind-Value="@comments"
          Placeholder="Enter your comments here"
          Rows="4" />

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="@SubmitComments">Submit</Button>
</div>

@code {
    private string comments = "";
    
    private void SubmitComments()
    {
        Console.WriteLine($"Comments submitted: {comments}");
        // Process the comments
    }
}
```

### Example 2: Textarea with Character Count and Limit

```razor
<Textarea @bind-Value="@description"
          Placeholder="Describe your product in detail"
          MaxLength="500"
          ShowCount="true"
          Rows="5" />

<div class="mt-3">
    <span class="text-muted">Provide a detailed description of your product. Maximum 500 characters.</span>
</div>

@code {
    private string description = "";
}
```

### Example 3: Auto-resizing Textarea

```razor
<Textarea @bind-Value="@feedback"
          Placeholder="Share your feedback"
          AutoSize="true"
          MinRows="2"
          MaxRows="10"
          OnResize="HandleResize" />

<div class="mt-3">
    <span class="text-muted">The textarea will grow as you type, up to 10 rows.</span>
</div>

@code {
    private string feedback = "";
    
    private void HandleResize()
    {
        Console.WriteLine("Textarea resized");
    }
}
```

### Example 4: Textarea with Form Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Name" placeholder="Your name" />
        <ValidationMessage For="@(() => model.Name)" />
    </div>
    
    <div class="mb-3">
        <Textarea @bind-Value="@model.Feedback"
                  Placeholder="Your feedback"
                  ShowLabel="true"
                  DisplayText="Feedback"
                  ShowRequiredMark="true"
                  Rows="5" />
        <ValidationMessage For="@(() => model.Feedback)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit Feedback</Button>
</ValidateForm>

@code {
    private FeedbackModel model = new FeedbackModel
    {
        Name = "",
        Feedback = ""
    };
    
    private void HandleValidSubmit()
    {
        // Process the feedback
        Console.WriteLine($"Feedback submitted by: {model.Name}");
        Console.WriteLine($"Feedback content: {model.Feedback}");
    }
    
    public class FeedbackModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Feedback is required")]
        [MinLength(10, ErrorMessage = "Feedback must be at least 10 characters")]
        [MaxLength(1000, ErrorMessage = "Feedback cannot exceed 1000 characters")]
        public string Feedback { get; set; }
    }
}
```

### Example 5: Textarea with Different Sizes

```razor
<div class="mb-3">
    <h6>Small Textarea</h6>
    <Textarea @bind-Value="@smallText"
              Placeholder="Small textarea"
              Size="Size.Small"
              Rows="2" />
</div>

<div class="mb-3">
    <h6>Medium Textarea (Default)</h6>
    <Textarea @bind-Value="@mediumText"
              Placeholder="Medium textarea"
              Size="Size.Medium"
              Rows="3" />
</div>

<div class="mb-3">
    <h6>Large Textarea</h6>
    <Textarea @bind-Value="@largeText"
              Placeholder="Large textarea"
              Size="Size.Large"
              Rows="4" />
</div>

@code {
    private string smallText = "";
    private string mediumText = "";
    private string largeText = "";
}
```

### Example 6: Textarea with Event Handling

```razor
<Textarea @bind-Value="@notes"
          Placeholder="Take notes here"
          Rows="4"
          OnFocus="HandleFocus"
          OnBlur="HandleBlur"
          OnKeyDown="HandleKeyDown"
          OnInput="HandleInput" />

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
    private string notes = "";
    private List<string> eventLogs = new();
    
    private void HandleFocus()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Textarea focused");
    }
    
    private void HandleBlur()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Textarea blurred");
    }
    
    private void HandleKeyDown(KeyboardEventArgs args)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Key pressed: {args.Key}");
    }
    
    private void HandleInput(ChangeEventArgs args)
    {
        var value = args.Value?.ToString() ?? "";
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Input changed: {value.Length} characters");
    }
    
    private void ClearLogs()
    {
        eventLogs.Clear();
    }
}
```

### Example 7: Markdown Editor with Preview

```razor
<div class="row">
    <div class="col-md-6">
        <h5>Markdown Input</h5>
        <Textarea @bind-Value="@markdownText"
                  Placeholder="Write your markdown here"
                  Rows="10"
                  AutoSize="true"
                  MinRows="10"
                  MaxRows="20"
                  OnValueChanged="UpdatePreview" />
    </div>
    <div class="col-md-6">
        <h5>Preview</h5>
        <div class="border p-3" style="min-height: 238px;">
            @((MarkupString)markdownPreview)
        </div>
    </div>
</div>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="SaveMarkdown">Save</Button>
    <Button Color="Color.Secondary" OnClick="ClearMarkdown">Clear</Button>
</div>

@code {
    private string markdownText = "# Hello World\n\nThis is a **markdown** preview example.\n\n- Item 1\n- Item 2\n- Item 3";
    private string markdownPreview = "";
    
    protected override void OnInitialized()
    {
        UpdatePreview();
    }
    
    private void UpdatePreview()
    {
        // In a real application, you would use a markdown parser library
        // This is a simplified example
        markdownPreview = ConvertMarkdownToHtml(markdownText);
    }
    
    private string ConvertMarkdownToHtml(string markdown)
    {
        // This is a very simplified markdown converter for demonstration
        // In a real application, use a proper markdown library
        var html = markdown
            .Replace("# ", "<h1>").Replace("\n#", "</h1>\n")
            .Replace("**", "<strong>").Replace("**", "</strong>")
            .Replace("\n- ", "\n<li>").Replace("\n", "</li>\n")
            .Replace("<li>", "<ul><li>").Replace("</li>\n</ul>", "</li></ul>\n");
        
        return html;
    }
    
    private void SaveMarkdown()
    {
        Console.WriteLine("Markdown saved");
        // Save the markdown content
    }
    
    private void ClearMarkdown()
    {
        markdownText = "";
        markdownPreview = "";
    }
}
```

## Customization Notes

The Textarea component can be customized using the following CSS variables:

```css
:root {
    --bb-textarea-border-color: #ced4da;
    --bb-textarea-border-radius: 0.25rem;
    --bb-textarea-padding: 0.375rem 0.75rem;
    --bb-textarea-font-size: 1rem;
    --bb-textarea-line-height: 1.5;
    --bb-textarea-color: #212529;
    --bb-textarea-bg: #fff;
    --bb-textarea-disabled-bg: #e9ecef;
    --bb-textarea-disabled-color: #6c757d;
    --bb-textarea-focus-border-color: #86b7fe;
    --bb-textarea-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-textarea-placeholder-color: #6c757d;
    --bb-textarea-count-color: #6c757d;
    --bb-textarea-count-font-size: 0.75rem;
}
```

Additionally, you can customize the appearance and behavior of the Textarea component by:

1. Using the `Rows` and `Cols` properties to adjust the initial size
2. Using the `AutoSize`, `MinRows`, and `MaxRows` properties for dynamic sizing
3. Using the `MaxLength` and `ShowCount` properties to manage content length
4. Using the `Size` property to adjust the component size
5. Using the `ShowLabel`, `DisplayText`, and `ShowRequiredMark` properties to customize the label
6. Using the `IsDisabled` and `IsReadOnly` properties to control the textarea state
7. Applying custom CSS classes to the component using the `ClassName` property