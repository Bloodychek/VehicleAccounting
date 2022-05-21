using Microsoft.EntityFrameworkCore;
using VehicleAccounting.Models;

namespace VehicleAccounting
{
    public class MainContext : DbContext
    {
        public DbSet<Application> applications { get; set; } = null!;
        public DbSet<Customer> customers { get; set; } = null!;
        public DbSet<Goods> goods { get; set; } = null!;
        public DbSet<OrderExecutor> orderExecutors { get; set; } = null!;
        public DbSet<Models.Route> routes { get; set; } = null!;
        public DbSet<Transport> transports { get; set; } = null!;
        public DbSet<Treaty> treaties { get; set; } = null!;
        public DbSet<TypeOfGoods> typeOfGoods { get; set; } = null!;

        public MainContext(DbContextOptions<MainContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
