using StackExchange.Redis;
using System;

namespace Infra.Pessoas.DataAccess.FactoryTypeConn
{
    public static class ConectaRedis
	{
		static ConectaRedis()
		{
			ConfigurationOptions option = new ConfigurationOptions
			{
				AbortOnConnectFail = false,
				EndPoints = { "redispessoas.exi58g.ng.0001.use1.cache.amazonaws.com" },
				
			};
			ConectaRedis.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
			{
				return ConnectionMultiplexer.Connect(option);
			});
		}

		private static Lazy<ConnectionMultiplexer> lazyConnection;

		public static ConnectionMultiplexer Connection
		{
			get
			{
				return lazyConnection.Value;
			}
		}
	}
}
