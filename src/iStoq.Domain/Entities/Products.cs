using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public Guid SupplierId { get; set; }
    public Supplier Supplier { get; set; } = null!;

    public Product() { }
    public Product(string name, string description, decimal price, Guid id, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Id = id;
        Stock = stock;
    }

    public Product(string name, string description, decimal price, int stock, Guid categoryId, Guid supplierId)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        CategoryId = categoryId;
        SupplierId = supplierId;
    }

    public Product(string name, string description, decimal price, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }

    public void Update(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
    public void Update(string name, string description, decimal price, int stock)
    {
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
    }



    public void AddStock(int quantity) => Stock += quantity;
    public void RemoveStock(int quantity) => Stock -= quantity;
}
