using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MusicRoom.Core.ViewModels.Home;
using MvvmCross;
using MusicRoom.Core.Services.Interfaces;
using MusicRoom.Core.Services.Implementations;

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

            Mvx.IoCProvider.RegisterType<IYoutubeSearchService, YoutubeSearchService>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
