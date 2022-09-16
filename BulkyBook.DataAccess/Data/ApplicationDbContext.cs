using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.DataAccess;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    //to make tables in the database
    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
}
