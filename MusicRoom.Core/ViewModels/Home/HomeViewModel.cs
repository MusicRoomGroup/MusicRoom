using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;

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
            set => SetProperty(ref _trackQuery, value);
        }

        public HomeViewModel(IAPIFactory factory)
        {
            _factory = factory;
        }

        public override async Task Initialize()
	    {
            await base.Initialize();
            _player = await _factory.BuildPlayerAPIAsync();
        }
    }
}
