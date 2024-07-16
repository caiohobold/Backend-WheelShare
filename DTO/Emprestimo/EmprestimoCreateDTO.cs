using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace EmprestimosAPI.DTO.Emprestimo
{
    public class EmprestimoCreateDTO
    {
        public int IdPessoa { get; set; }
        public int IdEquipamento { get; set; }
        public DateTime DataEmprestimo {  get; set; }
        public DateTime DataDevolucao { get; set; }
        public int IdUsuario { get; set; }
        public int idAssociacao { get; set; }

    }
}
