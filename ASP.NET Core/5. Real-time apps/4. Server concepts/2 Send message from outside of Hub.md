### Step 1: Read the Article

The article "Send messages from outside a hub" on Microsoft Docs for ASP.NET Core explains how to send messages to clients connected to a SignalR hub from outside the hub context. This is useful for scenarios where you need to push updates from background services, scheduled tasks, or other parts of your application that are not directly within the hub.

### Step 2: Summarize Key Points

#### Key Points:

1. **Access `IHubContext`:**
   - The `IHubContext` interface is used to interact with SignalR hubs from outside the hub class. It provides access to the `Clients` property, which allows sending messages to connected clients.

2. **Dependency Injection:**
   - `IHubContext` can be injected into services, controllers, or other parts of the application using dependency injection.
   - Ensure that the `IHubContext` is registered in the service container.

3. **Sending Messages:**
   - Use the `Clients` property of `IHubContext` to send messages to all clients, specific clients, groups, or users.

4. **Example Scenarios:**
   - Background services, such as hosted services or worker services, can use `IHubContext` to send real-time updates.
   - Controllers can use `IHubContext` to send notifications based on HTTP requests.

#### Example Code:

##### Injecting `IHubContext` in a Controller:
```csharp name=Controllers/NotificationController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IHubContext<ChatHub> _hubContext;

    public NotificationController(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> SendNotification(string user, string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        return Ok();
    }
}
```

##### Injecting `IHubContext` in a Background Service:
```csharp name=Services/NotificationService.cs
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class NotificationService : BackgroundService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public NotificationService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "BackgroundService", "Hello from the background service!");
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
```

##### Registering the Service:
```csharp name=Program.cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddHostedService<NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chathub");

app.Run();
```

### Summary:

- **Access `IHubContext`:** Use `IHubContext` to send messages to SignalR clients from outside the hub class.
- **Dependency Injection:** Inject `IHubContext` into services, controllers, or other parts of the application using DI.
- **Sending Messages:** Use the `Clients` property of `IHubContext` to send messages to connected clients.
- **Example Scenarios:** Background services and controllers can use `IHubContext` to send real-time updates and notifications.

For more detailed information, you can refer to the official article on Microsoft Docs: [Send messages from outside a hub](https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext).