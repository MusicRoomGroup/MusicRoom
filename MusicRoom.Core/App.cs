using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MusicRoom.Core.ViewModels.Home;
using MvvmCross;
using MusicRoom.Core.Services.Interfaces;
using MusicRoom.Core.Services.Implementations;
using MusicRoom.SignalRClient.Interfaces;
using MusicRoom.SignalRClient.Services;

namespace MusicRoom.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.IoCProvider.RegisterType<IChatService, ChatService>();
            Mvx.IoCProvider.RegisterType<IYoutubeSearchService, YouTubeSearchService>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
