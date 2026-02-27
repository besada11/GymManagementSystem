using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    public class BookingService (IUnitOfWork _unitOfWork , IMapper _mapper): IBookingService
    {
        public IEnumerable<SessionVM> GetAllSessionWithTrainerAndCategory()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            var SessionVMs = _mapper.Map<IEnumerable<SessionVM>>(sessions);
            
            foreach(var session in SessionVMs)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetAvailableSessions(session.Id);
            }
            return SessionVMs;
        }
    }
}
