using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using TeamAdmin.Core;
using TeamAdmin.Core.Caching;
using TeamAdmin.Core.Repositories;

namespace TeamAdmin.Lib.Caching
{
    public class CacheService:ICacheService
    {
        private IMemoryCache cache;
        private INotificationRepository notificationRepository;
        private int clubId = 1;

        public CacheService(IMemoryCache cache, INotificationRepository notificationRepository)
        {
            this.cache = cache;
            this.notificationRepository = notificationRepository;
        }
        public IEnumerable<Notification> GetActiveNotifications()
        {
            IEnumerable<Notification> activeNotifications = null;
            if (!cache.TryGetValue<IEnumerable<Notification>>(CacheKeys.Active_Notifications, out activeNotifications))
            {
                activeNotifications = notificationRepository.GetNotifications(clubId, true);
                if(activeNotifications != null && activeNotifications.Count() > 0)
                    cache.Set(CacheKeys.Active_Notifications, activeNotifications, new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30)} );
            }
            return activeNotifications;
        }

        public void ResetNotifications()
        {
            cache.Remove(CacheKeys.Active_Notifications);
        }
    }

    internal class CacheKeys
    {
        public const string Active_Notifications = "Active_Notifications";
    }
}
