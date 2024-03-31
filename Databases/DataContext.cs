using BE.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace BE.Databases
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
    }
}
