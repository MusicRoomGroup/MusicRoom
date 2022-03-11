using System;
using MusicRoom.Core.Services.Implementations;
using MusicRoom.Core.Services.Interfaces;
using MusicRoom.Core.ViewModels;
using MusicRoom.Core.ViewModels.Home;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Services;
using MusicRoom.UI.Pages;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

namespace MusicRoom.UI
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public AppBootstrapper()
        {
            Router = new RoutingState();

            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            Locator.CurrentMutable.RegisterLazySingleton(() => new ChatService(), typeof(IChatService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new YouTubeSearchService(), typeof(IYoutubeSearchService));

            Locator.CurrentMutable.Register(() => new HomePage(), typeof(IViewFor<HomeViewModel>));
            Locator.CurrentMutable.Register(() => new ChatView(), typeof(IViewFor<ChatViewModel>));

             this
                 .Router
                 .NavigateAndReset
                 .Execute(new HomeViewModel())
                 .Subscribe();
        }

        public Page CreateMainPage() => new RoutedViewHost();
    }
}
