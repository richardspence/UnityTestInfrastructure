using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Moq;

namespace TestInfrastructure
{
    /// <summary>
    /// Factory for retrieving the MockRepository
    /// </summary>
    public static class TestMockFactory
    {
        ///// <summary>
        ///// Creates the repository using the specified behavior.
        ///// </summary>
        ///// <param name="behavior">The behavior.</param>
        ///// <returns>The MockRepository</returns>
        //public static IMockRepository Create(MockBehavior behavior = MockBehavior.Default)
        //{
        //    //var container = UnityFactory.GetContainer();
        //    //container.AddNewExtension<Interception>();
        //    //return Create(container, behavior);
        //}

        public static IMockRepository Create(IUnityContainer container, MockBehavior behavior = MockBehavior.Default)
        {
            //register with unity with lifetime when it disposes
            container.RegisterType<IMockRepository, TestMockRepository>(
                new ContainerControlledLifetimeManager()
                , new Interceptor<InterfaceInterceptor>()
                , new InterceptionBehavior<AddMocksToUnityBehavior>()
                , new InjectionConstructor(behavior));

            //resolve the repository
            return container.Resolve<IMockRepository>();
        }



    }
}
