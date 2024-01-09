using InventoryManagementSystem;

public class SupplierManager
{
    private readonly string filePath;

    public SupplierManager(string filePath)
    {
        this.filePath = filePath;
    }

    public bool AddSupplier(Supplier supplier)
    {
        var suppliers = GetSuppliers();
        suppliers.Add(supplier);
        return WriteSuppliersToFile(suppliers);
    }

    public bool UpdateSupplier(Supplier supplier)
    {
        var suppliers = GetSuppliers();
        var existingSupplier = suppliers.FirstOrDefault(s => s.Name == supplier.Name);

        if (existingSupplier != null)
        {
            existingSupplier.ContactDetails = supplier.ContactDetails;
            return WriteSuppliersToFile(suppliers);
        }

        return false;
    }

    public bool RemoveSupplier(Supplier supplier)
    {
        var suppliers = GetSuppliers();
        var existingSupplier = suppliers.FirstOrDefault(s => s.Name == supplier.Name);

        if (existingSupplier != null)
        {
            suppliers.Remove(existingSupplier);
            return WriteSuppliersToFile(suppliers);
        }

        return false;
    }

    public List<Supplier> GetSuppliers()
    {
        var suppliers = new List<Supplier>();

        using (var reader = new StreamReader(filePath))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var supplierParts = line.Split(',');
                var supplier = GetSupplierById(int.Parse(supplierParts[0]));

                suppliers.Add(supplier);
            }
        }

        return suppliers;
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


    private bool WriteSuppliersToFile(List<Supplier> suppliers)
    {
        try
        {
            var lines = suppliers.Select(s => $"{s.Id},{s.Name},{s.ContactDetails}");
            File.WriteAllLines(filePath, lines);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public Supplier FindSupplierByName(string name)
    {
        var suppliers = GetSuppliers();

        var supplier = suppliers.FirstOrDefault(s => s.Name == name);

        return supplier;
    }
}