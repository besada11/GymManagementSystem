using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
        //JoinDate == CreatedAt from BaseClass
        public string? Photo { get; set; } = null!;

        public HealthRecord HealthRecord { get; set; } = null!;
        public ICollection<MemberShip> MemberShips { get; set; } = null!;
        public ICollection<MemberSession> MemberSessions { get; set; } = null!;
    }
}
