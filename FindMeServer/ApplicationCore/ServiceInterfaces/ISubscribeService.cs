using Microsoft.Azure.NotificationHubs;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ISubscribeService
    {
        Task<GcmRegistrationDescription> Subsribe(string[] tags, string regId);
        Task<bool> Unsubscribe(string[] tags, string id);
        Task<NotificationOutcome> Notify(NotificationConfig.Notification notification);
    }
}