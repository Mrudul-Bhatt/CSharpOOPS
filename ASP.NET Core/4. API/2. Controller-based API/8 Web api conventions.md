Sure! Let's delve into the two subpoints of the article "Use web API conventions" on Microsoft Docs for ASP.NET Core:

### 1. Apply Web API Conventions

#### Overview:
ASP.NET Core provides predefined conventions that can be applied to controllers or action methods to enforce consistent API behaviors and responses. These conventions help standardize aspects like status codes, response types, and error handling across different parts of the API.

#### Key Points:
- **Predefined Conventions:** ASP.NET Core includes predefined conventions such as `DefaultApiConventions` which can be applied to controllers or actions.
- **Attributes:** Use attributes like `[ApiConventionType]` and `[ApiConventionMethod]` to apply these conventions.
- **Standardization:** Applying conventions ensures consistency in API responses and documentation.

#### Example:
Applying predefined conventions to a controller:

```csharp name=ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = new Product { Id = id, Name = "Sample Product", Price = 9.99M };
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            // Simulate product creation logic
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}
```

In this example, the `ProductsController` applies the `DefaultApiConventions` convention to standardize responses for actions like `GetProduct` and `CreateProduct`.

### 2. Create Web API Conventions

#### Overview:
You can create custom web API conventions tailored to your application's specific needs. Custom conventions allow you to define standardized behaviors and responses for common patterns in your API.

#### Key Points:
- **Custom Conventions:** Define custom conventions by creating static classes and methods that specify common response behaviors.
- **Attributes:** Use attributes like `[ProducesResponseType]` and `[ProducesDefaultResponseType]` to define response types and status codes.
- **Reusable Components:** Custom conventions can be reused across multiple controllers and actions, ensuring consistency.

#### Example:
Creating a custom web API convention:

```csharp name=CustomApiConventions.cs
using Microsoft.AspNetCore.Mvc;

namespace MyApi.Conventions
{
    public static class CustomApiConventions
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static void Find(int id)
        {
            // Custom convention for find actions
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static void Create(object model)
        {
            // Custom convention for create actions
        }
    }
}
```

Applying the custom convention to a controller:

```csharp name=ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using MyApi.Conventions;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(CustomApiConventions))]
    public class ProductsController : ControllerBase
    {
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Find))]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = new Product { Id = id, Name = "Sample Product", Price = 9.99M };
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Create))]
        public ActionResult<Product> CreateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            // Simulate product creation logic
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }
    }
}
```

In this example, the `CustomApiConventions` class defines conventions for `Find` and `Create` actions. The `ProductsController` applies these custom conventions to standardize responses for `GetProduct` and `CreateProduct`.

### Summary:

- **Apply Web API Conventions:** Use predefined conventions like `DefaultApiConventions` by applying attributes such as `[ApiConventionType]` to controllers or actions to enforce consistent API behaviors and responses.
- **Create Web API Conventions:** Define custom conventions by creating static classes and methods with attributes like `[ProducesResponseType]` to specify common response behaviors. Apply these custom conventions to controllers or actions to ensure consistency across the API.

For more detailed information, you can refer to the official article on Microsoft Docs: [Use web API conventions](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/web-api-conventions).