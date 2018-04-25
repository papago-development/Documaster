using System;

namespace Documaster.Business.Wrappers
{
    public class DependencyContainerWrapper : IDependencyContainerWrapper
    {
        public object Resolve(Type type) => DependencyContainer.Resolve(type);
        public T Resolve<T>() => DependencyContainer.Resolve<T>();
        public T Resolve<T>(string name) => DependencyContainer.Resolve<T>(name);
    }
}