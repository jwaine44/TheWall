#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace TheWall.Models;

public class TheWallContext : DbContext 
{ 
    public TheWallContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users {get; set;} 
    public DbSet<Message> Messages {get; set;} 
    public DbSet<UserMessageComment> UserMessageComments {get; set;} 
}