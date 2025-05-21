# Drawer Component Documentation

## Overview
The Drawer component in BootstrapBlazor provides a panel that slides in from the edge of the screen. It's commonly used for displaying additional content or forms without navigating away from the current page, similar to a modal but with a different visual presentation.

## Features
- Multiple placement options (left, right, top, bottom)
- Customizable size
- Backdrop options
- Header and footer customization
- Scrollable content
- Animation effects
- Programmatic control (show/hide)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| Title | string | null | Title text for the drawer header |
| Width | int | 300 | Width of the drawer when placement is Left or Right |
| Height | int | 300 | Height of the drawer when placement is Top or Bottom |
| Placement | Placement | Right | Position of the drawer (Left, Right, Top, Bottom) |
| Size | Size | Medium | Size of the drawer (Small, Medium, Large, ExtraLarge) |
| IsBackdrop | bool | true | Whether to show a backdrop behind the drawer |
| IsScrolling | bool | false | Whether the drawer body should be scrollable when content exceeds the height |
| ShowCloseButton | bool | true | Whether to show the close button in the header |
| ShowFooter | bool | false | Whether to show the footer section |
| BodyTemplate | RenderFragment | null | Template for the drawer body content |
| HeaderTemplate | RenderFragment | null | Template for the drawer header content |
| FooterTemplate | RenderFragment | null | Template for the drawer footer content |

## Events

| Event | Description |
| --- | --- |
| OnClose | Triggered when the drawer is closed |
| OnShown | Triggered after the drawer is fully shown |
| OnHidden | Triggered after the drawer is fully hidden |

## Methods

| Method | Description |
| --- | --- |
| Show() | Shows the drawer |
| Hide() | Hides the drawer |
| Toggle() | Toggles the drawer visibility |

## Usage Examples

### Example 1: Basic Drawer

```razor
<Button Color="Color.Primary" OnClick="@ShowDrawer">Open Drawer</Button>

<Drawer @ref="drawerRef" Title="Basic Drawer">
    <BodyTemplate>
        <p>This is a basic drawer with default settings.</p>
        <p>It appears from the right side of the screen.</p>
    </BodyTemplate>
</Drawer>

@code {
    private Drawer? drawerRef;

    private void ShowDrawer()
    {
        drawerRef?.Show();
    }
}
```

### Example 2: Drawer with Different Placements

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Left))">Left Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Right))">Right Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Top))">Top Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Bottom))">Bottom Drawer</Button>
</div>

<Drawer @ref="drawerRef" Title="@($"{currentPlacement} Drawer")" Placement="@currentPlacement">
    <BodyTemplate>
        <p>This drawer appears from the @currentPlacement.ToString().ToLower() of the screen.</p>
    </BodyTemplate>
</Drawer>

@code {
    private Drawer? drawerRef;
    private Placement currentPlacement = Placement.Right;

    private void ShowDrawer(Placement placement)
    {
        currentPlacement = placement;
        drawerRef?.Show();
    }
}
```

### Example 3: Drawer with Custom Size

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Size.Small))">Small Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Size.Medium))">Medium Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Size.Large))">Large Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Size.ExtraLarge))">Extra Large Drawer</Button>
</div>

<Drawer @ref="drawerRef" Title="@($"{currentSize} Drawer")" Size="@currentSize">
    <BodyTemplate>
        <p>This drawer is displayed with the size: @currentSize</p>
    </BodyTemplate>
</Drawer>

@code {
    private Drawer? drawerRef;
    private Size currentSize = Size.Medium;

    private void ShowDrawer(Size size)
    {
        currentSize = size;
        drawerRef?.Show();
    }
}
```

### Example 4: Drawer with Custom Width/Height

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Right, 500))">Wide Right Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Left, 500))">Wide Left Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Top, 0, 400))">Tall Top Drawer</Button>
    <Button Color="Color.Primary" OnClick="@(() => ShowDrawer(Placement.Bottom, 0, 400))">Tall Bottom Drawer</Button>
</div>

<Drawer @ref="drawerRef" 
        Title="Custom Size Drawer" 
        Placement="@currentPlacement"
        Width="@currentWidth"
        Height="@currentHeight">
    <BodyTemplate>
        <p>This drawer has custom dimensions.</p>
        @if (currentPlacement == Placement.Left || currentPlacement == Placement.Right)
        {
            <p>Width: @currentWidth px</p>
        }
        else
        {
            <p>Height: @currentHeight px</p>
        }
    </BodyTemplate>
</Drawer>

@code {
    private Drawer? drawerRef;
    private Placement currentPlacement = Placement.Right;
    private int currentWidth = 300;
    private int currentHeight = 300;

    private void ShowDrawer(Placement placement, int width = 300, int height = 300)
    {
        currentPlacement = placement;
        currentWidth = width;
        currentHeight = height;
        drawerRef?.Show();
    }
}
```

### Example 5: Drawer with Form

```razor
<Button Color="Color.Primary" OnClick="@ShowDrawer">Edit Profile</Button>

<Drawer @ref="drawerRef" Title="Edit Profile" ShowFooter="true">
    <BodyTemplate>
        <Form Model="@profile" OnValidSubmit="@HandleValidSubmit">
            <div class="mb-3">
                <BootstrapInput @bind-Value="@profile.Name" placeholder="Enter name" />
            </div>
            
            <div class="mb-3">
                <BootstrapInput @bind-Value="@profile.Email" placeholder="Enter email" />
            </div>
            
            <div class="mb-3">
                <BootstrapInput @bind-Value="@profile.Bio" placeholder="Enter bio" rows="3" />
            </div>
        </Form>
    </BodyTemplate>
    <FooterTemplate>
        <Button Color="Color.Secondary" OnClick="@CloseDrawer">Cancel</Button>
        <Button Color="Color.Primary" OnClick="@SaveProfile">Save</Button>
    </FooterTemplate>
</Drawer>

@code {
    private Drawer? drawerRef;
    private ProfileModel profile = new ProfileModel();

    protected override void OnInitialized()
    {
        // Initialize with sample data
        profile = new ProfileModel
        {
            Name = "John Doe",
            Email = "john.doe@example.com",
            Bio = "Software developer with 5 years of experience."
        };
    }

    private void ShowDrawer()
    {
        drawerRef?.Show();
    }

    private void CloseDrawer()
    {
        drawerRef?.Hide();
    }

    private void SaveProfile()
    {
        // Save profile logic
        Console.WriteLine($"Saving profile: {profile.Name}");
        drawerRef?.Hide();
    }

    private void HandleValidSubmit()
    {
        SaveProfile();
    }

    private class ProfileModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = "";
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = "";
        
        public string Bio { get; set; } = "";
    }
}
```

### Example 6: Drawer with Custom Header and Footer

```razor
<Button Color="Color.Primary" OnClick="@ShowDrawer">Open Custom Drawer</Button>

<Drawer @ref="drawerRef" ShowFooter="true">
    <HeaderTemplate>
        <div class="d-flex justify-content-between align-items-center w-100 p-3">
            <div class="d-flex align-items-center">
                <i class="fa-solid fa-user-circle me-2 fs-4"></i>
                <h5 class="mb-0">User Profile</h5>
            </div>
            <Button Color="Color.None" OnClick="@CloseDrawer">
                <i class="fa-solid fa-times"></i>
            </Button>
        </div>
    </HeaderTemplate>
    <BodyTemplate>
        <div class="p-3">
            <div class="text-center mb-4">
                <div class="avatar mb-3" style="font-size: 4rem;">
                    <i class="fa-solid fa-user-circle"></i>
                </div>
                <h4>John Doe</h4>
                <p class="text-muted">Software Developer</p>
            </div>
            
            <div class="mb-3">
                <h6>Contact Information</h6>
                <p><i class="fa-solid fa-envelope me-2"></i> john.doe@example.com</p>
                <p><i class="fa-solid fa-phone me-2"></i> (123) 456-7890</p>
            </div>
            
            <div class="mb-3">
                <h6>Skills</h6>
                <div class="d-flex flex-wrap gap-2">
                    <Tag Color="Color.Primary">C#</Tag>
                    <Tag Color="Color.Primary">Blazor</Tag>
                    <Tag Color="Color.Primary">JavaScript</Tag>
                    <Tag Color="Color.Primary">CSS</Tag>
                    <Tag Color="Color.Primary">HTML</Tag>
                </div>
            </div>
        </div>
    </BodyTemplate>
    <FooterTemplate>
        <div class="d-flex justify-content-between w-100 p-3">
            <Button Color="Color.Secondary" OnClick="@CloseDrawer">Close</Button>
            <Button Color="Color.Primary" OnClick="@EditProfile">Edit Profile</Button>
        </div>
    </FooterTemplate>
</Drawer>

@code {
    private Drawer? drawerRef;

    private void ShowDrawer()
    {
        drawerRef?.Show();
    }

    private void CloseDrawer()
    {
        drawerRef?.Hide();
    }

    private void EditProfile()
    {
        // Edit profile logic
        Console.WriteLine("Editing profile");
    }
}
```

### Example 7: Nested Drawers

```razor
<Button Color="Color.Primary" OnClick="@ShowMainDrawer">Open Main Drawer</Button>

<Drawer @ref="mainDrawerRef" Title="Main Drawer">
    <BodyTemplate>
        <p>This is the main drawer.</p>
        <p>You can open a nested drawer from here.</p>
        <Button Color="Color.Secondary" OnClick="@ShowNestedDrawer">Open Nested Drawer</Button>
    </BodyTemplate>
</Drawer>

<Drawer @ref="nestedDrawerRef" Title="Nested Drawer" Placement="Placement.Left">
    <BodyTemplate>
        <p>This is a nested drawer.</p>
        <p>It appears from the left side while the main drawer is still open.</p>
    </BodyTemplate>
</Drawer>

@code {
    private Drawer? mainDrawerRef;
    private Drawer? nestedDrawerRef;

    private void ShowMainDrawer()
    {
        mainDrawerRef?.Show();
    }

    private void ShowNestedDrawer()
    {
        nestedDrawerRef?.Show();
    }
}
```

## Notes

- The Drawer component is similar to Modal but slides in from the edge of the screen instead of appearing in the center.
- Drawers are commonly used for navigation menus, filter panels, or detail views in responsive designs.
- When using a drawer with a form, it's often better to handle the form submission within the drawer rather than having the form submit to a different page.
- For accessibility, ensure that keyboard navigation works correctly within the drawer and that focus is properly managed when the drawer opens and closes.
- The component supports nesting, allowing you to open a drawer from within another drawer.
- When using custom width/height, be mindful of responsive design considerations to ensure the drawer works well on different screen sizes.