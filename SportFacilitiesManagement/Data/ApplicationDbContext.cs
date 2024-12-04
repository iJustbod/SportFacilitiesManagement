using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportFacilitiesManagement.Models;

namespace SportFacilitiesManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SportFacilitiesManagement.Models.Customer> Customer { get; set; } = default!;
        public DbSet<SportFacilitiesManagement.Models.Facility> Facility { get; set; } = default!;
        public DbSet<SportFacilitiesManagement.Models.Reservation> Reservation { get; set; } = default!;
    }
}
