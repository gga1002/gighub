using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Controllers.Api;
using GigHub.Core;
using Moq;
using GigHub.Test.Extensions;
using GigHub.Core.Repositories;
using FluentAssertions;
using GigHub.Core.Models;
using System.Web.Http.Results;

namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class GigsControllerTests
    {

        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IGigRepository>();
            var mockUoW = new Mock<IUnitOfWork>();
            mockUoW.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);

            _userId = "1";
            _controller = new GigsController(mockUoW.Object);
            _controller.MockCurrentUser(_userId, "user@mail.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();
            _mockRepository.Setup(r => r.GetGigWithAttendances(1)).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_ShouldReturnUnAuthorized()
        {
            var gig = new Gig{ ArtistId = _userId + "-"};
            _mockRepository.Setup(r => r.GetGigWithAttendances(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig { ArtistId = _userId};
            _mockRepository.Setup(r => r.GetGigWithAttendances(1)).Returns(gig);
            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
