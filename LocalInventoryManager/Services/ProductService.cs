using LocalInventoryManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocalInventoryManager.Services
{
    public class ProductService
    {
        //Get all products from the database
        public List<Product> GetAll()
        {
            using var db = new InventoryDbContext();
            return db.Products.ToList();
        }

        //Add new product to the database
        public void Add(Product product)
        {
            using var db = new InventoryDbContext();
            db.Products.Add(product);
            db.SaveChanges();
        }

        //Delete a product from the database by its ID
        public void Delete(int id)
        {
            using var db = new InventoryDbContext();
            var product = db.Products.Find(id);

            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        //Update an existing product in the database
        public void Update(Product product)
        {
            using var db = new InventoryDbContext();
            db.Products.Update(product);
            db.SaveChanges();
        }
    }
}