using System.IO;

namespace InventoryManagementSystem;

public class ProductManager
{
    private readonly string productsFilePath;

    public ProductManager(string productsFilePath)
    {
        this.productsFilePath = productsFilePath;
    }

    public bool AddProduct(Product product)
    {
        using (var writer = new StreamWriter(productsFilePath, true))
        {
            writer.WriteLine($"{product.Id},{product.Name},{product.Description},{product.Price},{product.StockQuantity},{product.Supplier.Id}");
        }
        return true;
    }

    public bool UpdateProduct(Product product, int id)
    {
        var products = File.ReadAllLines(productsFilePath).ToList();

        bool productUpdated = false;

        for (int i = 0; i < products.Count; i++)
        {
            var productParts = products[i].Split(',');
            if (int.Parse(productParts[0]) == id)
            {
                products[i] = $"{product.Id},{product.Name},{product.Description},{product.Price},{product.StockQuantity},{product.Supplier.Id}";
                productUpdated = true;
                break;
            }
        }

        if (productUpdated)
        {
            File.WriteAllLines(productsFilePath, products);
        }

        return productUpdated;
    }
    public bool RemoveProduct(int productId)
    {
        var products = File.ReadAllLines(productsFilePath).ToList();

        bool productRemoved = false;

        products.RemoveAll(p => p.StartsWith($"{productId},"));

        if (products.Count < int.Parse(File.ReadAllLines(productsFilePath).Length.ToString()))
        {
            File.WriteAllLines(productsFilePath, products);
            productRemoved = true;
        }

        return productRemoved;
    }
    
    public Product GetProductDetails(int productId)
    {
        var products = File.ReadAllLines(productsFilePath);

        var productParts = products.FirstOrDefault(p => p.StartsWith($"{productId},"))?.Split(',');
        if (productParts != null)
        {
            return new Product
            {
                Id = int.Parse(productParts[0]),
                Name = productParts[1],
                Description = productParts[2],
                Price = decimal.Parse(productParts[3]),
                StockQuantity = int.Parse(productParts[4]),
                Supplier = GetSupplierById(int.Parse(productParts[5]))
            };
        }

        return null;
    }
    public Supplier GetSupplierById(int supplierId)
    {
        var suppliers = File.ReadAllLines("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/suppliers.txt").ToList();

        foreach (var supplierLine in suppliers)
        {
            var supplierParts = supplierLine.Split(',');
            var supplier = new Supplier
            {
                Id = int.Parse(supplierParts[0]),
                Name = supplierParts[1],
                ContactDetails = supplierParts[2]
            };

            if (supplier.Id == supplierId)
            {
                return supplier;
            }
        }

        return null;
    }
    public List<Product> GetAllProducts()
    {
        var products = new List<Product>();

        using (var reader = new StreamReader(productsFilePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var productParts = line.Split(',');
                var product = new Product
                {
                    Id = int.Parse(productParts[0]),
                    Name = productParts[1],
                    Description = productParts[2],
                    Price = decimal.Parse(productParts[3]),
                    StockQuantity = int.Parse(productParts[4]),
                    Supplier = GetSupplierById(int.Parse(productParts[5]))
                };

                products.Add(product);
            }
        }

        return products;
    }
    
}
