using GymManagementBLL.ViewModels.AnalyticsViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IAnalyticsService
    {
        //get analytics data
        AnalyticsVM GetAnalyticsData();
    }
}
