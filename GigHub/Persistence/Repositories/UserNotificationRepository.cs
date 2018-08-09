using AutoMapper;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {

        private readonly ApplicationDbContext _context;
        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<NotificationDto> GetNewNotifications(string userId)
        {
            var result = _context.UserNotifications
                            .Where(un => un.UserId == userId && !un.IsRead)
                            .Select(un => un.Notification)
                            .Include(n => n.Gig.Artist);
            return result.Select(Mapper.Map<Notification, NotificationDto>);
        }

        public void MarAsRead(string userId)
        {
            var notifications = _context.UserNotifications
                                        .Where(un => un.UserId == userId && !un.IsRead)
                                        .ToList();

            notifications.ForEach(n => n.Read());
        }
    }
}