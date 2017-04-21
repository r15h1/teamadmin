using System.Collections.Generic;

namespace TeamAdmin.Core.Caching
{
    public interface ICacheService
    {
        IEnumerable<Notification> GetActiveNotifications();
        void ResetNotifications();
    }
}
