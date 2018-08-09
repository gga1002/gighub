using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Core;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendances
                                        .GetAttendance(userId, dto.GigId);

            if (attendances.Any())
            {
                return BadRequest("The attendance already exists.");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();
            
            return Ok();
        }


        [HttpDelete]
        public IHttpActionResult UnAttend(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _unitOfWork.Attendances
                                        .GetAttendance(userId, id)
                                        .SingleOrDefault();

            if (attendance == null)
            {
                return NotFound();
            }

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}
