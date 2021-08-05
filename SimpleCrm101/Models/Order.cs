using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Order :DomainObject
    {
        public Client client { get; set; }
        public Job JobTemplate { get; set; }
        public Employee employee { get; set; }
        public string JobStatus { get; set; }
        public Task task { get; set; }
        public double? Price { get; set; }
        public DateTime? Time { get; set; }     
        public bool OrderHasBeenPaid { get; set; }
        public Order()
        {
            task = new Task();
        }
    }
}
