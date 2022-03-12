using MusicRoom.Core.ViewModels;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;


namespace MusicRoom.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatView : ReactiveContentPage<ChatViewModel>
    {
        public ChatView()
        {
            InitializeComponent();
        }
    }
}
