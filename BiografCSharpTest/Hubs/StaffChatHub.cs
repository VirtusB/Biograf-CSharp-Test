using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BiografCSharpTest.Hubs
{
    public class StaffChatHub : Hub
    {
        public Task SendToAll(Message message)
        {
            return Clients.All.SendAsync("sendToAll", message);
        }
    }
}