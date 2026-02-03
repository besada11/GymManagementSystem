using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.AnalyticsViewModel;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class AnalyticsService(IUnitOfWork _unitOfWork) : IAnalyticsService
    {
        //get analytics data
        public AnalyticsVM GetAnalyticsData()
        {
            var SessionRepo = _unitOfWork.SessionRepository.GetAll();
            return new AnalyticsVM()
            {
                ActiveMember = _unitOfWork.GetRepository<MemberShip>().GetAll(m => m.Status=="Active").Count(),
                TotalTariner = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                TotalMember = _unitOfWork.GetRepository<MemberShip>().GetAll().Count(),
                UpcomingSession = SessionRepo.Count(s => s.StartDate > DateTime.Now),
                OngoingSession = SessionRepo.Count(s => s.StartDate <= DateTime.Now && s.EndDate >= DateTime.Now),  
                CompletedSession = SessionRepo.Count(s => s.EndDate < DateTime.Now)
            };
        }
    }
}
