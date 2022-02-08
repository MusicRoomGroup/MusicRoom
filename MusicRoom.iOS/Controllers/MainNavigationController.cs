using System;
using UIKit;
using YouTube.Player;

namespace MusicRoom.UI.iOS.Controllers
{
	public partial class MainNavigationController : UINavigationController
	{
		public MainNavigationController(IntPtr handle)
			: base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			UINavigationBar.Appearance.TintColor = UIColor.White;

			InitializeComponents();
			
			PlayerView.Init();
		}

		public override UIStatusBarStyle PreferredStatusBarStyle()
		{
			return UIStatusBarStyle.LightContent;
		}

        private void InitializeComponents()
		{
			PlayerView.BecameReady += PlayerView_BecameReady;
			PlayerView.StateChanged += PlayerView_StateChanged;
			PlayerView.PreferredWebViewBackgroundColor += PlayerView_PreferredWebViewBackgroundColor;
		}

        // To know when the Player View is ready to reproduce videos.
        private void PlayerView_BecameReady(object sender, EventArgs e)
		{
			Console.WriteLine($"Player is ready to reproduce videos");
		}

        // To know when Player View changes the video state.
        private void PlayerView_StateChanged(object sender, PlayerViewStateChangedEventArgs e)
		{
			Console.WriteLine($"Player changed state to {e.State}");

			if (e.State == PlayerState.Queued)
			{
			}

			if (e.State == PlayerState.Playing)
			{
			}
		}

        private UIColor PlayerView_PreferredWebViewBackgroundColor(PlayerView playerView)
		{
			return UIColor.Black;
		}


	}
}
