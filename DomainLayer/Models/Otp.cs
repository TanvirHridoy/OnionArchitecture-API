using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
	[Table("OTP")]
	public class Otp : BaseEntity
	{
		[Column("MOBILE_NUMBER")]
		public string MobileNumber { get; set; }
		[Column("EMAIL_ADDRESS")]
		public string EmailAddress { get; set; }
		[Required]
		[Column("GENERATION_DATE")]
		public DateTime GenerationDate { get; set; }

		[Required]
		[Column("OPT_TYPE_ID")]
		public int OtpTypeId { get; set; }
		public OtpType OtpType { get; set; }
		[Required]
		[Column("OTP_CARRIER_ID")]
		public int OtpCarrierId { get; set; }
		public OtpType OtpCarrier { get; set; }
		[Column("PASSCODE")]
		public string Passcode { get; set; }

	}
}
