using Microsoft.Data.SqlClient;
using Shopon.ADO.Util;
using Shopon.Common.Models;
using Shopon.Data.Contracts;

namespace Shopon.ADO
{
    public class CustomerADORepository : ICustomerRepository
    {
        private string connectionString = String.Empty;

        public CustomerADORepository()
        {
            //ConnectionUtil util = ConnectionUtil.getInstance();
            connectionString = ConnectionUtil.GetConnectionString();
        }
        public Customer GetCustomerById(int customerId)
        {
            Customer customer = null;
            try
            {
                string sqlSt = $"SELECT " +
                    $"customer_id AS CustomerId," +
                    $"customer_name AS CustomerName," +
                    $"mobileNumber AS MobileNumber," +
                    $"emailId AS EmailId " +
                    $"FROM customers " +
                    $"WHERE isDeleted = 0 " +
                    $"AND customer_id = @customer_id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlSt, connection))
                    {
                        command.Parameters.AddWithValue("@customer_id", customerId);
                        var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            customer = new Customer()
                            {
                                CustomerId = Convert.ToInt16(reader["customer_id"]),
                                CustomerName = reader["customer_name"].ToString(),
                                MobileNumber = reader["mobileNumber"].ToString(),
                                EmailId = reader["emailId"].ToString()
                            };

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // log
                throw;
            }
            return customer;
        
        }
    }
}
