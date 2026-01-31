using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _dbContext;

        public SessionRepository( GymDbContext dbContext) : base( dbContext )
        {
            _dbContext= dbContext;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(s => s.SessionTrainer).Include(s => s.SessionCategory).ToList();
        }

        public int GetAvailableSessions(int sessionId)
        {
           return _dbContext.MemberSessions.Count(x=>x.SessionId==sessionId);
        }
    }
}
