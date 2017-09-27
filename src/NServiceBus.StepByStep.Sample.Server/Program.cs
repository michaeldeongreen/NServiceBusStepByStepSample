using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.StepByStep.Sample.Server
{
    class Program
    {
        static async Task Main()
        {
            Console.Title = "NServiceBus.StepByStep.Sample.Server";
            var endpointConfiguration = new EndpointConfiguration("NServiceBus.StepByStep.Sample.Server");
            endpointConfiguration.UseSerialization<XmlSerializer>();
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();
            endpointConfiguration.SendFailedMessagesTo("error");

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
