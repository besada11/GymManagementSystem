using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository(GymDbContext _dbContext) : ISessionRepository
    {
        //Add
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Delete(Session session)
        {
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        //GetAll
        public IEnumerable<Session> GetAllSessions()
        {
            return _dbContext.Sessions.ToList();
        }

        //GetById
        public Session? GetById(int id)
        {
            return _dbContext.Sessions.Find(id);
        }

        //Update
        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
