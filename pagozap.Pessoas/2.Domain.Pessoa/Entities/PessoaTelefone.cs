using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Pessoas.Entities
{
    public class PessoaTelefone
    {
        [Key, Column(Order = 0)][BsonId]
        public string PessoaID { get; set; }
        [Key, Column(Order = 1)]
        public string DDD { get; set; }
        [Key, Column(Order = 2)]
        public string Numero { get; set; }
        public bool FlagPrincipal { get; set; }
		public bool WhatsApp { get; set; }
		public bool Telegram { get; set; }
		[BsonIgnore]
        public Pessoa Pessoa { get; set; }

    }
}
