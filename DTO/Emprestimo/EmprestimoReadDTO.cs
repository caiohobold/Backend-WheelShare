namespace EmprestimosAPI.DTO.Emprestimo
{
    public class EmprestimoReadDTO
    {
        public int Id { get; set; }
        public int IdPessoa { get; set; }
        public string nomePessoa { get; set; }
        public string telefonePessoa { get; set; }
        public int IdEquipamento { get; set; }
        public string nomeEquipamento { get; set; }
        public int cargaEquipamento { get; set; }
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; }
        public int Status { get; set; }
        public int IdUsuario { get; set; }
        public int idAssociacao { get; set; }

    }
}
