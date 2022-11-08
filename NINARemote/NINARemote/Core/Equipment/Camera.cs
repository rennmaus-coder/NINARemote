using NINARemote.Core.Equipment.Interfaces;
using NINARemote.Core.Interfaces;
using NINARemote.ViewModels;
using System;
using System.Threading.Tasks;

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

        public Camera()
        {
            Title = "Camera";

            DeviceFetcher = new CameraFetcher();
        }

        public async Task UpdateDevice()
        {
            CurrentInfo = await DeviceFetcher.Fetch();
        }
    }

    public class CameraFetcher : IFetchable<CameraInfo>
    {
        public int Interval { get; set; } = 0;
        public bool UseInterval { get; set; } = false;

        public async Task<CameraInfo> Fetch()
        {
            throw new NotImplementedException();
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
