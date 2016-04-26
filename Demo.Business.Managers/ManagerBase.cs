using System;
using System.ComponentModel.Composition;
using System.ServiceModel;
using Core.Common.Core;

namespace Demo.Business.Managers
{
    public class ManagerBase
    {
        /// <summary>
        /// post construction resolving the dependencies
        /// </summary>
        protected ManagerBase()
        {
            if (ObjectBase.Container != null)
            {
                ObjectBase.Container.SatisfyImportsOnce(this);
            }
        }

        /// <summary>
        /// centralized exception handling for service managers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operation"></param>
        /// <returns></returns>
        protected T ExecuteFaultHandledOperation<T>(Func<T> operation)
        {
            try
            {
                return operation.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                // log to db
                // ...

                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// centralized exception handling for service managers
        /// </summary>
        /// <param name="operation"></param>
        protected void ExecuteFaultHandledOperation(Action operation)
        {
            try
            {
                operation.Invoke();
            }
            catch (FaultException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                // log to db
                // ...

                throw new FaultException(ex.Message);
            }
        }
    }
}
