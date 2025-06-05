using AwesomeWebServiceTwo.Models;
using Microsoft.EntityFrameworkCore;

namespace AwesomeWebServiceTwo.Data;

public class UserDbContext : DbContext
{
    public DbSet<BlogUser?> BlogUsers { get; set; }

    public string DbPath { get; }

    public UserDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogusers.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}