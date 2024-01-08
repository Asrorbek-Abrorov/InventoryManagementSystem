namespace InventoryManagementSystem;

public class SupplierManager
{
    private readonly string filePath;

    public SupplierManager(string filePath)
    {
        this.filePath = filePath;
    }

    public void AddSupplier(Supplier supplier)
    {
        var suppliers = GetSuppliers();
        suppliers.Add(supplier);
        WriteSuppliersToFile(suppliers);
    }
    
    public void UpdateSupplier(Supplier supplier)
    {
        var suppliers = GetSuppliers();
        var existingSupplier = suppliers.FirstOrDefault(s => s.Name == supplier.Name);

        if (existingSupplier != null)
        {
            existingSupplier.ContactDetails = supplier.ContactDetails;
            existingSupplier.Products = supplier.Products;
            WriteSuppliersToFile(suppliers);
        }
    }


    public void RemoveSupplier(Supplier supplier)
    {
        var suppliers = GetSuppliers();
        var existingSupplier = suppliers.FirstOrDefault(s => s.Name == supplier.Name);

        if (existingSupplier != null)
        {
            suppliers.Remove(existingSupplier);
            WriteSuppliersToFile(suppliers);
        }
    }
    
    private List<Supplier> GetSuppliers()
    {
        var suppliers = new List<Supplier>();

        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var supplierParts = line.Split(',');
                var supplier = new Supplier
                {
                    Name = supplierParts[0],
                    ContactDetails = supplierParts[1],
                    Products = GetSupplierProducts(supplierParts[2])
                };

                suppliers.Add(supplier);
            }
        }

        return suppliers;
    }

    private List<Product> GetSupplierProducts(string supplierId)
    {
        var products = new List<Product>();

        using (var reader = new StreamReader($"products_{supplierId}.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var productParts = line.Split(',');
                var product = new Product
                {
                    Name = productParts[0],
                    Description = productParts[1],
                    Price = decimal.Parse(productParts[2]),
                    StockQuantity = int.Parse(productParts[3])
                };

                products.Add(product);
            }
        }
        return products;
    }

    private void WriteSuppliersToFile(List<Supplier> suppliers)
    {
        var lines = suppliers.Select(s => $"{s.Id},{s.Name},{s.ContactDetails}");
        File.WriteAllLines(filePath, lines);
    }
}