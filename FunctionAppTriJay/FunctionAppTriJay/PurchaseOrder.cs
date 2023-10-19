using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FunctionAppTriJay
{
    public static class PurchaseOrder
    {
        //Orchestrator function
        [FunctionName("SubmitPurchaseOrder")]
        public static async Task<List<string>> SubmitPurchaseOrder(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>(nameof(RequestApproval), "P01"));
            outputs.Add(await context.CallActivityAsync<string>(nameof(CompletePurchaseOrder), "P02"));
            //outputs.Add(await context.CallActivityAsync<string>(nameof(SayHello), "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }


        //One more Activity fucntion
        [FunctionName(nameof(CompletePurchaseOrder))]
        public static string CompletePurchaseOrder([ActivityTrigger] string purchaseOrderId, ILogger log)
        {
            log.LogInformation($"Completing purchas order : {purchaseOrderId} ");
            return purchaseOrderId;
        }

        //One more Activity fucntion
        [FunctionName(nameof(RequestApproval))]
        public static string RequestApproval([ActivityTrigger] string purchaseOrderId,ILogger log)
        {
            log.LogInformation($"Requesting approcval of purchase order : {purchaseOrderId} ");
            return purchaseOrderId;
        }

        ////Activity function
        //[FunctionName(nameof(SayHello))]
        //public static string SayHello([ActivityTrigger] string name, ILogger log)
        //{
        //    log.LogInformation("Saying hello to {name}.", name);
        //    return $"Hello {name}!";
        //}


        //Custom : One more Http start function
        //HTTP Starter function
        [FunctionName("ApprovePurchaseOrder")]
        public static async Task<HttpResponseMessage> ApprovePurchaseOrder(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("SubmitPurchaseOrder",null);

            log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);
            log.LogInformation("Purchase Order approval completed ");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }

        ////HTTP Starter function
        //[FunctionName("Function1_HttpStart")]
        //public static async Task<HttpResponseMessage> HttpStart(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
        //    [DurableClient] IDurableOrchestrationClient starter,
        //    ILogger log)
        //{
        //    // Function input comes from the request content.
        //    string instanceId = await starter.StartNewAsync("Function1", null);

        //    log.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

        //    return starter.CreateCheckStatusResponse(req, instanceId);
        //}
    }
}