# SweetAlert Component

## Overview
The SweetAlert component in BootstrapBlazor provides an enhanced replacement for JavaScript's standard alert, confirm, and prompt dialogs. It offers a more attractive, customizable, and responsive way to display messages and gather user input, with support for various alert types, animations, and interactive elements.

## Features
- Multiple alert types (success, error, warning, info, question)
- Customizable titles, content, and buttons
- Rich content support (HTML, images, icons)
- Auto-close functionality with timer
- Animation effects
- Input validation
- Stacked modals support
- Programmatic API for showing and handling alerts
- Responsive design
- Accessibility support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | `null` | Sets the title of the alert |
| `Content` | `string` | `null` | Sets the content text of the alert |
| `ShowClose` | `bool` | `true` | Controls whether to show the close button |
| `ShowFooter` | `bool` | `true` | Controls whether to show the footer with action buttons |
| `CloseButtonText` | `string` | `"Cancel"` | Text for the cancel/close button |
| `ConfirmButtonText` | `string` | `"OK"` | Text for the confirm/OK button |
| `ShowConfirmButton` | `bool` | `true` | Controls whether to show the confirm button |
| `Type` | `SwalType` | `SwalType.Question` | Sets the type of the alert (Success, Error, Warning, Info, Question) |
| `Timer` | `int` | `0` | Sets the auto-close timer in milliseconds (0 means no auto-close) |
| `IsAutoHide` | `bool` | `false` | When true, automatically closes the alert after Timer milliseconds |
| `IsConfirm` | `bool` | `false` | When true, displays as a confirmation dialog with both confirm and cancel buttons |
| `IsPrompt` | `bool` | `false` | When true, displays as a prompt dialog with input field |
| `InputType` | `string` | `"text"` | Sets the type of input field for prompt dialogs (text, password, email, etc.) |
| `InputPlaceHolder` | `string` | `null` | Sets the placeholder text for the input field in prompt dialogs |
| `InputValue` | `string` | `null` | Sets/gets the value of the input field in prompt dialogs |
| `InputValidator` | `Func<string, Task<string>>` | `null` | Function to validate input in prompt dialogs, returns error message or null if valid |

## Static Methods

| Method | Description |
| --- | --- |
| `Show(string title, string content)` | Shows a basic alert with the specified title and content |
| `ShowSuccess(string title, string content)` | Shows a success alert with the specified title and content |
| `ShowError(string title, string content)` | Shows an error alert with the specified title and content |
| `ShowWarning(string title, string content)` | Shows a warning alert with the specified title and content |
| `ShowInfo(string title, string content)` | Shows an info alert with the specified title and content |
| `ShowConfirm(string title, string content)` | Shows a confirmation dialog with the specified title and content |
| `ShowPrompt(string title, string placeholder)` | Shows a prompt dialog with the specified title and placeholder |

## Events

| Event | Description |
| --- | --- |
| `OnClose` | Triggered when the alert is closed |
| `OnConfirm` | Triggered when the confirm button is clicked |
| `OnResult` | Triggered when the alert is closed with a result (for confirm and prompt dialogs) |

## Usage Examples

### Example 1: Basic Alert
```csharp
<Button OnClick="ShowBasicAlert">Show Basic Alert</Button>

@code {
    private async Task ShowBasicAlert()
    {
        await SweetAlert.Show("Hello!", "This is a basic SweetAlert dialog.");
    }
}
```
This example shows a basic alert with a title and message using the static Show method.

### Example 2: Different Alert Types
```csharp
<div class="d-flex flex-wrap gap-2">
    <Button OnClick="ShowSuccessAlert" Color="Color.Success">Success</Button>
    <Button OnClick="ShowErrorAlert" Color="Color.Danger">Error</Button>
    <Button OnClick="ShowWarningAlert" Color="Color.Warning">Warning</Button>
    <Button OnClick="ShowInfoAlert" Color="Color.Info">Info</Button>
    <Button OnClick="ShowQuestionAlert" Color="Color.Secondary">Question</Button>
</div>

@code {
    private async Task ShowSuccessAlert()
    {
        await SweetAlert.ShowSuccess("Success!", "Operation completed successfully.");
    }
    
    private async Task ShowErrorAlert()
    {
        await SweetAlert.ShowError("Error!", "Something went wrong.");
    }
    
    private async Task ShowWarningAlert()
    {
        await SweetAlert.ShowWarning("Warning!", "This action might have consequences.");
    }
    
    private async Task ShowInfoAlert()
    {
        await SweetAlert.ShowInfo("Information", "Here's some information you should know.");
    }
    
    private async Task ShowQuestionAlert()
    {
        await SweetAlert.Show("Question?", "What would you like to do next?", SwalType.Question);
    }
}
```
This example demonstrates the different types of alerts available (success, error, warning, info, question), each with appropriate styling and icons.

### Example 3: Confirmation Dialog with Result Handling
```csharp
<Button OnClick="DeleteItem">Delete Item</Button>

@code {
    private async Task DeleteItem()
    {
        var result = await SweetAlert.ShowConfirm("Confirm Deletion", "Are you sure you want to delete this item? This action cannot be undone.");
        if (result)
        {
            // User confirmed, perform deletion
            await DeleteItemFromDatabase();
            await SweetAlert.ShowSuccess("Deleted", "The item has been deleted successfully.");
        }
    }
    
    private Task DeleteItemFromDatabase()
    {
        // Implementation of actual deletion
        return Task.CompletedTask;
    }
}
```
This example shows how to use a confirmation dialog and handle the result to perform an action only if the user confirms.

### Example 4: Prompt Dialog with Validation
```csharp
<Button OnClick="RenameItem">Rename Item</Button>

@code {
    private async Task RenameItem()
    {
        var result = await SweetAlert.ShowPrompt(new SwalOption
        {
            Title = "Rename Item",
            Content = "Enter a new name for this item:",
            InputPlaceHolder = "New name",
            InputValue = "Current Item Name",
            InputValidator = ValidateItemName
        });
        
        if (!string.IsNullOrEmpty(result))
        {
            // User entered a valid name, perform rename
            await RenameItemInDatabase(result);
            await SweetAlert.ShowSuccess("Renamed", $"The item has been renamed to '{result}'.");
        }
    }
    
    private Task<string> ValidateItemName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Task.FromResult("Name cannot be empty.");
        
        if (name.Length < 3)
            return Task.FromResult("Name must be at least 3 characters long.");
        
        return Task.FromResult<string>(null); // null means validation passed
    }
    
    private Task RenameItemInDatabase(string newName)
    {
        // Implementation of actual rename
        return Task.CompletedTask;
    }
}
```
This example demonstrates a prompt dialog with input validation, allowing users to enter a new name for an item with validation rules.

### Example 5: Auto-closing Alert with Timer
```csharp
<Button OnClick="ShowAutoCloseAlert">Show Auto-close Alert</Button>

@code {
    private async Task ShowAutoCloseAlert()
    {
        await SweetAlert.Show(new SwalOption
        {
            Title = "Auto-close Alert",
            Content = "This alert will close automatically in 3 seconds.",
            Type = SwalType.Info,
            Timer = 3000,
            IsAutoHide = true
        });
    }
}
```
This example shows an alert that automatically closes after 3 seconds (3000 milliseconds).

### Example 6: Alert with HTML Content and Custom Buttons
```csharp
<Button OnClick="ShowRichContentAlert">Show Rich Content Alert</Button>

@code {
    private async Task ShowRichContentAlert()
    {
        await SweetAlert.Show(new SwalOption
        {
            Title = "<strong>HTML <u>Content</u></strong>",
            Content = "<div class='text-center'><img src='/images/logo.png' style='width: 100px;'><p>You can use <b>HTML</b> in the content!</p></div>",
            IsHtml = true,
            ConfirmButtonText = "Awesome!",
            ShowCloseButton = true,
            CloseButtonText = "Not Impressed"
        });
    }
}
```
This example demonstrates an alert with HTML content, including an image and formatted text, along with custom button text.

### Example 7: Multi-step Wizard with SweetAlert
```csharp
<Button OnClick="StartWizard">Start Setup Wizard</Button>

@code {
    private async Task StartWizard()
    {
        // Step 1: Welcome
        var result = await SweetAlert.ShowConfirm(new SwalOption
        {
            Title = "Setup Wizard",
            Content = "Welcome to the setup wizard. This will help you configure your application. Would you like to continue?",
            ConfirmButtonText = "Start Setup",
            CloseButtonText = "Cancel"
        });
        
        if (!result)
            return;
        
        // Step 2: User Information
        var name = await SweetAlert.ShowPrompt(new SwalOption
        {
            Title = "Step 1: User Information",
            Content = "Please enter your name:",
            InputPlaceHolder = "Your name",
            ConfirmButtonText = "Next",
            ShowCloseButton = true,
            InputValidator = value => Task.FromResult(string.IsNullOrWhiteSpace(value) ? "Name is required" : null)
        });
        
        if (string.IsNullOrEmpty(name))
            return;
        
        // Step 3: Email
        var email = await SweetAlert.ShowPrompt(new SwalOption
        {
            Title = "Step 2: Contact Information",
            Content = "Please enter your email address:",
            InputPlaceHolder = "your.email@example.com",
            InputType = "email",
            ConfirmButtonText = "Next",
            ShowCloseButton = true,
            InputValidator = ValidateEmail
        });
        
        if (string.IsNullOrEmpty(email))
            return;
        
        // Step 4: Confirmation
        result = await SweetAlert.ShowConfirm(new SwalOption
        {
            Title = "Confirm Setup",
            Content = $"Please confirm your information:\n\nName: {name}\nEmail: {email}",
            ConfirmButtonText = "Complete Setup",
            CloseButtonText = "Go Back"
        });
        
        if (result)
        {
            // Final step: Success
            await SweetAlert.ShowSuccess("Setup Complete", "Your application has been configured successfully!");
        }
    }
    
    private Task<string> ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Task.FromResult("Email is required.");
        
        if (!email.Contains("@") || !email.Contains("."))
            return Task.FromResult("Please enter a valid email address.");
        
        return Task.FromResult<string>(null); // null means validation passed
    }
}
```
This example demonstrates a multi-step wizard using a series of SweetAlert dialogs to collect user information and confirm the setup process.

## CSS Customization

The SweetAlert component can be customized using the following CSS variables:

```css
--bb-swal-width: 32rem;
--bb-swal-padding: 1.25rem;
--bb-swal-border-radius: 0.3rem;
--bb-swal-background-color: #fff;
--bb-swal-color: #212529;
--bb-swal-title-color: #595959;
--bb-swal-title-font-size: 1.875rem;
--bb-swal-content-font-size: 1.125rem;
--bb-swal-icon-size: 5rem;
--bb-swal-footer-padding: 1rem 0 0;
--bb-swal-button-focus-box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
--bb-swal-confirm-button-bg: #3085d6;
--bb-swal-confirm-button-color: #fff;
--bb-swal-cancel-button-bg: #aaa;
--bb-swal-cancel-button-color: #fff;
--bb-swal-success-color: #a5dc86;
--bb-swal-error-color: #f27474;
--bb-swal-warning-color: #f8bb86;
--bb-swal-info-color: #3fc3ee;
--bb-swal-question-color: #87adbd;
```

## Notes

1. **Accessibility**: The SweetAlert component includes ARIA attributes for better accessibility. It uses `role="dialog"`, `aria-modal="true"`, and `aria-labelledby` attributes.

2. **Mobile Considerations**: On mobile devices, SweetAlert automatically adjusts its size and layout to ensure a good user experience on smaller screens.

3. **HTML Content**: When using HTML content with `IsHtml="true"`, be careful with user-generated content to prevent XSS attacks. Always sanitize any user input that will be displayed as HTML.

4. **Stacked Modals**: While SweetAlert supports stacked modals (showing one alert on top of another), it's generally better for usability to avoid deeply nested alerts.

5. **Performance**: For complex alerts or wizards with multiple steps, consider using a dedicated form component instead of chaining multiple SweetAlert calls, especially if the form requires complex validation or state management.