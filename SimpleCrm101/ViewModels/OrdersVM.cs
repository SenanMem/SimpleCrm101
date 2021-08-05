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
    public class OrdersVM : BaseVM
    {
        public EntityContext Entity { get; set; }
        public DapperContext dp { get; set; }

        public List<Order> Orders { get; set; }
        public Order SelectedOrder { get; set; }

        public bool SigInUser { get; set; }
        public Employee UserEmployee { get; set; }
        public ICommand AddOrderCmnd { get; set; }
        public ICommand EditOrderCmnd { get; set; }
        public ICommand DeleteOrderCmnd { get; set; }
        public ICommand UnfinishedOrderCmnd { get; set; }

        public bool ShowOnlyUnfinishedProjects { get; set; }

        public event Action<bool> AddEditOrderEvent;
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

        public OrdersVM(DapperContext dapper, EntityContext entity)
        {
            dp = dapper;
            Entity = entity;
            Load();
            AddOrderCmnd = new RelayCommand(AddOrderMethod);
            EditOrderCmnd = new RelayCommand(EditOrderMethod);
            DeleteOrderCmnd = new RelayCommand(DeleteOrderMethod);
            UnfinishedOrderCmnd = new RelayCommand(UnfinishedOrderMethod);
            SearchEvent += OrdersVM_SearchEvent;
        }

        private void UnfinishedOrderMethod()
        {
            if (ShowOnlyUnfinishedProjects)
            {
                Orders = Orders.Where(c => c.JobStatus!="End").ToList();
            }
            else
            {
                Load();
            }
        }

        private void OrdersVM_SearchEvent()
        {
            if (searchText == string.Empty) Load();
            else
            {
                List<Order> orders1;
                List<Order> orders;
                if (SigInUser)
                {
                    orders1 = UserEmployee.Orders;
                }
                else
                {
                    orders1 = dp.GetOrders();
                }
                orders = orders1.Where(c => c.JobTemplate.Title.Contains(SearchText)||c.client.LastName.Contains(SearchText)).ToList();
                Orders = orders;
            }
        }

        private void DeleteOrderMethod()
        {
            Entity.Orders.Remove(SelectedOrder);
            Entity.SaveChanges();
            if (SigInUser)
            {
                UserEmployee.Orders.Remove(SelectedOrder);
            }
            Load();
        }

        public void Load()
        {
            if (SigInUser)
            {
                Orders = UserEmployee.Orders;
            }
            else
            {
                Orders = dp.GetOrders();
            }
        }

        private void EditOrderMethod()
        {
            AddEditOrderEvent?.Invoke(true);
        }

        private void AddOrderMethod()
        {
            AddEditOrderEvent?.Invoke(false);
        }
    }
}
