using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Client:DomainObject
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public List<Job> Jobs { get; set; }
    }
}
