using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicRoom.Core;
using Xamarin.Forms;

namespace MusicRoom.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var bootstrapper = new AppBootstrapper();

            MainPage = bootstrapper.CreateMainPage();
        }
    }
}
