using System.ComponentModel.Composition.Hosting;
using Demo.Business.Business_Engines;
using Demo.Data.DataRepositories;

namespace Demo.Business.Bootstrapper
{
    public static class MefLoader
    {
        public static CompositionContainer Init()
        {
            var catalog = new AggregateCatalog();

            // it is enough to just reference repository assembly
            // because through eploring of the dependencies
            // of it, MEF will find the rest
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ProductRepository).Assembly));
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(ProductInventoryEngine).Assembly));

            var container = new CompositionContainer(catalog);

            return container;
        }
    }
}