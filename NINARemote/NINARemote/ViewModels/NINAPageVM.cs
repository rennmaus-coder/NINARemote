using System;
using System.Collections.Generic;
using System.Text;

namespace NINARemote.ViewModels
{
    public class NINAPageVM : BaseViewModel
    {
        private BaseViewModel _current;
        public BaseViewModel Current
        {
            get => _current;
            set => SetProperty(ref _current, value);
        }

        public NINAPageVM()
        {
            Title = "NINA";

            Current = MainViewModel.Instance.EquipmentVM;
        }
    }
}
