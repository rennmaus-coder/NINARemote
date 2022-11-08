using NINARemote.Core.Equipment;
using System;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace NINARemote.Views.Converter
{
    public class CurrentInfoToText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            StackLayout layoutDevice = new StackLayout() { Orientation = StackOrientation.Vertical };
            DeviceInfo info = (DeviceInfo)value;
            foreach (PropertyInfo prop in info.GetType().GetProperties())
            {
                layoutDevice.Children.Add(new Label() { Text = $"{prop.Name}: {prop.GetValue(info)}", TextColor = Color.White, FontSize = 14 });
            }
            return layoutDevice;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
