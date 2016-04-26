using System.ComponentModel.Composition;
using System.Linq;
using Demo.Business.Entities;
using Demo.Data.Contracts;
using System.Collections.Generic;

namespace Demo.Data.DataRepositories
{
    [Export(typeof(IProductRepository))]
    public class ProductRepository : IProductRepository
    {
        #region Db

        public List<Product> Products = new List<Product>
        {
            new Product
            {
                ProductId = 1,
                ArticleNumber = "syrgsr",
                Description = "Test product 1",
                IsActive = true,
                Name = "Product 1",
                Price = 100
            },
            new Product
            {
                ProductId = 2,
                ArticleNumber = "vhjukjuh",
                Description = "Test product 2",
                IsActive = false,
                Name = "Product 2",
                Price = 200
            },
            new Product
            {
                ProductId = 3,
                ArticleNumber = "23545",
                Description = "Test product 3",
                IsActive = true,
                Name = "Product 3",
                Price = 300
            },
            new Product
            {
                ProductId = 4,
                ArticleNumber = "25545",
                Description = "Test product 4",
                IsActive = false,
                Name = "Product 4",
                Price = 400
            }
        };

        #endregion

        #region IProductRepository implementation

        public Product GetProductByArticleNumber(string articleNumber)
        {
            return Products.FirstOrDefault(p => p.ArticleNumber.Equals(articleNumber));
        }

        public Product GetProductById(int productId)
        {
            return Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public Product[] GetActiveProducts()
        {
            return Products.Where(p => p.IsActive).ToArray();
        }

        public Product[] GetProducts()
        {
            return Products.ToArray();
        }

        public void ActivateProduct(int productId)
        {
            var firstOrDefault = Products.FirstOrDefault(p => p.ProductId == productId);
            if (firstOrDefault != null)
                firstOrDefault.IsActive = true;
        }

        public void DeactivateProduct(int productId)
        {
            var firstOrDefault = Products.FirstOrDefault(p => p.ProductId == productId);
            if (firstOrDefault != null)
                firstOrDefault.IsActive = false;
        }

        public Product UpdateProduct(Product product)
        {
            var p = Products.FirstOrDefault(x => x.ProductId == product.ProductId);

            if (p == null)
            {
                p = new Product
                {
                    ArticleNumber = product.ArticleNumber,
                    Description = product.Description,
                    IsActive = product.IsActive,
                    Name = product.Name,
                    Price = product.Price
                };

                Products.Add(p);
            }
            else
            {
                p.IsActive = product.IsActive;
                p.Name = product.Name;
                p.Price = product.Price;
                p.ArticleNumber = product.ArticleNumber;
                p.Description = product.Description;
            }           

            return p;
        }

        #endregion
    }
}
