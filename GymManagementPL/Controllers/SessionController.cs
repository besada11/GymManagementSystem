using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class SessionController(ISessionService _sessionService) : Controller
    {
        // Get all sessions
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        //Details of a session
        public IActionResult Details(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionByID(id);
            if(session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction("Index");
            }
            return View(session);
        }
    }
}
