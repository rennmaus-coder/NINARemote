using Newtonsoft.Json;
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
            string url = $"{Utility.GetAPIHost()}/api/equipment?property=guider";
            string json = await client.GetStringAsync(url);
            client.Dispose();

            RequestData data = JsonConvert.DeserializeObject<RequestData>(json);
            JObject res = JObject.Parse(data.Response.ToString());

            if (!data.Success)
            {
                App.PlatformMediator.MakeToast(data.Error);
                return new GuiderInfo();
            }

            if (!res.Value<bool>("Connected"))
            {
                return new GuiderInfo()
                {
                    Connected = res.Value<bool>("Connected"),
                    Description = res.Value<string>("Description"),
                    DriverInfo = res.Value<string>("DriverInfo"),
                    DriverVersion = res.Value<string>("DriverVersion"),
                    Name = res.Value<string>("Name"),
                    FetchTime = DateTime.Now
                };
            }

            JToken error = res.Value<JToken>("RMSError");
            JToken Dec = error.Value<JToken>("Dec");
            JToken Ra = error.Value<JToken>("RA");
            JToken Total = error.Value<JToken>("Total");

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
