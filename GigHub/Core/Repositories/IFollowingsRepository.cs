using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IFollowingsRepository
    {

        IEnumerable<Following> GetUserFollowings(string userId);
        IEnumerable<Following> GetFollowing(string follower, string followee);

        void Add(Following following);
        void Remove(Following following);
    }
}
