using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Net;
using GigHub.Core;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
     
        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs
                        .GetUserGigs(userId);
            return View(gigs);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var gigs = _unitOfWork.Gigs
                                    .GetGigUserAttending(userId);

            var followings = _unitOfWork.Followings
                                    .GetUserFollowings(userId)
                                    .ToLookup(f => f.FolloweeId);

            ViewBag.Title = "Attending";
            var model = new GigsViewModel
            {
                Header ="Gigs I'm Attending",
                Gigs = gigs,
                ShowActions = User.Identity.IsAuthenticated, 
                Followings = followings
            };

            return View("Gigs", model);
        }
        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Create()
        {
            var ViewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.Get(),
                Header = "Add a Gig",
                Id =0
            };
            ViewBag.Title = ViewModel.Header;
            return View("GigForm", ViewModel);
        }

        
        [HttpPost][Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.Get();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };
            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }


        public ActionResult Detail(int id)
        {
            var showActions = User.Identity.IsAuthenticated;
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGig(id);

            var following = _unitOfWork.Followings
                                        .GetFollowing(userId, gig.ArtistId)
                                        .Any();

            var viewModel = new GigDetailViewModel
            {
                Gig = gig,
                ShowActions = showActions, 
                Following = following
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGig(id);
            
            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);


            var ViewModel = new GigFormViewModel()
            {
                Genres = _unitOfWork.Genres.Get(),
                Date = gig.DateTime.ToString("dd MMM yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre = gig.GenreId,
                Venue = gig.Venue,
                Header = "Edit a Gig",
                Id =gig.Id
            };
            ViewBag.Title = ViewModel.Header;
            return View("GigForm", ViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _unitOfWork.Genres.Get();
                return View("GigForm", model);
            }
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGig(model.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            
            gig.Edit(model.Genre, model.GetDateTime(), model.Venue);

            _unitOfWork.Complete();
            return RedirectToAction("Mine", "Gigs");
        }
    }
}