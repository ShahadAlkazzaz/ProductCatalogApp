using Resources.Models;
using Resources.Services;

namespace ProductCatalogApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "products.json";
            var productService = new ProductService(filePath);

            while (true)
            {
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Show All Products");
                Console.WriteLine("3. Delete Product");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddProduct(productService);
                        break;
                    case "2":
                        ShowAllProducts(productService);
                        break;
                    case "3":
                        DeleteProduct(productService);
                        break;
                    case "4":
                        UpdateProduct(productService);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void AddProduct(ProductService productService)
        {
            Console.Write("Enter product name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

            Console.Write("Enter product price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Invalid price entered.");
                return;
            }

            var product = new Product
            {
                Name = name,
                Price = price
            };

            var result = productService.Create(product);
            if (result.Succeeded)
            {
                Console.WriteLine("Product added successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to add product: {result.Message}");
            }
        }

        static void ShowAllProducts(ProductService productService)
        {
            var result = productService.GetAll();
            if (result.Succeeded && result.Result != null)
            {
                foreach (var product in result.Result)
                {
                    Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price:N0} kr");
                }
            }
            else
            {
                Console.WriteLine("Failed to load products.");
            }
        }

        static void DeleteProduct(ProductService productService)
        {
            Console.Write("Enter the ID of the product to delete: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Product ID cannot be empty.");
                return;
            }

            var result = productService.Delete(id);
            if (result.Succeeded)
            {
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to delete product: {result.Message}");
            }
        }

        static void UpdateProduct(ProductService productService)
        {
            Console.Write("Enter the ID of the product to update: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id))
            {
                Console.WriteLine("Product ID cannot be empty.");
                return;
            }

            Console.Write("Enter the new name of the product: ");
            string? newName = Console.ReadLine();

            if (string.IsNullOrEmpty(newName))
            {
                Console.WriteLine("Product name cannot be empty.");
                return;
            }

            Console.Write("Enter the new price of the product: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
            {
                Console.WriteLine("Invalid price entered.");
                return;
            }

            var updatedProduct = new Product
            {
                Name = newName,
                Price = newPrice
            };

            var result = productService.Update(id, updatedProduct);
            if (result.Succeeded)
            {
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine($"Failed to update product: {result.Message}");
            }
        }
    }
}

