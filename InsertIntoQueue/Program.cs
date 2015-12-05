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
       

        static void Main(string[] args)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("tweetlyst");
            queue.CreateIfNotExists();
            for (int i = 0; i <= 10; i++)
            {
                AddToQueue();
            }
        }

        static  void AddToQueue()
        {
            // create TaaS ticket request
            var tweetQueueEntry = new TweetQueueSetter()
            {
                PartitionKey = "1",
                UserId = "A",
                RowKey ="B",
                AccessKey ="Akey",
                AccessToken = "Atoken",
                SearchTerms ="terms",
                ControlAction = "start",
                Time = DateTime.Now
            };

            // convert object to json string
            var ticketJson =  JsonConvert.SerializeObject(tweetQueueEntry);
           //var ticketJson =  Task.Factory.StartNew(() => JsonConvert.SerializeObject(tweetQueueEntry));

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("tweetlyst");

            // send message to queue
            var message = new CloudQueueMessage(ticketJson);
             queue.AddMessageAsync(message);
        }
    }
}
