using ApplicationLogic.Jobs;
using ApplicationLogic.Jobs.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Service.Controllers.Company
{
    [Produces("application/json")]
    [Route("api/job")]
    //[Authorize("Company")]
    public class JobController : Controller
    {
        
        private readonly IJobBusinessLogic _jobBusinessLogic;
        public JobController(IJobBusinessLogic jobBusinessLogic)
        {
            _jobBusinessLogic = jobBusinessLogic;
        }

        [HttpGet("jobs")]
        public async Task<IActionResult> GetAllJob(GetJobsRequest request)
        {
            var result = await _jobBusinessLogic.GetJobs(request);
            return Ok(result);
        }

        [HttpGet("GetJobsByCompanyId")]
        [Authorize("Company")]
        public async Task<IActionResult> GetJobsByCompanyId(string companyId)
        {
            var result = await _jobBusinessLogic.GetJobsByCompanyId(companyId);
            return Ok(result);
        }

        [HttpGet("GetJobById/{id}")]
        public async Task<IActionResult> GetJobById(string id)
        {
            var result = await _jobBusinessLogic.GetJobById(id);
            return this.OkResult(result);
        }

        // POST: api/Job
        [HttpPost("CreateJob")]
        [Authorize("Company")]
        public async Task<IActionResult> CreateJob([FromBody]CreateJobRequest job)
        {
            job.Email = User.Identity.Name;
            await _jobBusinessLogic.CreateJob(job);
            return this.OkResult();
        }
        
        // PUT: api/Job/5
        [HttpPut("UpdateJob")]
        [Authorize("Company")]
        public async Task<IActionResult> UpdateJob([FromBody]UpdateJobRequest job)
        {
            job.Email = User.Identity.Name;
            await _jobBusinessLogic.UpdateJob(job);
            return this.OkResult();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpGet("DeleteJob/{id}")]
        [Authorize("Company")]
        public async Task<IActionResult> DeleteJob(string id)
        {
            await _jobBusinessLogic.DeleteJob(id);
            return this.OkResult();
        }

        [HttpGet("jobLocations")]
        public async Task<IActionResult> GetJobsByLocation()
        {
            var result = await _jobBusinessLogic.GetJobLocations();
            return Ok(result);
        }

        [HttpGet("jobCategories")]
        public async Task<IActionResult> GetJobCategories()
        {
            var result = await _jobBusinessLogic.GetJobCategories();
            return Ok(result);
        }

        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            var result = await _jobBusinessLogic.GetLocations();
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _jobBusinessLogic.GetCategories();
            return Ok(result);
        }
    }
}
