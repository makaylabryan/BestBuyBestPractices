using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuyBestPractices
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products");
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO products (Name) VALUES (@name)",
                new { name = name });
            _conn.Execute("INSERT INTO products (Price) VALUES (@price)",
                new { price = price});
            _conn.Execute("INSERT INTO products (CategoryID) VALUES (@categoryID)",
                new { categoryID = categoryID });
        }

        public Product GetProductById(int id)
        {
            return _conn.QuerySingle<Product>("SELECT * FROM products WHERE ProductID = (@id)",
                new { id = id});
        }

        public void UpdateProduct(Product product)
        {
            _conn.Execute("UPDATE products " +
                "SET Name = @name," +
                "Price = @price, " +
                "CategoryID = @categoryID, " +
                "StockLevel = @stockLevel," +
                "OnSale = @onSale" +
                "WHERE ProductID = @id;",
                new
                {
                    name = product.Name,
                    price = product.Price,
                    categoryID = product.CategoryID,
                    stockLevel = product.StockLevel,
                    onSale = product.OnSale
                }) ;
        }

        public void DeleteProduct(int id)
        {
            _conn.Execute("DELETE FROM sales WHERE ProductID = @id;", new { id = id });
            _conn.Execute("DELETE FROM reviews WHERE ProductID = @id;", new { id = id });
            _conn.Execute("DELETE FROM products WHERE ProductID = @id;", new { id = id });
        }
    }
}
