using Microsoft.EntityFrameworkCore;
using MSMS.Data.Interfaces;
using MSMS.Models.Login;
using MSMS.Models.Notification;
using MSMS.Services;

namespace MSMS.Data.Repos;

public class NotificationRepository : INotificationDatabaseRepository
{
    private readonly DatabaseContext context;
    private ILogger<NotificationRepository> logger;
    private AccountService accountService;

    public NotificationRepository(  DatabaseContext context, ILogger<NotificationRepository> logger,
                                    AccountService accountService)
    {
        this.context = context;
        this.logger = logger;
        this.accountService = accountService;
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
        var notifications = context.Notifications.ToList();
        for(int i = 0; i < notifications.Count; i++)
        {
            var notification = notifications[i];
            notification.User = new Account
            {
                Name = "Test",
            };
        }

        return notifications;
        //return [.. context.Notifications.Include(notif => notif.User)];
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

    public async Task<Account> GetAccountById(int accountId)
    {
        var accounts = await accountService.GetAllAccounts();
        return accounts.First(a => a.EmployeeNumber == accountId);
    }
}
