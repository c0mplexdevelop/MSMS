﻿using MSMS.Models.Notification;

namespace MSMS.Data.Interfaces;

public interface INotificationDatabaseRepository : IDatabaseRepository<Notification>
{
    IEnumerable<Notification> GetNotificationByReference(NotificationReference type);
}
