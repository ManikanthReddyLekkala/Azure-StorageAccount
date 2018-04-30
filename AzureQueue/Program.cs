using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace AzureQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storage = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnection"));
            CloudQueueClient queueClient = storage.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("tasks");
            queue.CreateIfNotExists();

            //CloudQueueMessage message = new CloudQueueMessage("Hello World");
            //queue.AddMessage(message);

            //CloudQueueMessage message = queue.PeekMessage();
            //Console.WriteLine(message.AsString);

            CloudQueueMessage message = queue.GetMessage();
            Console.WriteLine(message.AsString);
            queue.DeleteMessage(message);
           




            Console.ReadLine();
        }
    }
}
