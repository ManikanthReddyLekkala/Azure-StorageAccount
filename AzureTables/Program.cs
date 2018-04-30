using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureTables.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTables
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("customer");
            table.CreateIfNotExists();

            //TableBatchOperation batch = new TableBatchOperation();
            //var customer1 = new CustomerUS("Mike", "mike@mike.mike");
            //var customer2 = new CustomerUS("Jane", "jane@jane.joe");
            //var customer3 = new CustomerUS("Joe", "joe@joe.joe");
            //batch.Insert(customer1);
            //batch.Insert(customer2);
            //batch.Insert(customer3);
            //table.ExecuteBatch(batch);



            //CreateCustomer(table, new CustomerUS("Mike","mike@localhost.local"));

            //GetCustomer(table,"US","mike@mike.mike");
            
            //var david = GetCustomer(table, "US", "mike@localhost.local");
            //david.Name = "David";
            //UpdateCustomer(table,david);
            Console.WriteLine(GetCustomer(table,"US","jane@jane.joe").Name);
            
            //var customer = GetCustomer(table, "US", "mike@localhost.local");
            //DeleteCustomer(table,customer);


            Console.Read();
        }
        static void DeleteCustomer(CloudTable table, CustomerUS customer)
        {
            TableOperation delete = TableOperation.Delete(customer);
            table.Execute(delete);
            Console.WriteLine("Customer deleted successfully");
        }
        static void UpdateCustomer(CloudTable table, CustomerUS customer)
        {
            TableOperation update = TableOperation.Replace(customer);
            table.Execute(update);
        }
        static void CreateCustomer(CloudTable table, CustomerUS customer)
        {
            TableOperation insert = TableOperation.Insert(customer);
            table.Execute(insert);
        }
        static CustomerUS GetCustomer(CloudTable table, string partitionKey, String rowKey)
        {
            TableOperation retrieve = TableOperation.Retrieve<CustomerUS>(partitionKey, rowKey);
            var result = table.Execute(retrieve);
            return (CustomerUS)result.Result;
        }
    }
}
