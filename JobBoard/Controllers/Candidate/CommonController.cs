using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLogic.Commons;
using ApplicationLogic.Commons.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Mvc;

namespace JobBoard.Service.Controllers.Candidate
{
    [Produces("application/json")]
    [Route("api/Common")]
    public class CommonController : Controller
    {
        private readonly ICommonBusinessLogic _commonBusinessLogic;
        public CommonController(ICommonBusinessLogic commonBusinessLogic)
        {
            _commonBusinessLogic = commonBusinessLogic;
        }

        // GET: api/Common/5
        [HttpGet("GetAllForStandart")]
        public async Task<IActionResult> GetAllForStandart()
        {
            var data = await _commonBusinessLogic.GetAllForStandardForm();
            return this.OkResult(data);
        }

        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            var data = await _commonBusinessLogic.GetAllCategory();
            return this.OkResult(data);
        }
        //[HttpGet("GetYearOfWork")]
        //public JsonResult GetYearOfWork()
        //{
        //    var listYear = new List<CommonRequest>();
        //    for (int i = 0; i <= 50; i++)
        //    {
        //        CommonRequest data = new CommonRequest();
        //        data.Name = i.ToString();
        //        data.Code = i.ToString();
        //        listYear.Add(data);
        //    }
        //    return Json(listYear);
        //}

    }
}
