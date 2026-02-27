using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.ViewModels.MemberShipsViewModels
{
    public class MemberShipVM
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; } = null!;
        public int PlanId { get; set; }
        public string PlanName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
