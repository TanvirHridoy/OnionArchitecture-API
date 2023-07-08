using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
	public class SmsResponse
	{
		public string Body { get; set; }
		public string MobileNumber { get; set; }
		public string Type { get; set; }
		public string ResponseString { get; set; }
	}
}
