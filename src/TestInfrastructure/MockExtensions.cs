using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.Language;
using Moq.Language.Flow;

namespace TestInfrastructure
{
    /// <summary>
    /// Mock extensions
    /// </summary>
    public static class MockExtensions
    {
        /// <summary>
        /// Throws the exception as part of the task result
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="ex">The ex.</param>
        /// <returns>The task result that will cause an <see cref="AggregateException"/> with <paramref name="ex"/> as the inner exception.</returns>
        public static IReturnsResult<TMock> ThrowsException<TMock, TResult>(this IReturns<TMock, Task<TResult>> mock, Exception ex)
            where TMock : class
        {
            return mock.Returns(ex.GetTask<TResult>());
        }

        /// <summary>
        /// Returns the specified result as a task.
        /// </summary>
        /// <typeparam name="TMock">The type of the mock.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="mock">The mock.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static IReturnsResult<TMock> Returns<TMock, TResult>(this IReturns<TMock, Task<TResult>> mock, TResult result)
            where TMock : class
        {
            return mock.Returns(Task.FromResult(result));
        }
    }
}
