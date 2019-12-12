using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Pessoas.Entities
{

    public enum TipoPessoa
    {
        NaoDefinido,
        PessoaFisica,
        PessoaJuridica
    }

    public class Pessoa
    {
        [Key][BsonId]
        public string PessoaID { get; set; }
        public string Nome { get; set; }
		public string PathImagem { get; set; }
		public virtual PessoaFisica PessoaFisica { get; set; }
        public virtual PessoaJuridica PessoaJuridica { get; set; }
        public virtual ICollection<PessoaTelefone> PessoaTelefone { get; set; }
        public virtual ICollection<PessoaEmail> PessoaEmail { get; set; }
        public virtual ICollection<PessoaEndereco> PessoaEndereco { get; set; }
        [NotMapped][BsonIgnore]
        public string NomeEvento { get; set; }
        [NotMapped][BsonIgnore]
        public string TipoEvento { get; set; }
        [NotMapped][BsonIgnore]
        public TipoPessoa TipoDePessoa { get { return GetTipoPessoa(); } }
		[Column("teste", TypeName = "Decimal")]
		public double teste { get; set; }
        private TipoPessoa GetTipoPessoa()
        {
            if (this.PessoaFisica != null)
            {
                return TipoPessoa.PessoaFisica;
            }
            else if (this.PessoaJuridica != null)
            {
                return TipoPessoa.PessoaJuridica;
            }
            else
            {
                return TipoPessoa.NaoDefinido;
            }
        }

        public void GerenciaID()
        {
            var chave = Guid.NewGuid().ToString();
            this.PessoaID = chave;

            if(this.PessoaTelefone != null && this.PessoaTelefone.Count > 0)
            {
                foreach (var item in this.PessoaTelefone)
                {
                    item.PessoaID = chave;
                }
            }

            if(this.PessoaEmail != null && this.PessoaEmail.Count > 0)
            {
				
                foreach (var item in this.PessoaEmail)
                {
					 item.GenerateID();
                    item.PessoaID = chave;
                }

            }

            if (this.PessoaEndereco != null && this.PessoaEndereco.Count > 0)
            {
                foreach (var item in this.PessoaEndereco)
                {
					 item.GenerateID();
                    item.PessoaID = chave;
                }
            }

            if(this.PessoaFisica != null)
            {
                this.PessoaFisica.PessoaID = chave;
            }

            if (this.PessoaJuridica != null)
            {
                this.PessoaJuridica.PessoaID = chave;
            }
        }
        
    }
}
