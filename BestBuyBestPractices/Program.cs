

using BestBuyBestPractices;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

string connString = config.GetConnectionString("DefaultConnection");

IDbConnection conn = new MySqlConnection(connString);

var departmentRepo = new DapperDepartmentRepository(conn);

var departments = departmentRepo.GetAllDepartments();

foreach (var department in departments)
{
    Console.WriteLine(department.DepartmentID);
    Console.WriteLine(department.Name);
    Console.WriteLine();
    Console.WriteLine();

}

var productRepo = new DapperProductRepository(conn);

var productToUpdate = productRepo.GetProductById(974);
productRepo.UpdateProduct(productToUpdate);

productToUpdate.Name = "UPDATED";
productToUpdate.Price = 11.00;
productToUpdate.CategoryID = 1;
productToUpdate.OnSale = false;
productToUpdate.StockLevel = 500;

var products = productRepo.GetAllProducts();
foreach (var product in products)
{
    Console.WriteLine(product.ProductID);
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.StockLevel);
    Console.WriteLine(product.OnSale);
    Console.WriteLine(product.CategoryID);
    Console.WriteLine();
    Console.WriteLine();


}
