namespace Domain.Pessoas.Entities.Retorno
{
    public class OBJMsg
    {
        public int StatusCode { get; set; }
        public string Mensagem { get; set; }
        public string IDTransctionQueue { get; set; }
        

        public OBJMsg MontaRetorno(int _StatusCode, string _Mensagem)
        {
            this.StatusCode = _StatusCode;
            this.Mensagem = _Mensagem;
            return this;
        }
    }
}
