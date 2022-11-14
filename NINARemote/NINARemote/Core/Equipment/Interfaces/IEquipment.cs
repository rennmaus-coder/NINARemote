using NINARemote.Core.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NINARemote.Core.Equipment.Interfaces
{
    public interface IEquipment<T>
    {
        IFetchable<T> DeviceFetcher { get; set; }
        T CurrentInfo { get; set; }
        Command UpdateDeviceCommand { get; set; }

        Task UpdateDevice();
    }
}
