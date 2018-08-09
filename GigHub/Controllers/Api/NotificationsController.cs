using GigHub.Core;
using GigHub.Core.Dtos;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public NotificationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _unitOfWork
                                        .Notifications
                                        .GetNewNotifications(userId)
                                        .ToList();

            return notifications;
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            _unitOfWork.Notifications.MarAsRead(userId);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
