using Microsoft.EntityFrameworkCore;
using MinApi.Models;

namespace MinApi
{
    public class TeamDbContext : DbContext
    {
        public TeamDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Team> Teams { get; set; }
    }
}
