using GalaSoft.MvvmLight.CommandWpf;
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
    public class AddEdit_ClientVM:BaseVM
    {
        public bool EditMode { get; set; }
        public Client client { get; set; }

        public ICommand SaveCmnd { get; set; }
        public ICommand CancelCmnd { get; set; }

        public event Action<bool,Client> SaveCancelEvent;

        public AddEdit_ClientVM()
        {
            client = new Client();
            SaveCmnd = new RelayCommand(SaveCmndMethod);
            CancelCmnd = new RelayCommand(CancelCmndMethod);
        }

        private void CancelCmndMethod()
        {
            SaveCancelEvent?.Invoke(false,null);
        }

        private void SaveCmndMethod()
        {
            SaveCancelEvent?.Invoke(true,client);
        }
    }
}
