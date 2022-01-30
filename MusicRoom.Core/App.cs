using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MusicRoom.Core.ViewModels.Home;
using MvvmCross;
using MusicRoom.API.Interfaces;
using MusicRoom.API.Factories;

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

            Mvx.IoCProvider.RegisterType<IAPIFactory, MusicAPIFactory>();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
