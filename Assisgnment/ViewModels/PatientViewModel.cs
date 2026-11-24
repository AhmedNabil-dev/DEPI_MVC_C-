using System;
using System.ComponentModel.DataAnnotations;

namespace Assisgnment.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient Name is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 120)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
    }
}
