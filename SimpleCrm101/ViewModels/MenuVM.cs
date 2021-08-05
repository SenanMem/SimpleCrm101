using GalaSoft.MvvmLight.Command;
using PropertyChanged;
using SimpleCrm101.Models;
using SimpleCrm101.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MenuVM : BaseVM
    {
        #region viewModels
        private AddEdit_ClientVM _addEdit_ClientVM { get; set; }
        private AddEdit_EmployeeVM _addEdit_EmployeeVM { get; set; }
        private AddEdit_OrderVM _addEdit_OrderVM { get; set; }
        private AddEdit_WorkVM _addEdit_WorkVM { get; set; }
        private ClientsVM _clientsVM { get; set; }
        private ExportVM _exportVM { get; set; }
        private JobsVM _jobsVM { get; set; }
        private OrdersVM _ordersVM { get; set; }
        private EmployeeVM _employeeVM { get; set; }
        private SettingsVM _settingsVM { get; set; }
        #endregion


        #region Commands
        public ICommand OrderCmnd { get; set; }
        public ICommand EmployeeCmnd { get; set; }
        public ICommand ClientsCmnd { get; set; }
        public ICommand WorkCmnd { get; set; }
        public ICommand ReportCmnd { get; set; }
        public ICommand SettingsCmnd { get; set; }
        public ICommand CloseWindowCmnd { get; set; }
        #endregion
        private EntityContext Entity { get; set; }
        public DapperContext dp { get; set; }

        public event Action CloseWindowEvenet;

        public BaseVM CurentVM { get; set; }
        public string CompName { get; set; }

        public Employee UserEmployee { get; set; }
        public bool SigInUser { get; set; } = false;


        public MenuVM()
        {

            OrderCmnd = new RelayCommand(OrderCmdMethod);
            EmployeeCmnd = new RelayCommand(EmployeeCmndMethod);
            ClientsCmnd = new RelayCommand(ClientsCmdMethod);
            WorkCmnd = new RelayCommand(WorkCmdMethod);
            ReportCmnd = new RelayCommand(ReportCmdMethod);
            SettingsCmnd = new RelayCommand(SettingsCmdMethod);
            CloseWindowCmnd = new RelayCommand(CloseWindowCmndMethod);

            Entity = new EntityContext();
            dp = new DapperContext();

            _addEdit_ClientVM = new AddEdit_ClientVM();
            _addEdit_EmployeeVM = new AddEdit_EmployeeVM();
            _addEdit_OrderVM = new AddEdit_OrderVM(Entity, dp);
            _addEdit_WorkVM = new AddEdit_WorkVM();
            _clientsVM = new ClientsVM(dp,Entity);
            _exportVM = new ExportVM();
            _jobsVM = new JobsVM(dp,Entity);
            _ordersVM = new OrdersVM(dp,Entity);
            _employeeVM = new EmployeeVM(dp,Entity);
            _settingsVM = new SettingsVM();

            _jobsVM.AddEditWorkEvent += _jobsVM_AddEditWorkEvent;
            _addEdit_WorkVM.SaveCancelEvent += _addEdit_WorkVM_SaveCancelCmnd;

            _clientsVM.AddEditClientsEvent += _clientsVM_AddEditClientsEvent;
            _addEdit_ClientVM.SaveCancelEvent += _addEdit_ClientVM_SaveCancelCmnd;

            _ordersVM.AddEditOrderEvent += _ordersVM_AddEditOrderEvent;
            _addEdit_OrderVM.SaveCancelCmnd += _addEdit_OrderVM_SaveCancelCmnd;

            _employeeVM.AddEditEmployeesEvent += _employeeVM_AddEditEmployeesEvent;
            _addEdit_EmployeeVM.SaveCancelEvent += _addEdit_EmployeeVM_SaveCancelCmnd;

            _settingsVM.ChangCompNameEvent += _settingsVM_ChangCompNameEvent;

            CompName = ApplicationSettings.CompanyName;

        }
        #region AddEdit Client Or Clients
        private void ClientsCmdMethod()
        {
            CurentVM = _clientsVM;
        }
        private void _clientsVM_AddEditClientsEvent(bool obj)
        {
            _addEdit_ClientVM.EditMode = obj;
            if (obj)
            {
                _addEdit_ClientVM.client = _clientsVM.SelectedClients;             
            }
            else
            {
                _addEdit_ClientVM.client = new Client();
            }
            CurentVM = _addEdit_ClientVM;
        }
        private void _addEdit_ClientVM_SaveCancelCmnd(bool obj, Client client)
        {
            if (obj)
            {
                if (_addEdit_ClientVM.EditMode)
                {
                    Entity.Clients.Update(client);
                }
                else
                {
                    Entity.Clients.Add(client);
                }
                Entity.SaveChanges();
            }
            _clientsVM.Load();
            _addEdit_ClientVM.EditMode = false;
            CurentVM = _clientsVM;
        }
        #endregion

        #region Export
        private void ReportCmdMethod()
        {
            if (SigInUser)
            {
                _exportVM.Orders = UserEmployee.Orders;
            }
            else
            {
                _exportVM.Orders = dp.GetOrders();
            }
            CurentVM = _exportVM;
        }
        #endregion

        #region AddEdit Jobs or Jobs

        private void WorkCmdMethod()
        {
            CurentVM = _jobsVM;
        }
        private void _jobsVM_AddEditWorkEvent(bool obj)
        {
            _addEdit_WorkVM.EditMode = obj;
            if (obj)
            {
                _addEdit_WorkVM.job = _jobsVM.SelectedJob;
            }
            else
            {
                _addEdit_WorkVM.job = new Job();
            }
            CurentVM = _addEdit_WorkVM;
        }
        private void _addEdit_WorkVM_SaveCancelCmnd(bool obj, Job job)
        {
            if (obj)
            {
                if (_addEdit_WorkVM.EditMode)
                {
                    Entity.Jobs.Update(job);
                }
                else
                {
                    Entity.Jobs.Add(job);
                }
                Entity.SaveChanges();
            }
            _jobsVM.Load();
            _addEdit_WorkVM.EditMode = false;
            CurentVM = _jobsVM;
        }

        #endregion

        #region AddEdit Employee or Employees

        private void EmployeeCmndMethod()
        {
            if (SigInUser)
            {
                _addEdit_EmployeeVM.employee = UserEmployee;
                CurentVM = _addEdit_EmployeeVM;
            }
            else
            {
                CurentVM = _employeeVM;
            }
        }
        private void _employeeVM_AddEditEmployeesEvent(bool obj)
        {
            _addEdit_EmployeeVM.EditMode = obj;
            if (obj)
            {
                _addEdit_EmployeeVM.employee = _employeeVM.SelectedEmployee;
            }
            else
            {
                _addEdit_EmployeeVM.employee = new Employee();
            }
            CurentVM = _addEdit_EmployeeVM;
        }
        private void _addEdit_EmployeeVM_SaveCancelCmnd(bool obj, Employee employee)
        {
            if (SigInUser)
            {
                if (obj)
                {
                    Entity.Employees.Update(employee);
                }
                Entity.SaveChanges();
            }
            else
            {
                if (obj)
                {
                    if (_addEdit_EmployeeVM.EditMode)
                    {
                        Entity.Employees.Update(employee);
                    }
                    else
                    {
                        Entity.Employees.Add(employee);
                    }
                    Entity.SaveChanges();
                }
                _employeeVM.Load();
                _addEdit_EmployeeVM.EditMode = false;
                CurentVM = _employeeVM;
            }
        }

        #endregion

        #region AddEdit Order or Orders

        private void OrderCmdMethod()
        {
            if (SigInUser)
            {
                _ordersVM.Orders = UserEmployee.Orders;
                _ordersVM.UserEmployee = UserEmployee;
                _ordersVM.SigInUser = SigInUser;
            }
            CurentVM = _ordersVM;
        }
        private void _ordersVM_AddEditOrderEvent(bool obj)
        {
            _addEdit_OrderVM.EditMode = obj;
            if (obj)
            {
                _addEdit_OrderVM.order = _ordersVM.SelectedOrder;
            }
            else
            {
                _addEdit_OrderVM.order = new Order();
            }
            if (SigInUser)
            {
                _addEdit_OrderVM.SigInUser = SigInUser;
                _addEdit_OrderVM.UserEmployee = UserEmployee;
            }
            _addEdit_OrderVM.Load();
            CurentVM = _addEdit_OrderVM;
        }
        private void _addEdit_OrderVM_SaveCancelCmnd(bool obj, Order order)
        {
            if (obj)
            {
                if (_addEdit_OrderVM.EditMode)
                {
                    Entity.Orders.Update(order);
                }
                else
                {
                    Entity.Orders.Add(order);
                    //if (SigInUser)
                    //{
                    //    _ordersVM.UserEmployee.Orders.Add(order);
                    //}
                }

                Entity.SaveChanges();
                //UserEmployee = _ordersVM.UserEmployee;
                _ordersVM.Load();
                _addEdit_OrderVM.EditMode = false;
            }
            CurentVM = _ordersVM;
        }

        #endregion

        #region Settings

        private void _settingsVM_ChangCompNameEvent()
        {
            CompName = ApplicationSettings.CompanyName;
        }
        private void SettingsCmdMethod()
        {
            if (SigInUser)
            {
                MessageBox.Show("Not allowed", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _settingsVM.CompanyName = ApplicationSettings.CompanyName;
                _settingsVM.Note1 = ApplicationSettings.Note1;
                _settingsVM.Note2 = ApplicationSettings.Note2;
                CurentVM = _settingsVM;
            }
        }

        #endregion


        private void CloseWindowCmndMethod()
        {
            CloseWindowEvenet?.Invoke();
        }
    }
}
