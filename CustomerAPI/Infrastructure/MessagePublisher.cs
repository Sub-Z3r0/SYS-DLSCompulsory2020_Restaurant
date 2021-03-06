﻿using System;
using System.Collections.Generic;
using EasyNetQ;
using SharedModels;

namespace CustomerAPI.Infrastructure
{
    public class MessagePublisher : IMessagePublisher, IDisposable
    {
        IBus bus;

        public MessagePublisher(string connectionString)
        {
            bus = RabbitHutch.CreateBus(connectionString);
        }

        public void Dispose()
        {
            bus.Dispose();
        }

        public void PublishOrderStatusChangedMessage(int? customerId, IList<OrderLine> orderLines, string topic)
        {
            var message = new OrderStatusChangedMessage
            { 
                CustomerId = customerId,
                OrderLines = orderLines 
            };

            bus.Publish(message, topic);
        }

    }
}
