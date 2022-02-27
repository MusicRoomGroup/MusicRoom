using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicRoom.SignalRClient.Interfaces
{
    public interface IChatService
    {
	    Task StartAsync();

	    bool IsConnected();

        Task SendMessage(string user, string message);

        event EventHandler<string> OnReceivedMessage;
    }
}
