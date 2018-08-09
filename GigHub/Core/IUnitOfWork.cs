using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IAttendanceRepository Attendances { get; }
        IFollowingsRepository Followings { get; }
        IGenreRepository Genres { get; }
        IGigRepository Gigs { get; }
        IUserNotificationRepository Notifications { get; }
        void Complete();
    }
}