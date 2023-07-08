using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.ConnectionManager
{
    public interface IOraConnection
    {
        public static string connString;

        IDbConnection GetConnection();
        Task<IDbConnection> GetConnectionAsync();
		void CloseConnection(IDbConnection conn);
        
    }
}
