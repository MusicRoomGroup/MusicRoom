using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MusicRoom.API.Interfaces;
using MusicRoom.API.Models;
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

        private ObservableCollection<Track> _tracks;
        public ObservableCollection<Track> Tracks
        {
            get => _tracks;
            set
            {
                _tracks = value;
                RaisePropertyChanged(() => Tracks);
            }
        }

        private Track _track;
        public Track Track
        {
            get => _track;
            set
            {
                _track = value;
                RaisePropertyChanged(() => Track);
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

        public IMvxCommand ConnectAsyncCommand 
	        => new MvxAsyncCommand(ConnectAsync);

        public IMvxCommand SearchAsyncCommand
            => new MvxAsyncCommand(SearchAsync);

        private async Task ConnectAsync()
        {
            _player = await _factory.BuildPlayerAPIAsync();
        }

        private async Task SearchAsync()
        {
            Tracks = new ObservableCollection<Track>(await _player.SearchTracksAsync(TrackQuery.Replace(" ", "+")));
	    }
    }
}
