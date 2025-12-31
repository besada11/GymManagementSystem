using Microsoft.Identity.Client.AuthScheme.PoP;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace GymManagementDAL.Entities
{
    // 1-1 relationship with Member [shared pk]
    public class HealthRecord : BaseClass
    {
        public  decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }= null!;
        public string? Note { get; set; }

    }
}
