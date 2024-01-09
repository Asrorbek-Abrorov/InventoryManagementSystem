using Spectre.Console;
using InventoryManagementSystem;

namespace InventoryManagementSystem.Displays
{
    public class ProductManagerUi
    {
        private readonly ProductManager productManager;

        public ProductManagerUi(ProductManager productManager)
        {
            this.productManager = productManager;
        }

        public void Run()
        {
            AnsiConsole.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Product Manager *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Products ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Create", "Update", "Delete",
                            "View product", "View all products",
                            "Back"
                        }));
                switch (choice)
                {
                    case "Create":
                        var product = CreateProduct();
                        var check = productManager.AddProduct(product);
                        AnsiConsole.WriteLine(check ? "Product created successfully" : "Something went wrong!");
                        break;

                    case "Update":
                        int id = AnsiConsole.Ask<int>("ID : ");
                        product = productManager.GetProductDetails(id);

                        if (product != null)
                        {
                            product = UpdateProduct();
                            check = productManager.UpdateProduct(product, id);
                            if (check)
                            {
                                AnsiConsole.WriteLine("Product updated successfully!");
                            }
                            else
                            {
                                AnsiConsole.WriteLine("Product not found!");
                            }
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Product not found");
                        }
                        break;

                    case "Delete":
                        id = AnsiConsole.Ask<int>("ID : ");
                        check = productManager.RemoveProduct(id);
                        if (check)
                        {
                            AnsiConsole.WriteLine("Product deleted successfully!");
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Product not found!");
                        }
                        break;

                    case "View product":
                        id = AnsiConsole.Ask<int>("ID : ");
                        product = productManager.GetProductDetails(id);

                        if (product is not null)
                        {
                            AnsiConsole.WriteLine("******************************");
                            AnsiConsole.WriteLine($"Id : {product.Id}");
                            AnsiConsole.WriteLine($"Name : {product.Name}");
                            AnsiConsole.WriteLine($"Description : {product.Description}");
                            AnsiConsole.WriteLine($"Price : {product.Price}");
                            AnsiConsole.WriteLine($"Stock Quantity : {product.StockQuantity}");
                            AnsiConsole.WriteLine($"Supplier : {product.Supplier.Name}");
                            AnsiConsole.WriteLine("******************************");
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Product not found");
                        }

                        break;

                    case "View all products":
                        var products = productManager.GetAllProducts();
                        foreach (var prod in products)
                        {
                            AnsiConsole.WriteLine("******************************");
                            AnsiConsole.WriteLine($"Id : {prod.Id}");
                            AnsiConsole.WriteLine($"Name : {prod.Name}");
                            AnsiConsole.WriteLine($"Description : {prod.Description}");
                            AnsiConsole.WriteLine($"Price : {prod.Price}");
                            AnsiConsole.WriteLine($"Stock Quantity : {prod.StockQuantity}");
                            AnsiConsole.WriteLine($"Supplier : {prod.Supplier.Name}");
                        }
                        break;

                    case "Back":
                        keepRunning = false;
                        break;
                }
                AnsiConsole.WriteLine();
                AnsiConsole.WriteLine("Enter to continue...");
                Console.ReadKey();
            }
        }

        public Product CreateProduct()
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("*** Creating ***");
            AnsiConsole.WriteLine();

            var name = AnsiConsole.Ask<string>("Name : ").Trim();

            var description = AnsiConsole.Ask<string>("Description : ").Trim();

            var price = AnsiConsole.Ask<decimal>("Price : ");

            var stockQuantity = AnsiConsole.Ask<int>("Stock Quantity : ");

            var supplierId = AnsiConsole.Ask<int>("Supplier ID : ");

            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                Supplier = new Supplier { Id = supplierId }
            };

            return product;
        }

        public Product UpdateProduct()
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("*** Updating ***");
            AnsiConsole.WriteLine();

            var name = AnsiConsole.Ask<string>("New Name : ").Trim();

            var description = AnsiConsole.Ask<string>("New Description : ").Trim();

            var price = AnsiConsole.Ask<decimal>("New Price : ");

            var stockQuantity = AnsiConsole.Ask<int>("New Stock Quantity : ");

            var supplierId = AnsiConsole.Ask<int>("New Supplier ID : ");

            var product = new Product
            {
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = stockQuantity,
                Supplier = new Supplier { Id = supplierId }
            };

            return product;
        }
    }
}