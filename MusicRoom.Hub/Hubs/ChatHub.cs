using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MusicRoom.SignalRClient.Interfaces;

namespace MusicRoom.Hub.Hubs
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public override async Task OnConnectedAsync() 
	    {
            await Groups.AddToGroupAsync(Context.ConnectionId, "ChatRoom");

            await base.OnConnectedAsync();
	    }

        public Task SendMessage(string user, string message) 
	    {
            return Clients.Group("ChatRoom").SendAsync("ReceiveMessage", user, message);
	    }
    }
}
