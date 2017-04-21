using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeamAdmin.Core.Repositories
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetNotifications(int clubId, bool active = false);        
        Notification SaveNotification(Notification notification);
        Notification GetNotification(long id);
        bool DeleteNotification(long id);
    }
}
