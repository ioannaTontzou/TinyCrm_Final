﻿using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System.Linq;

namespace TinyCrm.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        Product AddProduct(AddProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        bool UpdateProduct(string productId,
            UpdateProductOptions options);

        IQueryable<Product> SearchProduct(SearchProductOptions options);

        public Product GetProductById(string id);
    }
}