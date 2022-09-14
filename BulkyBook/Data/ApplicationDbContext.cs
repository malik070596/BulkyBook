using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        //to make ctegory table in the database
        public DbSet<Category> Categories { get; set; }
    }
}
