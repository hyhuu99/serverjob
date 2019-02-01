
using ApplicationLogic.Resumes;
using ApplicationLogic.Resumes.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Service.Controllers.ResumeController
{
    [Produces("application/json")]
    [Route("api/Resume")]

    public class ResumeController : Controller
    {
        private readonly IResumesBusinessLogic _resumeBusinessLogic;
        public ResumeController(IResumesBusinessLogic resumeBusinessLogic)
        {
            _resumeBusinessLogic = resumeBusinessLogic;
        }


        [Authorize("Candidate")]
        [HttpPost("CreateResumeStep1")]
        public async Task<IActionResult> CreateResume([FromBody]ResumeRequest resume)
        {

            resume.Email = User.Identity.Name;
            object id = await _resumeBusinessLogic.CreateResumes(resume);
            return this.OkResult(id);
        }

        [Authorize("Candidate")]
        [HttpPut("UpdateReumeStep")]
        public async Task<IActionResult> UpdateReume([FromBody]ResumeRequest resume)
        {

            resume.Email = User.Identity.Name;
            await _resumeBusinessLogic.UpdateResumes(resume);
            return this.OkResult();
        }

        [Authorize("Candidate")]
        [HttpGet("GetResumesByUser")]
        public async Task<IActionResult> GetResumesByUser()
        {

            string email = User.Identity.Name;
            List<ResumeRequest> result= await _resumeBusinessLogic.GetResumesByUser(email);
            if(result.Count==0)
            {
                return this.OkResult(null);
            }
            return this.OkResult(result[0]);
        }

        [HttpGet("GetResumesById/{id}")]
        public async Task<IActionResult> GetResumesById(string id)
        {
            var result = await _resumeBusinessLogic.GetResumesById(id);
            return this.OkResult(result);
        }

        [HttpPost("SearchResume")]
        public async Task<IActionResult> SearchResume([FromBody]SearchResumeRequest searchResume)
        {
            var result = await _resumeBusinessLogic.SearchResume(searchResume);
            return this.OkResult(result);
        }
    }
}
