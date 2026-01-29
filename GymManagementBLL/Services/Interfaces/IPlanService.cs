using GymManagementBLL.ViewModels.PlanViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IPlanService
    {
        //Get All Plan
        IEnumerable<PlanVM> GetAllPlan();
        
        //Get Plan By Id
        PlanVM? GetPlanById(int id);

        //To Update Plan
        UpdatePlanVM? GetPlanToUpdate(int planId);

        //Update Plan
        bool UpdatePlan(int Id , UpdatePlanVM updatedPlan);

        //Status Plan
        bool ToggleStatus(int Id);

    }
}
