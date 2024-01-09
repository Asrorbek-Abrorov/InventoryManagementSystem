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
                    ContactDetails = supplierParts[1]
                };

                suppliers.Add(supplier);
            }
        }

        return suppliers;
    }

    private void WriteSuppliersToFile(List<Supplier> suppliers)
    {
        var lines = suppliers.Select(s => $"{s.Id},{s.Name},{s.ContactDetails}");
        File.WriteAllLines(filePath, lines);
    }
}