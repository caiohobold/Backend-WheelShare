using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimosAPI.Models
{
    public enum EstadoEquipamento
    {
        Novo = 0,
        Usado = 1
    }

    public class Equipamento
    {

        [Key]
        [Column("id")]
        public int IdEquipamento { get; set; }

        [Required]
        [StringLength(255)]
        [Column("nome_equipamento")]
        public string NomeEquipamento { get; set; }

        [Required]
        [Column("id_categoria")]
        public int IdCategoria { get; set; }

        [ForeignKey("IdCategoria")]
        public Categoria Categoria { get; set; }

        [Required]
        [StringLength(10)]
        [Column("estado_equipamento")]
        public int EstadoEquipamento { get; set; }

        [Required]
        [StringLength(20)]
        [Column("carga_equipamento")]
        public int CargaEquipamento { get; set; }

        [StringLength(600)]
        [Column("descricao_equipamento")]
        public string DescricaoEquipamento { get; set; }

        [Column("foto1_equipamento")]
        public byte[]? Foto1 { get; set; }

        [Column("foto2_equipamento")]
        public byte[]? Foto2 { get; set; }

        [Required]
        [Column("id_local")]
        public int IdLocal { get; set; }

        [ForeignKey("IdLocal")]
        public Local Local { get; set; }

        [Required]
        [Column("idassociacao")]
        public int IdAssociacao { get; set; }

        [ForeignKey("IdAssociacao")]
        public Associacao Associacao { get; set; }

    }
}
