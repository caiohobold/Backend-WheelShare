using EmprestimosAPI.DTO.Associacao;
using EmprestimosAPI.DTO.Emprestimo;
using EmprestimosAPI.DTO.Pessoa;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaService(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public async Task<IEnumerable<PessoaReadDTO>> GetAllPessoasAsync(int pageNumber, int pageSize, int idAssociacao)
        {
            var pessoas = await _pessoaRepository.GetAllPessoasAsync(pageNumber, pageSize, idAssociacao);
            return pessoas.Select(a => new PessoaReadDTO
            {
                IdPessoa = a.IdPessoa,
                NomeCompleto = a.NomeCompleto,
                Cpf = a.Cpf,
                Email = a.Email,
                Telefone = a.Telefone,
                Descricao = a.Descricao,
                Endereco = a.Endereco,
                idAssociacao = a.idAssociacao,
                StatusEmprestimo = a.StatusEmprestimo,
                DataEmprestimo = a.DataEmprestimo
            }).ToList();
        }

        public async Task<PessoaReadDTO> GetPessoaById(int id, int idAssociacao)
        {
            var pessoa = await _pessoaRepository.GetPessoaByIdAsync(id, idAssociacao);
            if (pessoa == null) return null;

            var pessoaDTO = new PessoaReadDTO
            {
                IdPessoa = pessoa.IdPessoa,
                NomeCompleto = pessoa.NomeCompleto,
                Cpf = pessoa.Cpf,
                Email = pessoa.Email,
                Telefone = pessoa.Telefone,
                Descricao = pessoa.Descricao,
                Endereco = pessoa.Endereco,
                idAssociacao = pessoa.idAssociacao,
                StatusEmprestimo = pessoa.StatusEmprestimo,
                DataEmprestimo = pessoa.DataEmprestimo
            };

            return pessoaDTO;
        }

        public async Task<PessoaReadDTO> AddPessoaAsync(PessoaCreateDTO pessoaDTO)
        {
            var pessoa = new Pessoa
            {
                NomeCompleto = pessoaDTO.NomeCompleto,
                Cpf = pessoaDTO.Cpf,
                Email = pessoaDTO.Email,
                Telefone = pessoaDTO.Telefone,
                Descricao = pessoaDTO.Descricao,
                Endereco = pessoaDTO.Endereco,
                IdAssociacao = pessoaDTO.idAssociacao
            };

            var newPessoa = await _pessoaRepository.AddPessoaAsync(pessoa, pessoaDTO.idAssociacao);

            return new PessoaReadDTO
            {
                IdPessoa = newPessoa.IdPessoa,
                NomeCompleto = newPessoa.NomeCompleto,
                Cpf = newPessoa.Cpf,
                Email = newPessoa.Email,
                Telefone = newPessoa.Telefone,
                Descricao = newPessoa.Descricao,
                Endereco = newPessoa.Endereco,
                idAssociacao = newPessoa.IdAssociacao
            };
        }

        public async Task UpdatePessoaAsync(int id, PessoaUpdateDTO pessoaDTO)
        {
            var pessoa = await _pessoaRepository.GetPessoaByIdAsync(id, pessoaDTO.idAssociacao);
            if (pessoa == null)
            {
                throw new KeyNotFoundException("Pessoa Not Found");
            }

            pessoa.NomeCompleto = pessoaDTO.NomeCompleto;
            pessoa.Cpf = pessoaDTO.Cpf;
            pessoa.Email = pessoaDTO.Email;
            pessoa.Telefone = pessoaDTO.Telefone;
            pessoa.Descricao = pessoaDTO.Descricao;
            pessoa.Endereco = pessoaDTO.Endereco;
            pessoa.idAssociacao = pessoaDTO.idAssociacao;

            await _pessoaRepository.UpdatePessoaAsync(new Pessoa
            {
                IdPessoa = pessoa.IdPessoa,
                NomeCompleto = pessoa.NomeCompleto,
                Cpf = pessoa.Cpf,
                Email = pessoa.Email,
                Telefone = pessoa.Telefone,
                Descricao = pessoa.Descricao,
                Endereco = pessoa.Endereco,
                IdAssociacao = pessoa.idAssociacao
            }, pessoaDTO.idAssociacao);
        }

        public async Task DeletePessoaAsync(int id, int idAssociacao)
        {
            var pessoa = await _pessoaRepository.GetPessoaByIdAsync(id, idAssociacao);
            if (pessoa == null)
            {
                throw new KeyNotFoundException("Pessoa not found");
            }

            await _pessoaRepository.DeletePessoaAsync(id, idAssociacao);
        }
    }
}
