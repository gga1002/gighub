using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository: IGigRepository
    {
        internal IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Gig gig)
        {
            _context.Gigs.Add(gig);
        }


        public IEnumerable<Gig> Get()
        {
            return _context.Gigs
                        .Include(g => g.Artist)
                        .Include(g => g.Genre)
                        .Where(g => g.DateTime > DateTime.Now && g.IsCanceled == false)
                        .ToList();
        }

        public IEnumerable<Gig> GetUserGigs(string userId)
        {
            return _context.Gigs
                    .Where(g =>  g.ArtistId == userId 
                            && g.DateTime > DateTime.Now
                            && g.IsCanceled == false
                            )
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .ToList();
        }

        public IEnumerable<Gig> GetGigUserAttending(string userId)
        {
             return _context.Attendances
                            .Where(a => a.AttendeeId == userId)
                            .Select(a => a.Gig)
                            .Include(g => g.Artist)
                            .Include(g => g.Genre)
                            .ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                        .Include(g => g.Genre)
                        .Include(g => g.Artist)
                        .SingleOrDefault(g => g.Id == gigId);
        }

        public Gig GetGigWithAttendances(int gigId)
        {
            return _context.Gigs
                        .Include(g => g.Genre)
                        .Include(g => g.Artist)
                        .Include(g => g.Attendances)
                        .SingleOrDefault(g => g.Id == gigId);
        }

    }
}