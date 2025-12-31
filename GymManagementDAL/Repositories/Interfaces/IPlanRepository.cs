using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        //GetAll
        IEnumerable<Plan> GetAllPlans();

        //GetById
        Plan? GetById(int id);

        //Update
        int Update(Plan plan);
    }
}
