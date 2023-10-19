using Azure.Messaging.ServiceBus;

namespace PublisherApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient("Endpoint=sb://servbusjayanth.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKyiUGne+px0NPsN2mt700mc6AWw+rD08+ASbJgup2U=");
            var receiver = serviceBusClient.CreateReceiver("productstopic", "cheappurchases");


            //while(true)
            //{
            //    var messg= receiver.ReceiveMessagesAsync();
            //    Console.WriteLine($"{messg.ApplicationProperties["ProductId"]} {messg.ApplicationProperties["ProductName"]} {messg.ApplicationProperties["Price"]}");
            //}

            while (true)
            {
                var msg = await receiver.ReceiveMessageAsync();
                Console.WriteLine($"{msg.ApplicationProperties["ProductId"]} {msg.ApplicationProperties["ProductName"]} {msg.ApplicationProperties["Price"]}");
                Console.ReadKey();
            }

           


        }
    }
}