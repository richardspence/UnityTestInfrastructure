using Microsoft.Practices.Unity;

namespace TestInfrastructure
{
    public interface IContainerSink
    {
        IUnityContainer Container { get; }
    }
}