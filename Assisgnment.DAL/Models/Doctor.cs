using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assisgnment.DAL.Models
{
    public class Doctor : BaseEntity
    {
        [Required(ErrorMessage = "Doctor name is required!")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialization is required!")]
        [StringLength(100)]
        public string Specialization { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        public string? Address { get; set; }

        [Range(0, 1000000)]
        public decimal Salary { get; set; }

        public string? ImageName { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
