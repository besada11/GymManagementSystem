using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementBLL.ViewModels.MemberViewModels
{
    public class HealthRecordVM
    {
        [Required(ErrorMessage = "Height is required")]
        [Range(0.1, 300, ErrorMessage = "Height Must Be Between 0.1 and 300 Cm")]
        public decimal Height { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.1, 500, ErrorMessage = "Weight Must Be Between 0.1 and 500 Kg")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Blood Type is required")]
        [StringLength(3, ErrorMessage = "Blood Type Must Be 3 Characters Or Less")]
        public string BloodType { get; set; } = null!;

        public string? Notes { get; set; }

    }
}
