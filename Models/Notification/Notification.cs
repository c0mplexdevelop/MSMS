using MSMS.Models.Dashboard;
using MSMS.Models.Login;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSMS.Models.Notification;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateOnly CreatedAt { get; set; }
    public string Message { get; set; } = string.Empty;
    public NotificationReference ReferenceType { get; set; }

    public int AccountId { get; set; }
    [NotMapped]
    public Account User { get; set; } = null!; // Will be used to know which user done the action for notification.
}
