using AdsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AdsAPI.Data
{
    public class AdsDbContext : DbContext
    {
        public AdsDbContext(DbContextOptions<AdsDbContext> options) : base(options) { }

        public DbSet<Ad> Ads { get; set; }
    }
}
