using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimosAPI.Models
{
    public class Associacao
    {
        [Key]
        [Column("idassociacao")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAssociacao { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        [Column("emailprofissional")]
        public string EmailProfissional { get; set; }

        [Required]
        [StringLength(20)]
        [Column("cnpj")]
        public string Cnpj { get; set; }

        [Required]
        [StringLength(255)]
        [Column("razaosocial")]
        public string RazaoSocial { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nomefantasia")]
        public string NomeFantasia { get; set; }

        [Required]
        [StringLength(20)]
        [Column("numero_telefone")]
        public string NumeroTelefone { get; set; }

        [Required]
        [StringLength(255)]
        [Column("endereco")]
        public string Endereco { get; set; }

        [Required]
        [StringLength(100)]
        [Column("senha")]
        public string Senha { get; set; }
    }
}
