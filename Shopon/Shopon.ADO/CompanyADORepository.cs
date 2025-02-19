using Microsoft.Data.SqlClient;
using Shopon.ADO.Util;
using Shopon.Common.Models;
using Shopon.Data.Contracts;
using System.Data;
using System.Net.Http.Headers;

namespace Shopon.ADO
{
    public class CompanyADORepository : ICompanyRepository
    {
        private string connectionString;

        public CompanyADORepository()
        {
            //ConnectionUtil util = ConnectionUtil.getInstance();
            connectionString = ConnectionUtil.GetConnectionString();
        }

        public Company AddCompany(Company company)
        {
            try
            {
                string sqlSt = "SELECT company_id, company_name FROM companies";
                SqlDataAdapter adapter = null;
                DataSet dataSet = new DataSet();
                SqlCommandBuilder builder = null;

                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    adapter = new SqlDataAdapter(sqlSt, connection);
                    adapter.Fill(dataSet, "companies");
                    builder = new SqlCommandBuilder(adapter);

                    // This will get filled with the following data or we can say the select querry will be dumpted to the dataset

                    //Create row
                    DataRow datarow = dataSet.Tables["companies"].NewRow();
                    datarow[0] = company.CompanyId;
                    datarow[1] = company.CompanyName;
                    dataSet.Tables["companies"].Rows.Add(datarow);
                    adapter.Update(dataSet, "companies"); // write the data to the database 
                    // update content and get the latest data from the database 
                    return company;
                }
            } catch(Exception ex)
            {
                //log
                throw;
            }
        }

        public IEnumerable<Company> GetCompanies()
        {
            List<Company> companies = new List<Company>();
            try
            {
                DataTable dataTable = GetCompaniesData().Tables["companies"];
                //string sqlSt = "SELECT company_id, company_name FROM companies";
                //SqlDataAdapter adapter = null;
                //DataSet dataSet = new DataSet();
                //SqlCommandBuilder builder = null;

                //using (SqlConnection connection = new SqlConnection(connectionString))
                //{
                //    adapter = new SqlDataAdapter(sqlSt, connection);
                //    adapter.Fill(dataSet, "companies");
                //    builder = new SqlCommandBuilder(adapter);

                //    DataTable dataTable = dataSet.Tables["Companies"];
                    foreach(DataRow row in dataTable.Rows)
                    {
                        Company company = new Company()
                        {
                            CompanyId = Convert.ToInt16(row["company_id"]),
                            CompanyName = row["company_name"].ToString()
                        };
                        companies.Add(company);
                    }
                //}
            }
            catch (Exception ex)
            {
                //log
                throw;
            }
            return companies;

        }

        public Company GetCompanyById(int id)
        {
            Company company = null;
            try
            {
                //var companies = GetCompanies();
                //var company = companies.FirstOrDefault(x => x.CompanyId == id);
                //return company;

                DataTable table = GetCompaniesData().Tables["companies"];
                DataColumn[] keyDataColumn = new DataColumn[1];
                keyDataColumn[0] = table.Columns["company_id"];
                table.PrimaryKey = keyDataColumn;
                var dr = table.Rows.Find(id);
                if(dr != null)
                {
                    company = new Company
                    {
                        CompanyId = Convert.ToInt16(dr["company_id"]),
                        CompanyName = dr["company_name"].ToString()
                    };
                }
            } catch(Exception ex)
            {
                //log
                throw;
            }
            return company;
        }

        private DataSet GetCompaniesData()
        {
            string sqlSt = "SELECT company_id, company_name FROM companies";
            SqlDataAdapter adapter = null;
            DataSet dataSet = new DataSet();
            SqlCommandBuilder builder = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                adapter = new SqlDataAdapter(sqlSt, connection);
                adapter.Fill(dataSet, "companies");
                builder = new SqlCommandBuilder(adapter);
            }

            return dataSet;
        }
    }
}
