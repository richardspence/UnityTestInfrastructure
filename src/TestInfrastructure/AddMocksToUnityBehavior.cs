using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Moq;

namespace TestInfrastructure
{
    /// <summary>
    /// Automatically adds mocks created from a Mock Repositroy to unity.
    /// </summary>
    /// <remarks>
    /// This is a unity interception behavior that intercepts <see cref="MockRepository.Create<T>"/>
    /// </remarks>
    public class AddMocksToUnityBehavior : IInterceptionBehavior
    {
        IUnityContainer _container;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddMocksToUnityBehavior"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AddMocksToUnityBehavior(IUnityContainer container)
        {
            _container = container;
        }
        /// <summary>
        /// Returns the interfaces required by the behavior for the objects it intercepts.
        /// </summary>
        /// <returns>
        /// The required interfaces.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return new Type[] { typeof(IMockRepository) };
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //execute the next chain
            var result = getNext().Invoke(input, getNext);

            //if the method name is Create
            if (input.MethodBase.Name == "Create")
            {
                //let's get the generic argument for Create i.e. T in Create<T>
                var interfaceType = input.MethodBase.GetGenericArguments()[0];

                //set up ourlifetime to be controlled by the target object's disposed event
                var lifeTime = new ExternallyControlledLifetimeManager();
                var target = (IMockRepository)input.Target;
                target.Disposed += (s, e) => lifeTime.RemoveValue();

                //let's get our mock object
                var obj = result.ReturnValue as Mock;

                //let's register our mock with the container.                
                if (obj != null && obj.Object != null)
                {
                    _container.RegisterInstance(interfaceType, obj.Object, lifeTime);
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a flag indicating if this behavior will actually do anything when invoked.
        /// </summary>
        /// <remarks>
        /// This is used to optimize interception. If the behaviors won't actually
        ///             do anything (for example, PIAB where no policies match) then the interception
        ///             mechanism can be skipped completely.
        /// </remarks>
        public bool WillExecute
        {
            get { return true; }
        }
    }
}
