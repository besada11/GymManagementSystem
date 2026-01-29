using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementBLL.ViewModels.PlanViewModels
{
    internal class UpdatePlanVM
    {
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Plan Description is required")]
        [StringLength(200, ErrorMessage = "Plan Description must be less than 201 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration Days is required")]
        [Range(1, 365, ErrorMessage = "Duration Days must be between 1 and 365")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000.00, ErrorMessage = "Price must be between 0.01 and 10000.00")]
        public decimal Price { get; set; }
    }
}
