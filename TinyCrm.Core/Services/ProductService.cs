using System;
using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly TinyCrmDbContext context;

        public ProductService(TinyCrmDbContext ctx)
        {
            context = ctx ??
                throw new ArgumentNullException(nameof(ctx));
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public Product AddProduct(AddProductOptions options)
        {
            if (options == null) {
                return null;
                    
            }

            var product = GetProductById(options.Id); 

            if (product != null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.Name)) {
                return null;
            }

            if (options.Price <= 0) {
                return null;
            }

            if (options.ProductCategory ==
              ProductCategory.Invalid) {
                return null;
            }

            product = new Product() {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

            context.Add(product);
            try {
                context.SaveChanges();
            } catch (Exception) {
                return null;
            }

            return product;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool UpdateProduct(string productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }

            var product = GetProductById(productId);
            if (product == null) { 
                return false; 
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return false;
            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return false;
                } else {
                    product.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0) {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProductById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return null;
            }

            return context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);
        }

        public IQueryable<Product> SearchProduct(SearchProductOptions options)
        {
            if (options == null) {
                return null;
            }

            var query = context
                .Set<Product>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.Id)) {
                query = query.Where(p =>
                    p.Id == options.Id);
            }

            if (!string.IsNullOrWhiteSpace(options.Name)) {
                query = query.Where(p =>
                    p.Name == options.Name);
            }

            if (options.Category  != ProductCategory.Invalid) {
                query = query.Where(p =>
                    p.Category == options.Category);
            }
            if (!string.IsNullOrWhiteSpace(options.Description)) {
                query = query.Where(p =>
                    p.Description == options.Description);
            }



            return query.Take(500);
        }
    }
}
