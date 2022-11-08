using System;
using System.Collections.Generic;
using System.Text;

namespace NINARemote.ViewModels
{
    public class MainViewModel
    {
        public static MainViewModel Instance { get; set; }

        public NINAPageVM NINA { get; set; }
        public SettingsVM Settings { get; set; }
        public EquipmentVM EquipmentVM { get; set; }

        public MainViewModel()
        {
            Instance = this;

            Settings = new SettingsVM();
            NINA = new NINAPageVM();
            EquipmentVM = new EquipmentVM();
        }
    }
}
