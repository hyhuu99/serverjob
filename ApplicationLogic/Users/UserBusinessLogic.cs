using ApplicationLogic.Users.Messages;
using AspNetCore.Identity.MongoDbCore.Models;
using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Repository;
using Shared;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApplicationLogic.Users
{
    public class UserBusinessLogic : IUserBusinessLogic
    {
        
        private readonly IMongoDbRepository _mongoDbRepository;
        UserManager<ApplicationUser> _userManager;
        public UserBusinessLogic(IMapper mapper, 
            IMongoDbRepository mongoDbRepository,
            UserManager<ApplicationUser> userManager)
        {
            _mongoDbRepository = mongoDbRepository;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAccount(CreateUserRequest user)
        {
            ApplicationUser applicationUser = new ApplicationUser();
            applicationUser.Email = user.Email;
            applicationUser.UserName = user.Email;
            var userIdentity = new ApplicationUser { UserName = user.Email, Email = user.Email};
            var result = await _userManager.CreateAsync(userIdentity, user.Password);
            if(result.Succeeded)
            {
                CreateUserRole(user.Email, user.IsHr);
                return result;
            }
            return result;
        }


        //public async Task<IdentityResult> ChangePassword(ChangePasswordRequest changePassword,ClaimsPrincipal claims)
        //{
        //    var userModel = await _userManager.GetUserAsync(claims);
        //    var result = await _userManager.ChangePasswordAsync(userModel, changePassword.OldPassword,changePassword.NewPassword);
        //    return result;
        //}

        //public async Task<IdentityResult> ChangePhoneNumber(ChangeInfoRequest changeInfo, ClaimsPrincipal claims)
        //{
        //    var userModel = await _userManager.GetUserAsync(claims);
        //    var code = await _userManager.GenerateChangePhoneNumberTokenAsync(userModel, changeInfo.PhoneNumber);
        //    var result = await _userManager.ChangePasswordAsync(userModel, changeInfo.PhoneNumber, code);
        //    return result;
        //}

        //public async Task<IdentityResult> ChangeEmail(ChangeInfoRequest changeInfo, ClaimsPrincipal claims)
        //{
        //    var userModel = await _userManager.GetUserAsync(claims);
        //    var code = await _userManager.GenerateChangeEmailTokenAsync(userModel, changeInfo.Email);
        //    var result = await _userManager.ChangePasswordAsync(userModel, changeInfo.Email, code);
        //    return result;
        //}

        //public async Task<bool> IsPasswordValidate (ChangeInfoRequest changeInfo, ClaimsPrincipal claims)
        //{
        //    var userModel = await _userManager.GetUserAsync(claims);
        //    var result = await _userManager.CheckPasswordAsync(userModel, changeInfo.Password);
        //    return result;
        //}


        public async Task<string> GetUserRoleByEmail(string email)
        {
            var filter = Builders<UserRole>.Filter.Where(x => x.Email == email);
            IList<UserRole> listUserRole = await _mongoDbRepository.Find<UserRole>(filter);
            if( listUserRole.Count > 0)
            {
                return listUserRole.FirstOrDefault().RoleName;
            }
            return "";
        }

        private void CreateUserRole(string email,bool isHr)
        {
            UserRole userRole = new UserRole();
            userRole.Id= IdGeneratorHelper.IdGenerator();
            userRole.Email = email;
            if (isHr)
            {
                userRole.RoleName = EUserRole.Hr.ToString();
                _mongoDbRepository.Create<UserRole>(userRole);
                Company company = new Company();
                company.Id = IdGeneratorHelper.IdGenerator();
                company.Email = email;
                _mongoDbRepository.Create<Company>(company);
            }
            else
            {
                userRole.RoleName = EUserRole.Candidate.ToString();
                _mongoDbRepository.Create<UserRole>(userRole);
            }
        }
    }
}
