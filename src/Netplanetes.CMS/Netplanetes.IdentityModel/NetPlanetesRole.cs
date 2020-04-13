using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.IdentityModel
{
    public class NetPlanetesRole : IdentityRole
    {
        public NetPlanetesRole() : base() { }

        public NetPlanetesRole(string roleName) : base(roleName) { }
    }
}
