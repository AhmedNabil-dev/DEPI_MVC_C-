using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Assisgnment.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Doctor Name is required!")]
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

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
