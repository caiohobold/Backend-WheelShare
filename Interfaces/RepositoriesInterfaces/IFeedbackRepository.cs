using EmprestimosAPI.Models;

namespace EmprestimosAPI.Interfaces.RepositoriesInterfaces
{
    public interface IFeedbackRepository
    {
        Task AddFeedbackAsync(Feedback feedback);
    }
}
