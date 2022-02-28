using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Models;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace MusicRoom.Core.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly IChatService _chatService;

        public ChatViewModel(IChatService chatService)
        {
            _chatService = chatService;
            _chatService.OnReceivedMessage += _chatService_OnReceivedMessage;
        }

        private ChatMessage ChatMessage { get; set; } = new ChatMessage()
        {
            User = "Joe"
        };

        private string _message;
        public string Message 
	    {
            get => _message;
            set
            {
                _message = value;
                ChatMessage.Message = value;
                RaisePropertyChanged(() => Message);
            }
	    }

        public override async Task Initialize() 
	    {
            await _chatService.StartAsync();	
	    }

        public MvxObservableCollection<ChatMessage> Messages { get; set; } 
	        = new MvxObservableCollection<ChatMessage>();

        private void _chatService_OnReceivedMessage(object sender, ChatMessage e) => Messages.Add(e);

        public bool IsConnected => _chatService.IsConnected();

        public IMvxCommand SendMessageCommand => new MvxAsyncCommand(SendMessageAsync);

        private async Task SendMessageAsync()
        {
            if (!IsConnected) return;

            await _chatService.SendMessage(ChatMessage);
        }
    }
}
