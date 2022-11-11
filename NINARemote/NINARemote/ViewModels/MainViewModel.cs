using NINARemote.Core;
using System;
using System.IO;

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

            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName));

            Settings = new SettingsVM();
            NINA = new NINAPageVM();
            EquipmentVM = new EquipmentVM();
        }
    }
}
