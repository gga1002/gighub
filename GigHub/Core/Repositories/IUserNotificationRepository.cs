using System.Collections.Generic;
using GigHub.Core.Dtos;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        IEnumerable<NotificationDto> GetNewNotifications(string userId);
        void MarAsRead(string userId);
    }
}