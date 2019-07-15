using System;
using Documaster.Business.Services;
using Documaster.Business.Wrappers;
using Documaster.Data.DataAccess;
using Unity;
using Unity.AspNet.Mvc;

namespace Documaster.Business
{
    public static class DependencyContainer
    {
        private static readonly Lazy<IUnityContainer> _unityContainer = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static IUnityContainer Instance => _unityContainer.Value;

        public static T Resolve<T>() => _unityContainer.Value.Resolve<T>();

        public static void RegisterInstance<T>(T instance) => _unityContainer.Value.RegisterInstance(instance);

        public static object Resolve(Type t) => _unityContainer.Value.Resolve(t);

        public static T Resolve<T>(string name) => _unityContainer.Value.Resolve<T>(name);

        public static void RegisterTypes(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType(typeof(IDbContext), typeof(DocumasterDbContext), new PerRequestLifetimeManager());
            unityContainer.RegisterType(typeof(IGenericRepository<>), typeof(GenericRepository<>), new PerRequestLifetimeManager());
            unityContainer.RegisterType(typeof(IDependencyContainerWrapper), typeof(DependencyContainerWrapper), new PerRequestLifetimeManager());
            unityContainer.RegisterType(typeof(IUnitOfWork), typeof(UnitOfWork), new PerRequestLifetimeManager());

            //Category
            unityContainer.RegisterType(typeof(ICategoryService), typeof(CategoryService), new PerRequestLifetimeManager());

            //Project
            unityContainer.RegisterType(typeof(IProjectService), typeof(ProjectService), new PerRequestLifetimeManager());

            //ProjectRequirement
            unityContainer.RegisterType(typeof(IProjectRequirementService), typeof(ProjectRequirementService), new PerRequestLifetimeManager());

            //Customer
            unityContainer.RegisterType(typeof(ICustomerService), typeof(CustomerService), new PerRequestLifetimeManager());

            //Requirement
            unityContainer.RegisterType(typeof(IRequirementService), typeof(RequirementService), new PerRequestLifetimeManager());

            //OutputDocument
            unityContainer.RegisterType(typeof(IOutputDocumentService), typeof(OutputDocumentService), new PerRequestLifetimeManager());

            //ProjectStatus
            unityContainer.RegisterType(typeof(IProjectStatusService), typeof(ProjectStatusService), new PerRequestLifetimeManager());

            //NameEntity
            unityContainer.RegisterType(typeof(INamedEntityService<>), typeof(NamedEntityService<>), new PerRequestLifetimeManager());

            //Security
            unityContainer.RegisterType(typeof(ISecurityService), typeof(SecurityService), new PerRequestLifetimeManager());

            //Customize Tab
            unityContainer.RegisterType(typeof(ICustomizeTabService), typeof(CustomizeTabService), new PerRequestLifetimeManager());

            //Template service
            unityContainer.RegisterType(typeof(ITemplateService), typeof(TemplateService), new PerRequestLifetimeManager());

            //Project Template service
            unityContainer.RegisterType(typeof(IProjectTemplateService), typeof(ProjectTemplateService), new PerRequestLifetimeManager());

            //Replace placehorder service
            unityContainer.RegisterType(typeof(IReplacePlaceholderService), typeof(ReplacePlaceholderService), new PerRequestLifetimeManager());
        }
    }
}