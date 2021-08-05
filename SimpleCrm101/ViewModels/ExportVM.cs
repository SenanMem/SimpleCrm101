using GalaSoft.MvvmLight.Command;
using Ganss.Excel;
using PropertyChanged;
using SimpleCrm101.Models;
using SimpleCrm101.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SimpleCrm101.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ExportVM : BaseVM
    {
        public ICommand ExportCmnd { get; set; }
        public bool EveryCustomerOnANewPage { get; set; } = true;

        public DapperContext dp { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
        public List<OrderString> OrderStrings { get; set; } = new List<OrderString>();

        public static int FileCount = 0;
        public ExportVM()
        {
            OrderStrings = new List<OrderString>();
            ExportCmnd = new RelayCommand(ExportMethod);


        }
        public void Load()
        {
            foreach (var o in Orders)
            {
                OrderStrings.Add(new OrderString
                {
                    clientName = o.client.FirstName,
                    JobTitle = o.JobTemplate.Title,
                    JobPrice = o.JobTemplate.StandartPrice,
                    employeeName = o.employee.Name,
                    JobStatus = o.JobStatus,
                    task = o.task.Assignment,
                    Price = o.Price,
                    Time = o.Time.ToString(),
                    OrderHasBeenPaid = o.OrderHasBeenPaid.ToString()
                });
            }
        }
        private void ExportMethod()
        {
            Load();

            ExcelMapper mapper = new ExcelMapper();
            string newFile;
            if (EveryCustomerOnANewPage)
            {
                FileCount++; 
            newFile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{ApplicationSettings.CompanyName}NewReport{FileCount.ToString()}.xlsx";
            }
            else
            {
            newFile = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{ApplicationSettings.CompanyName}.xlsx";
            }
            mapper.Save(newFile, OrderStrings, "SheetName", true);
            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}";
            MessageBox.Show($"The reports were successfully saved in the following path::\n{path}", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
    public class OrderString
    {
        public string clientName { get; set; }
        public string JobTitle { get; set; }
        public double? JobPrice { get; set; }
        public string employeeName { get; set; }
        public string JobStatus { get; set; }
        public string task { get; set; }
        public double? Price { get; set; }
        public string Time { get; set; }
        public string OrderHasBeenPaid { get; set; }
    }
}
