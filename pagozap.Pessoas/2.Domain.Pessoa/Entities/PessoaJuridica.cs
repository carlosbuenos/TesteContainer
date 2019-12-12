using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.Pessoas.Entities
{
    public class PessoaJuridica
    {
        [Key][BsonId]
        public string PessoaID { get; set; }
        public string NomeFantasia { get; set; }
		public string RazaoSocial { get; set; }
		public string CNPJ { get; set; }
        [BsonIgnore]
        public Pessoa Pessoa { get; set; }
    }
}
