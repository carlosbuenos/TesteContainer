using CrossCutting.Pessoas;

namespace Infra.Pessoas.DataAccess.FactoryTypeConn
{
    public class ConectaMongoDB : ConectionDrirection
	{

		public override string StrConnect()
		{
            return Parameters.StrConnMongo;
		}
	}
}
