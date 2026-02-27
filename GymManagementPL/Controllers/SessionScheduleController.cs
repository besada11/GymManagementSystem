using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class SessionScheduleController (IBookingService _bookingService): Controller
    {
        public ActionResult Index()
        {
            var bookings = _bookingService.GetAllSessionWithTrainerAndCategory();
            return View(bookings);
        }
    }
}
