using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class BaseVM
    {
        public BaseVM CurrentViewModels { get; set; }
    }
}
