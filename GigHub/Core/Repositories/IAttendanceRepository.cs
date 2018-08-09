using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetAttendance(string userId, int gigId);
        IEnumerable<Attendance> GetUserAttendance(string userId);
        void Add(Attendance attendance);
        void Remove(Attendance attendance);
    }
}
