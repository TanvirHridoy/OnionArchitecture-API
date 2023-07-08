using DomainLayer.Models;
using RepositoryLayer.QueryBuilder;
using RepositoryLayer.RespositoryPattern;
using ServicesLayer.CustomerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesLayer.Utility.ExternalService;

namespace ServicesLayer.Data.EKyc
{
	public class OtpService : IOtpService
	{
		#region Property
		private IRepository<Otp> _repository;
		private readonly IOraQueryBuilder<Otp> _oraQueryBuilder;
		#endregion

		#region Constructor
		public OtpService(IRepository<Otp> repository, IOraQueryBuilder<Otp> oraQueryBuilder)
		{
			_repository = repository;
			_oraQueryBuilder = oraQueryBuilder;
		}
		#endregion


		public Otp GetOtp(int id)
		{
			return _repository.Get(id);
		}
		public async Task<int> AddOtp(Otp otp)
		{
			var passcode = await _oraQueryBuilder.GetPassword(otp.MobileNumber, otp.EmailAddress, otp.OtpTypeId, otp.OtpCarrierId);
			var message = "Use this token to proceed- " + Convert.ToString(passcode);
			var response = Sms.ForwardSms(otp.MobileNumber, message);

			return passcode;
		}
		public async Task<string> VerifyOtp(Otp otp)
		{
			var result = await _oraQueryBuilder.VerifyOtp(otp.MobileNumber, otp.EmailAddress, otp.OtpTypeId, otp.Passcode);
			
			return result;
		}
		
	}
}
