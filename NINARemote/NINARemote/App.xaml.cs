using NINARemote.ViewModels;
using Xamarin.Forms;

namespace NINARemote
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            new MainViewModel();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
