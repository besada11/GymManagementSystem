using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL.ViewModels.TrainerViewModels
{
    internal class TrainerVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string? Address { get; set; }

    }
}
