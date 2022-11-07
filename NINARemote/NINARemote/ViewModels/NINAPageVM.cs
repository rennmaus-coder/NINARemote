using System;
using System.Collections.Generic;
using System.Text;

namespace NINARemote.ViewModels
{
    public class NINAPageVM : BaseViewModel
    {
        public static NINAPageVM Instance { get; set; }

        public SettingsVM Settings { get; set; }
        public EquipmentVM EquipmentVM { get; set; }

        private BaseViewModel _current;
        public BaseViewModel Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public NINAPageVM()
        {
            Title = "NINA";

            Instance = this;

            Settings = new SettingsVM();
            EquipmentVM = new EquipmentVM();

            Current = EquipmentVM;
        }
    }
}
