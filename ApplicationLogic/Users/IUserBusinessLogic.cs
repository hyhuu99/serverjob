using ApplicationLogic.Users.Messages;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationLogic.Users
{
    public interface IUserBusinessLogic
    {
        Task<IdentityResult> CreateUserAccount(CreateUserRequest user);
        //Task<IdentityResult> ChangePassword(ChangePasswordRequest changePassword, ClaimsPrincipal claims);
        //Task<IdentityResult> ChangePhoneNumber(ChangeInfoRequest changeInfo, ClaimsPrincipal claims);
        //Task<IdentityResult> ChangeEmail(ChangeInfoRequest changeInfo, ClaimsPrincipal claims);
        //Task<bool> IsPasswordValidate(ChangeInfoRequest changeInfo, ClaimsPrincipal claims);
        Task<string> GetUserRoleByEmail(string email);
    }
}
