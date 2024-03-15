using Microsoft.EntityFrameworkCore;
using ProniaPrototype.Models;

namespace ProniaPrototype.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Opinion> Opinions { get; set; }
        public DbSet<ShippingArea> ShippingAreas { get; set; }
        public DbSet<Slider> Sliders { get; set; }
    }
}
