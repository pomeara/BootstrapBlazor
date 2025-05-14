# Carousel Component

## Overview
The Carousel component in BootstrapBlazor is a slideshow component for cycling through elements—images, cards, text, or custom content—like a carousel. It provides an interactive way to display multiple items in a limited space, allowing users to navigate through the items manually or automatically. Carousels are commonly used for showcasing featured content, product highlights, testimonials, or any collection of related items that benefit from a sequential display.

## Features
- Multiple slide navigation options (indicators, arrows, thumbnails)
- Automatic cycling with configurable interval
- Pause on hover functionality
- Touch swipe support for mobile devices
- Keyboard navigation support
- Multiple transition effects
- Customizable indicators and controls
- Support for various content types (images, text, custom components)
- Responsive design adaptation
- Accessibility support with ARIA attributes
- Play mode options (forward, backward, alternate)
- Event callbacks for slide changes

## Properties

| Property | Type | Default | Description |
| --- | --- | --- | --- |
| `Items` | `IEnumerable<CarouselItem>` | `null` | Collection of items to display in the carousel |
| `AutoPlay` | `bool` | `true` | Whether the carousel should automatically cycle through items |
| `Interval` | `int` | `3000` | Time in milliseconds between automatic cycling of items |
| `PauseOnHover` | `bool` | `true` | Whether to pause automatic cycling when hovering over the carousel |
| `ShowIndicators` | `bool` | `true` | Whether to show the slide indicators at the bottom |
| `ShowControls` | `bool` | `true` | Whether to show the previous/next arrow controls |
| `CrossFade` | `bool` | `false` | Whether to use crossfade transition effect instead of slide |
| `Dark` | `bool` | `false` | Whether to use dark theme styling |
| `PlayMode` | `CarouselPlayMode` | `CarouselPlayMode.Forward` | Direction of carousel playback (Forward, Backward, Alternate) |
| `ChildContent` | `RenderFragment` | `null` | Custom content to display inside the carousel |
| `Height` | `string` | `null` | Sets the height of the carousel |
| `Width` | `string` | `null` | Sets the width of the carousel |
| `ActiveIndex` | `int` | `0` | Index of the currently active slide |

## Events

| Event | Description |
| --- | --- |
| `OnSlideStart` | Triggered when a slide transition starts |
| `OnSlideEnd` | Triggered when a slide transition completes |
| `OnActiveIndexChanged` | Triggered when the active slide index changes |

## Usage Examples

### Example 1: Basic Image Carousel

```razor
<Carousel>
    <CarouselItem>
        <img src="/images/slide1.jpg" class="d-block w-100" alt="Slide 1">
    </CarouselItem>
    <CarouselItem>
        <img src="/images/slide2.jpg" class="d-block w-100" alt="Slide 2">
    </CarouselItem>
    <CarouselItem>
        <img src="/images/slide3.jpg" class="d-block w-100" alt="Slide 3">
    </CarouselItem>
</Carousel>
```

This example shows a basic carousel with three image slides. The carousel will automatically cycle through the images with default settings.

### Example 2: Carousel with Custom Controls and Settings

```razor
<Carousel AutoPlay="true"
          Interval="5000"
          PauseOnHover="true"
          ShowIndicators="true"
          ShowControls="true"
          CrossFade="true"
          OnSlideEnd="HandleSlideEnd">
    <CarouselItem>
        <img src="/images/slide1.jpg" class="d-block w-100" alt="Slide 1">
        <div class="carousel-caption d-none d-md-block">
            <h5>First Slide</h5>
            <p>Some description for the first slide.</p>
        </div>
    </CarouselItem>
    <CarouselItem>
        <img src="/images/slide2.jpg" class="d-block w-100" alt="Slide 2">
        <div class="carousel-caption d-none d-md-block">
            <h5>Second Slide</h5>
            <p>Some description for the second slide.</p>
        </div>
    </CarouselItem>
    <CarouselItem>
        <img src="/images/slide3.jpg" class="d-block w-100" alt="Slide 3">
        <div class="carousel-caption d-none d-md-block">
            <h5>Third Slide</h5>
            <p>Some description for the third slide.</p>
        </div>
    </CarouselItem>
</Carousel>

@code {
    private void HandleSlideEnd(int index)
    {
        Console.WriteLine($"Slide transition ended. Current slide index: {index}");
    }
}
```

This example demonstrates a carousel with custom settings, including a longer interval between slides, crossfade transition effect, and an event handler for the slide end event. It also includes captions for each slide.

### Example 3: Carousel with Custom Content

```razor
<Carousel Height="400px" Width="100%" AutoPlay="false">
    <CarouselItem>
        <div class="d-flex align-items-center justify-content-center h-100 bg-primary text-white">
            <div class="text-center p-5">
                <h2>Welcome to Our Service</h2>
                <p class="lead">We provide the best solutions for your business needs.</p>
                <Button Color="Color.Light">Learn More</Button>
            </div>
        </div>
    </CarouselItem>
    <CarouselItem>
        <div class="d-flex align-items-center justify-content-center h-100 bg-success text-white">
            <div class="text-center p-5">
                <h2>Our Features</h2>
                <p class="lead">Discover the amazing features we offer.</p>
                <Button Color="Color.Light">View Features</Button>
            </div>
        </div>
    </CarouselItem>
    <CarouselItem>
        <div class="d-flex align-items-center justify-content-center h-100 bg-danger text-white">
            <div class="text-center p-5">
                <h2>Get Started Today</h2>
                <p class="lead">Join thousands of satisfied customers.</p>
                <Button Color="Color.Light">Sign Up</Button>
            </div>
        </div>
    </CarouselItem>
</Carousel>
```

This example shows a carousel with custom content instead of just images. Each slide contains a colored background with text and a button, creating a feature showcase or call-to-action carousel.

### Example 4: Product Showcase Carousel

```razor
<Carousel Height="500px" Width="100%" PlayMode="CarouselPlayMode.Alternate">
    @foreach (var product in Products)
    {
        <CarouselItem>
            <div class="card h-100 border-0">
                <div class="row g-0 h-100">
                    <div class="col-md-6">
                        <img src="@product.ImageUrl" class="img-fluid h-100 object-fit-cover" alt="@product.Name">
                    </div>
                    <div class="col-md-6 d-flex align-items-center">
                        <div class="card-body">
                            <h3 class="card-title">@product.Name</h3>
                            <p class="card-text">@product.Description</p>
                            <p class="card-text"><strong>Price: </strong>$@product.Price.ToString("0.00")</p>
                            <Button Color="Color.Primary">Add to Cart</Button>
                        </div>
                    </div>
                </div>
            </div>
        </CarouselItem>
    }
</Carousel>

@code {
    private List<Product> Products = new List<Product>
    {
        new Product
        {
            Name = "Premium Headphones",
            Description = "High-quality noise-cancelling headphones with superior sound quality and comfort.",
            Price = 299.99,
            ImageUrl = "/images/products/headphones.jpg"
        },
        new Product
        {
            Name = "Smart Watch",
            Description = "Feature-rich smartwatch with health monitoring, notifications, and long battery life.",
            Price = 199.99,
            ImageUrl = "/images/products/smartwatch.jpg"
        },
        new Product
        {
            Name = "Wireless Speaker",
            Description = "Portable Bluetooth speaker with immersive 360-degree sound and waterproof design.",
            Price = 129.99,
            ImageUrl = "/images/products/speaker.jpg"
        }
    };

    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
```

This example demonstrates a product showcase carousel that displays products with images, descriptions, and prices. It uses the alternate play mode to cycle back and forth through the products.

### Example 5: Testimonial Carousel

```razor
<Carousel Height="300px" Width="100%" Interval="6000" Dark="true">
    @foreach (var testimonial in Testimonials)
    {
        <CarouselItem>
            <div class="d-flex flex-column align-items-center justify-content-center h-100 bg-light p-4">
                <div class="mb-3">
                    <img src="@testimonial.AvatarUrl" class="rounded-circle" width="80" height="80" alt="@testimonial.Name">
                </div>
                <div class="text-center">
                    <p class="lead mb-3">"@testimonial.Quote"</p>
                    <h5 class="mb-0">@testimonial.Name</h5>
                    <p class="text-muted">@testimonial.Title</p>
                </div>
            </div>
        </CarouselItem>
    }
</Carousel>

@code {
    private List<Testimonial> Testimonials = new List<Testimonial>
    {
        new Testimonial
        {
            Name = "John Smith",
            Title = "CEO, Tech Innovations",
            Quote = "This product has completely transformed how our team collaborates. Highly recommended!",
            AvatarUrl = "/images/avatars/john.jpg"
        },
        new Testimonial
        {
            Name = "Sarah Johnson",
            Title = "Marketing Director, Global Brands",
            Quote = "The customer support is exceptional. They went above and beyond to help us implement the solution.",
            AvatarUrl = "/images/avatars/sarah.jpg"
        },
        new Testimonial
        {
            Name = "Michael Chen",
            Title = "CTO, Future Systems",
            Quote = "We've seen a 40% increase in productivity since adopting this platform. The ROI is incredible.",
            AvatarUrl = "/images/avatars/michael.jpg"
        }
    };

    public class Testimonial
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Quote { get; set; }
        public string AvatarUrl { get; set; }
    }
}
```

This example shows a testimonial carousel that displays customer quotes with their photos and information. It uses a longer interval to give users more time to read each testimonial.

### Example 6: Carousel with Programmatic Control

```razor
<div class="mb-3">
    <Button Color="Color.Primary" OnClick="() => carousel.Previous()">Previous</Button>
    <Button Color="Color.Primary" OnClick="() => carousel.Next()">Next</Button>
    <Button Color="Color.Secondary" OnClick="() => carousel.Pause()">Pause</Button>
    <Button Color="Color.Success" OnClick="() => carousel.Play()">Play</Button>
    <Button Color="Color.Info" OnClick="() => carousel.GoTo(0)">First Slide</Button>
</div>

<Carousel @ref="carousel" AutoPlay="false" OnActiveIndexChanged="HandleIndexChanged">
    <CarouselItem>
        <img src="/images/slide1.jpg" class="d-block w-100" alt="Slide 1">
    </CarouselItem>
    <CarouselItem>
        <img src="/images/slide2.jpg" class="d-block w-100" alt="Slide 2">
    </CarouselItem>
    <CarouselItem>
        <img src="/images/slide3.jpg" class="d-block w-100" alt="Slide 3">
    </CarouselItem>
</Carousel>

<div class="mt-3">
    <p>Current Slide Index: @currentIndex</p>
</div>

@code {
    private Carousel carousel;
    private int currentIndex = 0;

    private void HandleIndexChanged(int index)
    {
        currentIndex = index;
    }
}
```

This example demonstrates programmatic control of the carousel using buttons to navigate, pause, play, and go to specific slides. It also tracks and displays the current slide index.

### Example 7: Responsive Carousel with Different Content for Mobile and Desktop

```razor
<div class="d-none d-md-block">
    <!-- Desktop Carousel -->
    <Carousel Height="500px">
        <CarouselItem>
            <div class="position-relative h-100">
                <img src="/images/desktop/banner1.jpg" class="d-block w-100 h-100 object-fit-cover" alt="Desktop Banner 1">
                <div class="position-absolute top-50 start-50 translate-middle text-center text-white">
                    <h1 class="display-4">Welcome to Our Platform</h1>
                    <p class="lead">Discover the full range of our services on your desktop</p>
                    <Button Color="Color.Light" Size="Size.Large">Get Started</Button>
                </div>
            </div>
        </CarouselItem>
        <!-- More desktop carousel items -->
    </Carousel>
</div>

<div class="d-block d-md-none">
    <!-- Mobile Carousel -->
    <Carousel Height="300px">
        <CarouselItem>
            <div class="position-relative h-100">
                <img src="/images/mobile/banner1.jpg" class="d-block w-100 h-100 object-fit-cover" alt="Mobile Banner 1">
                <div class="position-absolute bottom-0 start-0 p-3 text-white">
                    <h3>Welcome</h3>
                    <p>Optimized for mobile viewing</p>
                    <Button Color="Color.Light" Size="Size.Small">Start</Button>
                </div>
            </div>
        </CarouselItem>
        <!-- More mobile carousel items -->
    </Carousel>
</div>
```

This example shows how to create responsive carousels with different content optimized for desktop and mobile viewports using Bootstrap's responsive utility classes.

## Customization

The Carousel component can be customized using CSS variables:

```css
.carousel {
    /* Slide margin and padding */
    --bb-carousel-slide-margin: 0;
    --bb-carousel-slide-padding: 0 1rem;
    
    /* Slide dimensions */
    --bb-carousel-slide-width: 3rem;
    --bb-carousel-slide-height: 3rem;
    
    /* Slide appearance */
    --bb-carousel-slide-border-radisu: 50%;
    --bb-carousel-slide-border: 1px solid rgba(0, 0, 0, 0.1);
    --bb-carousel-slide-bg: rgba(255, 255, 255, 0.5);
    --bb-carousel-slide-color: #000;
}
```

You can override these variables in your CSS to customize the appearance of the Carousel component according to your design requirements.

Additionally, you can customize the Carousel component by:

1. Using the `AutoPlay` property to control automatic cycling
2. Using the `Interval` property to adjust the cycling speed
3. Using the `PauseOnHover` property to control pause behavior
4. Using the `ShowIndicators` and `ShowControls` properties to control navigation elements
5. Using the `CrossFade` property to change the transition effect
6. Using the `PlayMode` property to control the direction of cycling
7. Using the `Height` and `Width` properties to set dimensions
8. Using custom content inside `CarouselItem` elements for unique slide designs