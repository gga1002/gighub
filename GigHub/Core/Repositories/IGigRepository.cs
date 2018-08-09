using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        void Add(Gig gig);

        IEnumerable<Gig> Get();

        IEnumerable<Gig> GetUserGigs(string userId);

        IEnumerable<Gig> GetGigUserAttending(string userId);

        Gig GetGig(int gigId);

        Gig GetGigWithAttendances(int gigId);
    }
}
