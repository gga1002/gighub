using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Repositories;
using GigHub.Core.Models;
using System.Data.Entity;

namespace GigHub.Persistence.Repositories
{
    public class FollowingsRepository: IFollowingsRepository
    {
        internal ApplicationDbContext _context;

        public FollowingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Following> GetUserFollowings(string userId)
        {
            return _context.Followings
                    .Where(f => f.FollowerId == userId)
                    .Include(f => f.Followee)
                    .ToList();
        }
        public IEnumerable<Following> GetFollowing(string follower, string followee)
        {
            return _context.Followings
                                .Where(f => f.FollowerId == follower
                                        && f.FolloweeId == followee)
                                .Include(f => f.Followee)
                                .Include(f => f.Follower);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }
        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}