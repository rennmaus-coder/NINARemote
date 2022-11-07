using NINARemote.Core.Interfaces;

namespace NINARemote.Core.Equipment.Interfaces
{
    public interface IEquipment<T>
    {
        IFetchable<T> DeviceFetcher { get; set; }
    }
}
