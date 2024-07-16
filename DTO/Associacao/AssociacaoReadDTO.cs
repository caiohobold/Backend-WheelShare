namespace EmprestimosAPI.DTO.Associacao
{
    public class AssociacaoReadDTO
    {
        public int IdAssociacao { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string EmailProfissional { get; set; }
        public string Numero_Telefone { get; set; }
        public string Endereco { get; set; }
        public string senhaHash { get; set; }
    }
}
