using NINARemote.Core.Equipment;
using System.Collections.Generic;

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

        private List<string> _deviceList = new List<string>() 
        { 
            "Camera", 
            "Telescope", 
            "Focuser", 
            "Guider"
        };
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
        public Focuser Focuser { get; set; }
        public Guider Guider { get; set; }

        public EquipmentVM()
        {
            Instance = this;
            Title = "Equipment";

            Camera = new Camera();
            Telescope = new Telescope();
            Focuser = new Focuser();
            Guider = new Guider();

            CurrentDevice = Camera;
        }

        public void UpdateDevice()
        {
            switch (DeviceList[DeviceIndex])
            {
                case "Camera": CurrentDevice = Camera; break;
                case "Telescope": CurrentDevice = Telescope; break;
                case "Focuser": CurrentDevice = Focuser; break;
                case "Guider": CurrentDevice = Guider; break;
                default:
                    break;
            }
        }
    }
}
// How to add a new device
// -----------------------
// 1. Create a new class and implement IEquipment with a class that inherits from DeviceInfo as Type argument, inherit from BaseViewModel and specify a Title
// 2. Create an instance in the EquipmentVM constructor
// 3. Add the name of the device to the switch statement in UpdateDevice and DeviceList (has to be the same!)
// 4. Create a new view for the device
// 5. In the DataTemplateToViewConverter, add the Title to the switch statement and return a new instance of the view
//
// // If such error occurs, you need to change the heap size of the emulator (vm.heapsize): Fatal signal 11 (SIGSEGV), code 2 (SEGV_ACCERR)