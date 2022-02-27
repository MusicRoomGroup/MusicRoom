using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MusicRoom.SignalRClient.Interfaces;

namespace MusicRoom.SignalRClient.Services
{
    public class ChatService : IChatService
    {
        public event EventHandler<string> OnReceivedMessage;

        public ChatService()
        {
            var builder = new HubConnectionBuilder();

            _connection = builder
                .WithUrl("http://localhost:56744/chathub")
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) 
		        => OnReceivedMessage?.Invoke(this, message));
        }

        private readonly HubConnection _connection;

        public bool IsConnected() => _connection.ConnectionId != null;

        public async Task SendMessage(string user, string message)
            => await _connection.SendAsync("SendMessage", user, message);

        public async Task StartAsync()
            => await _connection.StartAsync();
    }
}
