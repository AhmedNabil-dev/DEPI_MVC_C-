using Assisgnment.DAL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Assisgnment.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Appointment Date & Time is required!")]
        [Display(Name = "Appointment Date & Time")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Status is required!")]
        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Doctor is required!")]
        [Display(Name = "Doctor")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "Patient is required!")]
        [Display(Name = "Patient")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
