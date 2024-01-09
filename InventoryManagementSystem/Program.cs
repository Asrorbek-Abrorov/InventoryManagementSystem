using InventoryManagementSystem;
using InventoryManagementSystem.Displays;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var productManager = new ProductManager(@"../../../../InventoryManagementSystem/Database/products.txt");
            var supplierManager = new SupplierManager(@"../../../../InventoryManagementSystem/Database/suppliers.txt");
            var mainUi = new MainUi(productManager, supplierManager);
            mainUi.Run();
        }
    }
}