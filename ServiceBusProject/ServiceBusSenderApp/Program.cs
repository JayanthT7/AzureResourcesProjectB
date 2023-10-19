using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;

namespace ServiceBusSenderApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ServiceBusAdministrationClient serviceBusAdministrationClient = new ServiceBusAdministrationClient("Endpoint=sb://servbusjayanth.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKyiUGne+px0NPsN2mt700mc6AWw+rD08+ASbJgup2U=");

            ServiceBusClient serviceBusClient = new ServiceBusClient("Endpoint=sb://servbusjayanth.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKyiUGne+px0NPsN2mt700mc6AWw+rD08+ASbJgup2U=");

            //just creating queue 
            if (!await serviceBusAdministrationClient.QueueExistsAsync("sendermyqueue"))
            {
                await serviceBusAdministrationClient.CreateQueueAsync("sendermyqueue");
            }

           

            ServiceBusSender serviceBusSender = serviceBusClient.CreateSender("sendermyqueue");

            //to add single message
            //await serviceBusSender.SendMessageAsync(new ServiceBusMessage("This is single messgae from local!"));


            //ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
            //serviceBusMessageBatch.TryAddMessage(new ServiceBusMessage("Hey! this is mess coming from locl! - 1"));
            //serviceBusMessageBatch.TryAddMessage(new ServiceBusMessage("Hey! this is mess coming from locl!- 2"));
            //serviceBusMessageBatch.TryAddMessage(new ServiceBusMessage("Hey! this is mess coming from locl! - 3"));

            //await serviceBusSender.SendMessageAsync(serviceBusMessageBatch);



            ServiceBusSender sender = serviceBusClient.CreateSender("sendermyqueue");
            ServiceBusMessageBatch batch = await sender.CreateMessageBatchAsync();

            batch.TryAddMessage(new ServiceBusMessage("This is Message 1"));
            batch.TryAddMessage(new ServiceBusMessage("This is Message 2"));
            batch.TryAddMessage(new ServiceBusMessage("This is Message 3"));

            await sender.SendMessagesAsync(batch);








        }
    }
}