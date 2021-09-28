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
            logger.LogInformation($"C# Queue trigger function processed: {vote}");

            return new TableData
            {
                PartitionKey = vote.VoteId,
                RowKey = $"{(DateTimeOffset.MaxValue.Ticks-vote.Timestamp.Ticks):d10}-{Guid.NewGuid():N}",
                Vote = vote.Answer
            };
        }
    }

    public class TableData
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Vote { get; set; }
    }

    public class NewVote
    {
        public string VoteId { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Vote { get; set; }
    }
}