using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.StepByStep.Sample.Subscriber
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "NServiceBus.StepByStep.Sample.Subscriber";
            var endpointConfiguration = new EndpointConfiguration("NServiceBus.StepByStep.Sample.Subscriber");
            endpointConfiguration.UseSerialization<XmlSerializer>();
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            try
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                await endpointInstance.Stop()
                    .ConfigureAwait(false);
            }
        }
    }
}
