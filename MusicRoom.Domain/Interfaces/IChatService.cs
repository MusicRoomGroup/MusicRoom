using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.SignalRClient.Models;

namespace MusicRoom.SignalRClient.Interfaces
{
    public interface IChatService
    {
        /// <summary>
        /// Initializes the connection ot the SignalR hub
        /// </summary>
        /// <param name="groupId">The group ID to which the connection belongs</param>
        /// <returns></returns>
        Task Start(Guid groupId);

        /// <summary>
        /// Checks to see if the client is connected to the hub
        /// </summary>
        /// <returns>A boolean indicating connection status</returns>
	    bool IsConnected();

        /// <summary>
        /// Provides an observable handle from which to subscribe streams of messages
        /// </summary>
        /// <returns>An observable message stream</returns>
        IObservable<ChatMessage> StreamMessages();

        /// <summary>
        /// Sends a message to the signalR hub
        /// </summary>
        /// <param name="message">
        /// A ChatMessage object containing the necessary message information like user, message, and group id
        /// </param>
        /// <returns></returns>
        Task SendMessage(ChatMessage message);
    }
}
