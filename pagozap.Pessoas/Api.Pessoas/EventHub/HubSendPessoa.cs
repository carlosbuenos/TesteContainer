using App.Pessoas.Interface;

namespace Api.Pessoas.EventHub
{
    /// <summary>
    /// 
    /// </summary>
    public class HubSendPessoa : BaseConfig
    {
        IPessoasApp _app;
        /// <summary>
        /// 
        /// </summary>
        public HubSendPessoa()
        {
            _app = Container.GetInstance<IPessoasApp>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Call(Domain.Pessoas.Entities.Pessoa obj)
        {
            switch (obj.TipoEvento.ToUpper())
            {
                case "I":
                    if (obj.TipoEvento.ToUpper().Equals("SAVE"))
                    {
                       _app.Save(obj);
                    }
                    break;
                case "U":
                    if (obj.TipoEvento.ToUpper().Equals("UPDATE"))
                    {
                        _app.Update(obj);
                    }
                    
                    break;
                case "D":
                    if (obj.TipoEvento.ToUpper().Equals("DELETE"))
                    {
                        _app.Delete(obj.PessoaID);
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
