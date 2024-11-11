using MSMS.Models.Dashboard;

namespace MSMS.Models.Notification;

public class NotificationViewModel
{
    public IEnumerable<Notification> Notifications { get; set; } = null!;
}
