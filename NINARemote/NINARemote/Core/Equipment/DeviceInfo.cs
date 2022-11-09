using System;
using System.Collections.Generic;
using System.Text;

namespace NINARemote.Core.Equipment
{
    public class DeviceInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string DriverInfo { get; set; }
        public string DriverVersion { get; set; }
        public bool Connected { get; set; }
        public DateTime FetchTime { get; set; }
    }
}
