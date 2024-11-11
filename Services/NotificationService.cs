using MSMS.Data.Repos;
using MSMS.Models.Dashboard;
using MSMS.Models.Notification;

namespace MSMS.Services;

public class NotificationService
{
    private readonly INotificationDatabaseRepository _notificationDatabaseRepository;

    public NotificationService(INotificationDatabaseRepository notificationDatabaseRepository)
    {
        _notificationDatabaseRepository = notificationDatabaseRepository;
    }

    public void Add(Notification model)
    {
        _notificationDatabaseRepository.Add(model);
        _notificationDatabaseRepository.SaveChanges();
    }

    public void Add(string title, string message, NotificationReference reference, User user)
    {
        var notification = new Notification
        {
            Title = title,
            Message = message,
            ReferenceType = reference,
            User = user,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now)
        };

        Add(notification);
    }

    public void Add(NotificationReference reference, string message, User user)
    {
        var notification = new Notification
        {
            ReferenceType = reference,
            Message = message,
            UserId = user.Id,
        };

        Add(notification);
    }
}
