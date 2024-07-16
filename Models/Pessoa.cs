using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimosAPI.Models
{
    public class Pessoa
    {
        [Key]
        [Column("id")]
        public int IdPessoa { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nome_completo")]
        public string NomeCompleto { get; set; }

        [Required]
        [StringLength(20)]
        [Column("cpf")]
        public string Cpf {  get; set; }

        [StringLength(100)]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [Column("telefone")]
        public string Telefone { get; set; }

        [StringLength (600)]
        [Column("descricao")]
        public string Descricao { get; set; }

        [StringLength (600)]
        [Column("endereco")]
        public string Endereco { get; set; }

        [Required]
        [Column("idassociacao")]
        public int IdAssociacao { get; set; }

        [ForeignKey("IdAssociacao")]
        public Associacao Associacao { get; set; }

    }
}
