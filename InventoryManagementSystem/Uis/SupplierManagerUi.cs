using Spectre.Console;
using InventoryManagementSystem;

namespace InventoryManagementSystem.Displays
{
    public class SupplierManagerUi
    {
        private readonly SupplierManager _supplierManager;

        public SupplierManagerUi(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        public void Run()
        {
            AnsiConsole.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                    new FigletText("* Supplier Manager *")
                        .LeftJustified()
                        .Color(Color.Red1));
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[green]*** Suppliers ***[/]?")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                            "Create", "Update", "Delete",
                            "View supplier by name", "View all suppliers",
                            "Back"
                        }));
                switch (choice)
                {
                    case "Create":
                        var supplier = CreateSupplier();
                        var check = _supplierManager.AddSupplier(supplier);
                        AnsiConsole.WriteLine(check ? "Supplier created successfully" : "Something went wrong!");
                        break;

                    case "Update":
                        string name = AnsiConsole.Ask<string>("Name : ").Trim();
                        supplier = _supplierManager.FindSupplierByName(name);

                        if (supplier != null)
                        {
                            supplier = UpdateSupplier();
                            check = _supplierManager.UpdateSupplier(supplier);
                            if (check)
                            {
                                AnsiConsole.WriteLine("Supplier updated successfully!");
                            }
                            else
                            {
                                AnsiConsole.WriteLine("Supplier not found!");
                            }
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Supplier not found");
                        }
                        break;

                    case "Delete":
                        name = AnsiConsole.Ask<string>("Name : ").Trim();
                        supplier = _supplierManager.FindSupplierByName(name);

                        if (supplier != null)
                        {
                            check = _supplierManager.RemoveSupplier(supplier);
                            if (check)
                            {
                                AnsiConsole.WriteLine("Supplier deleted successfully!");
                            }
                            else
                            {
                                AnsiConsole.WriteLine("Supplier not found!");
                            }
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Supplier not found");
                        }
                        break;

                    case "View supplier by name":
                        name = AnsiConsole.Ask<string>("Name : ").Trim();
                        supplier = _supplierManager.FindSupplierByName(name);

                        if (supplier is not null)
                        {
                            AnsiConsole.WriteLine("******************************");
                            AnsiConsole.WriteLine($"Id : {supplier.Id}");
                            AnsiConsole.WriteLine($"Name : {supplier.Name}");
                            AnsiConsole.WriteLine($"Contact Details : {supplier.ContactDetails}");
                            AnsiConsole.WriteLine("******************************");
                        }
                        else
                        {
                            AnsiConsole.WriteLine("Supplier not found");
                        }

                        break;

                    case "View all suppliers":
                        var suppliers = _supplierManager.GetSuppliers();
                        foreach (var supp in suppliers)
                        {
                            AnsiConsole.WriteLine("******************************");
                            AnsiConsole.WriteLine($"Id : {supp.Id}");
                            AnsiConsole.WriteLine($"Name : {supp.Name}");
                            AnsiConsole.WriteLine($"Contact Details : {supp.ContactDetails}");
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

        public Supplier CreateSupplier()
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("*** Creating ***");
            AnsiConsole.WriteLine();

            var name = AnsiConsole.Ask<string>("Name : ").Trim();

            var contactDetails = AnsiConsole.Ask<string>("Contact Details : ").Trim();

            var supplier = new Supplier
            {
                Name = name,
                ContactDetails = contactDetails
            };

            return supplier;
        }

        public Supplier UpdateSupplier()
        {
            AnsiConsole.Clear();
            AnsiConsole.WriteLine("*** Updating ***");
            AnsiConsole.WriteLine();

            var name = AnsiConsole.Ask<string>("New Name : ").Trim();

            var contactDetails = AnsiConsole.Ask<string>("New Contact Details : ").Trim();

            var supplier = new Supplier
            {
                Name = name,
                ContactDetails = contactDetails
            };

            return supplier;
        }
    }
}