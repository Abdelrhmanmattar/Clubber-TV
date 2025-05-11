using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Services.Service
{
    public class NotificationHub : Hub
    {
        private readonly ILogger<NotificationHub> _logger;

        public NotificationHub(ILogger<NotificationHub> logger)
        {
            _logger = logger;
        }
        public override async Task OnConnectedAsync()
        {
            _logger.LogInformation($"\n[Hub] Connected User: {Context.UserIdentifier ?? "null"}\n");
            await Clients.Caller.SendAsync("ReceiveMessage", "Connected to notification hub.");
            await base.OnConnectedAsync();
        }
        public async Task Sendmessage(string message)
        {
            try
            {
                await Clients.All.SendAsync("ReceiveMessage", message); // Consistent method name
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NotificationHub ---> {DateTime.UtcNow} \n{ex.Message} ");
            }
        }
    }
}
