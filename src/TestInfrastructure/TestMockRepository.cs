using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Practices.Unity;

namespace TestInfrastructure
{
    /// <summary>
    /// The test mock repository
    /// </summary>
    /// <remarks>
    /// Extends MockRepositroy, and exposes an event for disposing.
    /// </remarks>
    public class TestMockRepository : MockRepository, IMockRepository
    {
        // Summary:
        //     Initializes the repository with the given defaultBehavior for newly created
        //     mocks from the repository.
        //
        // Parameters:
        //   defaultBehavior:
        //     The behavior to use for mocks created using the Moq.MockFactory.Create<T0>()
        //     repository method if not overriden by using the Moq.MockFactory.Create<T0>(Moq.MockBehavior)
        //     overload.
        public TestMockRepository(MockBehavior defaultBehavior)
            : base(defaultBehavior)
        {

        }

        [OptionalDependency]
        public IUnityContainer Container { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            OnDisposed();
        }

        /// <summary>
        /// Called when [disposed].
        /// </summary>
        protected virtual void OnDisposed()
        {
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Occurs when [disposed].
        /// </summary>
        public event EventHandler Disposed;
    }
}
