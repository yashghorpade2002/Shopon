using Microsoft.Extensions.Configuration;

namespace Shopon.ADO.Util
{
    public static class ConnectionUtil
    {
        private static string connectionString = string.Empty; //@"Data Source=PUMA-77911-WL;Initial Catalog=db_shopon;User ID=sa;Password=Root@123456$;Trust Server Certificate=True";

        //private static ConnectionUtil instance = new ConnectionUtil();
        static ConnectionUtil()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var config = builder.Build();
            connectionString = config.GetConnectionString("Default");
        }

        // public static ConnectionUtil getInstance() => instance;

        public static string GetConnectionString() => connectionString;

    }
}
