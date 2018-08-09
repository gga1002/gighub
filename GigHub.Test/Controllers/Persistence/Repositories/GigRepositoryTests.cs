using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using GigHub.Core.Models;
using System.Data.Entity;
using System;
using GigHub.Test.Extensions;
using System.Collections.Generic;
using FluentAssertions;

namespace GigHub.Test.Controllers.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTests
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<IApplicationDbContext> _mockContext;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockContext = new  Mock<IApplicationDbContext>();
            _mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            _repository = new GigRepository(_mockContext.Object);
        }

        [TestMethod]
        public void GetUpComminGigsByArtist_GigIsInThePast_ShouldNotBeReturned()
        {
            //Arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(-1), ArtistId ="1" };
            var source = new List<Gig>() { gig };
            _mockGigs.SetSource(source);

            //Act
            var gigs = _repository.GetUserGigs("1");

            //Asert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComminGigsByArtist_GigIsCanceled_ShouldNotBeReturned()
        {
            //Arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();
            var source = new List<Gig>() { gig };
            _mockGigs.SetSource(source);

            //Act
            var gigs = _repository.GetUserGigs("1");

            //Asert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComminGigsByArtist_GigIsForDifferentArtist_ShouldNotBeReturned()
        {
            //Arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            var source = new List<Gig>() { gig };
            _mockGigs.SetSource(source);

            //Act
            var gigs = _repository.GetUserGigs(gig.ArtistId +"+");

            //Asert
            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetUpComminGigsByArtist_GigIsForGivenArtistAndIsInTheFuture_ShouldBeReturned()
        {
            //Arrange
            var gig = new Gig { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            var source = new List<Gig>() { gig };
            _mockGigs.SetSource(source);

            //Act
            var gigs = _repository.GetUserGigs(gig.ArtistId);

            //Asert
            gigs.Should().Contain(gig);
        }
    }
}
