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
    public class ClientsVM : BaseVM
    {
        public EntityContext Entity { get; set; }
        public DapperContext dp { get; set; }

        public List<Client> Clients { get; set; }
        public Client SelectedClients { get; set; }
        public ICommand AddClientsCmnd { get; set; }
        public ICommand EditClientsCmnd { get; set; }
        public ICommand DeleteClientsCmnd { get; set; }

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

        public event Action<bool> AddEditClientsEvent;
        public event Action SearchEvent;
        public ClientsVM(DapperContext dapper, EntityContext entity)
        {
            dp = dapper;
            Entity = entity;
            Load();
            AddClientsCmnd = new RelayCommand(AddClientMethod);
            EditClientsCmnd = new RelayCommand(EditClientMethod);
            DeleteClientsCmnd = new RelayCommand(DeleteClientMethod);
            SearchEvent += ClientsVM_SearchEvent;
        }

        private void ClientsVM_SearchEvent()
        {
            if (searchText == string.Empty) Load();
            else
            {
                List<Client> clients1 = dp.GetClients();
                List<Client> clients = clients1.Where(c => c.LastName.Contains(SearchText)).ToList();
                Clients = clients;
            }
        }
        private void DeleteClientMethod()
        {
            Entity.Clients.Remove(SelectedClients);
            Entity.SaveChanges();
            Load();
        }

        private void EditClientMethod()
        {
            AddEditClientsEvent?.Invoke(true);
        }

        private void AddClientMethod()
        {
            AddEditClientsEvent?.Invoke(false);
        }

        public void Load()
        {
                Clients = dp.GetClients();
        }
        
    }
}
