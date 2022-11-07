using System;
using System.Collections.Generic;
using System.Text;

namespace NINARemote.Core.Interfaces
{
    public interface IFetchable<T>
    {
        int Interval { get; set; }
        bool UseInterval { get; set; }

        T Fetch();
    }
}
