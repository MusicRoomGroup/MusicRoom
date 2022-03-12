using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Models;
using ReactiveUI;
using Splat;

namespace MusicRoom.Core.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly IChatService _chatService;

        public ChatViewModel(IChatService chatService = null)
        {
            _chatService = chatService ?? Locator.Current.GetService<IChatService>();

            if (_chatService != null)
                _chatService.OnReceivedMessage += _chatService_OnReceivedMessage;

            this.WhenAnyValue(vm => vm.Message)
                .ToProperty(this, nameof(ChatMessage.Message));

            SendMessageCommand = ReactiveCommand.CreateFromTask(async () => await SendMessageAsync());
        }

        private ChatMessage ChatMessage { get; set; } = new ChatMessage
        {
            GroupId = Guid.NewGuid(),
            User = "Joe"
        };

        [DataMember]
        private string _message;
        public string Message
	    {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
	    }

        public ObservableCollection<ChatMessage> Messages { get; set; }
	        = new ObservableCollection<ChatMessage>();

        // TODO: replace mvx interaction
        //public override async Task Initialize() => await _chatService.StartAsync(ChatMessage.GroupId);

        private void _chatService_OnReceivedMessage(object sender, ChatMessage e)
            => Messages.Add(e);

        public bool IsConnected => _chatService.IsConnected();

        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }

        private async Task SendMessageAsync()
        {
            if (IsConnected)
            {
                await _chatService.SendMessage(ChatMessage);
            }
        }
    }
}
