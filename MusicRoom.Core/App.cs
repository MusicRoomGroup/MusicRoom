using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MusicRoom.Core.ViewModels.Home;

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

            RegisterAppStart<HomeViewModel>();
        }
    }
}
