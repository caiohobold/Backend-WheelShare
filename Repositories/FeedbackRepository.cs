using EmprestimosAPI.Data;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly DbEmprestimosContext _context;

        public FeedbackRepository(DbEmprestimosContext context)
        {
            _context = context;
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }
    }
}
