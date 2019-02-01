using ApplicationLogic.Companys;
using ApplicationLogic.Companys.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobBoard.Service.Controllers.Company
{
    [Produces("application/json")]
    [Route("api/Company")]
    //[Authorize("Company")]
    public class CompanyController : Controller
    {
        private readonly ICompanyBusinessLogic _companyBusinessLogic;
        public CompanyController(ICompanyBusinessLogic companyBusinessLogic)
        {
            _companyBusinessLogic = companyBusinessLogic;
        }


        [HttpGet("GetCompanyByEmail")]
        [Authorize("Company")]
        public async Task<IActionResult> GetCompanyByEmail()
        {
            string email = User.Identity.Name;
            var result = await _companyBusinessLogic.GetCompanyByEmail(email);
            return this.OkResult(result);
        }

        // POST: api/Company
        [HttpPost("CreateCompanyInfo")]
        [Authorize("Company")]
        public async Task<IActionResult> CreateCompanyInfo([FromBody]CreateCompanyRequest company)
        {
            company.Email = User.Identity.Name;
            await _companyBusinessLogic.CreateCompany(company);
            return this.OkResult();
        }
        
        // PUT: api/Company/5
        [HttpPut("UpdateCompanyInfo")]
        [Authorize("Company")]
        public async Task<IActionResult> UpdateCompanyInfo([FromBody]UpdateCompanyRequest company)
        {
            company.Email = User.Identity.Name;
            await _companyBusinessLogic.UpdateCompany(company);
            return this.OkResult();
        }

        [HttpGet("GetCompanyById/{id}")]
        public async Task<IActionResult> GetCompanyById(string id)
        {
            var result = await _companyBusinessLogic.GetCompanyById(id);
            return this.OkResult(result);
        }

    }
}
