using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Employee:DomainObject,IAccount
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public List<Order> Orders { get; set; }
        public string Usename { get; set; }
        public string Password { get; set; }
    }
}
