using System.ComponentModel.DataAnnotations;
using SubdivisionManagement.Model;

namespace SubdivisionManagement.Model
{
    public class ContactRequest
    {
        public int Id { get; set; }
        public int HomeownerId { get; set; }
        
        [Required]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string QueryType { get; set; } = string.Empty;
        
        [Required]
        public string Message { get; set; } = string.Empty;
        
        public DateTime DateSubmitted { get; set; }
        
        [Required]
        public string Status { get; set; } = string.Empty;
        
        public virtual Homeowner Homeowner { get; set; } = null!;
        
        public string? StaffNotes { get; set; }
    }
}