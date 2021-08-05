using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using SimpleCrm101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class AddEdit_EmployeeVM : BaseVM
    {
        public ICommand SaveCmnd { get; set; }
        public ICommand CancelCmnd { get; set; }
        public bool EditMode { get; set; }

        public Employee employee { get; set; }


        public event Action<bool,Employee> SaveCancelEvent;

        public AddEdit_EmployeeVM()
        {
            SaveCmnd = new RelayCommand(SaveMethod);
            CancelCmnd = new RelayCommand(CancelMethod);
        }

        private void CancelMethod()
        {
            SaveCancelEvent?.Invoke(false,null);
        }

        private void SaveMethod()
        {
            SaveCancelEvent?.Invoke(true,employee);
        }
    }
}
