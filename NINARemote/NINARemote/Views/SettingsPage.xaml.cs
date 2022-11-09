using NINARemote.ViewModels;
using Xamarin.Forms;

namespace NINARemote.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void SaveSettings(object sender, FocusEventArgs e)
        {
            SettingsVM.Instance.SaveSettings();
        }
    }
}