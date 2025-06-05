using Microsoft.EntityFrameworkCore;
using SagaPattern.Core.Models;
using SagaPattern.Core.Models.Entities;

namespace PaymentService.Data;

public class PaymentDbContext : DbContext
{
    public DbSet<Payment> Payments { get; set; }
    
    public string DbPath { get; }

    public PaymentDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "payments.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}