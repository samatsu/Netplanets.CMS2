using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Netplanetes.IdentityModel;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.CMS.Web
{
    /// <summary>
    /// ASP.NET Identity を使用するための初期化処理. 他と同じようにNInjectを使っても実装できるがOwinを使用してみるテストもかねて
    /// Owinを使ってUserManager,RoleMaanger,DbContextを初期化する
    /// </summary>
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<NetPlanetesIdentityDbContext>(NetPlanetesIdentityDbContext.Create);
            app.CreatePerOwinContext<NetPlanetesUserManager>(NetPlanetesUserManager.Create);
            app.CreatePerOwinContext<NetPlanetesRoleManager>(NetPlanetesRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}