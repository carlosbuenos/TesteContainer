using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;

namespace Infra.Pessoas.EventBus.Consumer
{
    public class ConsumerQueue
    {
        public static IModel Channel { get; set; }

        public Pessoa ObjRetorno { get; set; }

        public Pessoa GetobjectoFromQueue()
        {
            var obj = GetMessagefromQueue(Parameters.hostRabbit, Parameters.hostUser, Parameters.hostPassword, QueueParameters.QueuelName, QueueParameters.KeyChannel);
            return obj;
        }

        public List<Pessoa> GetobjectosFromQueue()
        {
            var obj = GetMessagesfromQueue(Parameters.hostRabbit, Parameters.hostUser, Parameters.hostPassword, QueueParameters.QueuelName, QueueParameters.KeyChannel);

            return obj;
        }

        private ConnectionFactory Conn(string Host, string userName, string password)
        {
            return new BusConfig().BusConfigurations(Host, userName, password);
        }

        public IModel CreateChannel(string Host, string userName, string password, string QueuelName)
        {
            var conection = Conn(Host, userName, password).CreateConnection();
            var channel = conection.CreateModel();
            channel.QueueDeclare(queue: QueuelName, durable: false, exclusive: false, autoDelete: true, arguments: null);
            return channel;

        }

        public IModel CreateChannelListen(string Host, string userName, string password, string QueuelName)
        {
            var conection = Conn(Host, userName, password).CreateConnection();
            var channel = conection.CreateModel();
            channel.QueueDeclare(queue: QueuelName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 1, false);
            return channel;

        }

        public Pessoa GetMessagefromQueue(string Host, string userName, string password, string QueuelName, string KeyChannel)
        {
            var channel = CreateChannel(Host, userName, password, QueuelName);
            var data = channel.BasicGet(QueuelName, true);
            var message = Encoding.UTF8.GetString(data.Body);
            var model = JsonConvert.DeserializeObject<Pessoa>(message);
            channel.Close();
            return model;

        }

        public List<Pessoa> GetMessagesfromQueue(string Host, string userName, string password, string QueuelName, string KeyChannel)
        {
            var channel = CreateChannel(Host, userName, password, QueuelName);
            var data = channel.BasicGet(QueuelName, true);
            if (data != null)
            {
                var message = Encoding.UTF8.GetString(data.Body);
                var model = JsonConvert.DeserializeObject<List<Pessoa>>(message);
                channel.Close();
                return model;
            }
            return null;

        }

     


    }
}
