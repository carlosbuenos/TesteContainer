using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Pessoas.Entities
{
    public class PessoaEmail
    {
		public PessoaEmail()
		{
			
		}
        [Key][BsonId]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string PessoaEmailID { get; set; }
        public string PessoaID { get; set; }
        public string Email { get; set; }
        public bool FlagPrincipal { get; set; }
        [BsonIgnore]
        public Pessoa Pessoa { get; set; }

		public void GenerateID()
		{
			if (string.IsNullOrEmpty(this.PessoaEmailID))
			{
				this.PessoaEmailID = Guid.NewGuid().ToString();
			}
		}
    }
}
