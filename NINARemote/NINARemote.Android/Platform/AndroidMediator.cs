using Android.App;
using Android.Widget;
using NINARemote.Core.Interfaces;

namespace NINARemote.Droid.Platform
{
    public class AndroidMediator : IPlatformMediator
    {
        public void MakeToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}