using InventoryManagementSystem.Displays;
using Spectre.Console;

namespace InventoryManagementSystem
{
    public class MainUi
    {
        private readonly ProductManagerUi _productManagerUi;
        private readonly SupplierManagerUi _supplierManagerUi;

        public MainUi(ProductManager productManager, SupplierManager supplierManager)
        {
            _productManagerUi = new ProductManagerUi(productManager);
            _supplierManagerUi = new SupplierManagerUi(supplierManager);
        }

        public void Run()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Main Menu *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Choose an option ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Products", "Suppliers",
                            "Back"
                        }));
                switch (choice)
                {
                    case "Products":
                        _productManagerUi.Run();
                        break;

                    case "Suppliers":
                        _supplierManagerUi.Run();
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
    }
}