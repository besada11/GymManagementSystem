using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        //GetAll
        IEnumerable<Session> GetAllSessions();
        //GetById
        Session? GetById(int id);
        //Add
        int Add(Session session);
        //Update
        int Update(Session session);
        //Delete
        int Delete(Session session);
    }
}
