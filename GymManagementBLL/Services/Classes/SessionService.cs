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

        //To Update Session
        public UpdateSessionVM? GetSessionToUpdate(int id)
        {
            var session = _unitOfWork.SessionRepository.GetById(id);
            if(!IsSessionAvailableToUpdate(session!))
                return null;
            //Mapping Session to UpdateSessionVM
            var mapSession = _mapper.Map<UpdateSessionVM>(session);
            return mapSession;
        }

        //Update Session
        public bool UpdateSession(UpdateSessionVM updateSession, int id)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(id);
                if (!IsSessionAvailableToUpdate(session!))
                    return false;
                //Check if Trainer Exists
                if (TrainerExists(updateSession.TrainerId) == false)
                    return false;
                //check if start date is before end date
                if (IsValidSessionDates(updateSession.StartDate, updateSession.EndDate) == false)
                    return false;
                //Mapping UpdateSessionVM to Session entity
                _mapper.Map(updateSession, session);
                session!.UpdatedAt = DateTime.Now;

                _unitOfWork.SessionRepository.Update(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update session failed: {ex.Message}");
                return false;
            }
        }

        //Delete Session
        public bool DeleteSession(int id)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(id);
                if (!IsSessionAvailableToRemove(session!))
                    return false;
                _unitOfWork.SessionRepository.Delete(session);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete session failed: {ex.Message}");
                return false;
            }
        }

        //Get Trainer Sessions
        public IEnumerable<TrainerSelectVM> GetTrainerForDropdown()
        {
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();
            if (!trainers.Any()) return [];
            //mapping trainers to trainerSelectVM
            var mapTrainers = _mapper.Map<IEnumerable<TrainerSelectVM>>(trainers);
            return mapTrainers;
        }

        //Get Category Sessions
        public IEnumerable<CategorySelectVM> GetCategoryForDropdown()
        {
            var categories = _unitOfWork.GetRepository<Category>().GetAll();
            if (!categories.Any()) return [];
            //mapping categories to categorySelectVM
            var mapCategories = _mapper.Map<IEnumerable<CategorySelectVM>>(categories);
            return mapCategories;
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
            return endDate > startDate && DateTime.Now < startDate;
        }

        //Is Session Available To Update
        private bool IsSessionAvailableToUpdate(Session session)
        {
            //if session not null - no update
            if (session == null) return false;

            //if session completed - no update
            if (session.EndDate < DateTime.Now) return false;

            //if session started - no update
            if (session.StartDate <= DateTime.Now) return false;

            //if session has active booking - no update
            var activeBookings = _unitOfWork.SessionRepository.GetAvailableSessions(session.Id);
            if (activeBookings > 0) return false;

            return true;
        }

        //Is Session Available To Remove
        private bool IsSessionAvailableToRemove(Session session)
        {
            //if session not null - no remove
            if (session == null) return false;

            //if session started - no remove
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;

            //if session is Upcoming - no remove
            if (session.StartDate > DateTime.Now) return false;

            //if session has active booking - no remove
            var activeBookings = _unitOfWork.SessionRepository.GetAvailableSessions(session.Id);
            if (activeBookings > 0) return false;

            return true;
        }

        #endregion
    }
}
