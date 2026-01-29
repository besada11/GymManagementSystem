using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbContext;
        public UnitOfWork(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseClass, new()
        {
            var EntityType = typeof(TEntity);
            if (_repositories.TryGetValue(EntityType, out var repo))
                return (IGenericRepository<TEntity>)repo;

            var newRepo = new GenericRepository<TEntity>(_dbContext);
            _repositories[EntityType] = newRepo;
            return newRepo;

        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
