using Netplanetes.CMS.Models.DomainModel;
using Netplanetes.CMS.Web.Services;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Infrastracture
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _kernel;
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            InitBindings();
        }
        protected virtual void InitBindings()
        {
            _kernel.Bind<IContentRepository>().To<EFCFContentRepository>();
            _kernel.Bind<IContentService>().To<ContentService>();
            _kernel.Bind<INavigationService>().To<NavigationService>();
            _kernel.Bind<ICategoryMappingService>().To<CategoryMappingService>().InRequestScope();
            _kernel.Bind<IFeedService>().To<FeedService>();
        }
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return (controllerType == null) ? null : (IController) _kernel.Get(controllerType);
        }
    }
}