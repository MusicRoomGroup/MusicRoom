using System;
using MvvmCross.Base;
using MvvmCross.ViewModels;
using Xamarin.Forms;

namespace MusicRoom.UI.CustomViews
{
    public class HybridWebView : WebView
    {
        private IMvxInteraction<string> _interaction;
        public IMvxInteraction<string> Interaction
        {
            get => _interaction;
            set
            {
                if (_interaction != null)
                    _interaction.Requested -= OnInteractionRequestedAsync;

                _interaction = value;
                _interaction.Requested += OnInteractionRequestedAsync;
            }
        }

        public void Cleanup()
        {
            Interaction = null;	
	    }

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
            propertyName: "Uri",
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        public string Uri
        {
            get => (string)GetValue(UriProperty);
            set => SetValue(UriProperty, value);
        }

        private async void OnInteractionRequestedAsync(object sender, MvxValueEventArgs<string> eventArgs)
        {
            InvokeAction(eventArgs.Value);
        }

        public async void InvokeAction(string data)
        {
			if (Interaction == null || data == null)
			{
			    return;
			}

            await EvaluateJavaScriptAsync($"player.stopVideo()");
            await EvaluateJavaScriptAsync($"player.loadVideoById(\"{data}\", 0)");
            await EvaluateJavaScriptAsync($"player.playVideo()");
            //await EvaluateJavaScriptAsync(
            //    @$"player.stopVideo();
            //       player.loadVideoById(""{data}"", 0);
            //       player.playVideo();");
        }
    }
}
