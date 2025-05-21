# Avatar Component

## Overview
The Avatar component in BootstrapBlazor provides a visual representation of a user or entity. It's commonly used in user interfaces to display profile pictures, initials, or icons representing users. The component supports various shapes, sizes, and content types, making it versatile for different design requirements.

## Features
- Multiple shape options (circle, square)
- Various size variants (xs, sm, default, lg, xl, xxl)
- Support for images, icons, and text content
- Customizable background and text colors
- Fallback display when images fail to load
- Responsive design
- Accessibility support
- Integration with other components (lists, cards, comments)

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Shape` | `AvatarShape` | `AvatarShape.Circle` | Sets the shape of the avatar (Circle, Square) |
| `Size` | `Size` | `Size.None` (default) | Sets the size of the avatar (XS, SM, None, LG, XL, XXL) |
| `Src` | `string` | `null` | URL of the image to be displayed |
| `Alt` | `string` | `null` | Alternative text for the image for accessibility |
| `Icon` | `string` | `null` | Icon class name to display an icon instead of an image |
| `Text` | `string` | `null` | Text to display when no image or icon is provided |
| `Color` | `Color` | `Color.None` | Sets the background color of the avatar |
| `OnError` | `Func<Task>` | `null` | Callback function when image loading fails |
| `ChildContent` | `RenderFragment` | `null` | Custom content to display inside the avatar |

## Events

| Event | Description |
| --- | --- |
| `OnError` | Triggered when the image fails to load, allowing for fallback content |
| `OnClick` | Triggered when the avatar is clicked |

## Usage Examples

### Example 1: Basic Avatar with Image
```razor
<Avatar Src="/images/user.jpg" Alt="User Profile" />
```
This example shows a basic circular avatar with an image. The image will be displayed in the default size with a circular shape.

### Example 2: Avatar with Different Shapes and Sizes
```razor
<div class="d-flex align-items-center gap-3">
    <Avatar Shape="AvatarShape.Circle" Size="Size.XS" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Circle" Size="Size.SM" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Circle" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Circle" Size="Size.LG" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Circle" Size="Size.XL" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Circle" Size="Size.XXL" Src="/images/user.jpg" />
</div>

<div class="d-flex align-items-center gap-3 mt-3">
    <Avatar Shape="AvatarShape.Square" Size="Size.XS" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Square" Size="Size.SM" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Square" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Square" Size="Size.LG" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Square" Size="Size.XL" Src="/images/user.jpg" />
    <Avatar Shape="AvatarShape.Square" Size="Size.XXL" Src="/images/user.jpg" />
</div>
```
This example demonstrates avatars with different shapes (circle and square) and sizes (XS, SM, default, LG, XL, XXL).

### Example 3: Avatar with Icon
```razor
<div class="d-flex align-items-center gap-3">
    <Avatar Icon="fa-solid fa-user" Color="Color.Primary" />
    <Avatar Icon="fa-solid fa-user" Color="Color.Success" />
    <Avatar Icon="fa-solid fa-user" Color="Color.Info" />
    <Avatar Icon="fa-solid fa-user" Color="Color.Warning" />
    <Avatar Icon="fa-solid fa-user" Color="Color.Danger" />
    <Avatar Icon="fa-solid fa-user" Color="Color.Secondary" />
    <Avatar Icon="fa-solid fa-user" Color="Color.Dark" />
</div>
```
This example shows avatars with icons instead of images, using different background colors.

### Example 4: Avatar with Text
```razor
<div class="d-flex align-items-center gap-3">
    <Avatar Text="JD" Color="Color.Primary" />
    <Avatar Text="AB" Color="Color.Success" />
    <Avatar Text="CK" Color="Color.Info" />
    <Avatar Text="MN" Color="Color.Warning" />
    <Avatar Text="XY" Color="Color.Danger" />
</div>
```
This example demonstrates avatars displaying text (typically initials) with different background colors.

### Example 5: Avatar with Fallback
```razor
<Avatar Src="/images/nonexistent.jpg" OnError="HandleImageError">
    <span>JD</span>
</Avatar>

@code {
    private async Task HandleImageError()
    {
        // Log error or perform other actions
        Console.WriteLine("Image failed to load");
    }
}
```
This example shows an avatar with a fallback mechanism. If the image fails to load, it will display the content provided in the `ChildContent` (in this case, the initials "JD").

### Example 6: Avatar in a User Profile Card
```razor
<div class="card" style="width: 18rem;">
    <div class="card-body d-flex align-items-center">
        <Avatar Src="/images/user.jpg" Size="Size.LG" />
        <div class="ms-3">
            <h5 class="card-title mb-0">John Doe</h5>
            <p class="card-text text-muted">Software Developer</p>
        </div>
    </div>
    <ul class="list-group list-group-flush">
        <li class="list-group-item">Email: john.doe@example.com</li>
        <li class="list-group-item">Location: New York, USA</li>
        <li class="list-group-item">Joined: January 2023</li>
    </ul>
</div>
```
This example demonstrates using an avatar as part of a user profile card, combining it with other information.

### Example 7: Avatar Group
```razor
<div class="d-flex">
    <Avatar Src="/images/user1.jpg" style="margin-right: -8px; border: 2px solid white;" />
    <Avatar Src="/images/user2.jpg" style="margin-right: -8px; border: 2px solid white;" />
    <Avatar Src="/images/user3.jpg" style="margin-right: -8px; border: 2px solid white;" />
    <Avatar Text="+5" Color="Color.Secondary" style="border: 2px solid white;" />
</div>
```
This example shows how to create an avatar group, commonly used to display multiple users in a compact way, with an indicator for additional users.

## Customization

The Avatar component can be customized using CSS variables:

```css
.avatar {
    --bb-avatar-width: 50px;                /* Default width */
    --bb-avatar-height: 50px;               /* Default height */
    --bb-avatar-border-radius: 50%;         /* Border radius for circle shape */
    --bb-avatar-bg: #f0f0f0;                /* Default background color */
    --bb-avatar-color: #666;                /* Default text color */
    --bb-avatar-icon-font-size: 1.5rem;     /* Icon size */
}

/* Size variants */
.avatar-xs {
    --bb-avatar-width: 30px;
    --bb-avatar-height: 30px;
}

.avatar-sm {
    --bb-avatar-width: 40px;
    --bb-avatar-height: 40px;
}

.avatar-lg {
    --bb-avatar-width: 60px;
    --bb-avatar-height: 60px;
}

.avatar-xl {
    --bb-avatar-width: 70px;
    --bb-avatar-height: 70px;
}

.avatar-xxl {
    --bb-avatar-width: 80px;
    --bb-avatar-height: 80px;
}

/* Square shape */
.avatar-square {
    --bb-avatar-border-radius: 4px;
}
```

You can override these variables in your CSS to customize the appearance of the Avatar component according to your design requirements.