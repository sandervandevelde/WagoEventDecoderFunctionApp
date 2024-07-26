// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;
using Azure.Messaging;
using System.Text;
using Azure.Messaging.EventHubs.Producer;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;

namespace WagoEventDecoderFunctionApp
{
    public static class Functions
    {
        // Connect to the Custom App
        private static EventHubProducerClient producerClient 
            = new EventHubProducerClient(Environment.GetEnvironmentVariable("CustomAppEndpoint"));

        [FunctionName("CloudEventTriggerFunction")]
        public static async Task Run([EventGridTrigger] CloudEvent e, ILogger log)
        {
            var json = Encoding.Default.GetString(e.Data);

            var attribs = string.Empty;

            foreach (var a in e.ExtensionAttributes)
            {
                attribs = attribs + $"{a.Key}={a.Value}; ";
            }

            log.LogInformation($"EventGrid received Topic Event: source:'{e.Source}' type:'{e.Type}' subject:'{e.Subject}'");
            log.LogInformation($"EventGrid received attribs: '{attribs}'");
            log.LogInformation($"EventGrid received JSON: '{json}'");

            try
            {
                // Create a batch of events 
                using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

                // Add json message
                if (!eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(json))))
                {
                    log.LogWarning("Batch did not accept JSON");
                }

                await producerClient.SendAsync(eventBatch);

                log.LogInformation("JSON sent to Custom App");

            }
            catch (Exception ex)
            {
                log.LogError($"Exception while sending JSON to Custom App: '{ex.Message}'");
            }
        }
    }
}
