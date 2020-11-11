using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Configuration;

namespace WebJob1
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main()
        {
            var queue = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageAccount"].ConnectionString)
                .CreateCloudQueueClient()
                .GetQueueReference("stockchecks");

            queue.CreateIfNotExists();

            string continueLoop = String.Empty;

            // This while loop sends a new message every 5 seconds
            for (int x = 0; x < 100; x++)
            {
                var timestamp = DateTimeOffset.UtcNow.ToString("s");

                // Logging New Cloud Message
                var message = new CloudQueueMessage($"Stock check at {timestamp} completed");

                queue.AddMessage(message);

                Thread.Sleep(TimeSpan.FromSeconds(5));
            }

            //var config = new JobHostConfiguration();

            //if (config.IsDevelopment)
            //{
            //    config.UseDevelopmentSettings();
            //}

            //var host = new JobHost(config);
            //// The following code ensures that the WebJob will be running continuously
            //host.RunAndBlock();
        }
    }
}
