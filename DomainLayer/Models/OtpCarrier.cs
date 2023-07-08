using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
	[Table("OTP_CARRIER")]
	public class OtpCarrier : BaseEntity
	{
		[Required]
		[Column("OTP_CARRIER")]
		public string Name { get; set; }
	}
}
