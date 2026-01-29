using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IGenericRepository<TEntity> where TEntity : BaseClass , new()
    {
        //Get All
        IEnumerable<TEntity> GetAll();

        //Get By Id
        TEntity? GetById(int id);

        //Add Entity
        int Add(TEntity entity);

        //Update Entity
        int Update(TEntity entity);

        //Delete Entity
        int Delete(TEntity entity);

    }
}
