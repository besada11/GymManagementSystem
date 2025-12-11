using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Entities
{
    internal class Category : BaseClass
    {
        public string CategoryName { get; set; } = null!;

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
