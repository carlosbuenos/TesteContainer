using RabbitMQ.Client;
using System;

namespace Infra.Pessoas.EventBus
{
    public class BusConfig
    {
        public ConnectionFactory BusConfigurations(string _Host, string _Username, string _Password)
        {
            var config = new ConnectionFactory
            {
                Uri = new Uri("amqp://" + _Username + ":" + _Password + "@" + _Host + ":5672")
            };

            return config;
        }
    }
}
