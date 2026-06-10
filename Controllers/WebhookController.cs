using Microsoft.AspNetCore.Mvc;
using SecurityGuardAPI.Filters;

namespace SecurityGuardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        [ShopifyAuthorize]
        public IActionResult HandleWebhook([FromBody] object payload)
        {
            // If the execution reaches here, your ShopifyAuthorize filter passed it!
            return Ok(new { message = "Authorized! Security guard allowed the payload through." });
        }
    }
}
