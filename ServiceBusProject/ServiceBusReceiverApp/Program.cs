using Azure.Messaging.ServiceBus;
using System.Diagnostics;
using System.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        ServiceBusClient serviceBusClient = new ServiceBusClient("Endpoint=sb://servbusjayanth.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKyiUGne+px0NPsN2mt700mc6AWw+rD08+ASbJgup2U=");

        ServiceBusReceiver serviceBusReceiver = serviceBusClient.CreateReceiver("sendermyqueue");

        //while(true)
        //{
        //    var mssg = await serviceBusReceiver.ReceiveMessageAsync();
        //    Console.WriteLine(mssg.Body.ToString());
        //}

        ServiceBusProcessor processor = serviceBusClient.CreateProcessor("sendermyqueue", new ServiceBusProcessorOptions
        {
            ReceiveMode = ServiceBusReceiveMode.PeekLock
        });

        // Register a message handler to process messages from queue
        processor.ProcessMessageAsync += async args =>
        {
            string message = Encoding.UTF8.GetString(args.Message.Body);
            Console.WriteLine(message);
            //await args.CompleteMessageAsync(args.Message);
        };

        // Register an error handler to handle errors while processing queue
        processor.ProcessErrorAsync += args =>
        {
            Console.WriteLine(args.Exception);
            return Task.CompletedTask;
        };

        // Start processing queue
        await processor.StartProcessingAsync();

        Console.WriteLine("Press any key to stop receiving messages");
        Console.ReadKey();

        await processor.StopProcessingAsync();



    }
}