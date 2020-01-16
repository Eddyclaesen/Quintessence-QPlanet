using System;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace Quintessence.QService.Business.Base
{
    public abstract class RepositoryBase<TContext> : IDisposable
    {
        private IUnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{TContext}"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        protected RepositoryBase(IUnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RepositoryBase{TContext}"/> class.
        /// </summary>
        ~RepositoryBase()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        protected IUnityContainer Container
        {
            get { return _container; }
        }

        /// <summary>
        /// Creates the context.
        /// </summary>
        /// <returns></returns>
        public virtual TContext CreateContext()
        {
            var context = Container.Resolve<TContext>();
            return context;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _container = null;
            }
        }
    }
}
