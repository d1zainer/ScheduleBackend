
using Microsoft.AspNetCore.Mvc;
using Notification.WebApi.Models;
using Notification.WebApi.Services;


namespace Notification.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmailController(EmailSendService emailSendService) : ControllerBase
    {
        [HttpPost("/send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailMessage message)
        {
            var result = await emailSendService.SendEmailAsync(message);
            if (result.Item1)
                return Ok();
            return BadRequest(new { message = result.Item2 });
        }

    }
}
