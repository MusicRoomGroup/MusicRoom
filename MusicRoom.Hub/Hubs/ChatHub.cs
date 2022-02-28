using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using MusicRoom.SignalRClient.Interfaces;

namespace MusicRoom.Hub.Hubs
{
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task AddToGroupAsync(Guid groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId.ToString());

            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupId}.");
        }

        public async Task RemoveFromGroupAsync(Guid groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId.ToString());

            await Clients.Group(groupId.ToString()).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupId}.");
        }

        public Task SendMessage(string groupName, string user, string message) 
	    {
            return Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
	    }
    }
}
