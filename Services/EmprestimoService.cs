using EmprestimosAPI.Data;
using EmprestimosAPI.DTO.Emprestimo;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmprestimosAPI.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        private readonly DbEmprestimosContext _context;

        public EmprestimoService(IEmprestimoRepository emprestimoRepository, DbEmprestimosContext context)
        {
            _emprestimoRepository = emprestimoRepository;
            _context = context;
        }


        public async Task<IEnumerable<EmprestimoReadDTO>> GetAllEmp(int pageNumber, int pageSize, int idAssociacao)
        {
            var emprestimos = await _emprestimoRepository.GetAllEmp(pageNumber, pageSize, idAssociacao);
            return emprestimos.Select(emprestimo => new EmprestimoReadDTO
            {
                Id = emprestimo.Id,
                IdPessoa = emprestimo.Pessoa.IdPessoa,
                nomePessoa = emprestimo.Pessoa.NomeCompleto,
                IdEquipamento = emprestimo.Equipamento.IdEquipamento,
                nomeEquipamento = emprestimo.Equipamento.NomeEquipamento,
                cargaEquipamento = emprestimo.Equipamento.CargaEquipamento,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucaoEmprestimo,
                IdUsuario = emprestimo.Usuario.IdUsuario,
                Status = emprestimo.Status,
                idAssociacao = emprestimo.IdAssociacao
            }).ToList();
        }


        public async Task<EmprestimoReadDTO> GetEmpById(int id, int idAssociacao)
        {
            var emprestimo = await _emprestimoRepository.GetEmpById(id, idAssociacao);
            if (emprestimo == null) return null;

            return new EmprestimoReadDTO
            {
                Id = emprestimo.Id,
                IdPessoa = emprestimo.Pessoa.IdPessoa,
                nomePessoa = emprestimo.Pessoa.NomeCompleto,
                IdEquipamento = emprestimo.Equipamento.IdEquipamento,
                nomeEquipamento = emprestimo.Equipamento.NomeEquipamento,
                cargaEquipamento = emprestimo.Equipamento.CargaEquipamento,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucaoEmprestimo,
                IdUsuario = emprestimo.Usuario.IdUsuario,
                Status = emprestimo.Status,
                idAssociacao = emprestimo.IdAssociacao
            };
        }


        public async Task<IEnumerable<EmprestimoReadDTO>> GetEmpByPessoaId(int idPessoa, int idAssociacao)
        {
            var emprestimos = await _emprestimoRepository.GetEmpByPessoaId(idPessoa, idAssociacao);

            var orderedEmprestimos = emprestimos
                .OrderBy(e => e.Status)
                .Select(e => new EmprestimoReadDTO
                {
                    Id = e.Id,
                    IdPessoa = e.IdPessoa,
                    nomePessoa = e.Pessoa.NomeCompleto,
                    IdEquipamento = e.IdEquipamento,
                    nomeEquipamento = e.Equipamento.NomeEquipamento,
                    cargaEquipamento = e.Equipamento.CargaEquipamento,
                    DataEmprestimo = e.DataEmprestimo,
                    DataDevolucao = e.DataDevolucaoEmprestimo,
                    Status = e.Status,
                    IdUsuario = e.IdUsuario,
                    idAssociacao = e.IdAssociacao
                })
                .ToList();

            return orderedEmprestimos;
        }



        public async Task<IEnumerable<EmprestimoReadDTO>> GetActiveEmp(int pageNumber, int pageSize, int idAssociacao)
        {
            var emprestimos = await _emprestimoRepository.GetActiveEmp(pageNumber, pageSize, idAssociacao);
            return emprestimos.Select(emprestimo => new EmprestimoReadDTO
            {
                Id = emprestimo.Id,
                IdPessoa = emprestimo.Pessoa.IdPessoa,
                nomePessoa = emprestimo.Pessoa.NomeCompleto,
                IdEquipamento = emprestimo.Equipamento.IdEquipamento,
                nomeEquipamento = emprestimo.Equipamento.NomeEquipamento,
                cargaEquipamento = emprestimo.Equipamento.CargaEquipamento,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucaoEmprestimo,
                IdUsuario = emprestimo.Usuario.IdUsuario,
                Status = emprestimo.Status,
                idAssociacao = emprestimo.IdAssociacao
            }).ToList();
        }


        public async Task<IEnumerable<EmprestimoReadDTO>> GetEmpAtrasados(int idAssociacao)
        {
            var emprestimos = await _emprestimoRepository.GetEmpAtrasados(idAssociacao);
            return emprestimos.Select(e => new EmprestimoReadDTO
            {
                Id = e.Id,
                IdPessoa = e.Pessoa.IdPessoa,
                nomePessoa = e.Pessoa.NomeCompleto,
                telefonePessoa = e.Pessoa.Telefone,
                IdEquipamento = e.Equipamento.IdEquipamento,
                nomeEquipamento = e.Equipamento.NomeEquipamento,
                cargaEquipamento = e.Equipamento.CargaEquipamento,
                DataEmprestimo = e.DataEmprestimo,
                DataDevolucao = e.DataDevolucaoEmprestimo,
                IdUsuario = e.Usuario.IdUsuario,
                Status = e.Status,
                idAssociacao = e.IdAssociacao
            }).ToList();
        }


        public async Task<EmprestimoReadDTO> AddEmp(EmprestimoCreateDTO emprestimoDTO, int idAssociacao)
        {
            if (emprestimoDTO.DataDevolucao < emprestimoDTO.DataEmprestimo)
            {
                throw new ArgumentException("A data de devolução não pode ser anterior à data de início do empréstimo.");
            }

            var emprestimo = new Emprestimo
            {
                DataEmprestimo = emprestimoDTO.DataEmprestimo,
                DataDevolucaoEmprestimo = emprestimoDTO.DataDevolucao,
                IdPessoa = emprestimoDTO.IdPessoa,
                IdEquipamento = emprestimoDTO.IdEquipamento,
                IdUsuario = emprestimoDTO.IdUsuario,
                IdAssociacao = idAssociacao
            };

            var newEmprestimo = await _emprestimoRepository.AddEmp(emprestimo, idAssociacao);

            if (newEmprestimo.Pessoa == null)
            {
                await _context.Entry(newEmprestimo).Reference(e => e.Pessoa).LoadAsync();
            }
            if (newEmprestimo.Usuario == null)
            {
                await _context.Entry(newEmprestimo).Reference(e => e.Usuario).LoadAsync();
            }
            if (newEmprestimo.Equipamento == null)
            {
                await _context.Entry(newEmprestimo).Reference(e => e.Equipamento).LoadAsync();
            }

            return new EmprestimoReadDTO
            {
                Id = emprestimo.Id,
                IdPessoa = emprestimo.Pessoa.IdPessoa,
                nomePessoa = emprestimo.Pessoa.NomeCompleto,
                IdEquipamento = emprestimo.Equipamento.IdEquipamento,
                nomeEquipamento = emprestimo.Equipamento.NomeEquipamento,
                cargaEquipamento = emprestimo.Equipamento.CargaEquipamento,
                DataEmprestimo = emprestimo.DataEmprestimo,
                DataDevolucao = emprestimo.DataDevolucaoEmprestimo,
                IdUsuario = emprestimo.Usuario.IdUsuario,
                Status = emprestimo.Status,
                idAssociacao = emprestimo.IdAssociacao
            };
        }


        public async Task UpdateEmp(int id, EmprestimoUpdateDTO emprestimoDTO, int idAssociacao)
        {
            var emprestimo = await _emprestimoRepository.GetEmpById(id, idAssociacao);
            if (emprestimo == null)
            {
                throw new KeyNotFoundException("Empréstimo não encontrado.");
            }

            if (emprestimoDTO.DataDevolucao < emprestimoDTO.DataEmprestimo)
            {
                throw new ArgumentException("A data de devolução não pode ser anterior à data de início do empréstimo.");
            }

            emprestimo.DataEmprestimo = emprestimoDTO.DataEmprestimo;
            emprestimo.DataDevolucaoEmprestimo = emprestimoDTO.DataDevolucao;

            await _emprestimoRepository.UpdateEmp(emprestimo, idAssociacao);
        }


        public async Task DeleteEmp(int id, int idAssociacao)
        {
            var emprestimo = await _emprestimoRepository.GetEmpById(id, idAssociacao);

            if (emprestimo == null)
            {
                throw new KeyNotFoundException("Empréstimo não encontrado.");
            }

            await _emprestimoRepository.DeleteEmp(id, idAssociacao);
        }


        public async Task FinalizarEmp(int id, int idAssociacao)
        {
            var emprestimo = await _emprestimoRepository.GetEmpById(id, idAssociacao);
            if (emprestimo == null)
            {
                throw new KeyNotFoundException("Empréstimo não encontrado.");
            }

            if (emprestimo.Status != 0)
            {
                throw new InvalidOperationException("Este empréstimo já foi finalizado.");
            }

            emprestimo.Status = 1;
            emprestimo.DataEmprestimo = DateTime.SpecifyKind(emprestimo.DataEmprestimo, DateTimeKind.Utc);
            emprestimo.DataDevolucaoEmprestimo = DateTime.UtcNow;
            await _emprestimoRepository.UpdateEmp(emprestimo, idAssociacao);
        }

    }
}