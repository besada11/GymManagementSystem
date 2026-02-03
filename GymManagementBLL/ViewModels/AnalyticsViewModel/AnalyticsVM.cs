using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace GymManagementBLL.ViewModels.AnalyticsViewModel
{
    public class AnalyticsVM
    {
        public int TotalMember { get; set; }
        public int ActiveMember { get; set; }
        public int TotalTariner { get; set; }
        public int UpcomingSession { get; set; }
        public int OngoingSession { get; set; }
        public int CompletedSession { get; set; }

    }
}
