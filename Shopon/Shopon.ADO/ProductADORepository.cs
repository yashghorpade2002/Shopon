using Microsoft.Data.SqlClient;
using Shopon.ADO.Util;
using Shopon.Common.CustomException;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.ADO
{
    /// <summary>
    /// The ProductADORepository
    /// </summary>
    public class ProductADORepository : IProductRepository
    {
        //private string connectionString = @"Data Source=PUMA-77911-WL;Initial Catalog=db_shopon;User ID=sa;Password=Root@123456$;Trust Server Certificate=True";

        private string connectionString = string.Empty;

        public ProductADORepository()
        {
            //ConnectionUtil util = ConnectionUtil.getInstance();
            connectionString = ConnectionUtil.GetConnectionString();
        }
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetPaginatedProducts(int page, int productsPerPage)
        {
            try
            {
                string sqlSt = "sp_getProductsPaginated";
                List<Product> products = new List<Product>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlSt, connection))
                    {
                        // Use stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add parameters for pagination
                        command.Parameters.AddWithValue("@PageNumber", page);
                        command.Parameters.AddWithValue("@RecordsPerPage", productsPerPage);

                        // Execute the query and retrieve the paginated products
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var product = new Product
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = Convert.ToString(reader["Name"]),
                                    Price = Convert.ToDouble(reader["Price"]),
                                    ImageUrl = Convert.ToString(reader["ImageUrl"]),
                                    Company = new Company
                                    {
                                        CompanyName = Convert.ToString(reader["CompanyName"])
                                    }
                                };
                                products.Add(product);
                            }
                        }
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new InvalidOperationException("Error while fetching products.", ex);
            }
        }

        public Product? GetProductById(int id)
        {
            try
            {
                string sqlSt = $"sp_getProductsById";
                Product product = null;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlSt, connection))
                    {
                        // When using a stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;  // Only for stored procedure
                        command.Parameters.AddWithValue("@v_product_id", id);   // Here We take the parameters

                        //CREATE, ALTER = DDL - ExecuteNonQuery()
                        //INSERT, UPDATE = DML - ExecuteNonQuery()
                        //SELECT - DQL - ExecuteReader();
                        using (SqlDataReader result = command.ExecuteReader())
                        {
                            if (result.Read())
                            {
                                product = new Product();
                                product.Id = Convert.ToInt32(result["Id"]);
                                product.Name = Convert.ToString(result["Name"]);
                                product.Price = Convert.ToDouble(result["Price"]);
                                product.ImageUrl = Convert.ToString(result["ImageUrl"]);

                                product.Company = new Company()
                                {
                                    CompanyName = Convert.ToString(result["CompanyName"])
                                };
                                product.Ratings = new ProductRatings()
                                {
                                    FkProductId = Convert.ToInt32(result["ProductIdFK"]),
                                    ratings = Convert.ToInt32(result["Ratings"])
                                };
                                product.Categories = new Categories()
                                {
                                    category_Name = Convert.ToString(result["CategoryName"])
                                };
                            } else
                            {
                                throw new InvalidProductException("No Product Found");
                            }
                        }
                    }

                    return product;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
            // When you are going to connect external resource use "using" which will automatically close the connection
            //finally
            //{
            //    connection.Close();
            //}
        }

        public IEnumerable<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                //string sqlSt = $"SELECT pro.product_id as Id," +
                //               $"pro.product_name as Name" +
                //               $"pro.imageUrl as ImageUrl" +
                //               $"pro.price as Price" +
                //               $"com.company_name as CompanyName" +
                //               $"FROM [dbo].[products] as pro WITH(NOLOCK)" +
                //               $"INNER JOIN [dbo].[companies] as com WITH(NOLOCK)" +
                //               $"on com.company_id = pro.company_id AND" +
                //               $"com.isDeleted=0" +
                //               $"WHERE pro.isDeleted=0";
                string sqlSt = $"sp_getProducts";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlSt, connection))
                    {
                        // When using a stored procedure
                        command.CommandType = System.Data.CommandType.StoredProcedure;  // Only for stored procedure
                        
                        //CREATE, ALTER = DDL - ExecuteNonQuery()
                        //INSERT, UPDATE = DML - ExecuteNonQuery()
                        //SELECT - DQL - ExecuteReader();
                        using (SqlDataReader result = command.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                Product product = new Product();
                                product.Id = Convert.ToInt32(result["Id"]);
                                product.Name = Convert.ToString(result["Name"]);
                                product.Price = Convert.ToDouble(result["Price"]);
                                product.ImageUrl = Convert.ToString(result["ImageUrl"]);

                                product.Company = new Company()
                                {
                                    CompanyName = Convert.ToString(result["CompanyName"])
                                };

                                products.Add(product);
                            }
                        }
                    }

                    return products;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                throw;
            }
            // When you are going to connect external resource use "using" which will automatically close the connection
            //finally
            //{
            //    connection.Close();
            //}
        }

        public int GetTotalProductCount()
        {
            try
            {
                string sqlSt = "SELECT COUNT(*) FROM [products] WHERE isDeleted = 0";
                int totalProducts = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlSt, connection))
                    {
                        totalProducts = (int)command.ExecuteScalar();
                    }
                }

                return totalProducts;
            }
            catch (Exception ex)
            {
                // Log exception
                throw new InvalidOperationException("Error while fetching total product count.", ex);
            }
        }

        public IEnumerable<Product>? SearchProducts(string searchey)
        {
            List<Product> products = new List<Product>();
            try
            {
                string sqlSt = "sp_searchProducts";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand(sqlSt, connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@v_searchKey", searchey);
                        using (SqlDataReader result = command.ExecuteReader())
                        {
                            while (result.Read())
                            {
                                Product product = new Product();
                                product.Id = Convert.ToInt32(result["Id"]);
                                product.Name = result["Name"].ToString();
                                product.Price = Convert.ToDouble(result["Price"]);
                                product.ImageUrl = result["ImageUrl"].ToString();
                                product.Company = new Company()
                                {
                                    CompanyName = result["CompanyName"].ToString()
                                };
                                products.Add(product);
                            }
                        }
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
