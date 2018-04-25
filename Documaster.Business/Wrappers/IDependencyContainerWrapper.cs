using System;

namespace Documaster.Business.Wrappers
{
    public interface IDependencyContainerWrapper
    {
        object Resolve(Type t);
        T Resolve<T>();
        T Resolve<T>(string name);
    }
}