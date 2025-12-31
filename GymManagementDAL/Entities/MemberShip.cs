using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    public class MemberShip : BaseClass
    {
        //StartDate == CreatedAt from BaseClass
        public DateTime EndDate { get; set; }
        //Readondly property to check if the membership is active
        public string status
        {
            get
            {
                return EndDate >= DateTime.Now ? "Active" : "Expired";
            }
        }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
    }
}
