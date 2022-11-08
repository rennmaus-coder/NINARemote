using NINARemote.ViewModels;

namespace NINARemote.Core
{
    public static class Utility
    {
        public static string GetAPIHost() => $"http://{MainViewModel.Instance.Settings.IpAdress}:{MainViewModel.Instance.Settings.Port}";
    }
}
