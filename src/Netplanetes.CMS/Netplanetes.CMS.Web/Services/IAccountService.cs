using Microsoft.AspNet.Identity;
using Netplanetes.CMS.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    public interface IAccountService
    {
        Task<EditUserViewModel> FindUserById(string userId);
        IEnumerable<EditUserViewModel> RetrieveUsers();
        Task<IdentityResult> ChangePassword(string userId, string currentPassword, string newPassword);
        Task<IdentityResult> CreateUser(CreateUserViewModel model);
        Task<IdentityResult> UpdateUser(EditUserViewModel model);
        Task<IdentityResult> DeleteUser(string id);
    }
}
