using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowVM:BaseVM
    {
        public BaseVM CurrentVM { get; set; }
        private MenuVM _menuVM { get; set; }
        private LoginVM _loginVM { get; set; }

        public event Action CloseWindowEvenetMainWindow;


        public MainWindowVM()
        {
            _menuVM = new MenuVM();
            _loginVM = new LoginVM();
            _loginVM.LoginEvent += _loginVM_LoginEvent;
            _menuVM.CloseWindowEvenet += _menuVM_CloseWindowEvenet;
            CurrentVM = _loginVM;

        }

        private void _loginVM_LoginEvent(bool obj)
        {
            if (obj)
            {
                _menuVM.UserEmployee = _loginVM.employee;
                _menuVM.SigInUser = _loginVM.SigInUser;
                CurrentVM = _menuVM;
            }
        }

        private void _menuVM_CloseWindowEvenet()
        {
            CloseWindowEvenetMainWindow?.Invoke();
        }
    }
}
