using System;
using System.ServiceModel;
using System.Threading;

namespace Core.Common.ServiceModel
{
    public class UserClientBase<T> : ClientBase<T> where T : class
    {
        /// <summary>
        /// accessing soap header to transport username for our wcf services
        /// </summary>
        public UserClientBase()
        {
            this.CheckSoapHeader();
        }

        private void CheckSoapHeader()
        {
            var userName = Thread.CurrentPrincipal.Identity.Name;
            var header = new MessageHeader<string>(userName);

            var contextScope = new OperationContextScope(InnerChannel);
            OperationContext.Current.OutgoingMessageHeaders.Add(
                                      header.GetUntypedHeader("String", "System"));
        }        
    }
}
