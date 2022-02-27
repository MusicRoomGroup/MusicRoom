using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MusicRoom.ChatClient.Intefaces;

namespace MusicRoom.Hub.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync() 
	    {
            await Groups.AddToGroupAsync(Context.ConnectionId, "ChatRoom");

            await base.OnConnectedAsync();
	    }

        public Task SendMessage(string user, string message) 
	    {
            return Clients.All.ReceiveMessage(user, message);
	    }
    }
}
