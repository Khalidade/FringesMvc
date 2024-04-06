using Microsoft.AspNetCore.SignalR;

namespace FringesMVC.Hubs
{
    public class ChatHub : Hub
    {
        public async Task sendMessage(string user, string message)
        {
            Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now);
        }
    }
}
