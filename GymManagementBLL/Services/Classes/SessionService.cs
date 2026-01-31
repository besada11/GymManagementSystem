using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Repositories.Interfaces;


namespace GymManagementBLL.Services.Classes
{
    public class SessionService(IUnitOfWork _unitOfWork , IMapper _mapper) : ISessionService
    {
        //Gets all sessions
        public IEnumerable<SessionVM> GetAllSessions()
        {
            var Sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory();
            if (!Sessions.Any()) return [];

            //mapping sessions to sessionVM
            var mapSessions = _mapper.Map<IEnumerable<SessionVM>>(Sessions);
            foreach (var session in mapSessions)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetAvailableSessions(session.Id);
            }
            return mapSessions;
        }

        //Get Session Details
        public SessionVM? GetSessionByID(int id)
        {
            var session = _unitOfWork.SessionRepository.GetSessionByIDWithTrainerAndCategory(id);
            if(session == null) return null;
           
            //mapping session to sessionVM
            var mapSession = _mapper.Map<SessionVM>(session);
            mapSession.AvailableSlots = mapSession.Capacity - _unitOfWork.SessionRepository.GetAvailableSessions(session.Id);
            return mapSession;
        }
    }
}
