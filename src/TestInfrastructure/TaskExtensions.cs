using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestInfrastructure
{
    /// <summary>
    /// Test task extensions
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Gets the result and unwraps any exceptions from AggregateException.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="task">The task.</param>
        /// <returns>The result</returns>
        public static TResult GetResultAndUnwrapException<TResult>(this Task<TResult> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException ex)
            {
                Assert.AreEqual(1, ex.InnerExceptions.Count);
                throw ex.InnerException;
            }

        }

        /// <summary>
        /// Attaches an exception to a task
        /// </summary>
        /// <typeparam name="TResult">Type of task result</typeparam>
        /// <param name="ex">Exception</param>
        /// <returns>Task with exception</returns>
        public static Task<TResult> GetTask<TResult>(this Exception ex)
        {
            return Task<TResult>.Factory.StartNew(() => { throw ex; });
        }

    }
}
