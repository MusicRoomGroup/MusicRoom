using System;
using System.Threading.Tasks;
using MusicRoom.ChatClient.Intefaces;
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

        public MvxObservableCollection<string> Messages
			=> new MvxObservableCollection<string>(_chatClient.RecievedMessages);

        private readonly IChatClient _chatClient;

        public ChatViewModel(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }

        public bool IsConnected => _chatClient.IsConnected();

        public IMvxCommand SendMessageCommand
            => new MvxAsyncCommand(SendMessageAsync);

        private async Task SendMessageAsync()
        {
            if (!IsConnected) return;

            await _chatClient.SendMessage("joe", ChatMessage);
        }
    }
}
