using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    [Table("Price")]
    public class Price : BaseEntity
    {
        public string ProductId { get; set; }
        public float Rate { get; set; }
    }
}
