using Microsoft.EntityFrameworkCore;

namespace Homework12.Model
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Item> GoodItems { get; set; }
    }
}
