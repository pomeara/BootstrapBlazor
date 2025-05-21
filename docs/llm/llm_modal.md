# Modal Component Documentation

## Overview
The Modal component in BootstrapBlazor provides a way to create dialog boxes or popup windows that are displayed on top of the current page. Modals are useful for displaying content that requires user attention or interaction without navigating away from the current view.

## Features
- Multiple size options (small, medium, large, extra large, full screen)
- Customizable header, body, and footer sections
- Backdrop options (static, dismissible, none)
- Draggable modal windows
- Scrollable content
- Centered positioning
- Animation effects
- Programmatic control (show/hide)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Title | string | null | Title text for the modal header |
| Size | Size | Medium | Size of the modal (Small, Medium, Large, ExtraLarge, ExtraExtraLarge, Fullscreen) |
| IsCentered | bool | false | Whether to center the modal vertically in the viewport |
| IsScrolling | bool | false | Whether the modal body should be scrollable when content exceeds the height |
| ShowCloseButton | bool | true | Whether to show the close button in the header |
| ShowFooter | bool | true | Whether to show the footer section |
| ShowHeader | bool | true | Whether to show the header section |
| IsBackdrop | bool | true | Whether to show a backdrop behind the modal |
| IsStatic | bool | false | If true, clicking the backdrop will not close the modal |
| IsDraggable | bool | false | Whether the modal can be dragged by the user |
| BodyTemplate | RenderFragment | null | Template for the modal body content |
| HeaderTemplate | RenderFragment | null | Template for the modal header content |
| FooterTemplate | RenderFragment | null | Template for the modal footer content |

## Events

| Event | Description |
| --- | --- |
| OnClose | Triggered when the modal is closed |
| OnShown | Triggered after the modal is fully shown |
| OnHidden | Triggered after the modal is fully hidden |

## Methods

| Method | Description |
| --- | --- |
| Show() | Shows the modal |
| Hide() | Hides the modal |
| Toggle() | Toggles the modal visibility |

## Usage Examples

### Example 1: Basic Modal

```razor
<Button Color="Color.Primary" OnClick="@ShowModal">Open Modal</Button>

<Modal @ref="modalRef" Title="Basic Modal">
    <BodyTemplate>
        <p>This is a basic modal dialog with default settings.</p>
        <p>It has a header with a title and close button, and a footer with a close button.</p>
    </BodyTemplate>
</Modal>

@code {
    private Modal? modalRef;

    private void ShowModal()
    {
        modalRef?.Show();
    }
}
```

### Example 2: Modal with Custom Footer

```razor
<Button Color="Color.Primary" OnClick="@ShowModal">Open Modal with Custom Footer</Button>

<Modal @ref="modalRef" Title="Confirmation">
    <BodyTemplate>
        <p>Are you sure you want to delete this item?</p>
        <p>This action cannot be undone.</p>
    </BodyTemplate>
    <FooterTemplate>
        <Button Color="Color.Secondary" OnClick="@CloseModal">Cancel</Button>
        <Button Color="Color.Danger" OnClick="@DeleteItem">Delete</Button>
    </FooterTemplate>
</Modal>

@code {
    private Modal? modalRef;

    private void ShowModal()
    {
        modalRef?.Show();
    }

    private void CloseModal()
    {
        modalRef?.Hide();
    }

    private void DeleteItem()
    {
        // Perform delete operation
        Console.WriteLine("Item deleted");
        modalRef?.Hide();
    }
}
```

### Example 3: Modal with Different Sizes

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@(() => ShowSizedModal(Size.Small))">Small Modal</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowSizedModal(Size.Medium))">Medium Modal</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowSizedModal(Size.Large))">Large Modal</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowSizedModal(Size.ExtraLarge))">Extra Large Modal</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowSizedModal(Size.Fullscreen))">Fullscreen Modal</Button>
</div>

<Modal @ref="modalRef" Title="Sized Modal" Size="@currentSize">
    <BodyTemplate>
        <p>This modal is displayed with the size: @currentSize</p>
    </BodyTemplate>
</Modal>

@code {
    private Modal? modalRef;
    private Size currentSize = Size.Medium;

    private void ShowSizedModal(Size size)
    {
        currentSize = size;
        modalRef?.Show();
    }
}
```

### Example 4: Draggable Modal

```razor
<Button Color="Color.Primary" OnClick="@ShowModal">Open Draggable Modal</Button>

<Modal @ref="modalRef" Title="Draggable Modal" IsDraggable="true">
    <BodyTemplate>
        <p>This modal can be dragged around the screen by clicking and dragging the header.</p>
        <p>Try it now!</p>
    </BodyTemplate>
</Modal>

@code {
    private Modal? modalRef;

    private void ShowModal()
    {
        modalRef?.Show();
    }
}
```

### Example 5: Modal with Form

```razor
<Button Color="Color.Primary" OnClick="@ShowModal">Open Form Modal</Button>

<Modal @ref="modalRef" Title="User Registration">
    <BodyTemplate>
        <Form Model="@user" OnValidSubmit="@HandleValidSubmit">
            <div class="mb-3">
                <BootstrapInput @bind-Value="@user.Username" placeholder="Enter username" />
            </div>
            
            <div class="mb-3">
                <BootstrapInput @bind-Value="@user.Email" placeholder="Enter email" />
            </div>
            
            <div class="mb-3">
                <BootstrapInput @bind-Value="@user.Password" type="password" placeholder="Enter password" />
            </div>
            
            <div class="modal-footer">
                <Button Color="Color.Secondary" OnClick="@CloseModal">Cancel</Button>
                <Button Color="Color.Primary" Type="ButtonType.Submit">Register</Button>
            </div>
        </Form>
    </BodyTemplate>
    <FooterTemplate>
        <!-- Empty footer template to remove default footer -->
    </FooterTemplate>
</Modal>

@code {
    private Modal? modalRef;
    private UserModel user = new UserModel();

    private void ShowModal()
    {
        user = new UserModel(); // Reset form
        modalRef?.Show();
    }

    private void CloseModal()
    {
        modalRef?.Hide();
    }

    private void HandleValidSubmit()
    {
        // Process form submission
        Console.WriteLine($"User registered: {user.Username}");
        modalRef?.Hide();
    }

    private class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; } = "";
    }
}
```

### Example 6: Static Backdrop Modal

```razor
<Button Color="Color.Primary" OnClick="@ShowModal">Open Static Modal</Button>

<Modal @ref="modalRef" Title="Static Backdrop Modal" IsStatic="true">
    <BodyTemplate>
        <p>This modal has a static backdrop, which means it cannot be closed by clicking outside the modal.</p>
        <p>You must click the close button to dismiss it.</p>
    </BodyTemplate>
</Modal>

@code {
    private Modal? modalRef;

    private void ShowModal()
    {
        modalRef?.Show();
    }
}
```

### Example 7: Modal with Long Content and Scrolling

```razor
<Button Color="Color.Primary" OnClick="@ShowModal">Open Scrollable Modal</Button>

<Modal @ref="modalRef" Title="Scrollable Modal" IsScrolling="true">
    <BodyTemplate>
        <h5>Scrollable Content</h5>
        <p>This modal has a scrollable body to handle long content.</p>
        @for (int i = 1; i <= 20; i++)
        {
            <div class="mb-3">
                <h6>Section @i</h6>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam quis risus eget urna mollis ornare vel eu leo. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>
            </div>
        }
    </BodyTemplate>
</Modal>

@code {
    private Modal? modalRef;

    private void ShowModal()
    {
        modalRef?.Show();
    }
}
```

## Notes

- Modals are rendered at the application root level, so they appear above all other content regardless of where they are defined in the component hierarchy.
- When using `IsStatic="true"`, users can still close the modal using the Escape key unless you handle the keyboard events to prevent this.
- For accessibility, consider adding appropriate ARIA attributes and ensuring keyboard navigation works correctly within the modal.
- The Modal component supports nesting, allowing you to open a modal from within another modal.
- When using forms within modals, it's often better to handle the form submission within the modal rather than having the form submit to a different page.