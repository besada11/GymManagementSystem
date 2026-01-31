using GymManagementBLL.ViewModels.SessionViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ISessionService
    {
        //Gets all sessions
        IEnumerable<SessionVM> GetAllSessions();

        //Get Session Details 
        SessionVM? GetSessionByID(int id);

        //Create Session    
        bool CreateSession(CreateSessionVM createSession);

        //Update Session
        //bool UpdateSession(UpdateSessionVM updateSession);
    }
}
