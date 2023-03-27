using AulersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AulersAPI
{
    public class AppDbContext: DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {          
        }

        public DbSet<User> Users { get; set; }
    }
}
