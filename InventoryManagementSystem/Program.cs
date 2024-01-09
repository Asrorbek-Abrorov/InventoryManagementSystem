﻿namespace InventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        var productManager = new ProductManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/products.txt");

        var supplierManager = new SupplierManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/suppliers.txt");

        var supplier1 = new Supplier()
        {
            Name = "Supplier1",
            ContactDetails = "123456789",
            Products = new List<Product>()
            {
                productManager.GetProductDetails(1)
            }
        };

        var supplier2 = new Supplier()
        {
            Name = "Supplier2",
            ContactDetails = "123456789",
            Products = new List<Product>()
            {
                productManager.GetProductDetails(2)
            }
        };

        var product1 = new Product
        {
            Name = "Product 1",
            Description = "Description 1",
            Price = 10,
            StockQuantity = 100,
            Supplier = supplier1
        };

        productManager.AddProduct(product1);

        var product2 = new Product()
        {
            Name = "Product 2",
            Description = "Description 2",
            Price = 20,
            StockQuantity = 200,
            Supplier = supplier2
        };

        productManager.AddProduct(product2);

        supplierManager.AddSupplier(supplier1);
        supplierManager.AddSupplier(supplier2);
    }
}