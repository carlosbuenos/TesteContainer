using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Infra.Pessoas.EventBus.Hubs;
using Infra.Pessoas.EventBus.Producer;
using Infra.Pessoas.HubManagerData;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Infra.Pessoas.Repository
{
    public class ListenQueueMySql : RabbitMQBase
    {
        public void StartSerevrListenQueue(string Host, string userName, string password, string QueuelName, string KeyChannel)
        {
            Channel = CreateChannelListen(Host, userName, password, QueuelName);
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += EventingBasicConsumer_Received;
            var data = Channel.BasicConsume(QueuelName, true, consumer);

        }

        private void EventingBasicConsumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            var body = ea.Body;
            var props = ea.BasicProperties;
            var replyProps = Channel.CreateBasicProperties();
            replyProps.CorrelationId = QueueParameters.CorrelationID;
            Domain.Pessoas.Entities.Pessoa objeto = new Domain.Pessoas.Entities.Pessoa();

            try
            {
                var message = Encoding.UTF8.GetString(body);

                objeto = JsonConvert.DeserializeObject<Pessoa>(message);

                if (objeto != null && !string.IsNullOrEmpty(objeto.PessoaID))
                {
                    new HubSendPessoaMySql().Call(objeto);
                }
                Channel.Close();
            }
            catch
            {
                QueueParameters.QueuelName = Parameters.ListenMySQL;
                QueueParameters.KeyChannel = Parameters.ListenMySQL;
                if (objeto != null && !string.IsNullOrEmpty(objeto.PessoaID))
                {
                    QueueParameters.Message = objeto;
                    new PublisherQueue().Send();
                }
            }
            StartSerevrListenQueue(Parameters.host, Parameters.hostUser, Parameters.hostPassword, Parameters.ListenMySQL, Parameters.ListenMySQL);

        }

        /*private  void EventingBasicConsumer_Received(object sender, BasicDeliverEventArgs ea)
        {
            Domain.Pessoas.Entities.Pessoas objeto = new Domain.Pessoas.Entities.Pessoas();
            string response = null;

            var body = ea.Body;
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = QueueParameters.CorrelationID;

            try
            {

                var message = Encoding.UTF8.GetString(body);
                objeto = JsonConvert.DeserializeObject<Pessoa>(message);
                if (objeto != null)
                {

                    new HubSendPessoaMySql().Call(objeto);
                }
                channel.Close();

            }
            catch (Exception e)
            {
                QueueParameters.QueuelName = Parameters.ListenMongo;
                QueueParameters.KeyChannel = Parameters.ListenMongo;
                if (objeto != null && !string.IsNullOrEmpty(objeto.PessoaID))
                {
                    QueueParameters.Message = objeto;
                    new PublisherQueue().Send();
                }
            }
            StartSerevrListenQueue(QueueParameters.Host, QueueParameters.userName, QueueParameters.password, Parameters.ListenMongo, Parameters.ListenMongo);
        }*/
    }
}
