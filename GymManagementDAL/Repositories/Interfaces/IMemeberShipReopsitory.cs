using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IMemeberShipReopsitory : IGenericRepository<MemberShip>
    {
        //Get All Memberships with related data
        IEnumerable<MemberShip> GetAllMemberShips(Func<MemberShip, bool>? predicate = null);

        MemberShip? GetFirstOrDefault(Func<MemberShip, bool>? predicate = null);
    }
}
