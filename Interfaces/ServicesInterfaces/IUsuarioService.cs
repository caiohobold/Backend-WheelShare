using EmprestimosAPI.DTO.Usuario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmprestimosAPI.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioReadDTO>> GetAllUsers(int pageNumber, int pageSize, int idAssociacao);
        Task<UsuarioReadDTO> GetUserById(int id, int idAssociacao);
        Task<UsuarioReadDTO> AddUser(UsuarioCreateDTO usuarioDTO, int idAssociacao);
        Task<bool> ResetPasswordAsync(string email);
        Task UpdateUser(int id, UsuarioUpdateDTO usuarioDTO, int idAssociacao);
        Task DeleteUser(int id, int idAssociacao);
        Task ChangeUserPassword(int id, string newPassword, int idAssociacao);
    }
}

