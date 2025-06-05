using Microsoft.EntityFrameworkCore;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;

namespace InventoryService.Data;

public class InventoryDbContext : DbContext
{
    public DbSet<Inventory> Inventories { get; set; }
    
    public string DbPath { get; }

    public InventoryDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "inventory.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}