using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Classes
{
    internal class PlanService(IUnitOfWork _unitOfWork , IMapper _mapper) : IPlanService
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
            var plan = _mapper.Map<IEnumerable<PlanVM>>(plans);
            return plan;
        }

        //Get Plan By Id
        public PlanVM? GetPlanById(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);
            if (plan == null)
            {
                return null; 
            }
            var planVM = _mapper.Map<PlanVM>(plan);
            return planVM;

        }

        //To Update Plan
        public UpdatePlanVM? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan == null || plan.IsActive==false ||HasActiveMemberShips(planId))
            {
                return null;
            }
            var updatePlanVM = _mapper.Map<UpdatePlanVM>(plan);
            return updatePlanVM;

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
                _mapper.Map(updatedPlan, plan); 
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
