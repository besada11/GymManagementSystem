using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface ITrainerService
    {
        //Get All Trainers
        IEnumerable<TrainerVM> GetAllTrainers();

        //Create Trainer
        bool CreateTrainer(CreateTrainerVM createTrainerVM);

        //Get Trainer Details By Id
        TrainerVM? GetTrainerDetails(int trainerId);

        //Trainer To Update
        TrainerToUpdateVM? GetTrainerToUpdate(int trainerId);

        //Update Trainer Details
        bool UpdateTrainerDetails(int trainerId, TrainerToUpdateVM trainerUpdated);

        //Delete Trainer
        bool DeleteTrainerDetails(int trainerId);

    }
}
