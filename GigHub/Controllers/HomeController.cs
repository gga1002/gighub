using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using GigHub.Core;
using GigHub.Core.ViewModels;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var userId = User.Identity.GetUserId();

            var gigs = _unitOfWork.Gigs.Get();

            var attendances = _unitOfWork.Attendances
                                .GetUserAttendance(userId)
                                .ToLookup(a => a.GigId);

            var followings = _unitOfWork.Followings
                                .GetUserFollowings(userId)
                                .ToLookup(f => f.FolloweeId);
            
            if (!string.IsNullOrEmpty(query))
            {
                gigs = gigs.Where(g =>
                    g.Artist.Name.Contains(query) ||
                    g.Genre.Name.Contains(query) ||
                    g.Venue.Contains(query)
                ).ToList();
            }
            

            ViewBag.Title = "Home Page";
            var model = new GigsViewModel
            {
                Header = "Upcoming Gigs",
                Gigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                SearchTerm = query, 
                Attendances = attendances, 
                Followings = followings
            };

            return View("Gigs", model);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            

            var artists = _unitOfWork.Followings
                                .GetUserFollowings(userId)
                                .Select(f => f.Followee)
                                .ToList();

            return View("Following", artists); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}