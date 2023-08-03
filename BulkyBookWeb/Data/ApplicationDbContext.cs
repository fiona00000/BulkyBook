using BulkyBookWeb.Models;
using Microsoft.EntityFrameworkCore; // code first model instead of db first

namespace BulkyBookWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        //Constructor: hotkey: ctor
        // DbCOntextOptions<current constructor class>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        //Create category table using DbSet class
        public DbSet<Category> Categories { get; set; }
    }
}
