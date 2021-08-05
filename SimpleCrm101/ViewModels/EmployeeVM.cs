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
    public class EmployeeVM : BaseVM
    {
        public EntityContext Entity { get; set; }
        public DapperContext dp { get; set; }

        public List<Employee> Employees { get; set; }
        public Employee SelectedEmployee { get; set; }

        public ICommand AddEmployeesCmnd { get; set; }
        public ICommand EditEmployeesCmnd { get; set; }
        public ICommand DeleteEmployeesCmnd { get; set; }

        public event Action<bool> AddEditEmployeesEvent;
        public event Action SearchEvent;


        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                SearchEvent?.Invoke();
            }
        }

        public EmployeeVM(DapperContext dapper, EntityContext entity)
        {
            dp = dapper;
            Entity = entity;
            Load();
            AddEmployeesCmnd = new RelayCommand(AddEmployeesMethod);
            EditEmployeesCmnd = new RelayCommand(EditEmployeesMethod);
            DeleteEmployeesCmnd = new RelayCommand(DeleteEmployeesMethod);

            SearchEvent += EmployeeVM_SearchEvent;
        }

        private void EmployeeVM_SearchEvent()
        {
            if (searchText == string.Empty) Load();
            else
            {
                List<Employee> employees1 = dp.GetEmployee();
                List<Employee> employees = employees1.Where(e => e.Name.Contains(SearchText)).ToList();
                Employees = employees;
            }
        }

        public void Load()
        {
            Employees = dp.GetEmployee();
        }

        private void DeleteEmployeesMethod()
        {
            Entity.Employees.Remove(SelectedEmployee);
            Entity.SaveChanges();
            Load();
        }

        private void EditEmployeesMethod()
        {
            AddEditEmployeesEvent?.Invoke(true);

        }

        private void AddEmployeesMethod()
        {
            AddEditEmployeesEvent?.Invoke(false);
        }
    }

}
