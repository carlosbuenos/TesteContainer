using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Infra.Pessoas.EventBus.Hubs;
using Infra.Pessoas.EventBus.Producer;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Infra.Pessoas.Repository
{
    public class ListenQueueMongo 
    {
        private  IModel Channel { get; set; }
        public  ConnectionFactory BusConfigurations(string _Host, string _Username, string _Password)
        {
            var config = new ConnectionFactory
            {
                Uri = new Uri("amqp://" + _Username + ":" + _Password + "@" + _Host + ":5672")
            };

            return config;
        }

        private  ConnectionFactory Conn(string Host, string userName, string password)
        {
            return BusConfigurations(Host, userName, password);
        }

        public  IModel CreateChannelListen(string Host, string userName, string password, string QueuelName)
        {
            var conection = Conn(Host, userName, password).CreateConnection();
            var channel = conection.CreateModel();
            channel.QueueDeclare(queue: QueuelName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            return channel;

        }
        public  void StartSerevrListenQueue(string Host, string userName, string password, string QueuelName, string KeyChannel)
        {
            Channel = CreateChannelListen(Host, userName, password, QueuelName);
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += EventingBasicConsumer_Received;
            var data = Channel.BasicConsume(QueuelName, true, consumer);

        }

        private void EventingBasicConsumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            Pessoa objeto = new Pessoa();

            var body = ea.Body;
            var props = ea.BasicProperties;
            var replyProps = Channel.CreateBasicProperties();
            replyProps.CorrelationId = QueueParameters.CorrelationID;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                objeto = JsonConvert.DeserializeObject<Pessoa>(message);
                if (objeto != null && !string.IsNullOrEmpty(objeto.PessoaID))
                {
                    new HubSendPessoaMongo().Call(objeto);
                }
                Channel.Close();
                

            }
            catch
            {
                QueueParameters.QueuelName = Parameters.ListenMongo;
                QueueParameters.KeyChannel = Parameters.ListenMongo;
                if (objeto != null && !string.IsNullOrEmpty(objeto.PessoaID))
                {
                    QueueParameters.Message = objeto;
                    new PublisherQueue().Send();
                }
            }
            StartSerevrListenQueue(Parameters.host, Parameters.hostUser, Parameters.hostPassword, Parameters.ListenMongo, Parameters.ListenMongo);
        }
    }
}
