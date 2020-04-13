[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Netplanetes.CMS.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Netplanetes.CMS.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Netplanetes.CMS.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Netplanetes.CMS.Models.DomainModel;
    using Netplanetes.CMS.Web.Services;
    using CMS.Models.SearchModel;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IContentRepository>().To<EFCFContentRepository>();
            kernel.Bind<ISearchIndexProvider>().To<AzureSearchIndexProvider>();
            kernel.Bind<IContentService>().To<ContentService>();
            kernel.Bind<INavigationService>().To<NavigationService>().InRequestScope();
            kernel.Bind<ICategoryMappingService>().To<CategoryMappingService>().InRequestScope();
            kernel.Bind<IFeedService>().To<FeedService>();
            kernel.Bind<IContactService>().To<ContactService>();
            kernel.Bind<IAuthService>().To<AuthService>();
            kernel.Bind<IAccountService>().To<AuthService>();
            kernel.Bind<ISearchService>().To<SearchService>();
        }        
    }
}
