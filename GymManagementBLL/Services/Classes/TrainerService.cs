using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    public class TrainerService (IUnitOfWork _unitOfWork , IMapper _mapper) : ITrainerService
    {
        //Create Trainer
        public bool CreateTrainer(CreateTrainerVM createTrainerVM)
        {
            try
            {
                if (IsEmailExists(createTrainerVM.Email) || IsPhoneExists(createTrainerVM.Phone))
                    return false;
                if (createTrainerVM.Specialties == 0)
                    return false;
                var traner = _mapper.Map<Trainer>(createTrainerVM);
                _unitOfWork.GetRepository<Trainer>().Add(traner);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Delete Trainer
        public bool DeleteTrainerDetails(int trainerId)
        {
            try
            {
                var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
                if (trainer == null)
                    return false;

                // Check if trainer has any future sessions
                var hasFutureSessions = trainer.TrainerSessions?.Any(s => s.StartDate > DateTime.Now) ?? false;
                if (hasFutureSessions)
                    return false;

                _unitOfWork.GetRepository<Trainer>().Delete(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Get All Trainers
        public IEnumerable<TrainerVM> GetAllTrainers()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers == null || !trainers.Any()) return [];
            var trainerVM = _mapper.Map<IEnumerable<TrainerVM>>(trainers);
            return trainerVM;
        }

        //Get Trainer Details
        public TrainerVM? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
            if (trainer == null) return null;
            return _mapper.Map<TrainerVM>(trainer);
        }

        //Trainer To Update
        public TrainerToUpdateVM? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
            if (trainer == null) return null;
            return _mapper.Map<TrainerToUpdateVM>(trainer);
        }

        //Update Trainer Details
        public bool UpdateTrainerDetails(int trainerId, TrainerToUpdateVM trainerUpdated)
        {
            try
            {
                var emailExist = _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Email == trainerUpdated.Email && t.Id != trainerId);
                var phoneExist = _unitOfWork.GetRepository<Trainer>().GetAll(t => t.Phone == trainerUpdated.Phone && t.Id != trainerId);

                if(emailExist.Any() || phoneExist.Any())
                    return false;
                var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
                if (trainer == null) return false;
                _mapper.Map(trainerUpdated, trainer);
                _unitOfWork.GetRepository<Trainer>().Update(trainer);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Phone == phone).Any();
        }
        #endregion
    }
}
