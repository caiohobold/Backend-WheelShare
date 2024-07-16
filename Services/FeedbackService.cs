using EmprestimosAPI.DTO.Feedback;
using EmprestimosAPI.Interfaces.RepositoriesInterfaces;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using EmprestimosAPI.Models;

namespace EmprestimosAPI.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task ReportErrorAsync(FeedbackModel feedbackModel)
        {
            var feedback = new Feedback
            {
                UserName = feedbackModel.UserName,
                Message = feedbackModel.Message,
                Type = "Error",
                Date = feedbackModel.Date,
                IdAssociacao = feedbackModel.idAssociacao
            };
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }

        public async Task SendFeedbackAsync(FeedbackModel feedbackModel)
        {
            var feedback = new Feedback
            {
                UserName = feedbackModel.UserName,
                Message = feedbackModel.Message,
                Type = "Feedback",
                Date = feedbackModel.Date,
                IdAssociacao = feedbackModel.idAssociacao
            };
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }
    }
}
