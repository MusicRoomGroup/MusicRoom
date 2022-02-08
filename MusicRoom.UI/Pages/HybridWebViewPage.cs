using System;
using System.IO;
using Foundation;
using MusicRoom.UI.CustomViews;
using Xamarin.Forms;

namespace MusicRoom.UI.Pages
{
    public class HybridWebViewPage : ContentView
    {
        public HybridWebViewPage()
        {
            BuildPage();
        }

        public HybridWebView BuildWebView()
        {
			var baseHtml = @"<html><body><h1 style=""font-size=300px"">This is a test</h1></body></html>";
            var headerString = "<header><meta name='viewport' content='width=device-width, initial-scale=2.0, maximum-scale=5.0, minimum-scale=1.0, user-scalable=no'><style>img{max-width:100%}</style></header>";
            var finalHtml = headerString + baseHtml; //baseHtml is current loaded Html
            var youtubeHtml = @"<iframe width=""100%"" height=""100%"" src=""https://www.youtube.com/embed/T_-jjh2sX4Q?controls=0""></iframe>";
            var htmlSource = new HtmlWebViewSource
            {
                Html = youtubeHtml
            };

            var webView = new HybridWebView
            {
                HeightRequest=283,
                WidthRequest=375,
                VerticalOptions=LayoutOptions.FillAndExpand,
                HorizontalOptions=LayoutOptions.FillAndExpand,
                Uri = "Hybrid.html"
            };

            return webView;
        }

        public void BuildPage()
        {
            HybridWebView webView = BuildWebView();

            Content = new StackLayout
            {
                Children =
			    {
					webView
			    }
            };
        }
    }
}

