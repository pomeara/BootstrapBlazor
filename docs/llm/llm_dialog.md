# Dialog Component

## Overview
The Dialog component in BootstrapBlazor provides a modal dialog box for displaying content that requires user attention or interaction. Unlike the Modal component, Dialog is specifically designed for quick interactions like confirmations, alerts, or simple input collection, with built-in support for common dialog types.

## Features
- Multiple dialog types (information, success, warning, error, confirm, save)
- Customizable title and content
- Configurable buttons and button text
- Draggable dialog option
- Backdrop customization
- Keyboard interaction support (Esc to close)
- Programmatic API for showing dialogs
- Support for custom content templates

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | `null` | Sets the dialog title |
| `BodyText` | `string` | `null` | Sets the dialog content text |
| `BodyTemplate` | `RenderFragment` | `null` | Custom template for dialog content |
| `ShowCloseButton` | `bool` | `true` | Controls whether to show the close button in the header |
| `ShowFooter` | `bool` | `true` | Controls whether to show the footer with action buttons |
| `CloseButtonText` | `string` | `"Close"` | Text for the close/cancel button |
| `SaveButtonText` | `string` | `"Save"` | Text for the save/confirm button |
| `ShowSaveButton` | `bool` | `false` | Controls whether to show the save/confirm button |
| `Size` | `Size` | `Size.Medium` | Sets the size of the dialog (Small, Medium, Large) |
| `IsDraggable` | `bool` | `false` | Enables dragging of the dialog |
| `IsKeyboard` | `bool` | `true` | Enables keyboard interaction (Esc to close) |
| `IsBackdrop` | `bool` | `true` | Controls whether to show a backdrop behind the dialog |
| `IsAutoCloseDialog` | `bool` | `true` | Automatically closes the dialog after action button click |

## Static Methods

| Method | Description |
| --- | --- |
| `Show(string title, string content)` | Shows an information dialog with the specified title and content |
| `ShowSuccess(string title, string content)` | Shows a success dialog with the specified title and content |
| `ShowWarning(string title, string content)` | Shows a warning dialog with the specified title and content |
| `ShowError(string title, string content)` | Shows an error dialog with the specified title and content |
| `ShowConfirm(string title, string content)` | Shows a confirmation dialog with the specified title and content |
| `ShowSaveDialog(string title, string content)` | Shows a save dialog with the specified title and content |
| `ShowCloseDialog()` | Closes the currently open dialog |

## Events

| Event | Description |
| --- | --- |
| `OnClose` | Triggered when the dialog is closed |
| `OnSave` | Triggered when the save/confirm button is clicked |
| `OnCloseButtonClick` | Triggered when the close/cancel button is clicked |

## Usage Examples

### Example 1: Basic Information Dialog
```csharp
<Button OnClick="ShowInfoDialog">Show Info Dialog</Button>

@code {
    private Task ShowInfoDialog()
    {
        return Dialog.Show("Information", "This is an information message.");
    }
}
```
This example shows how to display a basic information dialog with a title and message using the static Show method.

### Example 2: Different Dialog Types
```csharp
<div class="d-flex flex-wrap gap-2">
    <Button OnClick="ShowInfoDialog">Info</Button>
    <Button OnClick="ShowSuccessDialog" Color="Color.Success">Success</Button>
    <Button OnClick="ShowWarningDialog" Color="Color.Warning">Warning</Button>
    <Button OnClick="ShowErrorDialog" Color="Color.Danger">Error</Button>
    <Button OnClick="ShowConfirmDialog" Color="Color.Secondary">Confirm</Button>
</div>

@code {
    private Task ShowInfoDialog() => Dialog.Show("Information", "This is an information message.");
    private Task ShowSuccessDialog() => Dialog.ShowSuccess("Success", "Operation completed successfully!");
    private Task ShowWarningDialog() => Dialog.ShowWarning("Warning", "This action might have consequences.");
    private Task ShowErrorDialog() => Dialog.ShowError("Error", "An error occurred while processing your request.");
    private Task ShowConfirmDialog() => Dialog.ShowConfirm("Confirm", "Are you sure you want to proceed?");
}
```
This example demonstrates the different types of dialogs available (information, success, warning, error, confirm).

### Example 3: Confirmation Dialog with Result Handling
```csharp
<Button OnClick="DeleteItem">Delete Item</Button>

@code {
    private async Task DeleteItem()
    {
        var result = await Dialog.ShowConfirm("Confirm Deletion", "Are you sure you want to delete this item? This action cannot be undone.");
        if (result)
        {
            // User confirmed, perform deletion
            await DeleteItemFromDatabase();
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

### Example 4: Custom Dialog with Template
```html
<Button OnClick="ShowCustomDialog">Show Custom Dialog</Button>

<Dialog @ref="customDialog" Title="Custom Form" ShowSaveButton="true" SaveButtonText="Submit" OnSave="HandleSubmit">
    <BodyTemplate>
        <div class="form-group">
            <label for="name">Name:</label>
            <input type="text" class="form-control" id="name" @bind="name" />
        </div>
        <div class="form-group mt-3">
            <label for="email">Email:</label>
            <input type="email" class="form-control" id="email" @bind="email" />
        </div>
    </BodyTemplate>
</Dialog>

@code {
    private Dialog customDialog;
    private string name;
    private string email;
    
    private Task ShowCustomDialog()
    {
        return customDialog.Show();
    }
    
    private async Task HandleSubmit()
    {
        // Process form submission
        await ProcessFormData(name, email);
    }
    
    private Task ProcessFormData(string name, string email)
    {
        // Implementation of form processing
        return Task.CompletedTask;
    }
}
```
This example demonstrates a custom dialog with a form template for collecting user input.

### Example 5: Draggable Dialog
```html
<Button OnClick="ShowDraggableDialog">Show Draggable Dialog</Button>

@code {
    private async Task ShowDraggableDialog()
    {
        await Dialog.Show(new DialogOption
        {
            Title = "Draggable Dialog",
            BodyText = "You can drag this dialog by its header.",
            IsDraggable = true,
            Size = Size.Small
        });
    }
}
```
This example shows how to create a draggable dialog using the DialogOption class to customize various properties.

### Example 6: Dialog without Backdrop
```csharp
<Button OnClick="ShowNoBackdropDialog">Show Dialog without Backdrop</Button>

@code {
    private async Task ShowNoBackdropDialog()
    {
        await Dialog.Show(new DialogOption
        {
            Title = "No Backdrop",
            BodyText = "This dialog doesn't have a backdrop, allowing interaction with elements behind it.",
            IsBackdrop = false
        });
    }
}
```
This example demonstrates how to create a dialog without a backdrop, allowing users to interact with elements behind the dialog.

### Example 7: Save Dialog with Custom Buttons
```csharp
<Button OnClick="ShowSaveDialogWithCustomButtons">Show Save Dialog</Button>

@code {
    private async Task ShowSaveDialogWithCustomButtons()
    {
        await Dialog.Show(new DialogOption
        {
            Title = "Save Document",
            BodyText = "Do you want to save changes to this document?",
            ShowSaveButton = true,
            SaveButtonText = "Save",
            CloseButtonText = "Don't Save",
            OnSaveCall = () => SaveDocument(),
            OnCloseCall = () => DiscardChanges()
        });
    }
    
    private Task SaveDocument()
    {
        // Implementation of document saving
        return Task.CompletedTask;
    }
    
    private Task DiscardChanges()
    {
        // Implementation of discarding changes
        return Task.CompletedTask;
    }
}
```
This example shows a save dialog with custom button text and separate callback functions for the save and close actions.

## CSS Customization

The Dialog component can be customized using the following CSS variables:

```css
--bb-dialog-header-padding: 1rem;
--bb-dialog-body-padding: 1rem;
--bb-dialog-footer-padding: 0.75rem;
--bb-dialog-border-radius: 0.3rem;
--bb-dialog-box-shadow: 0 0.5rem 1rem rgba(0, 0, 0, 0.15);
--bb-dialog-header-border-color: #dee2e6;
--bb-dialog-footer-border-color: #dee2e6;
--bb-dialog-backdrop-bg: rgba(0, 0, 0, 0.5);
--bb-dialog-content-bg: #fff;
--bb-dialog-content-color: #212529;
--bb-dialog-z-index: 1050;
```

## Notes

1. **Dialog vs Modal**: The Dialog component is optimized for quick interactions and common dialog types, while the Modal component is more flexible for complex custom content but requires more manual configuration.

2. **Accessibility**: The Dialog component includes ARIA attributes for better accessibility. It uses `role="dialog"`, `aria-modal="true"`, and `aria-labelledby` attributes.

3. **Return Values**: The ShowConfirm, ShowSaveDialog, and other confirmation-type methods return a Task<bool> that resolves to true if the user confirms and false if they cancel.

4. **Multiple Dialogs**: While it's technically possible to show multiple dialogs simultaneously, it's generally not recommended for usability reasons. Consider using a queue if you need to show multiple dialogs in sequence.

5. **Performance**: For dialogs that contain complex content or forms, consider using lazy loading or deferred rendering to improve performance.