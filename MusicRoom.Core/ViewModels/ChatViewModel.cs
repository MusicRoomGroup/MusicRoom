using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MusicRoom.SignalRClient.Interfaces;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace MusicRoom.Core.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private string _chatMessage;
        public string ChatMessage 
	    {
            get => _chatMessage;
            set
            {
                _chatMessage = value;
                RaisePropertyChanged(() => ChatMessage);
            }
	    }

        public override async Task Initialize() 
	    {
            await _chatService.StartAsync();	
	    }

        public MvxObservableCollection<string> Messages { get; set; } = new MvxObservableCollection<string>();

        private readonly IChatService _chatService;

        public ChatViewModel(IChatService chatService)
        {
            _chatService = chatService;
            _chatService.OnReceivedMessage += _chatService_OnReceivedMessage;
        }

        private void _chatService_OnReceivedMessage(object sender, string e)
        {
            Messages.Add(e);
        }

        public bool IsConnected => _chatService.IsConnected();

        public IMvxCommand SendMessageCommand
            => new MvxAsyncCommand(SendMessageAsync);

        private async Task SendMessageAsync()
        {
            if (!IsConnected) return;

            await _chatService.SendMessage("joe", ChatMessage);
        }
    }
}
