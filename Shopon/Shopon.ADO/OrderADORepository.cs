using Microsoft.Data.SqlClient;
using Shopon.ADO.Util;
using Shopon.Common.Models;
using Shopon.Data.Contracts;
using System.Transactions;

namespace Shopon.ADO
{
    public class OrderADORepository : IOrderRepository
    {
        private string connectionString;

        public OrderADORepository()
        {
            //ConnectionUtil util = ConnectionUtil.getInstance();
            connectionString = ConnectionUtil.GetConnectionString();
        }
        public Order AddOrder(Order order)
        {
            SqlTransaction transaction = null;
            try
            {
                
                string sqlSt = $"sp_insertOrder";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using(SqlCommand command = new SqlCommand(sqlSt, connection, transaction))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        //@v_customerId
                        SqlParameter customerId = command.Parameters.Add("@v_customerId", System.Data.SqlDbType.Int);
                        customerId.Value = order.Customer.CustomerId == 0 ? DBNull.Value : order.Customer.CustomerId;
                        customerId.Direction = System.Data.ParameterDirection.Input;

                        //@v_aspnetUserId
                        SqlParameter aspnetUserId = command.Parameters.Add("@v_aspnetUserId", System.Data.SqlDbType.VarChar, 450);
                        aspnetUserId.Value = order.Customer.ASPNetUserId == null ? DBNull.Value : order.Customer.ASPNetUserId;
                        aspnetUserId.Direction = System.Data.ParameterDirection.Input;

                        //@v_orderTotal
                        SqlParameter ordertotal = command.Parameters.Add("@v_orderTotal", System.Data.SqlDbType.Float);
                        ordertotal.Value = order.OrderTotal;
                        ordertotal.Direction = System.Data.ParameterDirection.Input;

                        //@v_orderId
                        SqlParameter orderId = command.Parameters.Add("@v_orderId", System.Data.SqlDbType.Int);
                        orderId.Direction = System.Data.ParameterDirection.Output;

                        int noOfRecordes = command.ExecuteNonQuery();
                        if(noOfRecordes > 0)
                        {
                            int newOrderId = Convert.ToInt32(command.Parameters["@v_orderId"].Value);
                            order.OrderId = newOrderId;

                            //Insert to OrderItems
                            bool isInserted = insertOrderItems(order.GetOrderItems(), newOrderId, connection, transaction);
                            if(isInserted)
                            {
                                transaction.Commit();
                            } else
                            {
                                transaction.Rollback();
                            }
                        }
                        return order;
                    }
                }

            }catch (Exception ex)
            {
                //log
                transaction.Rollback();
                throw;
            }
        }

        private bool insertOrderItems(IEnumerable<OrderItem> orderItems, int newOrderId, SqlConnection connection, SqlTransaction transaction)
        {
            bool isInserted = true;
            try
            {
                string sqlSt = $"sp_insertOrderItems";
                foreach(var item in orderItems)
                {
                    using(SqlCommand command = new SqlCommand(sqlSt, connection, transaction))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@v_orderId", newOrderId);
                        command.Parameters.AddWithValue("@v_productId", item.Product.Id);
                        command.Parameters.AddWithValue("@v_qty", item.Qty);
                        int noRec = command.ExecuteNonQuery();
                        if(noRec <= 0)
                        {
                            isInserted = false;
                            break;
                        }
                    }
                }

            } catch (Exception ex)
            {
                throw;
            }

            return isInserted;

        }
    }
}
