using Android.App;
using Android.Content;
using Android.OS;
using Firebase.Messaging;
using System;
using Xamarin.Forms;

namespace FindMeMobileClient.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            try
            {
                string[] body = { message.GetNotification().Body, message.GetNotification().Title };
                MessagingCenter.Send<object, string[]>(this, App.NotificationReceivedKey, body);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error extracting message: " + ex);
            }
        }
    }
}