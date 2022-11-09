﻿using Newtonsoft.Json;
using NINARemote.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NINARemote.ViewModels
{
    public class SettingsVM : BaseViewModel
    {
        public static SettingsVM Instance { get; set; }

        private string _ipAddress = "localhost";
        public string IpAdress
        {
            get => _ipAddress;
            set => SetProperty(ref _ipAddress, value);
        }

        private int _port = 1888;
        public int Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public SettingsVM()
        {
            Title = "Settings";

            // TODO: Load Settings
        }

        public void SaveSettings()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName, "Settings.json");
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }

        public void LoadSettings()
        {
            SettingsVM vm = JsonConvert.DeserializeObject<SettingsVM>(File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.ApplicationName, "Settings.json")));
            IpAdress = vm.IpAdress;
            Port = vm.Port;
        }
    }
}
