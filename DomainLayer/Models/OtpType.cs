using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
	[Table("OTP_TYPE")]
	public class OtpType : BaseEntity
	{
		[Required]
		[Column("NAME")]
		public string Name { get; set; }
		[Required]
		[Column("CHAR_LENGTH")]
		public int CharLength { get; set; }
		[Required]
		[Column("VALID_TILL")]
		public int ValidTill { get; set; }
	}
}
