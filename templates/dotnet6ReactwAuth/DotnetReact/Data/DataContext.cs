using Microsoft.EntityFrameworkCore;
using DotnetReact.Models;

namespace DotnetReact.Data
{
    public class DataContext : DbContext
    {
     public DataContext(DbContextOptions<DataContext> options): base (options){}

     public DbSet<User> Users { get; set; }
    }
}