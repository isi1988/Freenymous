using Freenymous.Data;
using Freenymous.Data.Users;
using Microsoft.EntityFrameworkCore;

namespace Freenymous.ProfileDownloader;

public class MainDbContext:DbContext
{
    //public MainDbContext() => Database.EnsureCreated();
    
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=free;Username=postgres;Password=123456");
}
