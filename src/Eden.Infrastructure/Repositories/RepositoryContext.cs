using Eden.Core.Entities.Auth;
using Eden.Core.Entities.Notes;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eden.Infrastructure.Repositories
{
    public class RepositoryContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;
        public RepositoryContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(@"Server=localhost;Database=Eden;Trusted_Connection=True;Trust Server Certificate=true", b => b.MigrationsAssembly("Eden.API"));

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }

        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Note> Notes { get; set; }
        public DbSet<NotesCategory> NotesCategories { get; set; }
        public DbSet<Note> Notes { get; set; }

    }
}
