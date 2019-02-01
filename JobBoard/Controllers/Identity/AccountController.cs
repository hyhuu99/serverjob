using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationLogic.Users;
using ApplicationLogic.Users.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;
using Shared.Mvc;

namespace JobBoard.Service.Controllers.Identity
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IUserBusinessLogic _usersBusinessLogic;

        public AccountController(IUserBusinessLogic UsersBusinessLogic)
        {
            _usersBusinessLogic = UsersBusinessLogic;
        }

        // POST: api/Account
        [HttpPost("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody]CreateUserRequest user)
        {
            var result = await _usersBusinessLogic.CreateUserAccount(user);
            if (result.Succeeded)
            {
                return this.OkResult();
            }
            else
            {
                if (result.Errors.Select(x => x.Code == ErrorCode.DuplicateEmail.ToString()).Any())
                    return this.ExceptionResult(ErrorCode.DuplicateEmail);
                else return this.ExceptionResult(ErrorCode.INVALID_DATA);
            }
        }

        [HttpGet("GetUserRole/{id}")]
        public async Task<IActionResult> GetUserRole(string id)
        {
            var result = await _usersBusinessLogic.GetUserRoleByEmail(id);
            return this.OkResult(result);
        }
    }
}
