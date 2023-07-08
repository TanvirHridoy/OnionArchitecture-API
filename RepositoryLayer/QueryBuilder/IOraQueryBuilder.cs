using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.QueryBuilder
{
    public interface IOraQueryBuilder<T> where T : class
    {
        IEnumerable<T> GetList(T entity);
        T SingleOrDefault(T entity);
        int? ExecuteAction(string query);
		Task<int> GetPassword(string mobileNo, string emailAddress, int otpTypeId, int otpCarrierId);
		Task<string> VerifyOtp(string mobileNo, string emailAddress, int otpTypeId, string passcode);
		

	}
}
