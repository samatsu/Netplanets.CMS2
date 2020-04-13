using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Netplanetes.IdentityModel
{
    public class NetPlanetesUserManager : UserManager<NetPlanetesUser>
    {
        public NetPlanetesUserManager(IUserStore<NetPlanetesUser> store)
            : base(store)
        {
        }
        public static NetPlanetesUserManager Create(IdentityFactoryOptions<NetPlanetesUserManager> options,IOwinContext context)
        {
            NetPlanetesIdentityDbContext db = context.Get<NetPlanetesIdentityDbContext>();
            NetPlanetesUserManager manager = new NetPlanetesUserManager(new UserStore<NetPlanetesUser>(db));
            manager.UserValidator = new UserValidator<NetPlanetesUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };
            return manager;
        }
    }
}