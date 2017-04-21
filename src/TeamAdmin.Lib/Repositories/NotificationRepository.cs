using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Repositories;
using TeamAdmin.Lib.Repositories.EFContext;

namespace TeamAdmin.Lib.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public bool DeleteNotification(long id)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var notification = context.Notifications.FirstOrDefault(n => n.NotificationId == id);
                if (notification == null) return false;

                context.Notifications.Remove(notification);
                context.SaveChanges();
                return true;
            }
        }

        public Notification GetNotification(long id)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                return context.Notifications.FirstOrDefault(n => n.NotificationId == id);
            }
        }

        public IEnumerable<Notification> GetNotifications(int clubId, bool active = false)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                if (active)
                    return context.Notifications.Where(n => n.ClubId == clubId && DateTime.Today >= n.StartDate && DateTime.Today < n.ExpiryDate).ToList();

                return context.Notifications.Where(n => n.ClubId == clubId).OrderByDescending(n => n.StartDate).OrderByDescending(n => n.ExpiryDate).ToList();
            }
        }

        public Notification SaveNotification(Notification notification)
        {
            if (notification.NotificationId.HasValue)
                return UpdateNotification(notification);

            return CreateNotification(notification);
        }        

        private Notification CreateNotification(Notification notification)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                notification.CreatedDate = DateTime.UtcNow;
                context.Notifications.Add(notification);
                context.SaveChanges();
                return notification;
            }
        }

        private Notification UpdateNotification(Notification notification)
        {
            using (var context = ContextFactory.Create<ClubContext>())
            {
                var item = context.Notifications.FirstOrDefault(n => n.NotificationId == notification.NotificationId);

                if (item == null) return null;

                item.Message = notification.Message;
                item.ExpiryDate = notification.ExpiryDate;
                item.StartDate = notification.StartDate;
                context.SaveChanges();
                return item;
            }
        }
    }
}
