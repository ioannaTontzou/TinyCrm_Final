using System;
using System.Linq;
using System.Collections.Generic;

using TinyCrm.Core.Data;
using TinyCrm.Core.Model;
using TinyCrm.Core.Model.Options;
using System.Threading.Tasks;
using TinyCrm.Core;

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

        
        public async Task<ApiResult<Product>>  AddProductAsync(AddProductOptions options)
        {
            if (options == null) {
                return new ApiResult<Product>(
                    StatusCodecs.BADREQUEST, "null options");
            }

            var product = GetProductById(options.Id); 

            if (product != null) {
                return new ApiResult<Product>(
                  StatusCodecs.Conflict, "Already Exist");
            }

            if (string.IsNullOrWhiteSpace(options.Name)) {
                return new ApiResult<Product>(
                 StatusCodecs.BADREQUEST, $"null {options}");
            }

            if (options.Price <= 0) {
                return new ApiResult<Product>(
                 StatusCodecs.BADREQUEST, "not valid Price");
            }

            if (options.ProductCategory ==
              ProductCategory.Invalid) {
                return new ApiResult<Product>(
                 StatusCodecs.BADREQUEST, $"null {options}");
            }

            product.Data = new Product() {
                Id = options.Id,
                Name = options.Name,
                Price = options.Price,
                Category = options.ProductCategory
            };

           await context.AddAsync(product);
            try {
                await context.SaveChangesAsync();
            } catch (Exception) {
                return new ApiResult<Product>(
                 StatusCodecs.InternalServerError, "problem save data");
            }

            return ApiResult<Product>.CreateSuccess(product.Data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
      
            public ApiResult<Product> UpdateProduct(string productId,
            UpdateProductOptions options)
        {
            if (options == null) {
                return new ApiResult<Product>(
                  StatusCodecs.BADREQUEST, "null options"
                  );
            }

            var product = GetProductById(productId);
            if (product == null) {
                return new ApiResult<Product>(
                  StatusCodecs.BADREQUEST, "null id"
                  );
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {
                product.Data.Description = options.Description;
            }

            if (options.Price != null &&
              options.Price <= 0) {
                return new ApiResult<Product>(
                 StatusCodecs.BADREQUEST, "null id"
                 );
            }

       if (options.Price != null) { 
                if (options.Price <= 0) {
                     return new ApiResult<Product>(
                  StatusCodecs.BADREQUEST, "null id" );
                }else {
                    product.Data.Price = options.Price.Value;
                }
            }

            if (options.Discount != null &&
              options.Discount < 0) {
                return new ApiResult<Product>(
                 StatusCodecs.BADREQUEST, "null id");
            }

            return ApiResult<Product>.CreateSuccess(product.Data);
           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ApiResult<Product> GetProductById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return new ApiResult<Product>(
                    StatusCodecs.BADREQUEST, "null Id");
            }

            var result =  context
                .Set<Product>()
                .SingleOrDefault(s => s.Id == id);

            if(result == null) {
                return new ApiResult<Product>(
                     StatusCodecs.NotFOUND, "not found");
            }

            return ApiResult<Product>.CreateSuccess(result);
                
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
