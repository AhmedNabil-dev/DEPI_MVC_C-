using System;
using System.ComponentModel.DataAnnotations;

namespace Assisgnment.DAL.Models
{
    public class Appointment : BaseEntity
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Scheduled";

        [StringLength(500)]
        public string? Notes { get; set; }

        [Required(ErrorMessage = "Doctor is required!")]
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "Patient is required!")]
        public int PatientId { get; set; }
        public Patient? Patient { get; set; }
    }
}
