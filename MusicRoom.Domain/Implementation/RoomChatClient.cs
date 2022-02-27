using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MusicRoom.ChatClient.Intefaces;

namespace MusicRoom.ChatClient.Implementation
{
    public class RoomChatClient : IChatClient
    {
        private readonly HubConnection _connection;

        public RoomChatClient(string hubEndpoint)
        {
            var builder = new HubConnectionBuilder();

            _connection = builder
                .WithUrl(hubEndpoint)
                .WithAutomaticReconnect()
                .Build();
        }

        public List<string> RecievedMessages { get; private set; } = new List<string>();

        public bool IsConnected() => _connection.ConnectionId != null;

        public async Task SendMessage(string user, string message)
            => await _connection.SendAsync("ReceiveMessage", user, message);

        public Task ReceiveMessage(string user, string message)
        {
            RecievedMessages.Add(message);

            return Task.CompletedTask;
	    }

        public async Task StartAsync()
            => await _connection.StartAsync();
    }
}
