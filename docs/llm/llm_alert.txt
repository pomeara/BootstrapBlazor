# Alert Component Documentation

## Overview
The Alert component in BootstrapBlazor is used to display important messages or notifications to users. It provides contextual feedback messages for typical user actions with a handful of available and flexible alert messages.

## Features
- Multiple color themes (primary, secondary, success, danger, warning, info, dark)
- Dismissible alerts with close button
- Support for icons
- Bar style variant
- Customizable content including HTML
- Fade-in/fade-out animations
- Auto-dismissal with configurable timeout

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Color | Color | Primary | Sets the color theme of the alert |
| ShowDismiss | bool | false | Whether to show the dismiss button |
| ShowBar | bool | false | Whether to show the alert with a colored bar on the left side |
| ShowIcon | bool | false | Whether to show an icon in the alert |
| Icon | string | null | Custom icon to display |
| IsAutoClose | bool | false | Whether the alert should automatically close after a period |
| Dismissible | bool | false | Whether the alert can be dismissed by the user |
| CloseButtonText | string | "Close" | Text for the close button |
| Timeout | int | 3000 | Time in milliseconds before auto-closing (if IsAutoClose is true) |

## Events

| Event | Description |
| --- | --- |
| OnDismiss | Triggered when the alert is dismissed |
| OnClosed | Triggered after the alert is fully closed |

## Usage Examples

### Example 1: Basic Alert

```razor
<Alert Color="Color.Primary">
    This is a primary alert—check it out!
</Alert>

<Alert Color="Color.Secondary">
    This is a secondary alert—check it out!
</Alert>

<Alert Color="Color.Success">
    This is a success alert—check it out!
</Alert>

<Alert Color="Color.Danger">
    This is a danger alert—check it out!
</Alert>

<Alert Color="Color.Warning">
    This is a warning alert—check it out!
</Alert>

<Alert Color="Color.Info">
    This is an info alert—check it out!
</Alert>

<Alert Color="Color.Dark">
    This is a dark alert—check it out!
</Alert>
```

### Example 2: Dismissible Alert

```razor
<Alert Color="Color.Success" ShowDismiss="true" OnDismiss="@OnDismissAlert">
    <strong>Well done!</strong> You successfully read this important alert message.
</Alert>

@code {
    private void OnDismissAlert()
    {
        // Handle the dismiss event
        Console.WriteLine("Alert was dismissed");
    }
}
```

### Example 3: Alert with Icon

```razor
<Alert Color="Color.Info" ShowIcon="true">
    <strong>Heads up!</strong> This alert needs your attention, but it's not super important.
</Alert>

<Alert Color="Color.Warning" ShowIcon="true" Icon="fa-solid fa-exclamation-triangle">
    <strong>Warning!</strong> Better check yourself, you're not looking too good.
</Alert>
```

### Example 4: Bar Style Alert

```razor
<Alert Color="Color.Primary" ShowBar="true">
    This is a primary alert with a bar style.
</Alert>

<Alert Color="Color.Danger" ShowBar="true" ShowIcon="true">
    <strong>Danger!</strong> This action cannot be undone.
</Alert>
```

### Example 5: Auto-closing Alert

```razor
<Button Color="Color.Primary" OnClick="@ShowAutoCloseAlert">Show Auto-close Alert</Button>

@if (showAlert)
{
    <Alert Color="Color.Info" IsAutoClose="true" Timeout="5000" OnClosed="@HandleClosed">
        This alert will automatically close in 5 seconds.
    </Alert>
}

@code {
    private bool showAlert = false;

    private void ShowAutoCloseAlert()
    {
        showAlert = true;
    }

    private void HandleClosed()
    {
        showAlert = false;
        // Additional logic after alert is closed
    }
}
```

### Example 6: Alert with HTML Content

```razor
<Alert Color="Color.Info">
    <h4 class="alert-heading">Well done!</h4>
    <p>
        Aww yeah, you successfully read this important alert message. This example text is going 
        to run a bit longer so that you can see how spacing within an alert works with this kind 
        of content.
    </p>
    <hr>
    <p class="mb-0">
        Whenever you need to, be sure to use margin utilities to keep things nice and tidy.
    </p>
</Alert>
```

### Example 7: Programmatically Controlled Alert

```razor
<Button Color="Color.Primary" OnClick="@ShowAlert">Show Alert</Button>
<Button Color="Color.Danger" OnClick="@HideAlert">Hide Alert</Button>

<Alert @ref="alertRef" Color="Color.Success" ShowDismiss="true">
    <strong>Success!</strong> Your operation has been completed.
</Alert>

@code {
    private Alert? alertRef;

    private void ShowAlert()
    {
        if (alertRef != null)
        {
            alertRef.Show();
        }
    }

    private void HideAlert()
    {
        if (alertRef != null)
        {
            alertRef.Close();
        }
    }
}
```

## CSS Customization

The Alert component uses the following CSS variables that can be customized:

```css
:root {
    --bb-alert-icon-margin-right: 0.5rem;
    --bb-alert-bar-width: 0.25rem;
}
```

You can override these variables in your own CSS to customize the appearance of the Alert component.

## Notes

- The Alert component is built on top of Bootstrap's alert component and extends its functionality.
- When using `ShowIcon="true"`, the component will automatically choose an appropriate icon based on the alert color if no custom icon is specified.
- The `OnDismiss` event is triggered when the user clicks the dismiss button, while `OnClosed` is triggered after the alert is fully closed (after animations complete).
- For accessibility, alerts include appropriate ARIA roles and attributes.