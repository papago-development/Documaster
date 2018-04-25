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
        }
    }
}