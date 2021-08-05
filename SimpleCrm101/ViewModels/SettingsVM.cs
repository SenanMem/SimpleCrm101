using GalaSoft.MvvmLight.CommandWpf;
using PropertyChanged;
using SimpleCrm101.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class SettingsVM : BaseVM
    {
        public ICommand DeveloperCmnd { get; set; }
        public ICommand SaveCmnd { get; set; }

        public string  CompanyName { get; set; }
        public string  Note1 { get; set; }
        public string  Note2 { get; set; }

        public event Action ChangCompNameEvent;

        public SettingsVM()
        {
            DeveloperCmnd = new RelayCommand(DeveleoperMethod);
            SaveCmnd = new RelayCommand(SaveMethod);
        }

        private void SaveMethod()
        {
            ApplicationSettings.CompanyName = CompanyName;
            ApplicationSettings.Note1 = Note1;
            ApplicationSettings.Note2 = Note2;
            ChangCompNameEvent?.Invoke();
        }

        private void DeveleoperMethod()
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = "https://github.com/SenanMem";
            myProcess.Start();
        }
    }
}
