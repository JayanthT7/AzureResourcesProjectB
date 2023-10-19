

using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using System.Security.Cryptography;

namespace PublisherApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient("Endpoint=sb://servbusjayanth.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKyiUGne+px0NPsN2mt700mc6AWw+rD08+ASbJgup2U=");

            ServiceBusAdministrationClient serviceBusAdministrationClient = new ServiceBusAdministrationClient("Endpoint=sb://servbusjayanth.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WKyiUGne+px0NPsN2mt700mc6AWw+rD08+ASbJgup2U=");

            if(!await serviceBusAdministrationClient.TopicExistsAsync("productstopic"))
            {
                await serviceBusAdministrationClient.CreateTopicAsync("productstopic");    
            }

            if (!await serviceBusAdministrationClient.SubscriptionExistsAsync("productstopic","cheappurchases"))
            {
               var options= new CreateSubscriptionOptions("productstopic", "cheappurchases");
                SqlRuleFilter sqlRuleFilter = new SqlRuleFilter("Price<2000");
                var ruleOptions = new CreateRuleOptions("PriceRule", sqlRuleFilter);
                await serviceBusAdministrationClient.CreateSubscriptionAsync(options, ruleOptions);

            }

            if (!await serviceBusAdministrationClient.SubscriptionExistsAsync("productstopic", "expensivepurchases"))
            {
                var options = new CreateSubscriptionOptions("productstopic", "expensivepurchases");
                SqlRuleFilter sqlRuleFilter = new SqlRuleFilter("Price>2000");
                var ruleOptions = new CreateRuleOptions("PriceRule", sqlRuleFilter);
                await serviceBusAdministrationClient.CreateSubscriptionAsync(options, ruleOptions);

            }


            var laptopMessages = new ServiceBusMessage();
            laptopMessages.ApplicationProperties["ProductId"] = 1;
            laptopMessages.ApplicationProperties["ProductName"] = "laptop";
            laptopMessages.ApplicationProperties["Price"] = 65000;

            var pendriveMessages = new ServiceBusMessage();
            pendriveMessages.ApplicationProperties["ProductId"] = 2;
            pendriveMessages.ApplicationProperties["ProductName"] = "pendrive";
            pendriveMessages.ApplicationProperties["Price"] = 1000;


            var sender = serviceBusClient.CreateSender("productstopic");
            await sender.SendMessageAsync(laptopMessages);
            await sender.SendMessageAsync(pendriveMessages);

        }
    }
}
