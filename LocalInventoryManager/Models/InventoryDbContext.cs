using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace LocalInventoryManager.Models
{
    internal class InventoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public InventoryDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                string folderPath = Path.Combine(appDataPath, "LocalInventoryManager");

                Directory.CreateDirectory(folderPath);

                string dbPath = Path.Combine(folderPath, "LocalInventory.db");

                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }
    }
}