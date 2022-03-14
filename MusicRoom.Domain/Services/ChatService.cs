using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Models;

namespace MusicRoom.SignalRClient.Services
{
    public class ChatService : IChatService
    {
        public ChatService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:56744/chathub")
                .WithAutomaticReconnect()
                .Build();
        }

        private readonly HubConnection _connection;

        public bool IsConnected() => _connection.ConnectionId != null;

        public IObservable<ChatMessage> StreamMessages()
            => Observable.Create<ChatMessage>(observer =>
            {
                _connection.On<string, string>("ReceiveMessage",
                    (usr, msg) => observer.OnNext(
                        new ChatMessage
                        {
                            Id = Guid.NewGuid(), User = usr, Message = msg
                        }));
                return Disposable.Empty;
            });

        public async Task SendMessage(ChatMessage message)
            => await _connection.SendAsync("SendMessage", message.GroupId, message.User, message.Message);

        public async Task Start(Guid groupId)
        {
            await _connection.StartAsync();
            await _connection.SendAsync("AddToGroupAsync", groupId);
        }
    }
}
