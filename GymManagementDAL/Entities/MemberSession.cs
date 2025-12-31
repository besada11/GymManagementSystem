using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    public class MemberSession : BaseClass
    {
        //BookingDate == CreatedAt from BaseClass
        public bool IsAttended { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;

        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
    }
}
