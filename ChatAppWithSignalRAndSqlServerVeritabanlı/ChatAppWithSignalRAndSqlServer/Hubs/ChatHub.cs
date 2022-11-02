using ChatAppWithSignalRAndSqlServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatAppWithSignalRAndSqlServer.Hubs
{
    public class ChatHub:Hub
    {
        public async Task SendMessage(Message message)
        {
            await Clients.All.SendAsync("receiveMessages", message);
        }
    }
}
