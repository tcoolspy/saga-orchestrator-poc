using Microsoft.EntityFrameworkCore;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;

namespace OrderService.Data;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public string DbPath { get; }

    public OrderDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "orders.db");
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}