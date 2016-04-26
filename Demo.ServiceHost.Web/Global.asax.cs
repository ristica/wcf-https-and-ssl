using Core.Common.Core;
using Demo.Business.Bootstrapper;
using System;

namespace Demo.ServiceHost.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ObjectBase.Container = MefLoader.Init();
        }
    }
}