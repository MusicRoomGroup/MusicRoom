using System;
using MusicRoom.Core.Models;
using ReactiveUI;
using Xamarin.Forms;

namespace MusicRoom.UI.CustomViews
{
    public class HybridWebView : WebView
    {
        public HybridWebView()
        {
            MessageBus.Current
                .Listen<YouTubeVideoListItem>()
                .WhereNotNull()
                .Subscribe(e => InvokeActionAsync(e.Id));
        }

        public void Cleanup()
        {
            // Interaction = null;
	    }

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            "Uri",
            typeof(string),
            typeof(HybridWebView),
            "YouTubePlayer.html");

        public string Uri
        {
            get => (string)GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }

        public async void InvokeActionAsync(string data)
        {
            await EvaluateJavaScriptAsync($"player.stopVideo()");
            await EvaluateJavaScriptAsync($"player.loadVideoById(\"{data}\", 0)");
            await EvaluateJavaScriptAsync($"player.playVideo()");
        }
    }
}
