using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.IoC;

namespace Infra.Pessoas.EventBus.Hubs
{
    public class HubSendPessoaMongo 
    {
        IPessoasMongoRepository _mongo;
        public HubSendPessoaMongo()
        {
            _mongo = IoCGeral.Container.GetInstance<IPessoasMongoRepository>();
        }
        public void Call(Domain.Pessoas.Entities.Pessoa obj)
        {
            switch (obj.TipoEvento.ToUpper())
            {
                case "I":
                    if (obj.NomeEvento.ToUpper().Equals("SAVE"))
                    {
                        _mongo.Save(obj);
                    }
                    break;
                case "U":
                    if (obj.NomeEvento.ToUpper().Equals("UPDATE"))
                    {
                        _mongo.Update(obj);
                    }

                    break;
                case "D":
                    if (obj.NomeEvento.ToUpper().Equals("DELETE"))
                    {
                        _mongo.Delete(obj.PessoaID);
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
