using RabbitMQ.Client;
using System;

namespace Infra.Pessoas.HubManagerData
{
    public class RabbitMQBase
    {
		public IModel Channel { get; set; }
		private ConnectionFactory RabbitConnection(string _Host, string _Username, string _Password)
		{
			var config = new ConnectionFactory
			{
				Uri = new Uri("amqp://" + _Username + ":" + _Password + "@" + _Host + ":5672")
			};

			return config;
		}

		private ConnectionFactory Conn(string Host, string userName, string password)
		{
			return RabbitConnection(Host, userName, password);
		}

		public IModel CreateChannelListen(string Host, string userName, string password, string QueuelName)
		{
			var conection = Conn(Host, userName, password).CreateConnection();
			var channel = conection.CreateModel();
			channel.QueueDeclare(queue: QueuelName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			channel.BasicQos(0, 1, false);
			return channel;

		}

		public IModel CreateChannel(string Host, string userName, string password, string QueuelName)
		{
			var conection = Conn(Host, userName, password).CreateConnection();
			var channel = conection.CreateModel();
			channel.QueueDeclare(queue: QueuelName, durable: true, exclusive: false, autoDelete: false, arguments: null);
			return channel;

		}
	}
}
