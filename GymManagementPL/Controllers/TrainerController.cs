using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class TrainerController(ITrainerService _trainerService) : Controller
    {
        //Get All Trainers
        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        //Get Details of Trainer by Id
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Trainer Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = " Trainer Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        //Create Trainer
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(CreateTrainerVM trainerVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Field");
                return View(nameof(Create), trainerVM);
            }
            var result = _trainerService.CreateTrainer(trainerVM);
            if (result)
            {
                TempData["SuccessMessage"] = " Trainer Created Successfully ";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Email or Phone number already exists";
                return View(nameof(Create), trainerVM);
            }
        }

        //Edit Trainer
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Trainer Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerToUpdate(id);
            if (trainer == null)
            {
                TempData["ErrorMessage"] = " Trainer Not Found ";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        [HttpPost]
        public ActionResult Edit([FromRoute] int id, TrainerToUpdateVM trainerVM)
        {
            if (!ModelState.IsValid)
                return View(trainerVM);

            var result = _trainerService.UpdateTrainerDetails(id, trainerVM);
            if (result)
            {
                TempData["SuccessMessage"] = " Trainer Updated Successfully ";
            }
            else
            {
                TempData["ErrorMessage"] = "Email or Phone number already exists";
            }
            return RedirectToAction(nameof(Index));
        }

        //Delete Trainer
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Trainer Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }
            var result = _trainerService.GetTrainerDetails(id);
            if (result == null)
            {
                TempData["ErrorMessage"] = "Trainer Delete Failed. It may be due to existing future sessions.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerName = result.Name;
            ViewBag.TrainerId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var result = _trainerService.DeleteTrainerDetails(id);
            if (result)
            {
                TempData["SuccessMessage"] = " Trainer Deleted Successfully ";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Delete Failed. It may be due to existing future sessions.";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}