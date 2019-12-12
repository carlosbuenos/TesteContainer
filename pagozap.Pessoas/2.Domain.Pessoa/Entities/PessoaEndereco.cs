using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Pessoas.Entities
{
    public class PessoaEndereco
    {
		public PessoaEndereco()
		{
			
		}
        [Key][BsonId]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PessoaEnderecoID { get; set; }
        public string PessoaID { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public bool FlagPrincipal { get; set; }
        [BsonIgnore]
        public Pessoa Pessoa { get; set; }

		public void GenerateID()
		{
			if (string.IsNullOrEmpty(this.PessoaEnderecoID))
			{
				this.PessoaEnderecoID = Guid.NewGuid().ToString();
			}
		}
	}
}
