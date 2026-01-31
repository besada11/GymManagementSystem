using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        // Get All Sessions with Trainer and Category
        IEnumerable<Session> GetAllSessionsWithTrainerAndCategory();

        // Get Session By ID with Trainer and Category
        Session? GetSessionByIDWithTrainerAndCategory(int sessionId);

        //Get Available Sessions
        int GetAvailableSessions(int sessionId);
    }
}
