using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Queue;

namespace InsertIntoQueue
{
    class Program
    {
        static string _queueName = "starttweetstream";

        static void Main(string[] args)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(_queueName);
            queue.CreateIfNotExists();
           // for (int i = 0; i <= 5; i++) no need for loop - just insert one message at a time.
            
                AddToQueue();
            
        }

        static  void AddToQueue()
        {
            string tableName;
            string searchTerm;
            Console.Write("Enter table name: ");
            tableName= Console.ReadLine();
            Console.Write("Enter Search Term: ");
           searchTerm = Console.ReadLine();


            var tweetQueueEntry = new TweetQueueSetter()
            {
                TableName =tableName,
                PartitionKey = "A",
                UserId = "userId",
                RowKey ="B",
                AccessKey ="Akey",
                AccessToken = "Atoken",
                SearchTerm =searchTerm,
                ControlAction = "start",
                Time = DateTime.Now
            };

            // convert object to json string
            var ticketJson =  JsonConvert.SerializeObject(tweetQueueEntry);
           //var ticketJson =  Task.Factory.StartNew(() => JsonConvert.SerializeObject(tweetQueueEntry));

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference(_queueName);

            // send message to queue
            var message = new CloudQueueMessage(ticketJson);
             queue.AddMessage(message);
            Console.WriteLine("Added to queue");
        }
    }
}
