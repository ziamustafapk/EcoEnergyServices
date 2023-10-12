using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using YourProjectName.Infrastructure.Configuration;
using YourProjectName.Models;

namespace YourProjectName.Infrastructure.DataContext
{
    public class YourProjectNameDbContext : DbContext
    {
        public YourProjectNameDbContext(DbContextOptions<YourProjectNameDbContext> options)
            : base(options)
        {
        }

        public DbSet<MyData> MyData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MyDataConfiguration());
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
