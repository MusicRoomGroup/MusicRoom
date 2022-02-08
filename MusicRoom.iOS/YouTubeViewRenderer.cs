using System.IO;
using Foundation;
using MusicRoom.iOS;
using MusicRoom.UI.CustomViews;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace MusicRoom.iOS
{
    public class HybridWebViewRenderer : WkWebViewRenderer, IWKScriptMessageHandler
    {
        private const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        private readonly WKUserContentController _userController;
        private static readonly WKWebViewConfiguration Config = new WKWebViewConfiguration
        {
            AllowsInlineMediaPlayback = true,
            MediaPlaybackRequiresUserAction = false,
            AllowsPictureInPictureMediaPlayback = true
        };

        public HybridWebViewRenderer() : this(Config)
        {
        }

        public HybridWebViewRenderer(WKWebViewConfiguration config) : base(config)
        {
            _userController = config.UserContentController;
            var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
            _userController.AddUserScript(script);
            _userController.AddScriptMessageHandler(this, "invokeAction");
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _userController.RemoveAllUserScripts();
                _userController.RemoveScriptMessageHandler("invokeAction");
                var hybridWebView = e.OldElement as HybridWebView;
                hybridWebView.Cleanup();
            }

            if (e.NewElement != null)
            {
                //var filename = Path.Combine(NSBundle.MainBundle.BundlePath, $"Content/{((HybridWebView)Element).Uri}");
                var filename = Path.Combine(NSBundle.MainBundle.BundlePath, ((HybridWebView)Element).Uri);
                LoadRequest(new NSUrlRequest(new NSUrl(filename, false)));
            }
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            ((HybridWebView)Element).InvokeAction(message.Body.ToString());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ((HybridWebView)Element).Cleanup();
            }
            base.Dispose(disposing);
        }        
    }

    public class MyNavigationDelegate : WKNavigationDelegate
    {
        public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
        {
            string fontSize = "200%"; // > 100% shows larger than previous
            string stringsss = string.Format("document.getElementsByTagName('body')[0].style.webkitTextSizeAdjust= '{0}'", fontSize);
            WKJavascriptEvaluationResult handler = (NSObject result, NSError err) =>
            {
                if (err != null)
                {
                    System.Console.WriteLine(err);
                }
                if (result != null)
                {
                    System.Console.WriteLine(result);
                }
            };
            webView.EvaluateJavaScript(stringsss, handler);
            //base.DidFinishNavigation(webView, navigation);
        }

    }
}