using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class SessionController(ISessionService _sessionService) : Controller
    {
        // Get all sessions
        public ActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            return View(sessions);
        }

        //Details of a session
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionByID(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction("Index");
            }
            return View(session);
        }

        //Create a session  
        public ActionResult Create()
        {
           LoadDropdownsForCategories();
           LoadDropdownsForTrainers();
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateSessionVM createSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdownsForCategories();
                LoadDropdownsForTrainers();
                return View(createSession);
            }
            var isCreated = _sessionService.CreateSession(createSession);
            if (isCreated)
            {
                TempData["SuccessMessage"] = "Session created successfully.";

                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create session. Please try again.";
                LoadDropdownsForCategories();
                LoadDropdownsForTrainers();
                return View(createSession);
            }
        }

        // Edit a session
        public ActionResult Edit (int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionToUpdate(id);
            if (session == null)
            {
                TempData["ErrorMessage"] = "Session not found.";
                return RedirectToAction("Index");
            }
            LoadDropdownsForTrainers();
            return View(session);
        }
        [HttpPost]
        public ActionResult Edit ([FromRoute]int id , UpdateSessionVM updateSession)
        {
            if (!ModelState.IsValid)
            {
                LoadDropdownsForTrainers();
                return View(updateSession);
            }
            var isUpdated = _sessionService.UpdateSession(updateSession,id);
            if (isUpdated)
            {
                TempData["SuccessMessage"] = "Session updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update session. Please try again.";
            }
            return RedirectToAction("Index");
        }

        // Delete a session
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction("Index");
            }
            var session = _sessionService.GetSessionByID(id);
            if (session==null)
            {
                TempData["ErrorMessage"] = "Failed to delete session. Please try again.";
                return RedirectToAction("Index");
            }
            ViewBag.SessionId = session.Id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var isDeleted = _sessionService.DeleteSession(id);
            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Session deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete session. Please try again.";
            }
            return RedirectToAction("Index");
        }

        #region Helper Methods
        private void LoadDropdownsForTrainers()
        {
            var trainers = _sessionService.GetTrainerForDropdown();
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
         private void LoadDropdownsForCategories()
         {
             var categories = _sessionService.GetCategoryForDropdown();
             ViewBag.Categories = new SelectList(categories, "Id", "Name");
         }

        #endregion
    }
}
