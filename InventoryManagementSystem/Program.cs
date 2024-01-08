namespace InventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        var productManager = new ProductManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/products.txt");

        var product1 = new Product
        {
            Name = "Product 1",
            Description = "This is product 1.",
            Price = 1567.00m,
            StockQuantity = 10,
            Supplier = new Supplier
            {
                Id = 1
            }
        };

        productManager.AddProduct(product1);

        var product2 = new Product
        {
            Name = "Product 2",
            Description = "This is product 2.",
            Price = 15.00m,
            StockQuantity = 15,
            Supplier = new Supplier
            {
                Name = "Supplier 2",
                Address = "456 Elm Street",
                PhoneNumber = "555-234-5678",
                EmailAddress = "supplier2@example.com"
            }
        };
        productManager.UpdateProduct(product2);
        productManager.AddProduct(product2);

        productManager.RemoveProduct(3);

        if (productManager != null)
        {
            var product3 = productManager.GetProductDetails(1);
            
            Console.WriteLine($"Product Name: {product3.Name}");
            Console.WriteLine($"Product Description: {product3.Description}");
            Console.WriteLine($"Product Price: {product3.Price}");
            Console.WriteLine($"Product Stock Quantity: {product3.StockQuantity}");
            Console.WriteLine($"Product Supplier: {product3.Supplier.Name}");
        }
    }
}