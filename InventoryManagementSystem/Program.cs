using InventoryManagementSystem;
using InventoryManagementSystem.Displays;

namespace InventoryManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var productManager = new ProductManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/products.txt");
            var supplierManager = new SupplierManager("/home/as_abrorov/RiderProjects/InventoryManagementSystem/InventoryManagementSystem/Database/suppliers.txt");
            var mainUi = new MainUi(productManager, supplierManager);
            mainUi.Run();
        }
    }
}