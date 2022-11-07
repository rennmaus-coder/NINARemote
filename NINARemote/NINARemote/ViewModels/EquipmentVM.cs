using NINARemote.Core.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace NINARemote.ViewModels
{
    public class EquipmentVM : BaseViewModel
    {
        private BaseViewModel _currentDevice;
        public BaseViewModel CurrentDevice
        {
            get => _currentDevice;
            set
            {
                SetProperty(ref _currentDevice, value);
            }
        }

        private List<string> _deviceList = new List<string>() { "Camera" };
        public List<string> DeviceList
        {
            get => _deviceList;
            set
            {
                SetProperty(ref _deviceList, value);
            }
        }

        private int _deviceIndex = 0;
        public int DeviceIndex
        {
            get => _deviceIndex;
            set
            {
                if (SetProperty(ref _deviceIndex, value))
                    UpdateDevice();
            }
        }

        public Camera Camera { get; set; }

        public EquipmentVM()
        {
            Camera = new Camera();

            CurrentDevice = Camera;
        }

        public void UpdateDevice()
        {
            switch (DeviceList[DeviceIndex])
            {
                case "Camera": CurrentDevice = Camera; break;
                default:
                    break;
            }
        }
    }
}
