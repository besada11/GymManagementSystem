using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class PlanRepository (GymDbContext _dbContext): IPlanRepository
    {

        //GetAll
        public IEnumerable<Plan> GetAllPlans()
        {
            return _dbContext.Plans.ToList();            
        }

        //GetById
        public Plan? GetById(int id)
        {
            return _dbContext.Plans.Find(id); 
        }

        //Update
        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
