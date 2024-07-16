namespace EmprestimosAPI.DTO.Usuario
{
    public class UsuarioReadDTO
    {
        public int IdUsuario { get; set; }
        public string Cpf {  get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public string EmailPessoal { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroTelefone { get; set; }
        public string SenhaHash { get; set; }
        public string AssociacaoNomeFantasia { get; set; }
        public int IdAssociacao {  get; set; }

    }
}
