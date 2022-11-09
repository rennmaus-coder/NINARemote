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
    public class Camera : BaseViewModel, IEquipment<CameraInfo>
    {
        public IFetchable<CameraInfo> DeviceFetcher { get; set; }

        private CameraInfo _currentInfo = new CameraInfo();
        public CameraInfo CurrentInfo
        {
            get => _currentInfo;
            set => SetProperty(ref _currentInfo, value);
        }

        public Command UpdateDeviceCommand { get; set; }

        public Camera()
        {
            Title = "Camera";

            DeviceFetcher = new CameraFetcher();

            UpdateDeviceCommand = new Command(async () =>
            {
                await UpdateDevice();
            });
        }

        public async Task UpdateDevice()
        {
            try
            {
                CameraInfo info = await DeviceFetcher.Fetch(); // In two lines, so info doesn't become null, if there is an error
                CurrentInfo = info;
            }
            catch (Exception e)
            {
                // TODO: Make alert if error
            }
        }
    }

    public class CameraFetcher : IFetchable<CameraInfo>
    {
        public int Interval { get; set; } = 0;
        public bool UseInterval { get; set; } = false;

        public async Task<CameraInfo> Fetch()
        {
            HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 0, 0, 0, 500) };
            string url = $"{Utility.GetAPIHost()}/api/equipment?property=camera";
            string json = await client.GetStringAsync(url);
            client.Dispose();

            JObject obj = JObject.Parse(json);
            JObject res = JObject.Parse(obj.Value<string>("Response"));

            if (!obj.Value<bool>("Success"))
            {
                // TODO: Make an alert
                return new CameraInfo();
            }

            return new CameraInfo()
            {
                Battery = res.Value<int>("Battery"),
                BitDepth = res.Value<int>("BitDepth"),
                Connected = res.Value<bool>("Connected"),
                Description = res.Value<string>("Description"),
                DriverInfo = res.Value<string>("DriverInfo"),
                DriverVersion = res.Value<string>("DriverVersion"),
                FetchTime = DateTime.Now,
                Gain = res.Value<int>("Gain"),
                IsExposing = res.Value<bool>("IsExposing"),
                IsSubSampleEnabled = res.Value<bool>("IsSubSampleEnabled"),
                Name = res.Value<string>("Name"),
                Offset = res.Value<int>("Offset"),
                PixelSize = res.Value<int>("PixelSize"),
                Temperature = res.Value<double>("Temperature"),
                TemperatureSetPoint = res.Value<double>("TemperatureSetPoint")
            };
        }
    }

    public class CameraInfo : DeviceInfo
    {
        public double Temperature { get; set; }
        public int Gain { get; set; }
        public int BitDepth { get; set; }
        public int Offset { get; set; }
        public bool IsSubSampleEnabled { get; set; }
        public double PixelSize { get; set; }
        public int Battery { get; set; }
        public double TemperatureSetPoint { get; set; }
        public bool IsExposing { get; set; }
    }
}
