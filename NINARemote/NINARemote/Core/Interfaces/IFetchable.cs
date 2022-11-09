using System.Threading.Tasks;

namespace NINARemote.Core.Interfaces
{
    public interface IFetchable<T>
    {
        int Interval { get; set; }
        bool UseInterval { get; set; }

        Task<T> Fetch();
    }
}
