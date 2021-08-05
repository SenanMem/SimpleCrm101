using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Job : DomainObject
    {
        public string Title { get; set; }
        public double? StandartPrice { get; set; }
    }
}
