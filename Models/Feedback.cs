using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmprestimosAPI.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("idassociacao")]
        public int IdAssociacao { get; set; }

        [ForeignKey("IdAssociacao")]
        public Associacao Associacao { get; set; }
    }
}
