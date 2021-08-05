using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using SimpleCrm101.Models;
using SimpleCrm101.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class LoginVM:BaseVM
    {
        private EntityContext Entity { get; set; }
        private DapperContext dp { get; set; }

        private List<Employee> _accounts { get; set; }
        public Employee employee { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        private bool _checkUser { get; set; }
        public bool SigInUser { get; set; } = false;

        public ICommand LoginCommand { get; set; }

        public event Action<bool> LoginEvent;

        public LoginVM()
        {
            _accounts = new List<Employee>();
            Entity = new EntityContext();
            dp = new DapperContext();
            loadUser();
            LoginCommand = new RelayCommand(loginMethod);
        }
        private void loginMethod()
        {
            
            _checkUser = false;
            if (Ceo.UserName == Username && Ceo.Password == Password)
            {
                _checkUser = true;
            }
            else
            {
                foreach (var account in _accounts)
                {
                    if (account.Password == Password && account.Usename == Username)
                    {
                        _checkUser = true;
                        employee = account;
                        SigInUser = true;
                        break;
                    }
                }
            }
            LoginEvent?.Invoke(_checkUser);
        }

        private void loadUser()
        {
            _accounts = dp.GetEmployee();
        }
    }
}
