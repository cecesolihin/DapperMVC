using Dapper;
using DapperMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DapperMVC.Repositories
{
    public class ProductRepository
    {
        public readonly string connectionString = ConfigurationManager.ConnectionStrings["connection"].ToString();
        public IList<Product> GetAll()
        {
            IList<Product> listdata = new List<Product>();
            try
            {
                var sql = @"SELECT [ProductId]
                                 ,[ProductName]
                                 ,[Price]
                             FROM[LATIHAN_DB].[dbo].[Product]";

                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    listdata = db.Query<Product>(sql).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return listdata;
        }
        public IList<Product> ProductsSearch(Product product)
        {
            IList<Product> listdata = new List<Product>();
            try
            {
                var sql = "exec [dbo].[Sp_Product_Search] @ProductId, @ProductName";
                var productId = string.IsNullOrEmpty(product.ProductId) ? "" : product.ProductId;
                var productName = string.IsNullOrEmpty(product.ProductName) ? "" : product.ProductName;
                using (SqlConnection db = new SqlConnection(connectionString))
                {
                    listdata = db.Query<Product>(sql, new { productId, productName }).ToList();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return listdata;
        }
        public Product ProductById(string productId)
        {
            Product dataProduct = new Product();
            var sql = "exec [dbo].[Sp_Product_ById] @ProductId";
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                 dataProduct = db.QuerySingle<Product>(sql, new { productId });
            }
            return dataProduct;
        }
        public void AddProduct(Product product)
        {
            var sql = "exec [dbo].[Sp_Product_Insert] @ProductId, @ProductName,@Price";
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                var exec = db.Execute(sql, new {product.ProductId, product.ProductName,product.Price });
            }
              
        }
        public void UpdateProduct(Product product)
        {
            var sql = "exec [dbo].[Sp_Product_Update] @ProductId, @ProductName,@Price";
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                var exec = db.Execute(sql, new { product.ProductId, product.ProductName, product.Price });
            }

        }
        public void DeleteProduct(string productId)
        {
            var sql = "exec [dbo].[Sp_Product_Delete] @ProductId";
            using (SqlConnection db = new SqlConnection(connectionString))
            {
                var exec = db.Execute(sql, new { productId});
            }

        }
    }
}