using GymManagementBLL.ViewModels.MemberShipsViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberShipService
    {
        // Get All Memberships 
        IEnumerable<MemberShipVM> GetAllMemberShips();

        //Create Membership
        bool  CreateMemberShip(CreateMemberShipVM memberShipVM);

        IEnumerable<MemberForSelectListVM> GetAllMemberForSelectList();
        IEnumerable<PlanForSelectListVM> GetAllPlanForSelectList();

        //Delete Membership
        bool DeleteMemberShip(int memberId);

    }
}
