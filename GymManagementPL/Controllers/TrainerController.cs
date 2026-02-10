using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController(ITrainerService _trainerService) : Controller
    {
        //Get All Trainers
        public ActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        //Get Details of Trainer by Id
        public ActionResult Details( int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = " Id of Trainer Can Not Be 0 Or Negative ";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);
            if(trainer == null)
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
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Field");
                return View(nameof(Create), trainerVM);
            }
            var result = _trainerService.CreateTrainer(trainerVM); 
            if(result)
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
    }

    //Edit Trainer

}