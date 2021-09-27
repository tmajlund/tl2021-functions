using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzNan.MessageReader
{
    public static class MessageReader
    {
        [Function("MessageReader")]
        public static void Run([QueueTrigger("votings", Connection = "AzureWebJobsStorage")] string myQueueItem,
            FunctionContext context)
        {
            var logger = context.GetLogger("MessageReader");
            logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
