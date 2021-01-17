using Microsoft.Extensions.Configuration;
using OreoAppCommonLayer.Model;
using OreoAppRepositoryLayer.IServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace OreoAppRepositoryLayer.Services
{
    public class ProductRL : IProductRL
    {
        public SqlConnection sqlconnection;

        public readonly IConfiguration configuration;
        public ProductRL(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlconnection = new SqlConnection(this.configuration.GetConnectionString("OreoContext"));
        }

        public List<Product> GetALLProducts()
        {
            List<Product> productList = new List<Product>();
            
            try
            {
                using (this.sqlconnection)
                {
                    SqlCommand command = new SqlCommand("spGetProducts", this.sqlconnection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    this.sqlconnection.Open();

                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            Product product = new Product();
                            product.ProductId = sqlDataReader.GetInt32(0);
                            product.ProductName = sqlDataReader.GetString(1);
                            product.ActualPrice = sqlDataReader.GetDouble(2);
                            product.DiscountedPrice = sqlDataReader.GetDouble(3);
                            product.ProductQuantity = sqlDataReader.GetInt32(4);
                            product.ProductImage = sqlDataReader.GetString(5);
                            productList.Add(product);
                        }
                    }
                }
                return productList;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                this.sqlconnection.Close();
            }
        }
    }
}
