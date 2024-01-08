namespace InventoryManagementSystem;

public class Product
{
    private static int id = 0;
    
    public int Id = ++id;
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public Supplier Supplier { get; set; }
}