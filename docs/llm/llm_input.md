# Input Component Documentation

## Overview
The Input component in BootstrapBlazor provides an enhanced text input field for collecting user data. It extends the standard HTML input element with additional features and styling consistent with the BootstrapBlazor design system. This component is a fundamental building block for forms, search interfaces, and any scenario requiring text input from users. It supports various input types, validation, and customization options to meet different application needs.

## Features
- Support for various input types (text, password, email, number, etc.)
- Two-way data binding
- Form validation integration
- Placeholder text support
- Prefix and suffix icons or text
- Clear button option
- Password visibility toggle
- Auto-focus capability
- Readonly and disabled states
- Size variants (small, medium, large)
- Character count display
- Maximum length restriction
- Custom styling options
- Event callbacks for various input actions
- Localization support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | string | "" | Gets or sets the input value |
| ValueChanged | EventCallback<string> | - | Callback when the input value changes |
| Type | string | "text" | The type of input (text, password, email, etc.) |
| Placeholder | string | "" | Placeholder text when the input is empty |
| MaxLength | int? | null | Maximum number of characters allowed |
| ShowClear | bool | false | Whether to show a clear button |
| ShowPassword | bool | false | Whether to show a password toggle button (for password inputs) |
| IsDisabled | bool | false | Whether the input is disabled |
| IsReadOnly | bool | false | Whether the input is read-only |
| AutoFocus | bool | false | Whether to automatically focus the input on load |
| Size | Size | Medium | The size of the input (Small, Medium, Large) |
| ShowLabel | bool | true | Whether to show the label |
| DisplayText | string | null | The text to display as the label |
| ShowRequiredMark | bool | true | Whether to show a required mark for required fields |
| RequiredMarkText | string | "*" | The text to use for the required mark |
| ShowCount | bool | false | Whether to show character count |
| PrefixText | string | null | Text to display as a prefix |
| SuffixText | string | null | Text to display as a suffix |
| PrefixIcon | string | null | Icon to display as a prefix |
| SuffixIcon | string | null | Icon to display as a suffix |
| OnEnterKey | EventCallback | - | Callback when the Enter key is pressed |
| OnEscKey | EventCallback | - | Callback when the Escape key is pressed |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the input value changes |
| OnFocus | Triggered when the input receives focus |
| OnBlur | Triggered when the input loses focus |
| OnKeyUp | Triggered when a key is released |
| OnKeyDown | Triggered when a key is pressed |
| OnEnterKey | Triggered when the Enter key is pressed |
| OnEscKey | Triggered when the Escape key is pressed |
| OnClear | Triggered when the clear button is clicked |

## Usage Examples

### Example 1: Basic Input

```razor
<BootstrapInput @bind-Value="@inputValue" Placeholder="Enter text here" />

<div class="mt-3">
    Input value: @inputValue
</div>

@code {
    private string inputValue = "";
}
```

### Example 2: Input with Label and Validation

```razor
<ValidateForm Model="@model" OnValidSubmit="@HandleValidSubmit">
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Username" 
                       ShowLabel="true" 
                       DisplayText="Username" 
                       Placeholder="Enter your username" />
        <ValidationMessage For="@(() => model.Username)" />
    </div>
    
    <div class="mb-3">
        <BootstrapInput @bind-Value="@model.Email" 
                       Type="email" 
                       ShowLabel="true" 
                       DisplayText="Email" 
                       Placeholder="Enter your email" />
        <ValidationMessage For="@(() => model.Email)" />
    </div>
    
    <Button Type="ButtonType.Submit">Submit</Button>
</ValidateForm>

@code {
    private UserModel model = new UserModel();
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Username: {model.Username}");
        Console.WriteLine($"Email: {model.Email}");
    }
    
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, ErrorMessage = "Username cannot exceed 20 characters")]
        public string Username { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
    }
}
```

### Example 3: Password Input with Toggle

```razor
<BootstrapInput @bind-Value="@password" 
               Type="password" 
               ShowLabel="true" 
               DisplayText="Password" 
               Placeholder="Enter your password" 
               ShowPassword="true" />

<div class="mt-3">
    <Button OnClick="@SubmitPassword">Submit</Button>
</div>

@code {
    private string password = "";
    
    private void SubmitPassword()
    {
        // Process the password
        Console.WriteLine($"Password submitted: {password}");
    }
}
```

### Example 4: Input with Icons and Clear Button

```razor
<BootstrapInput @bind-Value="@searchTerm" 
               Placeholder="Search..." 
               PrefixIcon="search" 
               ShowClear="true" 
               OnEnterKey="@Search" />

<div class="mt-3">
    @if (!string.IsNullOrEmpty(searchResult))
    {
        <div class="alert alert-info">
            @searchResult
        </div>
    }
</div>

@code {
    private string searchTerm = "";
    private string searchResult = "";
    
    private void Search()
    {
        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchResult = $"Search results for: {searchTerm}";
        }
        else
        {
            searchResult = "Please enter a search term";
        }
    }
}
```

### Example 5: Input with Character Count

```razor
<BootstrapInput @bind-Value="@message" 
               Placeholder="Enter your message" 
               ShowLabel="true" 
               DisplayText="Message" 
               MaxLength="100" 
               ShowCount="true" />

<div class="mt-3">
    <Button OnClick="@SendMessage" IsDisabled="@string.IsNullOrEmpty(message)">Send</Button>
</div>

@code {
    private string message = "";
    
    private void SendMessage()
    {
        // Send the message
        Console.WriteLine($"Message sent: {message}");
        message = "";
    }
}
```

### Example 6: Different Input Sizes

```razor
<div class="mb-3">
    <BootstrapInput @bind-Value="@smallInput" 
                   Placeholder="Small input" 
                   Size="Size.Small" />
</div>

<div class="mb-3">
    <BootstrapInput @bind-Value="@mediumInput" 
                   Placeholder="Medium input (default)" 
                   Size="Size.Medium" />
</div>

<div class="mb-3">
    <BootstrapInput @bind-Value="@largeInput" 
                   Placeholder="Large input" 
                   Size="Size.Large" />
</div>

@code {
    private string smallInput = "";
    private string mediumInput = "";
    private string largeInput = "";
}
```

### Example 7: Input with Prefix and Suffix

```razor
<div class="mb-3">
    <BootstrapInput @bind-Value="@username" 
                   Placeholder="Username" 
                   PrefixIcon="user" />
</div>

<div class="mb-3">
    <BootstrapInput @bind-Value="@website" 
                   Placeholder="Website" 
                   PrefixText="https://" 
                   SuffixText=".com" />
</div>

<div class="mb-3">
    <BootstrapInput @bind-Value="@price" 
                   Type="number" 
                   Placeholder="Price" 
                   PrefixText="$" 
                   SuffixIcon="tag" />
</div>

<div class="mb-3">
    <BootstrapInput @bind-Value="@email" 
                   Type="email" 
                   Placeholder="Email" 
                   SuffixText="@example.com" />
</div>

@code {
    private string username = "";
    private string website = "";
    private string price = "";
    private string email = "";
}
```

## Customization Notes

The Input component can be customized using the following CSS variables:

```css
:root {
    --bb-input-height: calc(1.5em + 0.75rem + 2px);
    --bb-input-padding-y: 0.375rem;
    --bb-input-padding-x: 0.75rem;
    --bb-input-font-size: 1rem;
    --bb-input-line-height: 1.5;
    --bb-input-color: #212529;
    --bb-input-bg: #fff;
    --bb-input-border-color: #ced4da;
    --bb-input-border-width: 1px;
    --bb-input-border-radius: 0.25rem;
    --bb-input-box-shadow: inset 0 1px 1px rgba(0, 0, 0, 0.075);
    --bb-input-focus-bg: #fff;
    --bb-input-focus-border-color: #86b7fe;
    --bb-input-focus-color: #212529;
    --bb-input-focus-box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
    --bb-input-placeholder-color: #6c757d;
    --bb-input-disabled-bg: #e9ecef;
    --bb-input-disabled-color: #6c757d;
    --bb-input-readonly-bg: #e9ecef;
    --bb-input-icon-width: 2.5rem;
    --bb-input-icon-color: #6c757d;
    --bb-input-icon-hover-color: #212529;
    --bb-input-clear-icon-color: #6c757d;
    --bb-input-clear-icon-hover-color: #dc3545;
    --bb-input-count-color: #6c757d;
    --bb-input-count-font-size: 0.75rem;
}
```

Additionally, you can customize the appearance and behavior of the Input component by:

1. Using the `Type` property to change the input type (text, password, email, etc.)
2. Using the `Size` property to adjust the input size
3. Using the `ShowLabel`, `DisplayText`, and `ShowRequiredMark` properties to customize the label
4. Using the `PrefixText`, `SuffixText`, `PrefixIcon`, and `SuffixIcon` properties to add additional elements
5. Using the `ShowClear` and `ShowPassword` properties to add interactive buttons
6. Using the `MaxLength` and `ShowCount` properties to manage input length
7. Applying custom CSS classes to the component using the `ClassName` property