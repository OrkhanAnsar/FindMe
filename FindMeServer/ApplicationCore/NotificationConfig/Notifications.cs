using Microsoft.Azure.NotificationHubs;

namespace ApplicationCore.NotificationConfig
{
    public class Notifications
    {
        public static Notifications Instance = new Notifications();

        public NotificationHubClient Hub { get; set; }

        private Notifications()
        {
            Hub = NotificationHubClient.CreateClientFromConnectionString("Endpoint=sb://findmenotificationnamespace.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=ksAz8K+npvyXolDze03luDoKi3zaRnSIP5K9HHaWoBk=",
                                                                            "FindMeNotification");
        }
    }
}