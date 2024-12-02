using Microsoft.EntityFrameworkCore;
using MSMS.Data.Interfaces;
using MSMS.Models.Notification;

namespace MSMS.Data.Repos;

public class NotificationRepository : INotificationDatabaseRepository
{
    private readonly DatabaseContext context;
    private ILogger<NotificationRepository> logger;

    public NotificationRepository(DatabaseContext context, ILogger<NotificationRepository> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public void Add(Notification model)
    {
        context.Notifications.Add(model);
    }

    public void Delete(Notification model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Notification> GetAll()
    {
        return [.. context.Notifications.Include(notif => notif.User)];
    }

    public Notification? GetById(int id)
    {
        return context.Notifications.Find(id);
    }

    public Notification? GetByIdWithNoTracking(int id)
    {
        return context.Notifications.FirstOrDefault(n => n.Id == id);
    }

    public IEnumerable<Notification> GetNotificationByReference(NotificationReference reference)
    {
        return context.Notifications.Where(n => n.ReferenceType == reference);
    }

    public void SaveChanges()
    {
        context.SaveChanges();
    }

    public void UpdateExistingModel(Notification model)
    {
        var entry = context.Procedure.Find(model.Id)!;
        context.Entry(entry).CurrentValues.SetValues(model);
    }
}
