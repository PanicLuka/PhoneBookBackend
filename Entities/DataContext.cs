using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PhoneBookBackend.Entities
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options , IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactDetails> Details { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("PhoneBookDB"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .HasData(new
                {
                    ContactId = 1,
                    FirstName = "Milos",
                    LastName = "Milic"
                });

            modelBuilder.Entity<ContactDetails>()
                .HasData(new
                {
                    ContactDetailsId = 1,
                    Name = "Home number",
                    Value = "90932320",
                    ContactId = 1
                });

            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = 1,
                    FirstName = "Stojan",
                    LastName = "Stojic",
                    Username = "stojke223",
                    Email = "Stojke@gmail.com",
                    Password = "secretPass"
                });
        }
    }
}
