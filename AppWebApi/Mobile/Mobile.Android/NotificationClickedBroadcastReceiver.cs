using Android.Content;

namespace Mobile.Droid
{
    public class NotificationClickedBroadcastReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {

            // TODO: Here you have to find out what view to show and call the action to show it.
            System.Diagnostics.Debug.WriteLine("You tabbed on the download notification");
        }
    }
}