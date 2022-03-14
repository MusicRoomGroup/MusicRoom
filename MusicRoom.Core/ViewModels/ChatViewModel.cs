using System;
using System.Collections.ObjectModel;
using System.Reactive;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace MusicRoom.Core.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        public ChatViewModel(IScreen screen = null, IChatService chatService = null) : base(screen)
        {
            ChatMessage = new ChatMessage { Id = Guid.NewGuid(), GroupId = Guid.NewGuid(), User = "Joe" };

            chatService ??= Locator.Current.GetService<IChatService>();

            chatService!.Start(ChatMessage.GroupId);

            chatService.StreamMessages().Subscribe(m => Messages.Add(m));

            SendMessageCommand = ReactiveCommand.CreateFromTask(
                async () => await chatService.SendMessage(ChatMessage),
                this.WhenAnyValue(
                    _ => chatService.IsConnected(),
                    vm => vm.Message,
                    (connected, msg) => connected && !string.IsNullOrEmpty(msg)));

            this.WhenAnyValue(vm => vm.Message)
                .ToProperty(this, nameof(ChatMessage.Message));
        }

        [Reactive] private ChatMessage ChatMessage { get; set; }

        [Reactive] public string Message { get; set; }

        public ObservableCollection<ChatMessage> Messages { get; } = new ObservableCollection<ChatMessage>();

        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }
    }
}
