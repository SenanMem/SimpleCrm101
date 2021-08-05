using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public static class ApplicationSettings
    {
        public static string CompanyName = "Mem Crm";
        public static string Note1 = "Note 1";
        public static string Note2 = "Note 2";
    }
}
