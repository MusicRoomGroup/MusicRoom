using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Models;

namespace MusicRoom.SignalRClient.Services
{
    public class ChatService : IChatService
    {
        public event EventHandler<ChatMessage> OnReceivedMessage;

        public ChatService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:56744/chathub")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) 
		        => OnReceivedMessage?.Invoke(this, new ChatMessage
					{
					    Id = Guid.NewGuid(),
					    User = user,
					    Message = message
					} ));
        }

        private readonly HubConnection _connection;

        public bool IsConnected() => _connection.ConnectionId != null;

        public async Task SendMessage(ChatMessage message)
            => await _connection.SendAsync("SendMessage", message.User, message.Message);

        public async Task StartAsync()
            => await _connection.StartAsync();
    }
}
