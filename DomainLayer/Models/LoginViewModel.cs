using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class LoginViewModel : BaseEntity
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string RememberMe { get; set; }
    }
}
