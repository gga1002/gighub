using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Controllers.Api;
using Moq;
using GigHub.Core.Repositories;
using GigHub.Core.Dtos;
using GigHub.Core;
using GigHub.Test.Extensions;
using FluentAssertions;
using System.Web.Http.Results;
using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Test.Controllers.Api
{
    [TestClass]
    public class AttendanceControllerTest
    {
        private string _userId;
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepository;       
        private Mock<IUnitOfWork> _mockUoW;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IAttendanceRepository>();
            _mockUoW = new Mock<IUnitOfWork>();
            _mockUoW.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);

            _userId = "1";
            _controller = new AttendancesController(_mockUoW.Object);
            _controller.MockCurrentUser(_userId, "user@mail.com");
        }

        [TestMethod]
        public void Attend_UserAlreadyAttendGig_ShouldReturnBadRequest()
        {
            //Arrange
            var dto = new AttendanceDto { GigId = 1 };
            List<Attendance> attendances = new List<Attendance>();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(attendances);
            var attendance = new Attendance {
                AttendeeId = _userId,
                GigId = 1
            };
            attendances.Add(attendance);
            //Act
            var result = _controller.Attend(dto);

            //Assert
            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            //Arrange
            var dto = new AttendanceDto { GigId = 1 };
            _controller.Attend(dto);

            //Act
            var result = _controller.Attend(dto);

            //Assert
            result.Should().BeOfType<OkResult>();
        }


        [TestMethod]
        public void Unattend_AttendancesDoesNotExist_ShouldReturnNotFound()
        {
            //Arrange

            //Act
            var result = _controller.UnAttend(1);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Unattend_ValidRequest_ShouldReturnOk()
        {

            //Arrange
            List<Attendance> attendances = new List<Attendance>();
            _mockRepository.Setup(r => r.GetAttendance(_userId, 1)).Returns(attendances);
            var attendance = new Attendance
            {
                AttendeeId = _userId,
                GigId = 1
            };
            attendances.Add(attendance);

            //Act
            var result = _controller.UnAttend(1);

            //Assert
            result.Should().BeOfType<OkResult>();

        }

    }
}
