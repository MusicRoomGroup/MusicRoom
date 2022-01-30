using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;
using MvvmCross.Commands;

namespace MusicRoom.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly IAPIFactory _factory;

        private IPlayerAPI _player;

        private string _trackQuery;
        public string TrackQuery
        {
            get => _trackQuery;
            set
            {
                _trackQuery = value;
                RaisePropertyChanged(() => TrackQuery);
            }
        }

        public HomeViewModel(IAPIFactory factory)
        {
            _factory = factory;
        }

        public override async Task Initialize()
	    {
            await base.Initialize();
        }

        public IMvxCommand ConnectAsyncCommand => new MvxAsyncCommand(ConnectAsync);

        public async Task ConnectAsync()
        {
            await _factory.BuildPlayerAPIAsync();	
	    }

    }
}
