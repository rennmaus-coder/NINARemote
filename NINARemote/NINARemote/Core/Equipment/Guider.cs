using Newtonsoft.Json.Linq;
using NINARemote.Core.Equipment.Interfaces;
using NINARemote.Core.Interfaces;
using NINARemote.ViewModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NINARemote.Core.Equipment
{
    public class Guider : BaseViewModel, IEquipment<GuiderInfo>
    {
        public IFetchable<GuiderInfo> DeviceFetcher { get; set; }
        public Command UpdateDeviceCommand { get; set; }

        private GuiderInfo _currentInfo = new GuiderInfo();
        public GuiderInfo CurrentInfo 
        {
            get => _currentInfo;
            set => SetProperty(ref _currentInfo, value);
        }

        public Guider()
        {
            Title = "Guider";

            DeviceFetcher = new GuiderFetcher();

            UpdateDeviceCommand = new Command(async () =>
            {
                await UpdateDevice();
            });
        }

        public async Task UpdateDevice()
        {
            try
            {
                GuiderInfo info = await DeviceFetcher.Fetch(); // In two lines, so info doesn't become null, if there is an error
                CurrentInfo = info;
            }
            catch (Exception e)
            {
                App.PlatformMediator.MakeToast(e.Message);
            }
        }
    }

    public class GuiderFetcher : IFetchable<GuiderInfo>
    {
        public int Interval { get; set; }
        public bool UseInterval { get; set; }

        public async Task<GuiderInfo> Fetch()
        {
            HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 0, 0, 0, 500) };
            string url = $"{Utility.GetAPIHost()}/api/equipment?property=telescope";
            string json = await client.GetStringAsync(url);
            client.Dispose();

            JObject obj = JObject.Parse(json);
            JObject res = JObject.Parse(obj.Value<string>("Response"));
            JObject error = JObject.Parse(res.Value<string>("RMSError"));
            JObject Dec = JObject.Parse(error.Value<string>("Dec"));
            JObject Ra = JObject.Parse(error.Value<string>("RA"));
            JObject Total = JObject.Parse(error.Value<string>("Total"));

            if (!obj.Value<bool>("Success"))
            {
                App.PlatformMediator.MakeToast("Request was not successful");
                return new GuiderInfo();
            }

            return new GuiderInfo()
            {
                Connected = res.Value<bool>("Connected"),
                Description = res.Value<string>("Description"),
                DriverInfo = res.Value<string>("DriverInfo"),
                DriverVersion = res.Value<string>("DriverVersion"),
                Name = res.Value<string>("Name"),
                FetchTime = DateTime.Now,
                DecArcSecondsError = Dec.Value<double>("Arcseconds"),
                DecPixelError = Dec.Value<int>("Pixel"),
                RaArcSecondsError = Ra.Value<double>("Arcseconds"),
                RaPixelError = Ra.Value<int>("Pixel"),
                TotalArcSecondsError = Total.Value<double>("Arcseconds"),
                TotalPixelError = Total.Value<int>("Pixel")
            };
        }
    }

    public class GuiderInfo : DeviceInfo
    {
        public int RaPixelError { get; set; }
        public double RaArcSecondsError { get; set; }

        public int DecPixelError { get; set; }
        public double DecArcSecondsError { get; set; }

        public int TotalPixelError { get; set; }
        public double TotalArcSecondsError { get; set; }
    }
}
