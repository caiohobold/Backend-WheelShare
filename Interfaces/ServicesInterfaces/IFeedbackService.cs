using EmprestimosAPI.DTO.Feedback;

namespace EmprestimosAPI.Interfaces.ServicesInterfaces
{
    public interface IFeedbackService
    {
        Task ReportErrorAsync(FeedbackModel feedback);
        Task SendFeedbackAsync(FeedbackModel feedback);
    }
}
