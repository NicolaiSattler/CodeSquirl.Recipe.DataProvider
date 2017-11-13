using Microsoft.EntityFrameworkCore;
//using CodeSquirl.Recipy.Model;

namespace CodeSquirl.Recipy.DataProvider
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        //public DbSet<Product> ProductCollection { get; set; }
    }
}
