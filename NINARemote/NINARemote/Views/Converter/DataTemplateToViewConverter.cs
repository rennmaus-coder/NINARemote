using NINARemote.Core.Equipment.View;
using NINARemote.ViewModels;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace NINARemote.Views.Converter
{

    /// <summary>
    /// Add your new view/viewmodel here to be able to set it via ContentView
    /// </summary>
    public class DataTemplateToViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (((BaseViewModel)value).Title)
            {
                case "NINA": return new NINAPage();
                case "Settings": return new SettingsPage();
                case "Equipment": return new EquipmentView();
                case "Camera": return new CameraView();
                default:
                    break;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
