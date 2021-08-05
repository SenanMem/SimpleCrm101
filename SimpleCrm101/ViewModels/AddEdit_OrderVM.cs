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
    public class AddEdit_OrderVM:BaseVM
    {
        public EntityContext Entity { get; set; }
        public DapperContext dp { get; set; }
        public bool EditMode { get; set; }
        public List<Client> Clients { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Job> Jobs { get; set; }
        public Order order { get; set; }
        public List<string> Jobstatus { get; set; } = new List<string> { "Queue", "Carried Out", "End" };

        public int ClientIndex { get; set; } = 0;
        public int JobIndex { get; set; } = 0;
        public int EmployeeIndex { get; set; } = 0;

        public Employee UserEmployee { get; set; }
        public bool SigInUser { get; set; }

        public ICommand SaveCmnd { get; set; }
        public ICommand CancelCmnd { get; set; }

        public event Action<bool,Order> SaveCancelCmnd;

        public AddEdit_OrderVM(EntityContext entity,DapperContext dapper)
        {
            Entity = entity;
            dp = dapper;
            if (!EditMode)
            {
                order = new Order();
            }
            Load();
            SaveCmnd = new RelayCommand(SaveCmndMethod);
            CancelCmnd = new RelayCommand(CancelCmndMethod);
        }

        public void Load()
        {
            Clients = dp.GetClients();
            Jobs = dp.GetJob();
            if (SigInUser)
            {
                Employees = new List<Employee> { UserEmployee };
            }
            else
            {
                Employees = dp.GetEmployee();
            }
            if (EditMode)
            {
                for (int i = 0; i < Clients.Count(); i++)
                {
                    if (Clients[i].Id == order.client.Id)
                    {
                        ClientIndex = i;
                    }
                }
                for (int i = 0; i < Jobs.Count(); i++)
                {
                    if (Jobs[i].Id == order.JobTemplate.Id)
                    {
                        JobIndex = i;
                    }
                }
                for (int i = 0; i < Employees.Count(); i++)
                {
                    if (Employees[i].Id == order.employee.Id)
                    {
                        EmployeeIndex = i;
                    }
                }
            }
        }

        private void CancelCmndMethod()
        {
            SaveCancelCmnd?.Invoke(false,null);
        }

        private void SaveCmndMethod()
        {
            SaveCancelCmnd?.Invoke(true, order);
        }
    }
}
