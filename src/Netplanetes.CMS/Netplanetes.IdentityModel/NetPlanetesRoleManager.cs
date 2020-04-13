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
    public class NetPlanetesRoleManager : RoleManager<NetPlanetesRole>
    {
        public NetPlanetesRoleManager(RoleStore<NetPlanetesRole> store)
            : base(store)
        {
        }

        public static NetPlanetesRoleManager Create(IdentityFactoryOptions<NetPlanetesRoleManager> options,IOwinContext context)
        {
            return new NetPlanetesRoleManager(new RoleStore<NetPlanetesRole>(context.Get<NetPlanetesIdentityDbContext>()));
        }
    }
}