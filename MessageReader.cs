using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzNan.MessageReader
{
    public static class MessageReader
    {
        [Function("MessageReader")]
        [TableOutput("Votings", Connection = "AzureWebJobsStorage")]
        public static TableData Run([QueueTrigger("votings", Connection = "AzureWebJobsStorage")] NewVote vote,
            FunctionContext context)
        {
            var logger = context.GetLogger("MessageReader");
            logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }

    public class TableData
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string UserInfo { get; set; }
        public string Vote { get; set; }
    }
}

//NewVote is a class not created yet. Need to understand the communication between api in swa-client and function-app