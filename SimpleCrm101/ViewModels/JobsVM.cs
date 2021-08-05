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
    public class JobsVM : BaseVM
    {
        public EntityContext Entity { get; set; }
        public DapperContext dp { get; set; }

        public List<Job> Jobs { get; set; }
        public Job SelectedJob { get; set; }

        public ICommand AddWorkCmnd { get; set; }
        public ICommand EditWorkCmnd { get; set; }
        public ICommand DeleteWorkCmnd { get; set; }


        public event Action<bool> AddEditWorkEvent;
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
        public JobsVM(DapperContext dapper, EntityContext entity)
        {
            dp = dapper;
            Entity = entity;
            Load();
            AddWorkCmnd = new RelayCommand(AddWorkMethod);
            EditWorkCmnd = new RelayCommand(EditWorkMethod);
            DeleteWorkCmnd = new RelayCommand(DeleteWorkMethod);
            SearchEvent += JobsVM_SearchEvent;
        }

        private void JobsVM_SearchEvent()
        {
            if (searchText == string.Empty) Load();
            else
            {
                List<Job> jobs1 = dp.GetJob();
                List<Job> jobs = jobs1.Where(e => e.Title.Contains(SearchText)).ToList();
                Jobs = jobs;
            }
        }

        private void DeleteWorkMethod()
        {
            Entity.Jobs.Remove(SelectedJob);
            Entity.SaveChanges();
            Load();
        }

        public void Load()
        {
            Jobs = dp.GetJob();
        }

        private void EditWorkMethod()
        {
            AddEditWorkEvent?.Invoke(true);
        }

        private void AddWorkMethod()
        {
            AddEditWorkEvent?.Invoke(false);
        }
    }
}
