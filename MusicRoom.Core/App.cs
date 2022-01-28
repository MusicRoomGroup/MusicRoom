using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MusicRoom.Core.ViewModels.Main;

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

            RegisterAppStart<MainViewModel>();
        }
    }
}
