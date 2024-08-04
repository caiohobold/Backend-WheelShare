using EmprestimosAPI.DTO.Usuario;
using EmprestimosAPI.Helpers;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface IUsuarioRepository
    {
        Task<PagedList<Usuario>> GetAllUsers(int pageNumber, int pageSize, int idAssociacao);
        Task<Usuario> GetUserById(int id, int idAssociacao);
        Task<Usuario> GetUserByEmailAsync(string email, int idAssociacao);
        Task<Usuario> GetUserByEmailNoAssocAsync(string email);
        Task<Usuario> AddUser(UsuarioCreateDTO usuarioDTO);
        Task UpdateUser(Usuario usuario, int idAssociacao);
        Task DeleteUser(int id, int idAssociacao);
    }
}
