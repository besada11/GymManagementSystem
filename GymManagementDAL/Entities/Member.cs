using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Member : GymUser
    {
        //JoinDate == CreatedAt from BaseClass
        public string? Photo { get; set; } = null!;
    }
}
