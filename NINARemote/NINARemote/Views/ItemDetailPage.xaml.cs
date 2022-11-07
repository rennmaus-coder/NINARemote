using NINARemote.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NINARemote.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}