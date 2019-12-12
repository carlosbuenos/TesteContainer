using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Pessoas.Entities
{
    public class PessoaFisica
    {
        [Key][BsonId]
        public string PessoaID { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string EstadoCivil { get; set; }
        public string Sexo { get; set; }
        [BsonIgnore]
        public virtual Pessoa Pessoa { get; set; }

        public bool Maior18(PessoaFisica _obj)
        {
            if ((DateTime.Now.Year - _obj.DataNascimento.Value.Year) >= 18)
            {
                return true;
            }
            return false;
        }

    }
}
