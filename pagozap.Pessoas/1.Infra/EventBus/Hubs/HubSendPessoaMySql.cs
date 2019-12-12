using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.IoC;

namespace Infra.Pessoas.EventBus.Hubs
{
    public class HubSendPessoaMySql 
    {
        IPessoasMySqlRepository _mySql;
        public HubSendPessoaMySql()
        {
            _mySql = IoCGeral.Container.GetInstance<IPessoasMySqlRepository>();
        }
        public void Call(Domain.Pessoas.Entities.Pessoa obj)
        {
            switch (obj.TipoEvento.ToUpper())
            {
                case "I":
                    if (obj.NomeEvento.ToUpper().Equals("SAVE"))
                    {
                        _mySql.Save(obj);
                    }
                    break;
                case "U":
                    if (obj.NomeEvento.ToUpper().Equals("UPDATE"))
                    {
                        _mySql.Update(obj);
                    }

                    break;
                case "D":
                    if (obj.NomeEvento.ToUpper().Equals("DELETE"))
                    {
                        _mySql.Delete(obj.PessoaID);
                    }
                    break;
                case "C":
                    break;
                default:
                    break;
            }
        }
    }
}
