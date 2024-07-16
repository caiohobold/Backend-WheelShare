using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimosAPI.Models
{
    public class Emprestimo
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("data_emprestimo")]
        [DataType(DataType.Date)]
        public DateTime DataEmprestimo { get; set; }

        [Required]
        [Column("status")]
        [DefaultValue(1)]
        public int Status { get; set; } 

        [Required]
        [Column("IdPessoa")]
        public int IdPessoa { get; set; }
        [ForeignKey("IdPessoa")]
        public Pessoa Pessoa { get; set; }

        [Required]
        [Column("IdEquipamento")]
        public int IdEquipamento { get; set; }
        [ForeignKey("IdEquipamento")]
        public Equipamento Equipamento { get; set; }

        [Required]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [Column("data_devolucao_emprestimo")]
        [DataType(DataType.Date)]
        public DateTime DataDevolucaoEmprestimo { get; set; }

        [Required]
        [Column("idassociacao")]
        public int IdAssociacao { get; set; }

        [ForeignKey("IdAssociacao")]
        public Associacao Associacao { get; set; }
    }
}
