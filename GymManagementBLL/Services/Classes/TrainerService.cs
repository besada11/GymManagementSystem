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
    internal class TrainerService (IUnitOfWork _unitOfWork) : ITrainerService
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
                _unitOfWork.GetRepository<Trainer>().Add(new Trainer
                {
                    Name = createTrainerVM.Name,
                    Email = createTrainerVM.Email,
                    Phone = createTrainerVM.Phone,
                    DateOfBirth = createTrainerVM.DateOfBirth,
                    Gender =createTrainerVM.Gender,
                    Address = new Address
                    {
                        Street = createTrainerVM.Street,
                        City = createTrainerVM.City,
                        BuildingNumber = createTrainerVM.BuildingNumber
                    },
                    Specialties = createTrainerVM.Specialties
                });
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
            return trainers.Select(t => new TrainerVM
            {
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                Specialization = t.Specialties.ToString(),
            });
        }

        //Get Trainer Details
        public TrainerVM? GetTrainerDetails(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
            if (trainer == null) return null;
            return new TrainerVM
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialization = trainer.Specialties.ToString(),
                Address = $"{trainer.Address.Street}, {trainer.Address.BuildingNumber}, {trainer.Address.City}"
            };
        }

        //Trainer To Update
        public TrainerToUpdateVM? GetTrainerToUpdate(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
            if (trainer == null) return null;
            return new TrainerToUpdateVM
            {
                Name = trainer.Name, //for display only
                Email = trainer.Email,
                Phone = trainer.Phone,
                Specialties = trainer.Specialties,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                BuildingNumber = trainer.Address.BuildingNumber
            };
        }

        //Update Trainer Details
        public bool UpdateTrainerDetails(int trainerId, TrainerToUpdateVM trainerUpdated)
        {
            try
            {
                if(IsEmailExists(trainerUpdated.Email)||IsPhoneExists(trainerUpdated.Phone))
                    return false;
                var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(m => m.Id == trainerId).FirstOrDefault();
                if (trainer == null) return false;
                trainer.Email = trainerUpdated.Email;
                trainer.Phone = trainerUpdated.Phone;
                trainer.Specialties = trainerUpdated.Specialties;
                trainer.Address.Street = trainerUpdated.Street;
                trainer.Address.City = trainerUpdated.City;
                trainer.Address.BuildingNumber = trainerUpdated.BuildingNumber;
                trainer.UpdatedAt = DateTime.Now;
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
