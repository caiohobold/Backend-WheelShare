namespace EmprestimosAPI.DTO.Usuario
{
    public class UsuarioCreateDTO
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroTelefone { get; set; }
        public string EmailPessoal { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public int IdAssociacao { get; set; }
    }
}
