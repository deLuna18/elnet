using Microsoft.EntityFrameworkCore;

namespace SubdivisionManagement.Model
{
    public class HomeContext : DbContext
    {
        public HomeContext(DbContextOptions<HomeContext> options) : base(options) { }
        public DbSet<Homeowner> Homeowners { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Staff> Staffs { get; set; }  
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
    }
}

