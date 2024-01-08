namespace InventoryManagementSystem;

public class Supplier
{
    private static int id = 0;
    public int Id = ++id; 
    public string Name { get; set; }
    public string ContactDetails { get; set; }
    public List<Product> Products { get; set; }
}