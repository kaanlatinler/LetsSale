using Microsoft.EntityFrameworkCore;
using LetsSale.Models.Entities;

namespace LetsSale.Database
{
    public class CarSaleDBContext: DbContext
    {
        public CarSaleDBContext(DbContextOptions<CarSaleDBContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarCat> CarCategories { get; set; }
        public DbSet<Employees> Employee { get; set; }
        public DbSet<ERank> ERanks { get; set; }
        public DbSet<Car_Pics> CarPics { get; set; }
        public DbSet<Saled_Cars> SaledCars { get; set; }
        public DbSet<User_Cars> UserCars { get; set; }
        public DbSet<Notifies> Notify { get; set; }
        public DbSet<Service_Cars> ServiceCars { get; set; }
    }
}
