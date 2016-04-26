using System;
using System.ComponentModel.Composition;
using System.Linq;
using Demo.Business.Common;

namespace Demo.Business.Business_Engines
{
    [Export(typeof(IProductInventoryEngine))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProductInventoryEngine : IProductInventoryEngine
    {
        public string GenerateArticleNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
