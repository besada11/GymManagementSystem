using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
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

        //Create Session
        public bool CreateSession(CreateSessionVM createSession)
        {
            try
            {
                //Check if Trainer Exists
                if (TrainerExists(createSession.TrainerId) == false)
                    return false;

                //Check if Category Exists
                if (CategoryExists(createSession.CategoryId) == false)
                    return false;

                //check if start date is before end date
                if (IsValidSessionDates(createSession.StartDate, createSession.EndDate) == false)
                    return false;

                //Check if capacity less than 25
                if (createSession.Capacity > 25 || createSession.Capacity < 0)
                    return false;

                //Mapping CreateSessionVM to Session entity
                var sessionEntity = _mapper.Map<Session>(createSession);

                _unitOfWork.GetRepository<Session>().Add(sessionEntity);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating session: {ex.Message}");
                return false;
            }
        }


        #region Helpers

        //Check if trainer exists
        private bool TrainerExists(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);
            return trainer != null;
        }

        //Check if category exists
        private bool CategoryExists(int categoryId)
        {
            var category = _unitOfWork.GetRepository<Category>().GetById(categoryId);
            return category != null;
        }

        //Validate session dates
        private bool IsValidSessionDates(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }

        #endregion
    }
}
