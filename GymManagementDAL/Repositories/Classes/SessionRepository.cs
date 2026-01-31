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

        // Get all sessions with trainer and category
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory()
        {
            return _dbContext.Sessions.Include(s => s.SessionTrainer).Include(s => s.SessionCategory).ToList();
        }

        // Get available sessions by session ID
        public int GetAvailableSessions(int sessionId)
        {
           return _dbContext.MemberSessions.Count(x=>x.SessionId==sessionId);
        }

        // Get session by ID with trainer and category
        public Session? GetSessionByIDWithTrainerAndCategory(int sessionId)
        {
            return _dbContext.Sessions.Include(s => s.SessionTrainer)
                                      .Include(s => s.SessionCategory)
                                      .FirstOrDefault(s => s.Id == sessionId);
        }
    }
}
