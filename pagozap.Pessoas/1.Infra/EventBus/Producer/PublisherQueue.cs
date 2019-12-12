using CrossCutting.Pessoas;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Infra.Pessoas.EventBus.Producer
{
    public class PublisherQueue: IPublisherQueue
    {
        public void Send()
        {
            PubMessage(Parameters.hostRabbit, QueueParameters.UserName, QueueParameters.Password,QueueParameters.Message, QueueParameters.QueuelName, QueueParameters.KeyChannel);
        }

        public void SendResponse()
        {
            PubMessage(Parameters.hostRabbit, QueueParameters.UserName, QueueParameters.Password, QueueParameters.Message, QueueParameters.QueuelName, QueueParameters.KeyChannel);
        }

        private ConnectionFactory Conn(string Host, string userName, string password)
        {
            return new BusConfig().BusConfigurations(Host, userName, password);
        }

        public IModel CreateChannel(string Host, string userName, string password, string QueuelName)
        {
            var conection = Conn(Host, userName, password).CreateConnection();
            var channel = conection.CreateModel();
            channel.QueueDeclare(queue: QueuelName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            return channel;

        }

        public void PubMessage(string Host, string userName, string password, Object Message, string QueuelName, string KeyChannel)
        {
            var objserializer = JsonConvert.SerializeObject(Message);
            var body = Encoding.UTF8.GetBytes(objserializer);
            var channel = CreateChannel(Host, userName, password, QueuelName);
            var properties = channel.CreateBasicProperties();
            QueueParameters.CorrelationID = Guid.NewGuid().ToString();
            properties.CorrelationId = QueueParameters.CorrelationID;
            properties.ReplyTo = KeyChannel;
            var delivery = properties.Persistent = false;
            channel.BasicPublish(exchange: "",
                                 routingKey: KeyChannel,
                                 basicProperties: properties,
                                 body: body);

        }

       
    }
}
