using MSMS.Models.Notification;

namespace MSMS.Data.Repos;

public interface INotificationDatabaseRepository : IDatabaseRepository<Notification> 
{
    IEnumerable<Notification> GetNotificationByReference(NotificationReference type);
}
