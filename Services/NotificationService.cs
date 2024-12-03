using MSMS.Data.Interfaces;
using MSMS.Models.Dashboard;
using MSMS.Models.Login;
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

    public void Add(string title, string message, NotificationReference reference, int accountID)
    {
        var notification = new Notification
        {
            Title = title,
            Message = message,
            ReferenceType = reference,
            AccountId = accountID,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now)
        };

        Add(notification);
    }

    public void Add(NotificationReference reference, string message, Account user)
    {
        var notification = new Notification
        {
            ReferenceType = reference,
            Message = message,
            AccountId = user.EmployeeNumber,
        };

        Add(notification);
    }
}
