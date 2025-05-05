using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SubdivisionManagement.Model
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Navigation property for support requests
        public virtual ICollection<SupportRequest> SupportRequests { get; set; }

        public Admin(string username, string passwordHash)
        {
            Username = username;
            PasswordHash = passwordHash;
            SupportRequests = new List<SupportRequest>();
        }
    }

    public class SupportRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string HomeownerName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string QueryType { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = "Pending";

        public string AdminResponse { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? ResolvedAt { get; set; }

        // Foreign key for Admin
        public int? AdminId { get; set; }
        public virtual Admin? Admin { get; set; }
    }
}
