using Core.Common.Core;
using Demo.Business.Bootstrapper;
using Demo.Business.Managers;

namespace Demo.ServiceHost.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // set up the dependency container because instantiating
            // shopping manager and dependencies of it (ShoppingManager)
            ObjectBase.Container = MefLoader.Init();

            System.Console.WriteLine("-------------------------------------------------");
            System.Console.WriteLine("");
            System.Console.WriteLine("     Starting up services");
            System.Console.WriteLine("");
            System.Console.WriteLine("-------------------------------------------------");
            System.Console.WriteLine("");

            // initalize the service manager           
            var hostInventoryManager = new System.ServiceModel.ServiceHost(typeof(InventoryManager));

            // start the service manager
            StartService(hostInventoryManager, "InventoryManager");
            System.Console.WriteLine("");
            System.Console.ReadKey();

            StopService(hostInventoryManager, "InventoryManager");

            System.Console.WriteLine("");
            System.Console.ReadKey();
        }

        /// <summary>
        /// logging the service manager parameter
        /// </summary>
        /// <param name="host"></param>
        /// <param name="service"></param>
        private static void StartService(System.ServiceModel.ServiceHost host, string service)
        {
            host.Open();
            System.Console.WriteLine("     => {0} started...", service);

            foreach (var endpoint in host.Description.Endpoints)
            {
                System.Console.WriteLine("     => Listening on endpoint:");
                System.Console.WriteLine("");
                System.Console.WriteLine("\t    Address : {0}", endpoint.Address.Uri);
                System.Console.WriteLine("\t    Binding : {0}", endpoint.Binding.Name);
                System.Console.WriteLine("\t    Contract: {0}", endpoint.Contract.ConfigurationName);
            }

            System.Console.WriteLine("");
            System.Console.WriteLine("-------------------------------------------------");
        }

        /// <summary>
        /// do some housekeeping
        /// </summary>
        /// <param name="host"></param>
        /// <param name="serviceDescription"></param>
        private static void StopService(System.ServiceModel.ServiceHost host, string serviceDescription)
        {
            // do not abort!!!
            host.Close();
            System.Console.WriteLine("Service {0} stopped.", serviceDescription);
        }
    }
}
