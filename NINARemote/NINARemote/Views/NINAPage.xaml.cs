using NINARemote.ViewModels;
using Xamarin.Forms;

namespace NINARemote.Views
{
    public partial class NINAPage : ContentPage
    {
        public NINAPage()
        {
            InitializeComponent();
        }

        private void EquipmentCheck(object sender, CheckedChangedEventArgs e)
        {
            MainViewModel.Instance.NINA.Current = MainViewModel.Instance.EquipmentVM;
        }
    }
}