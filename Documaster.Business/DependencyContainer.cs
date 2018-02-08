using System;
using Documaster.Business.Services;
using Documaster.Data.DataAccess;
using Unity;

namespace Documaster.Business
{
    public static class DependencyContainer
    {
        private static readonly Lazy<IUnityContainer> _unityContainer = new Lazy<IUnityContainer>( () =>
        {
            var container = new UnityContainer();
            RegisterTypes( container );
            return container;
        } );

        public static IUnityContainer Instance => _unityContainer.Value;

        public static T Resolve<T>() => _unityContainer.Value.Resolve<T>();

        public static void RegisterInstance<T>( T instance ) => _unityContainer.Value.RegisterInstance( instance );

        public static object Resolve( Type t ) => _unityContainer.Value.Resolve( t );

        public static T Resolve<T>( string name ) => _unityContainer.Value.Resolve<T>( name );

        public static void RegisterTypes( IUnityContainer unityContainer )
        {
            unityContainer.RegisterType( typeof( IDbContext ), typeof( DocumasterDbContext ) );
            unityContainer.RegisterType( typeof( IGenericEntityService<> ), typeof( GenericEntityService<> ) );
        }
    }
}