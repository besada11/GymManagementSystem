using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    public class Category : BaseClass
    {
        public string CategoryName { get; set; } = null!;

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
