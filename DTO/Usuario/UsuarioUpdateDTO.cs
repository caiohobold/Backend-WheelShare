namespace EmprestimosAPI.DTO.Usuario
{
    public class UsuarioUpdateDTO
    {
        public string Cpf { get; set; }
        public string NomeCompleto { get; set; }
        public string NumeroTelefone { get; set; }
        public string EmailPessoal { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
    }
}
