using Netplanetes.CMS.Models.DomainModel;
using Netplanetes.CMS.Web.Services;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Netplanetes.CMS.Web.Infrastracture
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {

            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        public IBindingToSyntax<T> Bind<T>()
        {
            return _kernel.Bind<T>();
        }
        public IKernel Kernel
        {
            get { return _kernel; }
        }
        private void AddBindings()
        {
            _kernel.Bind<IContentRepository>().To<EFCFContentRepository>();
            _kernel.Bind<IContentService>().To<ContentService>();
            _kernel.Bind<INavigationService>().To<NavigationService>();
            _kernel.Bind<ICategoryMappingService>().To<CategoryMappingService>().InRequestScope();
            _kernel.Bind<IFeedService>().To<FeedService>();
        }

    }
}