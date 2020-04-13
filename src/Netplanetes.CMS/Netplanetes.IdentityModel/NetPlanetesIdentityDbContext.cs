using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netplanetes.IdentityModel
{
    public class NetPlanetesIdentityDbContext : IdentityDbContext<NetPlanetesUser>
    {
        public NetPlanetesIdentityDbContext() : base("IdentityDb") { }

        static NetPlanetesIdentityDbContext()
        {
            Database.SetInitializer<NetPlanetesIdentityDbContext>(new IdentityDbInit());
        }
        public static NetPlanetesIdentityDbContext Create()
        {
            return new NetPlanetesIdentityDbContext();
        }
    }
    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<NetPlanetesIdentityDbContext>
    {
        protected override void Seed(NetPlanetesIdentityDbContext context)
        {
            base.Seed(context);
            BuildInitialData(context);
        }
        public void BuildInitialData(NetPlanetesIdentityDbContext context)
        {
            NetPlanetesUserManager manager = new NetPlanetesUserManager(new UserStore<NetPlanetesUser>(context));

            string username = ConfigurationManager.AppSettings["np:MasterName"] ?? "cmsadmin";
            string email = ConfigurationManager.AppSettings["np:MasterEmail"] ?? "cmsadmin@dev.local";
            string password = ConfigurationManager.AppSettings["np:MasterPassword"] ?? "cmsadminPassword!";
            NetPlanetesUser user = new NetPlanetesUser
            {
                UserName = username,
                Email = email,
            };
            var result = manager.CreateAsync(user, password);
            result.Wait();
            return;

        }
    }
}
