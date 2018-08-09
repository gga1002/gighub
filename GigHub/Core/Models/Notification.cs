using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class Notification
    {
        public int Id { get;  private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type{ get; private set; }
        public DateTime? OriginalDateTime { get; private set; }
        public string  OriginalVenue { get; private set; }

        [Required]
        public Gig Gig { get; private set; }

        protected Notification()
        {

        }

        public Notification(Gig gig, NotificationType type)
        {
            if (gig == null)
                throw new ArgumentNullException("Gig");
            Gig = gig;
            Type = type;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated);
        }

        public static Notification GigUpdated(Gig gig)
        {
            var notification = new Notification(gig, NotificationType.GigUpdated);
            notification.OriginalDateTime = gig.DateTime;
            notification.OriginalVenue = gig.Venue;

            return notification;

        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }
        
    }
}