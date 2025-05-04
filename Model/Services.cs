using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubdivisionManagement.Model
{
    public class Services
    {
        [Key]
        public int Id { get; set; }

        public int? HomeownerId { get; set; }
        [ForeignKey("HomeownerId")]
        public Homeowner? Homeowner { get; set; }

        [Required]
        public string ServiceType { get; set; } = string.Empty;

        [Required]
        public string Priority { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = string.Empty;

        public DateTime DateSubmitted { get; set; }

        public int? AssignedStaffId { get; set; }
        [ForeignKey("AssignedStaffId")]
        public Staff? AssignedStaff { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        public string? StaffNotes { get; set; }
    }
}