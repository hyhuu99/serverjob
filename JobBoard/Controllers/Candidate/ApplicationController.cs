using ApplicationLogic.Applications;
using ApplicationLogic.Applications.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Service.Controllers.Candidate
{
    [Produces("application/json")]
    [Route("api/Application")]
    //[Authorize("Candidate")]
    public class ApplicationController : Controller
    {
        private readonly IApplycationBusinessLogic _applycationBusinessLogic;
        public ApplicationController(IApplycationBusinessLogic applycationBusinessLogic)
        {
            _applycationBusinessLogic = applycationBusinessLogic;
        }
        
        // POST: api/Application
        [HttpPost("CreateApplication")]
        [Authorize("Candidate")]
        public async Task<IActionResult> CreateApplication([FromBody]CreateApplycationRequest application)
        {
            application.Email = User.Identity.Name;
            await _applycationBusinessLogic.CreateApplication(application);
            return this.Ok();
        }

        [HttpGet("IsExistApplyByJob/{id}")]
        public async Task<IActionResult> IsExistApplyByJob(string id)
        {
            string email = User.Identity.Name;
            object result = await _applycationBusinessLogic.IsExistApplyByJob(id,email);

            return this.OkResult(result);
        }

        [HttpGet("GetApplyByJob/{jobId}")]
        [Authorize("Company")]
        public async Task<IActionResult> GetApplyByJob(string jobId)
        {
            var result = await _applycationBusinessLogic.GetApplyByJob(jobId);
            return this.OkResult(result);
        }

        [HttpGet("GetApplyById/{applyId}")]
        [Authorize("Company")]
        public async Task<IActionResult> GetApplyById(string applyId)
        {
            var result = await _applycationBusinessLogic.GetApplyById(applyId);
            return this.OkResult(result);
        }
    }
}
