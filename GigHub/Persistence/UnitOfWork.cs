using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork :  IUnitOfWork
    {
        internal ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context )
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Genres = new GenreRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingsRepository(_context);
            Notifications = new UserNotificationRepository(_context);
        }

        public IGigRepository Gigs { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IFollowingsRepository Followings { get; private set; }

        public IAttendanceRepository Attendances { get; private set; }

        public IUserNotificationRepository Notifications{get; private set;}

        public void Complete()
        {
            _context.SaveChanges();
        }
    }


}