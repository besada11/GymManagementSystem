using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService(IUnitOfWork _unitOfWork) : IPlanService
    {
        //Get All Plans
        public IEnumerable<PlanVM> GetAllPlan()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plans == null || !plans.Any())
            {
                // Handle no plans found
                return [];
            }
            return plans.Select(plan => new PlanVM
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            }).ToList();

        }

        //Get Plan By Id
        public PlanVM? GetPlanById(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan == null)
            {
                return null; 
            }
            return new PlanVM
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };

        }

        //To Update Plan
        public UpdatePlanVM? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan == null || plan.IsActive==false ||HasActiveMemberShips(planId))
            {
                return null;
            }

            return new UpdatePlanVM
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
               PlanName= plan.Name
            };
        }

        //SoftDelete Plan
        public bool ToggleStatus(int Id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(Id);
            if (plan == null||HasActiveMemberShips(Id))
            {
                return false;
            }
            plan.IsActive = plan.IsActive == true ? false : true ;
            plan.UpdatedAt = DateTime.Now;
            try
            {
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;   
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Update Plan
        public bool UpdatePlan(int Id, UpdatePlanVM updatedPlan)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(Id);
           if(plan ==null|| HasActiveMemberShips(Id))
                return false;
            try
            {
                (plan.Description, plan.Price, plan.DurationDays, plan.UpdatedAt)
                 = (updatedPlan.Description, updatedPlan.Price, updatedPlan.DurationDays, DateTime.Now);
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
          
        }

        #region Healper Methods
        private bool HasActiveMemberShips(int planId)
        {
            var activeMemberships = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(m => m.PlanId == planId && m.Status=="Active");
            return activeMemberships.Any();
        }
        #endregion

    }
}
