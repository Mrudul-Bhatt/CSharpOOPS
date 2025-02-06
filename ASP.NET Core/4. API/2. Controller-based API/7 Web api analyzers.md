Sure! Let's delve into the three subpoints of the article "Use web API analyzers" on Microsoft Docs for ASP.NET Core:

### 1. Reference the Analyzer Package

#### Overview:
To use web API analyzers in an ASP.NET Core project, you need to reference the `Microsoft.AspNetCore.Mvc.Analyzers` package. This package contains a set of analyzers that help identify and fix common issues in web API projects.

#### Key Points:
- **NuGet Package:** The package required is `Microsoft.AspNetCore.Mvc.Analyzers`.
- **Installation Methods:** You can install the package using the NuGet Package Manager in Visual Studio, or via the .NET CLI.
- **Automatic Analysis:** Once referenced, the analyzers automatically run during build and provide feedback in the form of warnings and errors.

#### Example:
Using the .NET CLI to install the package:
```bash
dotnet add package Microsoft.AspNetCore.Mvc.Analyzers
```
In Visual Studio:
1. Right-click on the project in Solution Explorer.
2. Select "Manage NuGet Packages".
3. Search for `Microsoft.AspNetCore.Mvc.Analyzers`.
4. Click "Install".

### 2. Analyzers for Web API Conventions

#### Overview:
Web API analyzers include a set of rules that enforce web API conventions. These conventions help ensure consistency and best practices in web API development.

#### Key Points:
- **Conventions:** Analyzers enforce conventions such as proper use of attributes, correct return types, and appropriate action method signatures.
- **Common Issues:** Analyzers help identify common issues like missing `[ProducesResponseType]` attributes, incorrect route templates, and unhandled exceptions.
- **Feedback:** Analyzers provide feedback in the form of compiler warnings and errors, guiding developers to fix issues early in the development process.

#### Example:
Common issues detected by web API analyzers:
- Missing `[ProducesResponseType]` attribute:
  ```csharp
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
      [HttpGet("{id}")]
      public IActionResult GetProduct(int id)
      {
          var product = new Product { Id = id, Name = "Sample Product", Price = 9.99M };
          return Ok(product);
      }
  }
  ```
  The analyzer would suggest adding `[ProducesResponseType(typeof(Product), 200)]` to indicate that the action returns a `Product` with a `200 OK` status.

- Incorrect route template:
  ```csharp
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
      [HttpGet("product/{id}")]
      public IActionResult GetProduct(int id)
      {
          var product = new Product { Id = id, Name = "Sample Product", Price = 9.99M };
          return Ok(product);
      }
  }
  ```
  The analyzer would check for consistency in route templates and suggest corrections if needed.

### 3. Analyzers Require Microsoft.NET.Sdk.Web

#### Overview:
Web API analyzers require the project to use the `Microsoft.NET.Sdk.Web` SDK. This SDK provides the necessary infrastructure for building web applications and includes support for web API analyzers.

#### Key Points:
- **SDK Requirement:** Ensure the project file (`.csproj`) targets the `Microsoft.NET.Sdk.Web` SDK.
- **Project File Configuration:** The project file should include the appropriate SDK reference.

#### Example:
Configuring the project file to use `Microsoft.NET.Sdk.Web`:

```xml name=ProductsApi.csproj
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Analyzers" Version="6.0.0" />
  </ItemGroup>

</Project>
```

In this example, the project file is configured to use the `Microsoft.NET.Sdk.Web` SDK, and the `Microsoft.AspNetCore.Mvc.Analyzers` package is referenced.

### Summary:

- **Reference the Analyzer Package:** Install the `Microsoft.AspNetCore.Mvc.Analyzers` package to enable web API analyzers in the project.
- **Analyzers for Web API Conventions:** Analyzers enforce web API conventions and identify common issues, providing feedback to help developers fix problems early.
- **Analyzers Require Microsoft.NET.Sdk.Web:** Ensure the project targets the `Microsoft.NET.Sdk.Web` SDK to use web API analyzers.

For more detailed information, you can refer to the official article on Microsoft Docs: [Use web API analyzers](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/analyzers).