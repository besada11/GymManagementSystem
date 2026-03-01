using GymManagementBLL.ViewModels.BookingViewModels;
using GymManagementBLL.ViewModels.MemberShipsViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IBookingService
    {
        //Get All session
        IEnumerable<SessionVM> GetAllSessionWithTrainerAndCategory();

        IEnumerable<MemberForSessionVM> GetAllMemberForUpComingSession(int sessionId);

    }
}
