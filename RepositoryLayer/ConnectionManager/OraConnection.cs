using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace RepositoryLayer.ConnectionManager
{
    public class OraConnection: IOraConnection
    {
        //public static string connString = Configuration ConfigurationManager.AppSettings..ConnectionStrings["OraConnectionString"].ConnectionString;
        //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=host id)(PORT=specified port number)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=service name)));User Id = user Id; Password = your password; ";
        //public static string connString = ConfigurationManager.ConnectionStrings["OraConnectionString"].ConnectionString;

        private IConfiguration _config;
        public static string connString;
        public OraConnection(IConfiguration config)
        {
            _config = config;
            connString = _config.GetConnectionString("OraConnectionString");
        }

        public IDbConnection GetConnection()
        {
            

            var conn = new OracleConnection(connString);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        public void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
        }

		public async Task<IDbConnection> GetConnectionAsync()
		{


			var conn = new OracleConnection(connString);
			if (conn.State == ConnectionState.Closed)
			{
			    await conn.OpenAsync();
			}
			return conn;
		}

	}
}
