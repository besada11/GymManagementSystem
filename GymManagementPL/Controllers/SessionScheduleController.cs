using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionScheduleController(IBookingService _bookingService) : Controller
    {
        public ActionResult Index()
        {
            var bookings = _bookingService.GetAllSessionWithTrainerAndCategory();
            return View(bookings);
        }

        public ActionResult GetMembersForUpcomingSession(int id)
        {
            var member = _bookingService.GetAllMemberForSession(id);
            ViewData["SessionId"] = id;
            return View(member);
        }

        public ActionResult GetMembersForOngoingSession(int id)
        {
            var member = _bookingService.GetAllMemberForSession(id);
            ViewData["SessionId"] = id;
            return View(member);
        }

        public ActionResult Create(int sessionId)
        {
            var members = _bookingService.GetAllMembersForSelectList(sessionId);
            var memberSelectList = new SelectList(members, "Id", "Name");
            ViewBag.Member = memberSelectList;
            ViewData["SessionId"] = sessionId;

            return View(new CreateBookingVM { SessionId = sessionId });
        }

        [HttpPost]
        public ActionResult Create(CreateBookingVM model)
        {
            var result = _bookingService.CreateBooking(model);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking created successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create booking. Please try again.";
            }
            return RedirectToAction("GetMembersForUpcomingSession", new { id = model.SessionId });
        }

        [HttpPost]
        public IActionResult Attended(MemberAttendOrCancelVM model)
        {
            var result = _bookingService.MemberAttended(model);

            if (result)
                TempData["SuccessMessage"] = "Member attended successfully";
            else
                TempData["ErrorMessage"] = "Member attendance can't be marked";

            return RedirectToAction(nameof(GetMembersForOngoingSession), new { id = model.SessionId });
        }

        [HttpPost]
        public IActionResult Cancel(MemberAttendOrCancelVM model)
        {
            var result = _bookingService.CancelBooking(model);

            if (result)
                TempData["SuccessMessage"] = "Booking cancelled successfully";
            else
                TempData["ErrorMessage"] = "Booking can't be cancelled";
            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = model.SessionId });
        }

    }
}