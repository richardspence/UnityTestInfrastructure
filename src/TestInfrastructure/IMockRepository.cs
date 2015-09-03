using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Moq;

namespace TestInfrastructure
{
    /// <summary>
    /// Mock repository interface.
    /// </summary>
    /// <remarks>
    /// This interface is pulled from <see cref="MockRepository"/>, but also contains an event for disposing.
    /// </remarks>
    public interface IMockRepository : IDisposable
    {
        // Summary:
        //     Access the universe of mocks of the given type, to retrieve those that behave
        //     according to the LINQ query specification.
        //
        // Type parameters:
        //   T:
        //     The type of the mocked object to query.
        IQueryable<T> Of<T>() where T : class;
        //
        // Summary:
        //     Access the universe of mocks of the given type, to retrieve those that behave
        //     according to the LINQ query specification.
        //
        // Parameters:
        //   specification:
        //     The predicate with the setup expressions.
        //
        // Type parameters:
        //   T:
        //     The type of the mocked object to query.
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By design")]
        IQueryable<T> Of<T>(Expression<System.Func<T, bool>> specification) where T : class;
        //
        // Summary:
        //     Creates an mock object of the indicated type.
        //
        // Type parameters:
        //   T:
        //     The type of the mocked object.
        //
        // Returns:
        //     The mocked object created.
        [EditorBrowsable(EditorBrowsableState.Never)]
        T OneOf<T>() where T : class;
        //
        // Summary:
        //     Creates an mock object of the indicated type.
        //
        // Parameters:
        //   specification:
        //     The predicate with the setup expressions.
        //
        // Type parameters:
        //   T:
        //     The type of the mocked object.
        //
        // Returns:
        //     The mocked object created.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "By Design")]
        T OneOf<T>(Expression<System.Func<T, bool>> specification) where T : class;


        // Summary:
        //     Creates a new mock with the default Moq.MockBehavior specified at factory
        //     construction time.
        //
        // Type parameters:
        //   T:
        //     Type to mock.
        //
        // Returns:
        //     A new Moq.Mock<T>.
        Mock<T> Create<T>() where T : class;
        //
        // Summary:
        //     Creates a new mock with the given behavior.
        //
        // Parameters:
        //   behavior:
        //     Behavior to use for the mock, which overrides the default behavior specified
        //     at factory construction time.
        //
        // Type parameters:
        //   T:
        //     Type to mock.
        //
        // Returns:
        //     A new Moq.Mock<T>.
        Mock<T> Create<T>(MockBehavior behavior) where T : class;
        //
        // Summary:
        //     Creates a new mock with the default Moq.MockBehavior specified at factory
        //     construction time and with the the given constructor arguments for the class.
        //
        // Parameters:
        //   args:
        //     Constructor arguments for mocked classes.
        //
        // Type parameters:
        //   T:
        //     Type to mock.
        //
        // Returns:
        //     A new Moq.Mock<T>.
        //
        // Remarks:
        //     The mock will try to find the best match constructor given the constructor
        //     arguments, and invoke that to initialize the instance. This applies only
        //     to classes, not interfaces.
        Mock<T> Create<T>(params object[] args) where T : class;
        //
        // Summary:
        //     Creates a new mock with the given behavior and with the the given constructor
        //     arguments for the class.
        //
        // Parameters:
        //   behavior:
        //     Behavior to use for the mock, which overrides the default behavior specified
        //     at factory construction time.
        //
        //   args:
        //     Constructor arguments for mocked classes.
        //
        // Type parameters:
        //   T:
        //     Type to mock.
        //
        // Returns:
        //     A new Moq.Mock<T>.
        //
        // Remarks:
        //     The mock will try to find the best match constructor given the constructor
        //     arguments, and invoke that to initialize the instance. This applies only
        //     to classes, not interfaces.
        Mock<T> Create<T>(MockBehavior behavior, params object[] args) where T : class;

        //
        // Summary:
        //     Verifies all verifiable expectations on all mocks created by this factory.
        //
        // Exceptions:
        //   Moq.MockException:
        //     One or more mocks had expectations that were not satisfied.
        void Verify();
        //
        // Summary:
        //     Verifies all verifiable expectations on all mocks created by this factory.
        //
        // Exceptions:
        //   Moq.MockException:
        //     One or more mocks had expectations that were not satisfied.
        void VerifyAll();


        /// <summary>
        /// Occurs when [disposed].
        /// </summary>
        event EventHandler Disposed;

    }
}
