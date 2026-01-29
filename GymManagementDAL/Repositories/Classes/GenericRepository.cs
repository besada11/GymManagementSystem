using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    internal class GenericRepository<TEntity>(GymDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseClass , new()
    {
        public int Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public int Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return _dbContext.SaveChanges();
        }
    }
}
