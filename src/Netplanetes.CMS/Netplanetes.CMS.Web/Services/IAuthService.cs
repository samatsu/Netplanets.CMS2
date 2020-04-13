using Netplanetes.CMS.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.CMS.Web.Services
{
    /// <summary>
    /// ユーザー名とパスワードで認証を行う認証サービス
    /// </summary>
    public interface IAuthService
    {
        Task<ClaimsIdentity> CreateIdentity(LoginViewModel model);
    }
}
