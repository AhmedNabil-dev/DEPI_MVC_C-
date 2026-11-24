using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assisgnment.DAL.Models
{
    public class Patient : BaseEntity
    {
        [Required(ErrorMessage = "Patient name is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 120)]
        public int Age { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
