using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Data
{
    public class DataContext : DbContext
    {
        // Constructor for the DataContext class that accepts DbContextOptions as a parameter
        // This constructor is used to configure the DbContext with the provided options
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // DbSet representing the Users table in the database
        public DbSet<Users> Users { get; set; }

        // DbSet representing the Contact table in the databas
        public DbSet<Contact> Contact { get; set; }

    }
}
