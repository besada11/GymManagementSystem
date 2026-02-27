using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> GetRepository<TEntity>()where TEntity :BaseClass , new();

        public ISessionRepository SessionRepository { get; }
        public IMemeberShipReopsitory MemeberShipReopsitory { get; }

        int SaveChanges();  
    }
}
