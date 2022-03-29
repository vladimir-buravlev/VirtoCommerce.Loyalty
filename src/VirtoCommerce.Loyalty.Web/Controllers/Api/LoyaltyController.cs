using VirtoCommerce.Loyalty.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace VirtoCommerce.Loyalty.Web.Controllers.Api
{
    [Route("api/Loyalty")]
    public class LoyaltyController : Controller
    {
        // GET: api/VirtoCommerce.Loyalty
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}
