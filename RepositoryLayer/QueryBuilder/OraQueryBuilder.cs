using Dapper;
using DomainLayer.Models;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using RepositoryLayer.ConnectionManager;
using RepositoryLayer.RespositoryPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.WebRequestMethods;

namespace RepositoryLayer.QueryBuilder
{
    public class OraQueryBuilder<T> : IOraQueryBuilder<T> where T : class
    {
        private readonly IOraConnection _oraConnection;
        public OraQueryBuilder(IOraConnection oraConnection) 
        {
            _oraConnection = oraConnection;
        }
        public IEnumerable<T> GetList(T entity)
        {
            IDbConnection connection = _oraConnection.GetConnection();

            var result = connection.Query<T>(GetColumnList(entity));

            _oraConnection.CloseConnection(connection);

            return result;
        }

        public T SingleOrDefault(T entity)
        {
            IDbConnection connection = _oraConnection.GetConnection();

            var result = connection.QueryFirstOrDefault<T>(GetColumnList(entity));

            _oraConnection.CloseConnection(connection);

            return result;
        }

        public int? ExecuteAction(string query)
        {
            IDbConnection connection = _oraConnection.GetConnection();

            var result = connection.Execute(query);

            _oraConnection.CloseConnection(connection);

            return result;
        }

        private static string GetColumnList(T entity)
        {
            string selectedColumns = "Select ";
            foreach (var prop in entity.GetType().GetProperties())
            {
                if (!prop.Name.Contains("_"))
                {
                    selectedColumns = selectedColumns + ConvertToPascalCase(prop.Name) + " AS " + prop.Name + ",";
                }
            }

            string query = selectedColumns.Remove(selectedColumns.Length - 1);

            query = query + " From " + ConvertToPascalCase(entity.GetType().Name);

            return query;
        }

        private static string ConvertToPascalCase(string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
		}


		public async Task<int> GetPassword(string mobileNo, string emailAddress, int otpTypeId, int otpCarrierId)
		{
			//IDbConnection connection = await _oraConnection.GetConnectionAsync();

			//var result = connection.Execute(query);

			//using (var con = new OracleConnection(connection.ConnectionString))
			using (var con = await _oraConnection.GetConnectionAsync())
			{

				var dynamicParameters = new DynamicParameters();
				dynamicParameters.AddDynamicParams(new { mobileNo });
				dynamicParameters.AddDynamicParams(new { emailAddress });
				dynamicParameters.AddDynamicParams(new { otpTypeId });
				dynamicParameters.AddDynamicParams(new { otpCarrierId });

				dynamicParameters.Add("Password", DbType.Int32, direction: ParameterDirection.Output);

				var results = await con.QueryAsync<int>("dcbs.SP_GENERATE_OTP", dynamicParameters,
					commandType: CommandType.StoredProcedure);

				var password = dynamicParameters.Get<int>("Password");
				con.Close();
				//Console.WriteLine($"Got {count} records");

				return password;
			}
		}

		public async Task<string> VerifyOtp(string p_mobileNo, string p_emailAddress, int p_otpTypeId, string p_passcode)
        {
			using (var con = await _oraConnection.GetConnectionAsync())
			{

				var dynamicParameters = new DynamicParameters();
				dynamicParameters.AddDynamicParams(new { p_mobileNo });
				dynamicParameters.AddDynamicParams(new { p_emailAddress });
				dynamicParameters.AddDynamicParams(new { p_otpTypeId });
				dynamicParameters.AddDynamicParams(new { p_passcode });

				dynamicParameters.Add("P_RESULT", DbType.String, direction: ParameterDirection.Output);

				var results = await con.QueryAsync<int>("dcbs.SP_VERIFY_OTP", dynamicParameters,
					commandType: CommandType.StoredProcedure);

				var result = dynamicParameters.Get<int>("P_RESULT");
				con.Close();
                //Console.WriteLine($"Got {count} records");
                if (result == 1)
                    return "ok";
                else
				return "nok";
			}
		}
	}
}

