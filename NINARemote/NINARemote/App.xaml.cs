using NINARemote.Core.Interfaces;
using NINARemote.ViewModels;
using Xamarin.Forms;

namespace NINARemote
{
    public partial class App : Application
    {
        public static IPlatformMediator PlatformMediator { get; private set; }
        public App(IPlatformMediator platform)
        {
            InitializeComponent();
            PlatformMediator = platform;
            new MainViewModel();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            MainViewModel.Instance.Settings.SaveSettings();
        }

        protected override void OnResume()
        {
        }
    }
}
