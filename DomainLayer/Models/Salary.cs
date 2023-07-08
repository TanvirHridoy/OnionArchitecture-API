using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Salary : BaseEntity
    {
        public int Year { get; set; }
        public int MonthId { get; set; }
    }
}
