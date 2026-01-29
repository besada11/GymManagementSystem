using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class GenericRepository<TEntity>(GymDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseClass , new()
    {
        //Add Entity
        public void Add(TEntity entity) => _dbContext.Set<TEntity>().Add(entity);



        //Delete Entity
        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);


        //Get All Entities
        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition == null) 
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();  
        }

        //Get Entity By Id
        public TEntity? GetById(int id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        //Update Entity
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);


    }
}
