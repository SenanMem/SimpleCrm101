﻿using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm101.Models
{
    public interface IAccount
    {
        public string Usename { get; set; }
        public string Password { get; set; }
    }
}
