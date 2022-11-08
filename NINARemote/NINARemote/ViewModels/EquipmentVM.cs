using NINARemote.Core.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NINARemote.ViewModels
{
    public class EquipmentVM : BaseViewModel
    {
        public static EquipmentVM Instance { get; set; }

        private BaseViewModel _currentDevice;
        public BaseViewModel CurrentDevice
        {
            get => _currentDevice;
            set
            {
                SetProperty(ref _currentDevice, value);
            }
        }

        private List<string> _deviceList = new List<string>() { "Camera", "Telescope" };
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
        public Telescope Telescope { get; set; }

        public EquipmentVM()
        {
            Instance = this;
            Title = "Equipment";

            Camera = new Camera();
            Telescope = new Telescope();

            CurrentDevice = Camera;
        }

        public async void UpdateDevice()
        {
            switch (DeviceList[DeviceIndex])
            {
                case "Camera": CurrentDevice = Camera; await Camera.UpdateDevice(); break;
                case "Telescope": CurrentDevice = Telescope; await Telescope.UpdateDevice(); break;
                default:
                    break;
            }
        }
    }
}
