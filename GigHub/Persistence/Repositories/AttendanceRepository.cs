using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
{
    public class AttendanceRepository: IAttendanceRepository
    {
        internal ApplicationDbContext _context;


        public AttendanceRepository(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        public IEnumerable<Attendance> GetUserAttendance(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId).ToList();
        }

        public IEnumerable<Attendance> GetAttendance(string userId, int gigId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.GigId == gigId);
        }

        public void Add(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
        }

        public void Remove(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
        }
    }
}