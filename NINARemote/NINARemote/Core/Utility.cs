using NINARemote.ViewModels;

namespace NINARemote.Core
{
    public static class Utility
    {
        public static string GetAPIHost() => $"http://{MainViewModel.Instance.Settings.IpAddress}:{MainViewModel.Instance.Settings.Port}";
    }

    public class RequestData
    {
        public object Response { get; set; }
        public string Error { get; set; }
        public bool Success { get; set; }
        public string Type { get; set; }
    }
}
