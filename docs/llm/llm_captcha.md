# Captcha Component Documentation

## Overview
The Captcha component in BootstrapBlazor provides a security verification mechanism to distinguish human users from automated bots. It implements interactive challenges that are easy for humans to solve but difficult for automated programs, helping to protect websites from spam, abuse, and automated attacks. The component offers various verification methods including slider puzzles, image recognition, and text-based challenges, making it a versatile tool for enhancing application security.

## Features
- **Interactive Verification**: Engaging user verification through slider puzzles and other challenges
- **Multiple Verification Types**: Support for different types of CAPTCHA challenges
- **Customizable Appearance**: Adjustable styling to match application design
- **Refresh Capability**: Option to generate new challenges when needed
- **Validation Feedback**: Visual indicators for successful or failed verification attempts
- **Form Integration**: Seamless integration with form validation
- **Accessibility Support**: Designed with accessibility considerations
- **Mobile-Friendly**: Responsive design for various device sizes
- **Event Callbacks**: Notifications for verification attempts and results
- **Security Measures**: Built-in protections against common bypass techniques
- **Localization Support**: Multi-language support for instructions and messages

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Value | bool | false | Gets or sets whether the CAPTCHA has been successfully verified |
| ValueChanged | EventCallback<bool> | - | Callback when the verification status changes |
| CaptchaType | CaptchaType | Slider | The type of CAPTCHA challenge to display |
| RefreshText | string | "Refresh" | Text for the refresh button |
| SliderText | string | "Slide to verify" | Text displayed on the slider |
| SuccessText | string | "Verification successful" | Text displayed after successful verification |
| FailureText | string | "Verification failed" | Text displayed after failed verification |
| Width | int | 320 | Width of the CAPTCHA component in pixels |
| Height | int | 160 | Height of the CAPTCHA component in pixels |
| ThresholdDistance | int | 5 | Acceptable error margin for slider verification in pixels |
| AutoRefreshOnFailure | bool | true | Whether to automatically refresh the CAPTCHA after failed attempts |
| MaxAttempts | int | 3 | Maximum number of verification attempts before auto-refresh |
| IsDisabled | bool | false | Whether the CAPTCHA component is disabled |
| ShowRefreshButton | bool | true | Whether to show the refresh button |
| ClassName | string | "" | Additional CSS class for the component |

## Events

| Event | Description |
| --- | --- |
| OnValueChanged | Triggered when the verification status changes |
| OnVerifying | Triggered when verification is in progress |
| OnVerified | Triggered when verification is completed (success or failure) |
| OnSuccess | Triggered when verification is successful |
| OnFailure | Triggered when verification fails |
| OnRefresh | Triggered when the CAPTCHA is refreshed |
| OnMaxAttemptsReached | Triggered when the maximum number of attempts is reached |

## Usage Examples

### Example 1: Basic Slider Captcha

```razor
<Captcha @bind-Value="@isVerified" />

<div class="mt-3">
    Verification status: @(isVerified ? "Verified" : "Not verified")
</div>

@code {
    private bool isVerified = false;
}
```

### Example 2: Captcha with Form Integration

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
    
    <div class="mb-3">
        <label>Verification</label>
        <Captcha @bind-Value="@model.IsVerified"
                OnSuccess="@HandleCaptchaSuccess" />
        <ValidationMessage For="@(() => model.IsVerified)" />
    </div>
    
    <Button Type="ButtonType.Submit" IsDisabled="@(!model.IsVerified)">Submit</Button>
</ValidateForm>

@code {
    private UserModel model = new UserModel();
    
    private void HandleValidSubmit()
    {
        // Process the form data
        Console.WriteLine($"Form submitted: {model.Username}, {model.Email}");
    }
    
    private void HandleCaptchaSuccess()
    {
        Console.WriteLine("CAPTCHA verification successful");
    }
    
    public class UserModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Verification is required")]
        [MustBeTrue(ErrorMessage = "Please complete the verification")]
        public bool IsVerified { get; set; }
    }
    
    // Custom validation attribute
    public class MustBeTrueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is bool boolValue && boolValue)
            {
                return ValidationResult.Success;
            }
            
            return new ValidationResult(ErrorMessage);
        }
    }
}
```

### Example 3: Customized Captcha Appearance

```razor
<Captcha @bind-Value="@isVerified"
        Width="400"
        Height="180"
        SliderText="Drag to verify"
        SuccessText="Great! You're human"
        FailureText="Oops! Try again"
        RefreshText="New challenge"
        ClassName="custom-captcha"
        OnSuccess="@HandleSuccess"
        OnFailure="@HandleFailure" />

<div class="mt-3">
    <div class="@statusClass">
        @statusMessage
    </div>
</div>

<style>
    .custom-captcha {
        border-radius: 8px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }
</style>

@code {
    private bool isVerified = false;
    private string statusMessage = "Please complete the verification";
    private string statusClass = "text-muted";
    
    private void HandleSuccess()
    {
        statusMessage = "Verification successful!";
        statusClass = "text-success";
    }
    
    private void HandleFailure()
    {
        statusMessage = "Verification failed. Please try again.";
        statusClass = "text-danger";
    }
}
```

### Example 4: Captcha with Event Handling

```razor
<Captcha @bind-Value="@isVerified"
        OnVerifying="@HandleVerifying"
        OnVerified="@HandleVerified"
        OnSuccess="@HandleSuccess"
        OnFailure="@HandleFailure"
        OnRefresh="@HandleRefresh"
        OnMaxAttemptsReached="@HandleMaxAttemptsReached" />

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
    private bool isVerified = false;
    private List<string> eventLogs = new();
    
    private void HandleVerifying()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Verification in progress...");
    }
    
    private void HandleVerified(bool result)
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Verification completed: {(result ? "Success" : "Failure")}");
    }
    
    private void HandleSuccess()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Verification successful");
    }
    
    private void HandleFailure()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Verification failed");
    }
    
    private void HandleRefresh()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] CAPTCHA refreshed");
    }
    
    private void HandleMaxAttemptsReached()
    {
        eventLogs.Add($"[{DateTime.Now:HH:mm:ss}] Maximum attempts reached");
    }
    
    private void ClearLogs()
    {
        eventLogs.Clear();
    }
}
```

### Example 5: Multiple Captchas on One Page

```razor
<div class="row">
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">
                <h5 class="mb-0">Login Form</h5>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <BootstrapInput Placeholder="Username" />
                </div>
                <div class="mb-3">
                    <BootstrapInput Type="password" Placeholder="Password" />
                </div>
                <div class="mb-3">
                    <Captcha @bind-Value="@loginVerified" />
                </div>
                <Button IsDisabled="@(!loginVerified)">Login</Button>
            </div>
        </div>
    </div>
    
    <div class="col-md-6">
        <div class="card">
            <div class="card-header">
                <h5 class="mb-0">Registration Form</h5>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <BootstrapInput Placeholder="Email" />
                </div>
                <div class="mb-3">
                    <BootstrapInput Type="password" Placeholder="Create Password" />
                </div>
                <div class="mb-3">
                    <Captcha @bind-Value="@registerVerified" />
                </div>
                <Button IsDisabled="@(!registerVerified)">Register</Button>
            </div>
        </div>
    </div>
</div>

@code {
    private bool loginVerified = false;
    private bool registerVerified = false;
}
```

### Example 6: Programmatic Captcha Control

```razor
<Captcha @ref="captchaRef"
        @bind-Value="@isVerified"
        AutoRefreshOnFailure="false"
        OnSuccess="@HandleSuccess"
        OnFailure="@HandleFailure" />

<div class="mt-3">
    <div class="@statusClass">
        @statusMessage
    </div>
</div>

<div class="mt-3">
    <Button Color="Color.Primary" OnClick="RefreshCaptcha">Manual Refresh</Button>
    <Button Color="Color.Secondary" OnClick="ResetStatus">Reset Status</Button>
</div>

@code {
    private Captcha captchaRef;
    private bool isVerified = false;
    private string statusMessage = "Please complete the verification";
    private string statusClass = "text-muted";
    private int failureCount = 0;
    
    private void HandleSuccess()
    {
        statusMessage = "Verification successful!";
        statusClass = "text-success";
        failureCount = 0;
    }
    
    private void HandleFailure()
    {
        failureCount++;
        statusMessage = $"Verification failed ({failureCount} attempts). Please try again.";
        statusClass = "text-danger";
        
        if (failureCount >= 3)
        {
            RefreshCaptcha();
        }
    }
    
    private void RefreshCaptcha()
    {
        captchaRef.Refresh();
        statusMessage = "CAPTCHA refreshed. Please try again.";
        statusClass = "text-muted";
    }
    
    private void ResetStatus()
    {
        isVerified = false;
        statusMessage = "Please complete the verification";
        statusClass = "text-muted";
        failureCount = 0;
    }
}
```

### Example 7: Captcha in a Multi-Step Form

```razor
<div class="card">
    <div class="card-header">
        <h5 class="mb-0">Multi-Step Form - Step @currentStep of 3</h5>
    </div>
    <div class="card-body">
        @if (currentStep == 1)
        {
            <div class="mb-3">
                <label>Personal Information</label>
                <BootstrapInput @bind-Value="@model.Name" Placeholder="Full Name" />
                <BootstrapInput @bind-Value="@model.Email" Type="email" Placeholder="Email" class="mt-2" />
            </div>
            <Button Color="Color.Primary" OnClick="NextStep">Next</Button>
        }
        else if (currentStep == 2)
        {
            <div class="mb-3">
                <label>Account Details</label>
                <BootstrapInput @bind-Value="@model.Username" Placeholder="Username" />
                <BootstrapInput @bind-Value="@model.Password" Type="password" Placeholder="Password" class="mt-2" />
            </div>
            <Button Color="Color.Secondary" OnClick="PreviousStep">Previous</Button>
            <Button Color="Color.Primary" OnClick="NextStep">Next</Button>
        }
        else if (currentStep == 3)
        {
            <div class="mb-3">
                <label>Verification</label>
                <Captcha @bind-Value="@model.IsVerified" />
            </div>
            <div class="mt-3">
                <Button Color="Color.Secondary" OnClick="PreviousStep">Previous</Button>
                <Button Color="Color.Success" IsDisabled="@(!model.IsVerified)" OnClick="SubmitForm">Submit</Button>
            </div>
        }
    </div>
</div>

@if (isSubmitted)
{
    <div class="alert alert-success mt-3">
        Form submitted successfully!
    </div>
}

@code {
    private int currentStep = 1;
    private bool isSubmitted = false;
    private FormModel model = new FormModel();
    
    private void NextStep()
    {
        if (currentStep < 3)
        {
            currentStep++;
        }
    }
    
    private void PreviousStep()
    {
        if (currentStep > 1)
        {
            currentStep--;
        }
    }
    
    private void SubmitForm()
    {
        // Process form submission
        isSubmitted = true;
    }
    
    private class FormModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }
    }
}
```

## Customization Notes

The Captcha component can be customized using the following CSS variables:

```css
:root {
    --bb-captcha-refresh-padding-left: 5px;
    --bb-captcha-radius: 4px;
    --bb-captcha-footer-bg: #f7f9fa;
    --bb-captcha-footer-color: #45494c;
    --bb-captcha-footer-margin-top: 8px;
    --bb-captcha-footer-height: 40px;
    --bb-captcha-bar-border: 1px solid #e4e7eb;
    --bb-captcha-bar-bg: #f7f9fa;
    --bb-captcha-bar-color: #45494c;
    --bb-captcha-bar-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
    --bb-captcha-bar-invalid-border: 1px solid #f56c6c;
    --bb-captcha-bar-invalid-bg: #fef0f0;
    --bb-captcha-bar-invalid-mask-bg: rgba(255, 0, 0, 0.1);
    --bb-captcha-bar-valid-border: 1px solid #67c23a;
    --bb-captcha-bar-valid-bg: #f0f9eb;
    --bb-captcha-bar-valid-mask-bg: rgba(0, 255, 0, 0.1);
}
```

Additionally, you can customize the appearance and behavior of the Captcha component by:

1. Using the `Width` and `Height` properties to adjust the component size
2. Using the `SliderText`, `SuccessText`, and `FailureText` properties to customize messages
3. Using the `RefreshText` property to change the refresh button text
4. Using the `CaptchaType` property to select different verification methods
5. Using the `ThresholdDistance` property to adjust verification sensitivity
6. Using the `AutoRefreshOnFailure` and `MaxAttempts` properties to control retry behavior
7. Applying custom CSS classes to the component using the `ClassName` property