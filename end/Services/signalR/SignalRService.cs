using code_quests.Core.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Services.Service
{
    public class SignalRService : ISignalRService
    {
        private readonly IHubContext<NotificationHub> hubContext;

        public SignalRService(IHubContext<NotificationHub> _hubContext, ILogger<SignalRService> logger)
        {
            hubContext = _hubContext;
        }
        public async Task SendMessageAll(string message)
        {
            try
            {
                await hubContext.Clients.All.SendAsync("ReceiveMessage", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SignalRService ---> {DateTime.UtcNow} \n{ex.Message} ");
            }
        }


        public async Task SendMessageUser(string UserID, string message)
        {
            try
            {
                await hubContext.Clients.User(UserID).SendAsync("ReceiveMessage", message); // Consistent method name

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }
}
