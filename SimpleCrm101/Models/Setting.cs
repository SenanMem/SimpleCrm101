using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Setting
    {
        public string CompanyName { get; set; }
        public string Note1 { get; set; }
        public string Note2 { get; set; }
    }
}
