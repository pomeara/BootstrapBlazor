# Light Component

## Overview
The Light component in BootstrapBlazor provides a customizable lighting or highlighting effect that can be used to draw attention to specific elements or areas of the user interface. It can create visual focus points, simulate light sources, or provide interactive highlighting effects. This component is particularly useful for creating engaging user experiences, highlighting important information, or adding visual interest to otherwise static interfaces.

## Features
- **Customizable Light Source**: Configure the position, size, intensity, and color of the light effect
- **Multiple Light Types**: Support for spotlight, ambient, point, and directional light effects
- **Animation Support**: Animate light properties like position, intensity, and color
- **Interactive Positioning**: Optionally follow cursor movement for interactive light effects
- **Gradient Options**: Create gradient light effects with multiple colors
- **Shadow Effects**: Optional shadow casting for enhanced realism
- **Performance Optimized**: Uses CSS and minimal JavaScript for efficient rendering
- **Responsive Design**: Adapts to different screen sizes and orientations
- **Customizable Falloff**: Control how the light intensity diminishes with distance
- **Blend Modes**: Various blend modes for different visual effects
- **Multiple Instances**: Support for multiple light sources on a single page

## Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `Type` | LightType | LightType.Spotlight | The type of light effect (Spotlight, Ambient, Point, Directional) |
| `Color` | string | "#ffffff" | The color of the light in hexadecimal, RGB, or named format |
| `Intensity` | double | 1.0 | The brightness of the light (0.0 to 1.0) |
| `Size` | int | 300 | The size of the light effect in pixels |
| `X` | double? | null | The horizontal position of the light (0.0 to 1.0 or null for center) |
| `Y` | double? | null | The vertical position of the light (0.0 to 1.0 or null for center) |
| `FollowCursor` | bool | false | Whether the light should follow the cursor position |
| `Animated` | bool | false | Whether the light should be animated |
| `AnimationDuration` | int | 2000 | The duration of the animation in milliseconds |
| `AnimationType` | AnimationType | AnimationType.None | The type of animation (None, Pulse, Move, Flicker, ColorShift) |
| `Falloff` | double | 0.8 | How quickly the light intensity diminishes with distance (0.0 to 1.0) |
| `BlendMode` | BlendMode | BlendMode.Normal | The blend mode for the light effect |
| `CastShadow` | bool | false | Whether the light should cast shadows |
| `ShadowIntensity` | double | 0.5 | The intensity of the shadow (0.0 to 1.0) |
| `ShadowColor` | string | "#000000" | The color of the shadow |
| `Gradient` | bool | false | Whether to use a gradient effect for the light |
| `GradientColors` | List<string> | null | List of colors for the gradient effect |
| `Enabled` | bool | true | Whether the light effect is enabled |
| `ZIndex` | int | 10 | The z-index of the light element |
| `TargetSelector` | string | null | CSS selector for the target element to apply the light effect to |
| `Class` | string | null | Additional CSS class for the light element |

## Events

| Event | Description |
|-------|-------------|
| `OnPositionChanged` | Triggered when the position of the light changes |
| `OnIntensityChanged` | Triggered when the intensity of the light changes |
| `OnColorChanged` | Triggered when the color of the light changes |
| `OnMouseEnter` | Triggered when the mouse enters the light area |
| `OnMouseLeave` | Triggered when the mouse leaves the light area |
| `OnClick` | Triggered when the light area is clicked |

## Usage Examples

### Example 1: Basic Spotlight Effect
```razor
<div class="light-demo-container">
    <div class="content-box">
        <h3>Spotlight Effect</h3>
        <p>This example shows a basic spotlight effect positioned in the center of the container.</p>
        
        <Light Type="LightType.Spotlight" 
               Color="#ffcc00" 
               Intensity="0.8" 
               Size="300" />
    </div>
</div>

<style>
    .light-demo-container {
        position: relative;
        width: 100%;
        height: 300px;
        background-color: #2c3e50;
        color: white;
        overflow: hidden;
        margin-bottom: 30px;
    }
    
    .content-box {
        position: relative;
        z-index: 2;
        padding: 20px;
        text-align: center;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        align-items: center;
    }
</style>
```

### Example 2: Interactive Cursor-Following Light
```razor
<div class="light-demo-container">
    <div class="content-box">
        <h3>Interactive Light</h3>
        <p>Move your cursor around this container to see the light follow your movements.</p>
        
        <Light Type="LightType.Spotlight" 
               Color="#00aaff" 
               Intensity="0.7" 
               Size="200" 
               FollowCursor="true" />
    </div>
</div>
```

### Example 3: Animated Pulsing Light
```razor
<div class="light-demo-container">
    <div class="content-box">
        <h3>Pulsing Light Effect</h3>
        <p>This light pulses by animating its intensity and size.</p>
        
        <Light Type="LightType.Point" 
               Color="#ff5500" 
               Intensity="0.8" 
               Size="250" 
               Animated="true" 
               AnimationType="AnimationType.Pulse" 
               AnimationDuration="3000" />
    </div>
</div>
```

### Example 4: Multiple Light Sources
```razor
<div class="light-demo-container">
    <div class="content-box">
        <h3>Multiple Light Sources</h3>
        <p>This example demonstrates using multiple light sources with different colors and positions.</p>
    </div>
    
    <Light Type="LightType.Spotlight" 
           Color="#ff0066" 
           Intensity="0.6" 
           Size="200" 
           X="0.25" 
           Y="0.3" />
           
    <Light Type="LightType.Spotlight" 
           Color="#00ff66" 
           Intensity="0.6" 
           Size="200" 
           X="0.75" 
           Y="0.3" />
           
    <Light Type="LightType.Spotlight" 
           Color="#3366ff" 
           Intensity="0.6" 
           Size="200" 
           X="0.5" 
           Y="0.7" />
</div>
```

### Example 5: Gradient Light with Shadow
```razor
<div class="light-demo-container">
    <div class="content-box">
        <h3>Gradient Light with Shadow</h3>
        <p>This example shows a gradient light effect that also casts shadows.</p>
        
        <div class="shadow-objects">
            <div class="shadow-box"></div>
            <div class="shadow-box"></div>
            <div class="shadow-box"></div>
        </div>
        
        <Light Type="LightType.Point" 
               Gradient="true" 
               GradientColors="new List<string> { "#ff9900", "#ff0066", "#9900ff" }" 
               Intensity="0.8" 
               Size="350" 
               X="0.5" 
               Y="0.3" 
               CastShadow="true" 
               ShadowIntensity="0.7" 
               ShadowColor="#000000" />
    </div>
</div>

<style>
    .shadow-objects {
        display: flex;
        justify-content: space-around;
        width: 80%;
        margin-top: 30px;
    }
    
    .shadow-box {
        width: 60px;
        height: 60px;
        background-color: white;
        border-radius: 4px;
    }
</style>
```

### Example 6: Interactive Card Highlighting
```razor
@code {
    private string activeCardId = null;
    
    private void SetActiveCard(string id)
    {
        activeCardId = id;
    }
    
    private void ClearActiveCard()
    {
        activeCardId = null;
    }
    
    private bool IsCardActive(string id)
    {
        return activeCardId == id;
    }
}

<div class="cards-container">
    <div class="card-row">
        @for (int i = 1; i <= 3; i++)
        {
            var cardId = $"card-{i}";
            <div class="highlight-card" 
                 id="@cardId"
                 @onmouseenter="() => SetActiveCard(cardId)" 
                 @onmouseleave="ClearActiveCard">
                <div class="card-content">
                    <h4>Card @i</h4>
                    <p>Hover over this card to highlight it with a light effect.</p>
                </div>
                
                @if (IsCardActive(cardId))
                {
                    <Light Type="LightType.Spotlight" 
                           Color="#00aaff" 
                           Intensity="0.6" 
                           Size="200" 
                           TargetSelector="#@cardId" />
                }
            </div>
        }
    </div>
</div>

<style>
    .cards-container {
        padding: 30px;
        background-color: #f0f0f0;
        border-radius: 8px;
    }
    
    .card-row {
        display: flex;
        justify-content: space-around;
        flex-wrap: wrap;
        gap: 20px;
    }
    
    .highlight-card {
        position: relative;
        width: 300px;
        height: 200px;
        background-color: #ffffff;
        border-radius: 8px;
        box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        overflow: hidden;
        transition: transform 0.3s ease;
    }
    
    .highlight-card:hover {
        transform: translateY(-5px);
    }
    
    .card-content {
        position: relative;
        z-index: 2;
        padding: 20px;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
    }
</style>
```

### Example 7: Interactive Product Showcase
```razor
@code {
    private class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LightColor { get; set; }
    }
    
    private List<Product> products = new List<Product>
    {
        new Product
        {
            Id = "product-1",
            Name = "Premium Headphones",
            Description = "High-quality wireless headphones with noise cancellation.",
            ImageUrl = "/images/headphones.jpg",
            LightColor = "#ff5500"
        },
        new Product
        {
            Id = "product-2",
            Name = "Smart Watch",
            Description = "Feature-rich smartwatch with health monitoring and notifications.",
            ImageUrl = "/images/smartwatch.jpg",
            LightColor = "#00aaff"
        },
        new Product
        {
            Id = "product-3",
            Name = "Wireless Speaker",
            Description = "Portable Bluetooth speaker with immersive sound quality.",
            ImageUrl = "/images/speaker.jpg",
            LightColor = "#66cc33"
        }
    };
    
    private string activeProductId = null;
    
    private void SetActiveProduct(string id)
    {
        activeProductId = id;
    }
    
    private void ClearActiveProduct()
    {
        activeProductId = null;
    }
    
    private bool IsProductActive(string id)
    {
        return activeProductId == id;
    }
    
    private Product GetActiveProduct()
    {
        return products.FirstOrDefault(p => p.Id == activeProductId);
    }
}

<div class="product-showcase">
    <div class="product-list">
        @foreach (var product in products)
        {
            <div class="product-item @(IsProductActive(product.Id) ? "active" : "")" 
                 id="@product.Id"
                 @onclick="() => SetActiveProduct(product.Id)">
                <h4>@product.Name</h4>
                <div class="product-image" style="background-image: url('@product.ImageUrl');"></div>
            </div>
        }
    </div>
    
    <div class="product-detail">
        @if (activeProductId != null)
        {
            var product = GetActiveProduct();
            <div class="detail-content">
                <h3>@product.Name</h3>
                <div class="product-image-large" style="background-image: url('@product.ImageUrl');"></div>
                <p>@product.Description</p>
                <button class="btn btn-primary">Add to Cart</button>
            </div>
            
            <Light Type="LightType.Spotlight" 
                   Color="@product.LightColor" 
                   Intensity="0.7" 
                   Size="400" 
                   X="0.5" 
                   Y="0.5" 
                   Falloff="0.7" 
                   BlendMode="BlendMode.Overlay" />
        }
        else
        {
            <div class="detail-placeholder">
                <p>Select a product to view details</p>
            </div>
        }
    </div>
</div>

<style>
    .product-showcase {
        display: flex;
        background-color: #f8f9fa;
        border-radius: 8px;
        overflow: hidden;
        margin-bottom: 30px;
    }
    
    .product-list {
        width: 30%;
        background-color: #ffffff;
        padding: 20px;
        border-right: 1px solid #dee2e6;
    }
    
    .product-item {
        padding: 15px;
        margin-bottom: 15px;
        border-radius: 4px;
        cursor: pointer;
        transition: background-color 0.3s ease;
    }
    
    .product-item:hover {
        background-color: #f0f0f0;
    }
    
    .product-item.active {
        background-color: #e9ecef;
    }
    
    .product-image {
        height: 80px;
        background-size: contain;
        background-position: center;
        background-repeat: no-repeat;
        margin-top: 10px;
    }
    
    .product-detail {
        position: relative;
        width: 70%;
        padding: 30px;
        min-height: 400px;
        display: flex;
        align-items: center;
        justify-content: center;
        overflow: hidden;
    }
    
    .detail-content {
        position: relative;
        z-index: 2;
        text-align: center;
        width: 100%;
    }
    
    .product-image-large {
        height: 200px;
        background-size: contain;
        background-position: center;
        background-repeat: no-repeat;
        margin: 20px 0;
    }
    
    .detail-placeholder {
        color: #6c757d;
        font-style: italic;
    }
</style>
```

## CSS Customization

The Light component can be customized using CSS variables and classes:

```css
/* Custom Light styling */
.bb-light {
    --bb-light-z-index: 10;
    --bb-light-blend-mode: normal;
    --bb-light-transition-duration: 300ms;
    
    position: absolute;
    pointer-events: none;
    transition: all var(--bb-light-blend-mode) ease;
    mix-blend-mode: var(--bb-light-blend-mode);
    z-index: var(--bb-light-z-index);
}

/* Light types */
.bb-light-spotlight {
    border-radius: 50%;
    background: radial-gradient(circle, rgba(255,255,255,1) 0%, rgba(255,255,255,0) 70%);
}

.bb-light-point {
    border-radius: 50%;
    background: radial-gradient(circle, rgba(255,255,255,1) 0%, rgba(255,255,255,0) 100%);
}

.bb-light-ambient {
    background: rgba(255,255,255,0.3);
}

.bb-light-directional {
    background: linear-gradient(to bottom, rgba(255,255,255,1) 0%, rgba(255,255,255,0) 100%);
}

/* Animation types */
.bb-light-pulse {
    animation: bbLightPulse var(--bb-light-animation-duration) infinite alternate ease-in-out;
}

.bb-light-flicker {
    animation: bbLightFlicker var(--bb-light-animation-duration) infinite;
}

.bb-light-move {
    animation: bbLightMove var(--bb-light-animation-duration) infinite alternate ease-in-out;
}

.bb-light-color-shift {
    animation: bbLightColorShift var(--bb-light-animation-duration) infinite alternate;
}

/* Animation keyframes */
@keyframes bbLightPulse {
    0% { opacity: 0.5; transform: scale(0.8); }
    100% { opacity: 1; transform: scale(1.2); }
}

@keyframes bbLightFlicker {
    0%, 100% { opacity: 1; }
    50% { opacity: 0.7; }
    60% { opacity: 0.85; }
    70% { opacity: 0.6; }
    80% { opacity: 0.95; }
}

@keyframes bbLightMove {
    0% { transform: translate(-20px, -20px); }
    100% { transform: translate(20px, 20px); }
}

@keyframes bbLightColorShift {
    0% { filter: hue-rotate(0deg); }
    100% { filter: hue-rotate(360deg); }
}

/* Blend modes */
.bb-light-blend-normal { mix-blend-mode: normal; }
.bb-light-blend-multiply { mix-blend-mode: multiply; }
.bb-light-blend-screen { mix-blend-mode: screen; }
.bb-light-blend-overlay { mix-blend-mode: overlay; }
.bb-light-blend-darken { mix-blend-mode: darken; }
.bb-light-blend-lighten { mix-blend-mode: lighten; }
.bb-light-blend-color-dodge { mix-blend-mode: color-dodge; }
.bb-light-blend-color-burn { mix-blend-mode: color-burn; }
.bb-light-blend-hard-light { mix-blend-mode: hard-light; }
.bb-light-blend-soft-light { mix-blend-mode: soft-light; }
.bb-light-blend-difference { mix-blend-mode: difference; }
.bb-light-blend-exclusion { mix-blend-mode: exclusion; }
```

## JavaScript Interop

The Light component uses JavaScript interop for cursor tracking and animation effects. Here's an example of the JavaScript code that might be used internally:

```javascript
// This is a simplified example of the JavaScript interop used by the component
window.bootstrapBlazorLight = {
    instances: {},
    
    // Initialize a new light instance
    initialize: function (id, dotNetReference, options) {
        if (this.instances[id]) {
            this.dispose(id);
        }
        
        const container = document.getElementById(options.containerId || 'body');
        if (!container) return false;
        
        const lightElement = document.createElement('div');
        lightElement.id = id;
        lightElement.className = `bb-light bb-light-${options.type.toLowerCase()}`;
        
        // Apply initial styles
        this.applyStyles(lightElement, options);
        
        // Add to container
        container.appendChild(lightElement);
        
        // Store instance data
        this.instances[id] = {
            element: lightElement,
            container: container,
            dotNetReference: dotNetReference,
            options: options,
            mouseMoveHandler: null
        };
        
        // Set up cursor following if enabled
        if (options.followCursor) {
            this.setupCursorFollowing(id);
        }
        
        return true;
    },
    
    // Apply styles to the light element
    applyStyles: function (element, options) {
        if (!element) return false;
        
        element.style.width = `${options.size}px`;
        element.style.height = `${options.size}px`;
        element.style.opacity = options.intensity;
        element.style.zIndex = options.zIndex;
        
        // Position the light
        if (options.x !== null && options.y !== null) {
            const container = element.parentElement;
            const left = options.x * container.offsetWidth - (options.size / 2);
            const top = options.y * container.offsetHeight - (options.size / 2);
            
            element.style.left = `${left}px`;
            element.style.top = `${top}px`;
        } else {
            element.style.left = '50%';
            element.style.top = '50%';
            element.style.transform = 'translate(-50%, -50%)';
        }
        
        // Apply color
        if (options.gradient && options.gradientColors && options.gradientColors.length > 1) {
            // Create gradient background
            const gradientType = options.type.toLowerCase() === 'directional' ? 'linear-gradient' : 'radial-gradient';
            let gradientString = '';
            
            if (gradientType === 'linear-gradient') {
                gradientString = `${gradientType}(to bottom, `;
            } else {
                gradientString = `${gradientType}(circle, `;
            }
            
            options.gradientColors.forEach((color, index) => {
                const percentage = index / (options.gradientColors.length - 1) * 100;
                gradientString += `${color} ${percentage}%`;
                
                if (index < options.gradientColors.length - 1) {
                    gradientString += ', ';
                }
            });
            
            gradientString += ')';
            element.style.background = gradientString;
        } else {
            // Apply single color based on light type
            switch (options.type.toLowerCase()) {
                case 'spotlight':
                    element.style.background = `radial-gradient(circle, ${options.color} 0%, transparent ${options.falloff * 100}%)`;
                    break;
                case 'point':
                    element.style.background = `radial-gradient(circle, ${options.color} 0%, transparent 100%)`;
                    break;
                case 'ambient':
                    element.style.background = options.color;
                    element.style.opacity = options.intensity * 0.3;
                    break;
                case 'directional':
                    element.style.background = `linear-gradient(to bottom, ${options.color} 0%, transparent 100%)`;
                    break;
            }
        }
        
        // Apply blend mode
        if (options.blendMode) {
            element.style.mixBlendMode = options.blendMode.toLowerCase();
            element.classList.add(`bb-light-blend-${options.blendMode.toLowerCase()}`);
        }
        
        // Apply animation
        if (options.animated && options.animationType) {
            element.classList.add(`bb-light-${options.animationType.toLowerCase()}`);
            element.style.setProperty('--bb-light-animation-duration', `${options.animationDuration}ms`);
        }
        
        return true;
    },
    
    // Set up cursor following behavior
    setupCursorFollowing: function (id) {
        const instance = this.instances[id];
        if (!instance) return false;
        
        const mouseMoveHandler = (e) => {
            const rect = instance.container.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;
            
            instance.element.style.left = `${x - (instance.options.size / 2)}px`;
            instance.element.style.top = `${y - (instance.options.size / 2)}px`;
            
            // Notify .NET of position change
            const normalizedX = x / rect.width;
            const normalizedY = y / rect.height;
            instance.dotNetReference.invokeMethodAsync('OnPositionChanged', normalizedX, normalizedY);
        };
        
        instance.container.addEventListener('mousemove', mouseMoveHandler);
        instance.mouseMoveHandler = mouseMoveHandler;
        
        return true;
    },
    
    // Update light properties
    updateProperties: function (id, properties) {
        const instance = this.instances[id];
        if (!instance) return false;
        
        // Update options with new properties
        Object.assign(instance.options, properties);
        
        // Apply updated styles
        this.applyStyles(instance.element, instance.options);
        
        // Update cursor following if needed
        if (properties.hasOwnProperty('followCursor')) {
            if (properties.followCursor) {
                this.setupCursorFollowing(id);
            } else if (instance.mouseMoveHandler) {
                instance.container.removeEventListener('mousemove', instance.mouseMoveHandler);
                instance.mouseMoveHandler = null;
            }
        }
        
        return true;
    },
    
    // Dispose of a light instance
    dispose: function (id) {
        const instance = this.instances[id];
        if (!instance) return false;
        
        // Remove event listeners
        if (instance.mouseMoveHandler) {
            instance.container.removeEventListener('mousemove', instance.mouseMoveHandler);
        }
        
        // Remove element from DOM
        if (instance.element && instance.element.parentNode) {
            instance.element.parentNode.removeChild(instance.element);
        }
        
        // Remove instance data
        delete this.instances[id];
        
        return true;
    }
};
```

## Accessibility

The Light component follows accessibility best practices:

- Ensures that light effects don't interfere with content readability
- Provides options to disable animations for users who prefer reduced motion
- Maintains proper contrast ratios for text content
- Uses ARIA attributes where appropriate
- Ensures that interactive elements remain accessible when light effects are applied

## Browser Compatibility

The Light component is compatible with all modern browsers that support CSS blend modes and animations. For older browsers, the component can provide fallback styles or disable certain advanced features while maintaining core functionality.

## Integration with Other Components

The Light component works well with many other BootstrapBlazor components:

- Use with `Card` components to create highlighted cards
- Combine with `Button` components for interactive button effects
- Use with `Modal` or `Drawer` components to highlight important dialogs
- Integrate with `Image` components for creative image effects
- Use with `Carousel` or `Tabs` components to highlight active items