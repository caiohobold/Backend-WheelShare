namespace EmprestimosAPI.DTO.Pessoa
{
    public class PessoaUpdateDTO
    {
        public string NomeCompleto { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string Descricao { get; set; }
        public int idAssociacao { get; set; }
    }
}
