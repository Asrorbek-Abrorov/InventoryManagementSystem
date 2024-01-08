using System.IO;

namespace InventoryManagementSystem;

public class ProductManager
{
    private readonly string productsFilePath;

    public ProductManager(string productsFilePath)
    {
        this.productsFilePath = productsFilePath;
    }

    public void AddProduct(Product product)
    {
        using (var writer = new StreamWriter(productsFilePath, true))
        {
            writer.WriteLine($"{product.Id},{product.Name},{product.Description},{product.Price},{product.StockQuantity},{product.Supplier.Id}");
        }
    }

    public void UpdateProduct(Product product, int id)
    {
        var products = File.ReadAllLines(productsFilePath).ToList();

        for (int i = 0; i < products.Count; i++)
        {
            var productParts = products[i].Split(',');
            if (int.Parse(productParts[0]) == id)
            {
                products[i] = $"{product.Id},{product.Name},{product.Description},{product.Price},{product.StockQuantity},{product.Supplier.Id}";
                break;
            }
        }

        File.WriteAllLines(productsFilePath, products);
    }

    public void RemoveProduct(int productId)
    {
        var products = File.ReadAllLines(productsFilePath).ToList();

        products.RemoveAll(p => p.StartsWith($"{productId},"));

        File.WriteAllLines(productsFilePath, products);
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
                Supplier = new Supplier
                {
                    Id = int.Parse(productParts[5])
                }
            };
        }

        return null;
    }
}
