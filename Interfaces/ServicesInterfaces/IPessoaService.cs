using EmprestimosAPI.DTO.Pessoa;

namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface IPessoaService
    {
        Task<IEnumerable<PessoaReadDTO>> GetAllPessoasAsync(int pageNumber, int pageSize, int idAssociacao);
        Task<PessoaReadDTO> GetPessoaById(int id, int idAssociacao);
        Task<PessoaReadDTO> AddPessoaAsync(PessoaCreateDTO pessoaDTO);
        Task UpdatePessoaAsync(int id, PessoaUpdateDTO pessoaDTO);
        Task DeletePessoaAsync(int id, int idAssociacao);
    }
}
