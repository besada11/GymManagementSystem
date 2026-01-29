using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseClass , new()
    {
        //Get All
        IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null);

        //Get By Id
        TEntity? GetById(int id);

        //Add Entity
        void Add(TEntity entity);

        //Update Entity
        void Update(TEntity entity);

        //Delete Entity
        void Delete(TEntity entity);

    }
}
