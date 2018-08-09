using GigHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Core.ViewModels
{
    public class GigsViewModel
    {
        public string Header { get; set; }
        public IEnumerable<Gig> Gigs { get; set; }
        public bool ShowActions { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; internal set; }
        public ILookup<string, Following> Followings { get; internal set; }
    }
}