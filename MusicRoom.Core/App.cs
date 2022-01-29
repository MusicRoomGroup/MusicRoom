using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MusicRoom.Forms.Core.ViewModels.Home;

namespace MusicRoom.Forms.Core
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
