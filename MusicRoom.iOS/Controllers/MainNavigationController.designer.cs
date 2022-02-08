// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MusicRoom.UI.iOS.Controllers
{
	[Register ("MainNavigationController")]
	partial class MainNavigationController
	{
		[Outlet]
		YouTube.Player.PlayerView PlayerView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PlayerView != null)
			{
				PlayerView.Dispose();
				PlayerView = null;
			}
		}
	}
}
