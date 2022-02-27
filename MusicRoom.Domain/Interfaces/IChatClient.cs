using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MusicRoom.ChatClient.Intefaces
{
    public interface IChatClient
    {
	    Task StartAsync();

	    bool IsConnected();

        Task SendMessage(string user, string message);

	    Task ReceiveMessage(string user, string message);

		List<string> RecievedMessages { get; }
    }
}
