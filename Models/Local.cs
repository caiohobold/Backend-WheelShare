using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosAPI.Models
{
    public class Local
    {
        [Key]
        [Column("id_local")]
        public int IdLocal { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nome_local")]
        public string NomeLocal { get; set; }

        public ICollection<Equipamento> Equipamentos { get; set; }

        [Required]
        [Column("idassociacao")]
        public int IdAssociacao { get; set; }

        [ForeignKey("IdAssociacao")]
        public Associacao Associacao { get; set; }

    }
}
