using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Netplanetes.IdentityModel;
using Netplanetes.CMS.Web.Models.Account;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Netplanetes.CMS.Web.Areas.Admin.Models;

namespace Netplanetes.CMS.Web.Services
{
    public class AuthService : IAuthService, IAccountService
    {
        public async Task<IdentityResult> CreateUser(CreateUserViewModel model)
        {
            NetPlanetesUser user = new NetPlanetesUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            return result;
        }
        public async Task<EditUserViewModel> FindUserById(string userId)
        {
            NetPlanetesUser user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                EditUserViewModel model = new EditUserViewModel()
                {
                    ID = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return model;
            }
            return null;
        }
        public IEnumerable<EditUserViewModel> RetrieveUsers()
        {
            List<EditUserViewModel> list = new List<EditUserViewModel>();
            return UserManager.Users.Select(x => new EditUserViewModel { ID = x.Id, Email = x.Email, UserName = x.UserName }).ToList();
        }
        public async Task<IdentityResult> UpdateUser(EditUserViewModel model)
        {
            NetPlanetesUser user = await UserManager.FindByIdAsync(model.ID);
            if (user != null)
            {
                user.Email = model.Email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    return validEmail;
                }
                IdentityResult result = await UserManager.UpdateAsync(user);
                return result;
            }
            return IdentityResult.Failed("User not found");
        }
        public async Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            return await UserManager.ChangePasswordAsync(userId, currentPassword, newPassword);
        }
        public async Task<IdentityResult> DeleteUser(string id)
        {
            NetPlanetesUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                return result;
            }
            return IdentityResult.Failed();
        }
        public async Task<ClaimsIdentity> CreateIdentity(LoginViewModel model)
        {
            NetPlanetesUser user = await UserManager.FindAsync(model.UserName, model.Password);
            if (user == null) return null;

            ClaimsIdentity identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }
        private NetPlanetesUserManager UserManager
        {
            get
            {
                // この実装は別に NInject でも十分対応可能
                return HttpContext.Current.GetOwinContext().GetUserManager<NetPlanetesUserManager>();
            }
        }
    }
}