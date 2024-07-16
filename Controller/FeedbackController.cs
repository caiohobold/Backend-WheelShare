using EmprestimosAPI.DTO.Feedback;
using EmprestimosAPI.Interfaces.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmprestimosAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [Authorize]
        [HttpPost("report-error")]
        public async Task<IActionResult> ReportError([FromBody] FeedbackModel feedback)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _feedbackService.ReportErrorAsync(feedback);
            return Ok();
        }

        [Authorize]
        [HttpPost("send-feedback")]
        public async Task<IActionResult> SendFeedback([FromBody] FeedbackModel feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _feedbackService.SendFeedbackAsync(feedback);
            return Ok();
        }
    }
}
