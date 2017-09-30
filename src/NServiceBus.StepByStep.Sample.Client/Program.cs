﻿using NServiceBus.StepByStep.Sample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.StepByStep.Sample.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // This makes it easier to tell console windows apart
            Console.Title = "NServiceBus.StepByStep.Sample.Client";

            // The endpoint name will be used to determine queue names and serves
            // as the address, or identity, of the endpoint
            var endpointConfiguration = new EndpointConfiguration(
                endpointName: "NServiceBus.StepByStep.Sample.Client");

            endpointConfiguration.SendFailedMessagesTo("error");

            // Use XML to serialize and deserialize messages (which are just
            // plain classes) to and from message queues
            endpointConfiguration.UseSerialization<XmlSerializer>();

            // Ask NServiceBus to automatically create message queues
            endpointConfiguration.EnableInstallers();

            // Store messages on disk for this example, rather than in
            // a real queue.
            endpointConfiguration.UseTransport<LearningTransport>();

            // Store information on disk for this example, rather than in
            // a database. In this sample, only subscription information is stored
            endpointConfiguration.UsePersistence<LearningPersistence>();

            // Initialize the endpoint with the finished configuration
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);
            try
            {
                await SendOrder(endpointInstance)
                    .ConfigureAwait(false);
            }
            finally
            {
                await endpointInstance.Stop()
                    .ConfigureAwait(false);
            }
        }

        static async Task SendOrder(IEndpointInstance endpointInstance)
        {
            Console.WriteLine("Press enter to send a message");
            Console.WriteLine("Press any key to exit");

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key != ConsoleKey.Enter)
                {
                    return;
                }
                var id = Guid.NewGuid();

                var placeOrder = new PlaceOrder
                {
                    Product = "New shoes",
                    Id = id
                };
                await endpointInstance.Send("Samples.StepByStep.Server", placeOrder)
                    .ConfigureAwait(false);
                Console.WriteLine($"Sent a PlaceOrder message with id: {id:N}");
            }
        }
    }
}
