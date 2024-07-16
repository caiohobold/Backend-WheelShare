using EmprestimosAPI.DTO.Local;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Services
{
    public class LocalService : ILocalService
    {
        private readonly ILocalRepository _localRepository;

        public LocalService(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        public async Task<IEnumerable<LocalReadDTO>> GetAllLocaisAsync(int pageNumber, int pageSize, int idAssociacao)
        {
            return await _localRepository.GetAllLocaisAsync(pageNumber, pageSize, idAssociacao);
        }

        public async Task<LocalReadDTO> GetLocalByIdAsync(int id, int idAssociacao)
        {
            var local = await _localRepository.GetLocalByIdAsync(id, idAssociacao);
            if (local == null) return null;

            return new LocalReadDTO
            {
                IdLocal = local.IdLocal,
                NomeLocal = local.NomeLocal,
                idAssociacao = local.IdAssociacao
            };
        }

        public async Task<LocalReadDTO> AddLocalAsync(LocalCreateDTO localDTO)
        {
            var local = new Local
            {
                NomeLocal = localDTO.NomeLocal,
                IdAssociacao = localDTO.idAssociacao
            };

            var newLocal = await _localRepository.AddLocalAsync(local, localDTO.idAssociacao);

            return new LocalReadDTO
            {
                IdLocal = newLocal.IdLocal,
                NomeLocal = newLocal.NomeLocal,
                idAssociacao = newLocal.IdAssociacao
            };
        }

        public async Task UpdateLocalAsync(int id, LocalUpdateDTO localDTO)
        {
            var local = await _localRepository.GetLocalByIdAsync(id, localDTO.idAssociacao);
            if (local == null)
            {
                throw new KeyNotFoundException("Local não encontrado.");
            }

            local.NomeLocal = localDTO.NomeLocal;
            local.IdAssociacao = localDTO.idAssociacao;
            await _localRepository.UpdateLocalAsync(local, localDTO.idAssociacao);
        }

        public async Task DeleteLocalAsync(int id, int idAssociacao)
        {
            await _localRepository.DeleteLocalAsync(id, idAssociacao);
        }
    }
}
