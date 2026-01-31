using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    public class SessionService(IUnitOfWork _unitOfWork) : ISessionService
    {
        public IEnumerable<SessionVM> GetAllSessions()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!Sessions.Any()) return [];
            return Sessions.Select(s => new SessionVM
            {
               Id=s.Id,
               Description=s.Description,
               StartDate=s.StartDate,
                EndDate=s.EndDate,
                Capacity=s.Capacity,
                CategoryName=s.SessionCategory.CategoryName,
                TrainerName=s.SessionTrainer.Name,
                AvailableSlots=s.Capacity - _unitOfWork.SessionRepository.GetAvailableSessions(s.Id),
            });
        }
    }
}
