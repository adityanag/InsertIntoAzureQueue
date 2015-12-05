using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;
using System.Configuration;
using InsertIntoQueue;

namespace ReadFromQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("tweetlyst");
            var messages =  queue.GetMessages(32, TimeSpan.FromMinutes(2), null, null);
            
            foreach (var message in messages)
            {
                try
                {
                    // if processing was not possible, delete the messagecheck for unprocessable messages
                    if (message.DequeueCount < 5)
        {
                        var ticketRequest = JsonConvert.DeserializeObject<TweetQueueSetter>(message.AsString);
                        // process the ticket request (expensive operation)
                        Console.WriteLine(ticketRequest);
                    }
        else
        {
                       Console.WriteLine("Processing failed.");
                    }
                    // delete message so that it becomes invisible for other workers
                    queue.DeleteMessageAsync(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine("message", e.Message);
                }
            }

        }
    }
}
