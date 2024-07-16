using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimosAPI.Models
{
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int IdUsuario {  get; set; }

        [Required]
        [StringLength(20)]
        [Column("cpf")]
        public string Cpf { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nome_completo")]
        public string NomeCompleto{ get; set; }

        [Required]
        [StringLength(20)]
        [Column("numero_telefone")]
        public string NumeroTelefone { get; set; }

        [Required]
        [EmailAddress]
        [Column("email_pessoal")]
        [StringLength(100)]
        public string EmailPessoal { get; set;}

        [Required]
        [StringLength(100)]
        [Column("senha")]
        public string SenhaHash { get; set;}

        [Required]
        [DataType(DataType.Date)]
        [Column("data_nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [StringLength(255)]
        [Column("endereco")]
        public string Endereco { get; set; }

        [Required]
        [Column("idassociacao")]
        public int IdAssociacao { get; set; }

        [ForeignKey("IdAssociacao")]
        public Associacao Associacao { get; set; }
    }
}
