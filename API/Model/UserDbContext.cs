using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace API.Model
{
    public class UserDbContext : DbContext
    {

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItem { get;set; }
        public DbSet<Combo> Combo { get; set; }
        public DbSet<ComboDetail> ComboDetail { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }



    }
}
