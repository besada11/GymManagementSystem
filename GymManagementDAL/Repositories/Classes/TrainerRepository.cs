using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class TrainerRepository (GymDbContext _dbContext) : ITrainerRepository
    {
        //Add
        public int Add(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Delete(Trainer trainer)
        {
            var existingTrainer = _dbContext.Trainers.Find(trainer.Id);
            if (existingTrainer == null) return 0;

            _dbContext.Trainers.Remove(existingTrainer);
            return _dbContext.SaveChanges();
        }

        //Get All
        public IEnumerable<Trainer> GetAllTrainers()
        {
            return _dbContext.Trainers.ToList();
        }

        //Get By Id
        public Trainer? GetById(int id)
        {
            return _dbContext.Trainers.Find(id);
        }

        //Update
        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
