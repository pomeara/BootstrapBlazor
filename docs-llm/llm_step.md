# Step Component

## Overview
The Step component in BootstrapBlazor provides a visual representation of a multi-step process or workflow. It helps users understand their progress through a sequence of actions, showing completed steps, the current step, and upcoming steps. This component is commonly used in multi-page forms, checkout processes, onboarding flows, and any scenario where a task is broken down into sequential steps.

## Features
- Multiple display modes (horizontal, vertical)
- Customizable step status (wait, process, finish, error)
- Icon support for each step
- Description and subtitle support
- Clickable steps for navigation
- Responsive design
- Custom step templates
- Progress indicator
- Dynamic step generation
- Accessibility support

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<StepItem>` | `null` | Collection of step items to display |
| `ActiveIndex` | `int` | `0` | Index of the currently active step (0-based) |
| `IsVertical` | `bool` | `false` | When true, displays steps vertically instead of horizontally |
| `IsClickable` | `bool` | `false` | When true, allows clicking on steps to navigate |
| `IsNavigation` | `bool` | `false` | When true, shows navigation buttons (previous/next) |
| `ShowProgress` | `bool` | `false` | When true, shows a progress indicator |
| `StepTemplate` | `RenderFragment<StepItem>` | `null` | Custom template for rendering step items |
| `ChildContent` | `RenderFragment` | `null` | Content to display in the step content area |

## StepItem Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Text` | `string` | `null` | Text label for the step |
| `Description` | `string` | `null` | Additional description for the step |
| `Icon` | `string` | `null` | Icon class for the step |
| `Status` | `StepStatus` | `StepStatus.Wait` | Status of the step (Wait, Process, Finish, Error) |
| `IsDisabled` | `bool` | `false` | When true, the step is disabled and cannot be clicked |
| `Template` | `RenderFragment` | `null` | Custom template for this specific step |
| `ContentTemplate` | `RenderFragment` | `null` | Custom template for the step content |

## Events

| Event | Description |
| --- | --- |
| `OnStepClick` | Triggered when a step is clicked (if IsClickable is true) |
| `OnStepChange` | Triggered when the active step changes |

## Usage Examples

### Example 1: Basic Horizontal Steps
```csharp
<Step ActiveIndex="@activeIndex">
    <Items>
        <StepItem Text="Step 1" Status="StepStatus.Finish" />
        <StepItem Text="Step 2" Status="StepStatus.Process" />
        <StepItem Text="Step 3" Status="StepStatus.Wait" />
        <StepItem Text="Step 4" Status="StepStatus.Wait" />
    </Items>
</Step>

@code {
    private int activeIndex = 1; // 0-based index, so this is the second step
}
```
This example shows a basic horizontal step component with four steps, where the first step is completed, the second step is in progress, and the remaining steps are waiting.

### Example 2: Vertical Steps with Icons and Descriptions
```csharp
<Step ActiveIndex="@activeIndex" IsVertical="true">
    <Items>
        <StepItem Text="Login" Description="Sign in to your account" Icon="fa fa-user" Status="StepStatus.Finish" />
        <StepItem Text="Verification" Description="Verify your identity" Icon="fa fa-shield-alt" Status="StepStatus.Process" />
        <StepItem Text="Payment" Description="Enter payment details" Icon="fa fa-credit-card" Status="StepStatus.Wait" />
        <StepItem Text="Confirmation" Description="Review and confirm order" Icon="fa fa-check-circle" Status="StepStatus.Wait" />
    </Items>
</Step>

@code {
    private int activeIndex = 1;
}
```
This example demonstrates vertical steps with icons and descriptions, providing more context for each step in the process.

### Example 3: Clickable Steps for Navigation
```csharp
<Step ActiveIndex="@activeIndex" IsClickable="true" OnStepClick="OnStepClick">
    <Items>
        <StepItem Text="Personal Info" Status="@GetStatus(0)" />
        <StepItem Text="Contact Info" Status="@GetStatus(1)" />
        <StepItem Text="Payment Details" Status="@GetStatus(2)" />
        <StepItem Text="Review" Status="@GetStatus(3)" />
    </Items>
</Step>

<div class="mt-4 p-3 border rounded">
    @switch (activeIndex)
    {
        case 0:
            <h4>Personal Information</h4>
            <p>Enter your personal details below.</p>
            <!-- Personal info form fields -->
            break;
        case 1:
            <h4>Contact Information</h4>
            <p>Enter your contact details below.</p>
            <!-- Contact info form fields -->
            break;
        case 2:
            <h4>Payment Details</h4>
            <p>Enter your payment information below.</p>
            <!-- Payment form fields -->
            break;
        case 3:
            <h4>Review</h4>
            <p>Review your information before submitting.</p>
            <!-- Review summary -->
            break;
    }
    
    <div class="mt-3 d-flex justify-content-between">
        <Button Disabled="@(activeIndex == 0)" OnClick="PreviousStep">Previous</Button>
        <Button Color="Color.Primary" OnClick="NextStep">
            @(activeIndex == 3 ? "Submit" : "Next")
        </Button>
    </div>
</div>

@code {
    private int activeIndex = 0;
    private List<bool> completedSteps = new() { false, false, false, false };
    
    private StepStatus GetStatus(int index)
    {
        if (index == activeIndex)
            return StepStatus.Process;
        else if (completedSteps[index])
            return StepStatus.Finish;
        else
            return StepStatus.Wait;
    }
    
    private void OnStepClick(int index)
    {
        // Only allow navigation to completed steps or the next available step
        if (index <= activeIndex || (index == activeIndex + 1) || completedSteps[index])
        {
            activeIndex = index;
        }
    }
    
    private void PreviousStep()
    {
        if (activeIndex > 0)
        {
            activeIndex--;
        }
    }
    
    private void NextStep()
    {
        if (activeIndex < 3)
        {
            // Mark current step as completed
            completedSteps[activeIndex] = true;
            activeIndex++;
        }
        else
        {
            // Handle form submission
            // This would typically save the data and navigate to a confirmation page
        }
    }
}
```
This example shows how to create a multi-step form with clickable steps for navigation, along with previous and next buttons. The content changes based on the active step.

### Example 4: Steps with Progress Indicator
```csharp
<Step ActiveIndex="@activeIndex" ShowProgress="true">
    <Items>
        <StepItem Text="Step 1" Status="StepStatus.Finish" />
        <StepItem Text="Step 2" Status="StepStatus.Finish" />
        <StepItem Text="Step 3" Status="StepStatus.Process" />
        <StepItem Text="Step 4" Status="StepStatus.Wait" />
        <StepItem Text="Step 5" Status="StepStatus.Wait" />
    </Items>
</Step>

<div class="mt-3 d-flex justify-content-end">
    <Button Color="Color.Primary" OnClick="NextStep">Next Step</Button>
</div>

@code {
    private int activeIndex = 2;
    
    private void NextStep()
    {
        if (activeIndex < 4)
        {
            activeIndex++;
        }
    }
}
```
This example demonstrates steps with a progress indicator, showing how far the user has progressed through the workflow.

### Example 5: Custom Step Templates
```csharp
<Step ActiveIndex="@activeIndex">
    <StepTemplate Context="step">
        <div class="custom-step @(step.Status == StepStatus.Process ? "active" : "")">
            <div class="step-icon">
                @if (step.Status == StepStatus.Finish)
                {
                    <i class="fa fa-check"></i>
                }
                else if (step.Status == StepStatus.Error)
                {
                    <i class="fa fa-times"></i>
                }
                else
                {
                    <span>@(step.Index + 1)</span>
                }
            </div>
            <div class="step-content">
                <div class="step-title">@step.Text</div>
                @if (!string.IsNullOrEmpty(step.Description))
                {
                    <div class="step-description">@step.Description</div>
                }
            </div>
        </div>
    </StepTemplate>
    <Items>
        <StepItem Text="Account Created" Description="Your account has been created" Status="StepStatus.Finish" />
        <StepItem Text="Profile Setup" Description="Set up your profile information" Status="StepStatus.Process" />
        <StepItem Text="Preferences" Description="Configure your preferences" Status="StepStatus.Wait" />
        <StepItem Text="Completion" Description="Complete the setup process" Status="StepStatus.Wait" />
    </Items>
</Step>

<style>
    .custom-step {
        display: flex;
        align-items: center;
        padding: 10px;
    }
    
    .custom-step.active {
        background-color: rgba(0, 123, 255, 0.1);
        border-radius: 4px;
    }
    
    .step-icon {
        width: 32px;
        height: 32px;
        border-radius: 50%;
        background-color: #e9ecef;
        display: flex;
        align-items: center;
        justify-content: center;
        margin-right: 12px;
    }
    
    .custom-step.active .step-icon {
        background-color: #007bff;
        color: white;
    }
    
    .step-title {
        font-weight: bold;
    }
    
    .step-description {
        font-size: 0.875rem;
        color: #6c757d;
    }
</style>

@code {
    private int activeIndex = 1;
}
```
This example shows how to create custom step templates with custom styling, providing a unique look and feel for the steps.

### Example 6: Dynamic Step Generation
```csharp
<Step ActiveIndex="@activeIndex">
    <Items>
        @foreach (var step in steps)
        {
            <StepItem Text="@step.Title" 
                      Description="@step.Description" 
                      Status="@step.Status" 
                      Icon="@step.Icon" />
        }
    </Items>
</Step>

@code {
    private int activeIndex = 0;
    private List<StepData> steps = new();
    
    protected override void OnInitialized()
    {
        // Generate steps dynamically
        steps = new List<StepData>
        {
            new StepData { Title = "Requirements", Description = "Check system requirements", Icon = "fa fa-list-check", Status = StepStatus.Finish },
            new StepData { Title = "Download", Description = "Download installation files", Icon = "fa fa-download", Status = StepStatus.Process },
            new StepData { Title = "Installation", Description = "Install the application", Icon = "fa fa-cog", Status = StepStatus.Wait },
            new StepData { Title = "Configuration", Description = "Configure settings", Icon = "fa fa-sliders", Status = StepStatus.Wait },
            new StepData { Title = "Completion", Description = "Complete the setup", Icon = "fa fa-flag-checkered", Status = StepStatus.Wait }
        };
    }
    
    private class StepData
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public StepStatus Status { get; set; }
    }
}
```
This example demonstrates how to dynamically generate steps from a data source, which is useful when the number of steps or their content may vary.

### Example 7: Error State and Step Validation
```csharp
<Step ActiveIndex="@activeIndex" IsNavigation="true" OnStepChange="OnStepChange">
    <Items>
        <StepItem Text="Basic Info" Status="@GetStepStatus(0)" />
        <StepItem Text="Contact Info" Status="@GetStepStatus(1)" />
        <StepItem Text="Account Setup" Status="@GetStepStatus(2)" />
        <StepItem Text="Confirmation" Status="@GetStepStatus(3)" />
    </Items>
    <ChildContent>
        <div class="p-3">
            @switch (activeIndex)
            {
                case 0:
                    <EditForm Model="@model" OnValidSubmit="ValidateStep">
                        <DataAnnotationsValidator />
                        <ValidationSummary />
                        
                        <div class="mb-3">
                            <label for="firstName" class="form-label">First Name</label>
                            <InputText id="firstName" @bind-Value="model.FirstName" class="form-control" />
                            <ValidationMessage For="@(() => model.FirstName)" />
                        </div>
                        
                        <div class="mb-3">
                            <label for="lastName" class="form-label">Last Name</label>
                            <InputText id="lastName" @bind-Value="model.LastName" class="form-control" />
                            <ValidationMessage For="@(() => model.LastName)" />
                        </div>
                        
                        <Button Type="ButtonType.Submit" Color="Color.Primary">Validate & Continue</Button>
                    </EditForm>
                    break;
                    
                case 1:
                    <EditForm Model="@model" OnValidSubmit="ValidateStep">
                        <DataAnnotationsValidator />
                        <ValidationSummary />
                        
                        <div class="mb-3">
                            <label for="email" class="form-label">Email</label>
                            <InputText id="email" @bind-Value="model.Email" class="form-control" />
                            <ValidationMessage For="@(() => model.Email)" />
                        </div>
                        
                        <div class="mb-3">
                            <label for="phone" class="form-label">Phone</label>
                            <InputText id="phone" @bind-Value="model.Phone" class="form-control" />
                            <ValidationMessage For="@(() => model.Phone)" />
                        </div>
                        
                        <Button Type="ButtonType.Submit" Color="Color.Primary">Validate & Continue</Button>
                    </EditForm>
                    break;
                    
                case 2:
                    <EditForm Model="@model" OnValidSubmit="ValidateStep">
                        <DataAnnotationsValidator />
                        <ValidationSummary />
                        
                        <div class="mb-3">
                            <label for="username" class="form-label">Username</label>
                            <InputText id="username" @bind-Value="model.Username" class="form-control" />
                            <ValidationMessage For="@(() => model.Username)" />
                        </div>
                        
                        <div class="mb-3">
                            <label for="password" class="form-label">Password</label>
                            <InputText id="password" type="password" @bind-Value="model.Password" class="form-control" />
                            <ValidationMessage For="@(() => model.Password)" />
                        </div>
                        
                        <Button Type="ButtonType.Submit" Color="Color.Primary">Validate & Continue</Button>
                    </EditForm>
                    break;
                    
                case 3:
                    <div>
                        <h4>Registration Summary</h4>
                        <p>Please review your information before submitting:</p>
                        
                        <dl class="row">
                            <dt class="col-sm-3">Name:</dt>
                            <dd class="col-sm-9">@model.FirstName @model.LastName</dd>
                            
                            <dt class="col-sm-3">Email:</dt>
                            <dd class="col-sm-9">@model.Email</dd>
                            
                            <dt class="col-sm-3">Phone:</dt>
                            <dd class="col-sm-9">@model.Phone</dd>
                            
                            <dt class="col-sm-3">Username:</dt>
                            <dd class="col-sm-9">@model.Username</dd>
                        </dl>
                        
                        <Button Color="Color.Success" OnClick="SubmitForm">Submit Registration</Button>
                    </div>
                    break;
            }
        </div>
    </ChildContent>
</Step>

@code {
    private int activeIndex = 0;
    private RegistrationModel model = new();
    private List<bool> stepValidated = new() { false, false, false, true }; // Last step doesn't need validation
    private List<bool> stepError = new() { false, false, false, false };
    
    private StepStatus GetStepStatus(int index)
    {
        if (index == activeIndex)
            return StepStatus.Process;
        else if (stepError[index])
            return StepStatus.Error;
        else if (stepValidated[index])
            return StepStatus.Finish;
        else
            return StepStatus.Wait;
    }
    
    private void OnStepChange(int newIndex)
    {
        // Only allow navigation to validated steps or the current step + 1
        if (stepValidated[newIndex] || newIndex == activeIndex || newIndex == activeIndex + 1)
        {
            activeIndex = newIndex;
        }
    }
    
    private void ValidateStep()
    {
        // Mark current step as validated
        stepValidated[activeIndex] = true;
        stepError[activeIndex] = false;
        
        // Move to next step
        if (activeIndex < 3)
        {
            activeIndex++;
        }
    }
    
    private void SubmitForm()
    {
        // Here you would typically submit the form data to your backend
        Console.WriteLine("Form submitted successfully!");
        
        // Reset the form or navigate to a success page
    }
    
    private class RegistrationModel
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 50 characters")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "Username is required")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Username must be between 4 and 20 characters")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", 
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
        public string Password { get; set; }
    }
}
```
This example demonstrates a multi-step form with validation, showing how to handle error states and ensure each step is properly validated before proceeding.

## CSS Customization

The Step component can be customized using the following CSS variables:

```css
--bb-step-size: 32px;
--bb-step-font-size: 16px;
--bb-step-line-height: 32px;
--bb-step-icon-size: 16px;
--bb-step-title-font-size: 16px;
--bb-step-description-font-size: 14px;
--bb-step-description-color: #6c757d;
--bb-step-wait-color: #bfbfbf;
--bb-step-wait-bg: #f5f5f5;
--bb-step-process-color: #1890ff;
--bb-step-process-bg: #e6f7ff;
--bb-step-finish-color: #52c41a;
--bb-step-finish-bg: #f6ffed;
--bb-step-error-color: #ff4d4f;
--bb-step-error-bg: #fff2f0;
--bb-step-disabled-color: #d9d9d9;
--bb-step-line-color: #e8e8e8;
--bb-step-line-width: 1px;
--bb-step-line-style: solid;
--bb-step-connector-padding: 8px;
--bb-step-item-margin: 16px;
```

## Service Integration

The Step component can be integrated with the `StepService` for more advanced scenarios:

```csharp
@inject StepService StepService

<Step ActiveIndex="@activeIndex">
    <Items>
        @foreach (var step in StepService.Steps)
        {
            <StepItem Text="@step.Text" 
                      Description="@step.Description" 
                      Status="@step.Status" 
                      Icon="@step.Icon" />
        }
    </Items>
</Step>

@code {
    private int activeIndex = 0;
    
    protected override void OnInitialized()
    {
        // Subscribe to step changes
        StepService.OnStepChanged += HandleStepChanged;
        
        // Initialize steps if needed
        if (!StepService.Steps.Any())
        {
            StepService.InitializeSteps(new List<StepItem>
            {
                new StepItem { Text = "Step 1", Status = StepStatus.Process },
                new StepItem { Text = "Step 2", Status = StepStatus.Wait },
                new StepItem { Text = "Step 3", Status = StepStatus.Wait }
            });
        }
    }
    
    private void HandleStepChanged(int index)
    {
        activeIndex = index;
        StateHasChanged();
    }
    
    public void Dispose()
    {
        // Unsubscribe from events
        StepService.OnStepChanged -= HandleStepChanged;
    }
}
```

To use the `StepService`, you need to register it in your application's service collection:

```csharp
builder.Services.AddBootstrapBlazor();
// or specifically
builder.Services.AddSingleton<StepService>();
```

## Notes

1. **Accessibility**: The Step component includes ARIA attributes for better accessibility. It uses `aria-current` to indicate the current step and appropriate roles for navigation.

2. **Responsive Design**: For horizontal steps, consider the available screen width. On smaller screens, the component may need to adjust its layout or switch to vertical mode to ensure all steps are visible and usable.

3. **Performance**: For a large number of steps, consider using virtualization or pagination to improve performance, especially on mobile devices.

4. **Integration with Routing**: The Step component can be integrated with Blazor's routing system to synchronize the active step with the current route, providing a seamless navigation experience.

5. **State Management**: When using the Step component for multi-step forms or processes, consider using a state management solution to persist user input across steps, especially if users might navigate away and return later.