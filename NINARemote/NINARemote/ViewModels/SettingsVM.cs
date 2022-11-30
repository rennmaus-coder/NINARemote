using Xamarin.Forms;

namespace NINARemote.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        public static SettingsVM Instance { get; set; }

        private string _ipAddress = "localhost";
        public string IpAddress
        {
            get => _ipAddress;
            set
            {
                SetProperty(ref _ipAddress, value);
                Application.Current.Properties["IpAddress"] = IpAddress;
                SaveSettings();
            }
        }

        private int _port = 1888;
        public int Port
        {
            get => _port;
            set
            {
                SetProperty(ref _port, value);
                Application.Current.Properties["Port"] = Port;
                SaveSettings();
            }
        }

        public SettingsVM()
        {
            Title = "Settings";

            Instance = this;

            LoadSettings();
        }

        public void SaveSettings()
        {
            Application.Current.SavePropertiesAsync();
        }

        public void LoadSettings()
        {
            if (!Application.Current.Properties.ContainsKey("IpAddress"))
                return;

            IpAddress = Application.Current.Properties["IpAddress"].ToString();
            Port = int.Parse(Application.Current.Properties["Port"].ToString());
        }
    }
}
