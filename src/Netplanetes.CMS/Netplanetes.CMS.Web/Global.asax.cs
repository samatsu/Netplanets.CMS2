using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;

namespace Netplanetes.CMS.Web
{
    public class Global : HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute
            {
                View = "HandleError"
            });
        }
        void Application_Start(object sender, EventArgs e)
        {
            // アプリケーションのスタートアップで実行するコードです
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterGlobalFilters(GlobalFilters.Filters);

            // DI用の Controller Factory を設定
            //DependencyResolver.SetResolver(new Netplanetes.CMS.Web.Infrastracture.NinjectDependencyResolver());
            //GlobalConfiguration.Configuration.DependencyResolver = new Netplanetes.CMS.Web.Infrastracture.NinjectDependencyResolvekr();
            //DependecyResolverをNInjectで実装した場合は、デフォルトのコントローラーファクトリーを使用してもOK
            //ControllerBuilder.Current.SetControllerFactory(new Netplanetes.CMS.Web.Infrastracture.NinjectControllerFactory());
            //InitializeData.Init();
        }
    }
}