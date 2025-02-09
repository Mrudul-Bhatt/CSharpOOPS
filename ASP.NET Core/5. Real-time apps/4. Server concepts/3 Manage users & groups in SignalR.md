Sure! Let's delve into the key points of the article "Manage users and groups in SignalR" on Microsoft Docs for ASP.NET Core:

### 1. Users in SignalR

#### Overview:
SignalR allows you to send messages to specific users. This feature is useful for private messaging, notifications, and other scenarios where you need to target individual users rather than broadcasting to all connected clients.

#### Key Points:
- **User Identification:** SignalR uses user identifiers to identify and send messages to specific users.
- **User Identifier:** By default, SignalR uses the `ClaimsPrincipal` from the `HttpContext.User` to determine the user identifier. This can be customized if needed.
- **Sending Messages:** Use the `Clients.User` method to send messages to a specific user.

#### Example:

```csharp name=Hubs/ChatHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessageToUser(string userId, string message)
    {
        await Clients.User(userId).SendAsync("ReceiveMessage", message);
    }
}
```

##### Customizing User Identifier:

```csharp name=Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddSignalR(options =>
    {
        options.UserIdProvider = new CustomUserIdProvider();
    });
}

public class CustomUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        // Custom logic to determine the user ID
        return connection.User?.FindFirst(ClaimTypes.Name)?.Value;
    }
}
```

### 2. Groups in SignalR

#### Overview:
Groups in SignalR allow you to manage and send messages to collections of connections. This feature is useful for broadcasting messages to a subset of connected clients, such as chat rooms or topic-based notifications.

#### Key Points:
- **Group Management:** SignalR provides methods to add and remove connections from groups.
- **Sending Messages:** Use the `Clients.Group` method to send messages to all connections in a specific group.
- **Transient Membership:** Group membership is transient and needs to be managed each time a connection is established.

#### Example:

```csharp name=Hubs/ChatHub.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task JoinGroup(string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
    }

    public async Task LeaveGroup(string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
    }

    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }
}
```

##### Managing Groups in Background Services:

```csharp name=Services/GroupManagementService.cs
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class GroupManagementService
{
    private readonly IHubContext<ChatHub> _hubContext;

    public GroupManagementService(IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task AddUserToGroup(string connectionId, string groupName)
    {
        await _hubContext.Groups.AddToGroupAsync(connectionId, groupName);
    }

    public async Task RemoveUserFromGroup(string connectionId, string groupName)
    {
        await _hubContext.Groups.RemoveFromGroupAsync(connectionId, groupName);
    }

    public async Task SendMessageToGroup(string groupName, string message)
    {
        await _hubContext.Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }
}
```

### Summary:

- **Users in SignalR:** Use user identifiers to send messages to specific users. Customize the user identifier if needed and use `Clients.User` to target individual users.
- **Groups in SignalR:** Manage connections in groups to send messages to a subset of clients. Use `Clients.Group` to broadcast messages to all connections in a group and manage group membership using `AddToGroupAsync` and `RemoveFromGroupAsync`.

For more detailed information, you can refer to the official article on Microsoft Docs: [Manage users and groups in SignalR](https://docs.microsoft.com/en-us/aspnet/core/signalr/groups).