using Microsoft.EntityFrameworkCore;
using DotnetReact.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetReact.Data
{
    public class DataContext : DbContext
    {
     public DataContext(DbContextOptions<DataContext> options): base (options){}

     public DbSet<User> Users { get; set; }
    }
}