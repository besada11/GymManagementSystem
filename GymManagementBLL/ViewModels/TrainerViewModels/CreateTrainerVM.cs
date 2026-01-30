using GymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GymManagementBLL.ViewModels.TrainerViewModels
{
    internal class CreateTrainerVM
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters long")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email Must be between 5 and 100 characters long")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone Number Must Be Valid Egyptian PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 30 characters long")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 and 30 characters long")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "BuildingNumber is required")]
        [Range(1, 1000, ErrorMessage = "BuildingNumber Must Be Between 1 and 1000")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Specialization is required")]
        public Specialties Specialties { get; set; }

    }
}
