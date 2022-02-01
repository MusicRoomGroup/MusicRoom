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
                Task.Run(async () => await SearchAsync());
            }
        }

        private ObservableCollection<Track> _trackList;
        public ObservableCollection<Track> TrackList
        {
            get => _trackList;
            set
            {
                _trackList = value;
                RaisePropertyChanged(() => TrackList);
            }
        }

        private PagedResult<Track> _trackPage;
        public PagedResult<Track> TrackPage
        {
            get => _trackPage;
            set
            {
                _trackPage = value;
                RaisePropertyChanged(() => TrackPage);
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
                Task.Run(async () => await _player.PlaySong(Track.Uri));
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

        public IMvxCommand<Track> PlaySongAsyncCommand
            => new MvxAsyncCommand<Track>(PlayAsync);

        private async Task ConnectAsync()
        {
            _player = await _factory.BuildPlayerAPIAsync();
        }

        private async Task SearchAsync()
        {
            TrackPage = await _player.SearchTracksAsync(TrackQuery.Replace(" ", "+"));

            TrackList = new ObservableCollection<Track>(TrackPage.Results);
	    }

        private async Task PlayAsync(Track track)
        {
            await _player.PlaySong(Track.Uri);
	    }
    }
}
