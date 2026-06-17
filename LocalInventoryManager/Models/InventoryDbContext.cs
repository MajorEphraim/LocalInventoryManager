using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace LocalInventoryManager.Models
{
    internal class InventoryDbContext : DbContext
    {
        //Maps the InventoryItem class to the database table
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Get the path to the user's local application data folder
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //Combine the local application data folder path with the name of the database file
            string folderPath = Path.Combine(appDataPath, "LocalInventoryManager");

            //Create the directory if it doesn't exist
            Directory.CreateDirectory(folderPath);
            string dbPath = Path.Combine(folderPath, "LocalInventory.db");

            //Configure the DbContext to use SQLite with the specified database file path
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
