using NINARemote.Core.Interfaces;
using System.Threading.Tasks;

namespace NINARemote.Core.Equipment.Interfaces
{
    public interface IEquipment<T>
    {
        IFetchable<T> DeviceFetcher { get; set; }

        Task UpdateDevice();
    }
}
