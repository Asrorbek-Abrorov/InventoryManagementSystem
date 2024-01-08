namespace InventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        var productManager = new ProductManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/products.txt");
        
        var supplierManager = new SupplierManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/suppliers.txt");
        
        var pruduct1 = new Product
        {
            Name = "Product 1",
            Description = "Description 1",
            Price = 10,
            StockQuantity = 100,
            Supplier = new Supplier
            {
                Name = "Supplier",
                ContactDetails = "ContactDetails"
            }
        };
        
        productManager.AddProduct(pruduct1);
        
        var pruduct2 = new Product()
        {
            Name = "Product 2",
            Description = "Description 2",
            Price = 20,
            StockQuantity = 200,
            Supplier = new Supplier
            {
                Name = "Supplier updated",
                ContactDetails = "ContactDetails updated"
            }
        };
        
        
        
        productManager.UpdateProduct(pruduct2, 1);

    }
}

