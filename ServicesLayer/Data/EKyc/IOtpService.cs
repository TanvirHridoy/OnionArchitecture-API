using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Data.EKyc
{
	public interface IOtpService
	{
		Otp GetOtp(int id);
		Task<int> AddOtp(Otp otp);
		Task<string> VerifyOtp(Otp otp);
	}
}
