using CrossCutting.Pessoas;

namespace Infra.Pessoas.DataAccess.FactoryTypeConn
{
    public class ConectaMySql : ConectionDrirection
    {
        public string Cosscuting { get; private set; }

        public override string StrConnect()
        {
            return Parameters.strConnMySql;
		}
    }
}
