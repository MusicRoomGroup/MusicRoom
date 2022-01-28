using Foundation;
using MvvmCross.Platforms.Ios.Core;
using MusicRoom.Core;

namespace MusicRoom.iOS
{
    [Register(nameof(AppDelegate))]
    public class AppDelegate : MvxApplicationDelegate<Setup, App>
    {
    }
}
