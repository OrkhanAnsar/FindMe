using System.Collections.Generic;

namespace ApplicationCore.NotificationConfig
{
    public class DeviceRegistration
    {
        public string Platform { get; set; }
        public string Handle { get; set; }
        public string[] Tags { get; set; }
    }

    public class Notification
    {
        public IEnumerable<string> Tags { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}