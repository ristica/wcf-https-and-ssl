using System;
using Core.Common.Core;
using Demo.Business.Bootstrapper;

namespace Demo.WebHost
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ObjectBase.Container = MefLoader.Init();
        }
    }
}