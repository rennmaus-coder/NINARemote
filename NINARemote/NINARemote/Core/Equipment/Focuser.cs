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
    public class Focuser : BaseViewModel, IEquipment<FocuserInfo>
    {
        public IFetchable<FocuserInfo> DeviceFetcher { get; set; }

        private FocuserInfo _currentInfo = new FocuserInfo();
        public FocuserInfo CurrentInfo 
        {
            get => _currentInfo;
            set => SetProperty(ref _currentInfo, value);
        }

        public Command UpdateDeviceCommand { get; set; }

        public Focuser() 
        {
            Title = "Focuser";
            DeviceFetcher = new FocuserFetcher();

            UpdateDeviceCommand = new Command(async () =>
            {
                await UpdateDevice();
            });
        }

        public async Task UpdateDevice()
        {
            try
            {
                FocuserInfo info = await DeviceFetcher.Fetch(); // In two lines, so info doesn't become null, if there is an error
                CurrentInfo = info;
            }
            catch (Exception e)
            {
                App.PlatformMediator.MakeToast(e.Message);
            }
        }
    }

    public class FocuserFetcher : IFetchable<FocuserInfo>
    {
        public int Interval { get; set; }
        public bool UseInterval { get; set; }

        public async Task<FocuserInfo> Fetch()
        {
            HttpClient client = new HttpClient() { Timeout = new TimeSpan(0, 0, 0, 0, 500) };
            string url = $"{Utility.GetAPIHost()}/api/equipment?property=focuser";
            string json = await client.GetStringAsync(url);
            client.Dispose();

            RequestData data = JsonConvert.DeserializeObject<RequestData>(json);
            JObject res = JObject.Parse(data.Response.ToString());

            if (!data.Success)
            {
                App.PlatformMediator.MakeToast(data.Error);
                return new FocuserInfo();
            }

            return new FocuserInfo()
            {
                Connected = res.Value<bool>("Connected"),
                Description = res.Value<string>("Description"),
                DriverInfo = res.Value<string>("DriverInfo"),
                DriverVersion = res.Value<string>("DriverVersion"),
                FetchTime = DateTime.Now,
                IsMoving = res.Value<bool>("IsMoving"),
                Name= res.Value<string>("Name"),
                Position= res.Value<int>("Position"),
                StepSize= res.Value<int>("StepSize"),
                Temperature = res.Value<double>("Temperature")
            };
        }
    }

    public class FocuserInfo : DeviceInfo
    {
        public bool IsMoving { get; set; }
        public int StepSize { get; set; }
        public int Position { get; set; }
        public double Temperature { get; set; }
    }
}
