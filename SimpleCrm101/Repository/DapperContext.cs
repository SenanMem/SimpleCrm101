using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SimpleCrm101.Models;
using System.Windows;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace SimpleCrm101.Repository
{
    public class DapperContext
    {
        public SqlConnection Connection { get; set; }
        public DapperContext(string ConnectionString="")
        {
            if (ConnectionString == string.Empty)
            {

                var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName).AddJsonFile("AppSetting.json");
                var config = configuration.Build();
                ConnectionString = config.GetConnectionString("DefaultConnection");
            }
            Connection = new SqlConnection();
            Connection.ConnectionString = ConnectionString;
        }
        private void Open()
        {
            Connection.Open();
        }
        private void Close()
        {
            Connection.Close();
        }
        public List<Client> GetClients(string SqlCommand = @"SELECT * FROM Clients c LEFT OUTER JOIN Jobs j ON j.ClientId=c.Id")
        {
            this.Open();
            List<Client> Clients = new List<Client>() { new Client() { Id = -1 } };
            Connection.Query<Client, Job, Client>(SqlCommand, (client, job) =>
            {
                if (Clients.Last().Id == client.Id)
                {
                    Clients.Last().Jobs.Add(job);
                }
                else
                {
                    if (job != null)
                        client.Jobs.Add(job);
                    Clients.Add(client);
                }
                return client;
            });
            Clients.RemoveAt(0);
            this.Close();
            return Clients;
        }
        public List<Employee> GetEmployee(string SqlCommand = @"SELECT * FROM Employees e LEFT OUTER JOIN (Orders o INNER JOIN Clients c ON o.clientId=c.Id INNER JOIN Jobs j ON o.JobTemplateId=j.Id INNER JOIN Task t ON o.taskId=t.Id)ON  o.EmployeeId=e.Id")
        {

            this.Open();
            List<Employee> Employees = new List<Employee>() { new Employee { Id = -1 } };
            Connection.Query<Employee, Order, Client, Job, Models.Task, Employee>(SqlCommand, (employee, order, client, job, task) =>
            {
                if (job == null && client == null && task == null)
                {
                    order = null;
                }
                else
                {
                    order.JobTemplate = job;
                    order.task = task;
                    order.client = client;
                    order.employee = employee;
                }
                if (Employees.Last().Id == employee.Id)
                {
                    Employees.Last().Orders.Add(order);
                }
                else
                {
                    if (order != null)
                        employee.Orders.Add(order);
                    Employees.Add(employee);
                }
                return employee;
            });
            Employees.RemoveAt(0);
            this.Close();
            return Employees;
        }
        public List<Job> GetJob(string SqlCommand = "SELECT * FROM Jobs")
        {
            this.Open();
            var Jobs = Connection.Query<Job>(SqlCommand).ToList();
            this.Close();
            return Jobs;
        }
        public List<Order> GetOrders()
        {
            var employees = GetEmployee();
            List<Order> Orders = new List<Order>();
            foreach (var item in employees)
            {
                if (item.Orders != null)
                {
                    foreach (var order in item.Orders)
                    {
                        Orders.Add(order);
                    }
                }
            }
            return Orders;
        }

    }
}
