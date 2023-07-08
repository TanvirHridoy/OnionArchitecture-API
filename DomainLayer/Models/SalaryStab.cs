using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    [Table("SALARY_SLAB")]
    public class SalaryStab : BaseEntity
    {
        [Column("SLAB_NAME")]
        public string Name { get; set; }
    }
}
