using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.SignalRClient.Models;

namespace MusicRoom.SignalRClient.Interfaces
{
    public interface IChatService
    {
        Task StartAsync(Guid groupId);

	    bool IsConnected();

        Task SendMessage(ChatMessage message);

        event EventHandler<ChatMessage> OnReceivedMessage;
    }
}
