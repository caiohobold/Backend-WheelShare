using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Associacao;
using EmprestimosAPI.Interfaces.Account;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.Services;
using EmprestimosAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmprestimosAPI.Services
{
    public class AssociacaoService : IAssociacaoService
    {
        private readonly IAssociacaoRepository _repository;
        private readonly DbEmprestimosContext _context;
        private readonly HashingService _hashingService;

        public AssociacaoService(IAssociacaoRepository repository, DbEmprestimosContext context, HashingService hashingService)
        {
            _repository = repository;
            _context = context;
            _hashingService = hashingService;
        }

        public async Task<IEnumerable<AssociacaoReadDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var associacoes = await _repository.GetAllAssocAsync(pageNumber, pageSize);
            return associacoes.Select(a => new AssociacaoReadDTO
            { 
                IdAssociacao = a.IdAssociacao,
                RazaoSocial = a.RazaoSocial,
                NomeFantasia = a.NomeFantasia,
                EmailProfissional = a.EmailProfissional
            }).ToList();
        }

        public async Task<AssociacaoReadDTO> GetByIdAsync(int id)
        {
            var associacao = await _repository.GetAssocById(id);
            if (associacao == null) return null;
            return new AssociacaoReadDTO
            {
                IdAssociacao = associacao.IdAssociacao,
                RazaoSocial = associacao.RazaoSocial,
                NomeFantasia = associacao.NomeFantasia,
                EmailProfissional = associacao.EmailProfissional,
                Endereco = associacao.Endereco,
                Numero_Telefone = associacao.NumeroTelefone,
                senhaHash = associacao.Senha
                
            };
        }

        public async Task<AssociacaoReadDTO> CreateAsync(AssociacaoCreateDTO associacaoDTO)
        {
            var associacao = new Associacao
            {
                EmailProfissional = associacaoDTO.EmailProfissional,
                Cnpj = associacaoDTO.CNPJ,
                RazaoSocial = associacaoDTO.RazaoSocial,
                NomeFantasia = associacaoDTO.NomeFantasia,
                NumeroTelefone = associacaoDTO.Numero_Telefone,
                Endereco = associacaoDTO.Endereco,
                Senha = associacaoDTO.Senha
            };

            associacao.Senha = _hashingService.HashAssocPassword(associacao, associacaoDTO.Senha);

            var newAssociacao = await _repository.AddAssoc(associacao);

            return new AssociacaoReadDTO
            {
                IdAssociacao = newAssociacao.IdAssociacao,
                RazaoSocial = newAssociacao.RazaoSocial,
                NomeFantasia = newAssociacao.NomeFantasia,
                EmailProfissional = newAssociacao.EmailProfissional
            };
        }

        public async Task UpdateAsync(int id, AssociacaoUpdateDTO associacaoDTO)
        {
            var associacao = await _repository.GetAssocById(id);
            if(associacao == null)
            {
                throw new KeyNotFoundException("Associação Not Found");
            }

            associacao.NomeFantasia = associacaoDTO.NomeFantasia;
            associacao.NumeroTelefone = associacaoDTO.Numero_Telefone;
            associacao.EmailProfissional = associacaoDTO.EmailProfissional;
            associacao.Endereco = associacaoDTO.Endereco;
            associacao.RazaoSocial = associacaoDTO.RazaoSocial;

            await _repository.UpdateAssoc(associacao);
        }

        public async Task DeleteAsync(int id)
        {
            var associacao = await _repository.GetAssocById(id);
            if (associacao == null)
                throw new KeyNotFoundException("Associação not found");

            await _repository.DeleteAssoc(id);
        }

        public async Task ChangeAssocPassword(int id, string newPassword)
        {
            var assoc = await _repository.GetAssocById(id);
            if(assoc != null)
            {
                assoc.Senha = _hashingService.HashAssocPassword(assoc, newPassword);
                await _repository.UpdateAssoc(assoc);
            }
        }
    }
}
