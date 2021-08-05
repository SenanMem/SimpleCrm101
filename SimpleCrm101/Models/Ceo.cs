using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    [AddINotifyPropertyChangedInterface]
    public static class Ceo
    {
        public static string UserName { get; set; } = "Senan";
        public static string Password { get; set; } = "Senan";
    }
}
