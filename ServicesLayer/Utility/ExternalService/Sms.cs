using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Utility.ExternalService
{
	public static class Sms
	{
		public static SmsResponse ForwardSms(string mobileNumber, string message)
		{
			try
			{
				string url = String.Format("http://apibd.rmlconnect.net/bulksms/personalizedbulksms?username=OpusBDENT&password=hxIi6jyZ&destination={0}&source=8809617611359&message={1}", mobileNumber, message);

				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "GET";

				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				string responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

				var log = new SmsResponse
				{
					Body = message,
					MobileNumber = mobileNumber,
					Type = "SMS",
					ResponseString = responseString
				};



				return log;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}

}
