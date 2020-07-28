
using HDBH.Models;
using Microsoft.EntityFrameworkCore;

namespace HDBH.SQLData
{
    public class BaseDbContext : DbContext
    {
        private readonly string _connString;
        public BaseDbContext(string connstring)
        {
            _connString = connstring;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

        public DbSet<AttributeActionResult> AttributeActionResult { get; set; }


    }
}