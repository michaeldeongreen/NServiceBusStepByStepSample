using NServiceBus.Logging;
using NServiceBus.StepByStep.Sample.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NServiceBus.StepByStep.Sample.Subscriber
{
    public class OrderCreatedHandler
    {
        static ILog log = LogManager.GetLogger<OrderCreatedHandler>();

        public Task Handle(OrderPlaced message, IMessageHandlerContext context)
        {
            log.Info($"Handling: OrderPlaced for Order Id: {message.OrderId}");
            return Task.CompletedTask;
        }
    }
}
