using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.MemberShipsViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
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
        public IEnumerable<MemberForSessionVM> GetAllMemberForSession(int sessionId)
        {
            var bookingRepo = _unitOfWork.BookingRepository;
            var membersForSession = bookingRepo.GetSessionsById(sessionId);
            var memberForSessionMap= _mapper.Map<IEnumerable<MemberForSessionVM>>(membersForSession);
            return memberForSessionMap;
        }

        public bool CreateBooking(CreateBookingVM model)
        {
            var session = _unitOfWork.SessionRepository.GetById(model.SessionId);
            if(session is null || session.StartDate<= DateTime.Now)
                return false;

            var membershipRepo = _unitOfWork.MemeberShipReopsitory;
            var activeMembership = membershipRepo.GetFirstOrDefault(m=>m.Status == "Active" && m.MemberId == model.MemberId);

            if(activeMembership is null) 
                return false;

            var sessionRepo = _unitOfWork.SessionRepository;
            var availableSlots = session.Capacity - sessionRepo.GetAvailableSessions(model.SessionId);

            if(availableSlots == 0)
                return false;

            var booking = _mapper.Map<MemberSession>(model);
            booking.IsAttended = false;
            
            _unitOfWork.BookingRepository.Add(booking);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool MemberAttended(MemberAttendOrCancelVM model)
        {
            try
            {
                var memberSession = _unitOfWork.GetRepository<MemberSession>()
                                           .GetAll(X => X.MemberId == model.MemberId && X.SessionId == model.SessionId)
                                           .FirstOrDefault();
                if (memberSession is null) return false;

                memberSession.IsAttended = true;
                memberSession.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<MemberSession>().Update(memberSession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        public bool CancelBooking(MemberAttendOrCancelVM model)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(model.SessionId);
                if (session is null || session.StartDate <= DateTime.Now) return false;

                // BUSINESS RULE #5: A booking can only be cancelled for future sessions. Once the session has started, cancellation is not allowed.
                var Booking = _unitOfWork.BookingRepository.GetAll(X => X.MemberId == model.MemberId && X.SessionId == model.SessionId)
                                                           .FirstOrDefault();
                if (Booking is null) return false;
                _unitOfWork.BookingRepository.Delete(Booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }




        #region Helper Methods
        public IEnumerable<MemberForSelectListVM> GetAllMembersForSelectList(int sessionId)
        {
            var bookedMemberIds = _unitOfWork.BookingRepository
                .GetAll(s => s.SessionId == sessionId)
                .Select(s => s.MemberId).ToList();

            var activeMemberIds = _unitOfWork.MemeberShipReopsitory
                .GetAll(ms => ms.EndDate >= DateTime.Now)
                .Select(ms => ms.MemberId).ToList();

            var availableMembers = _unitOfWork.GetRepository<Member>()
                .GetAll(m => activeMemberIds.Contains(m.Id) && !bookedMemberIds.Contains(m.Id));

            return _mapper.Map<IEnumerable<MemberForSelectListVM>>(availableMembers);
        }
        #endregion
    }
}
