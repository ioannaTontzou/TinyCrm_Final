using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    interface IProductService
    {
        /// <summary>
        /// Add a new Product
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        bool AddProduct(AddProductOptions options);

        /// <summary>
        /// Update product details
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        bool UpdateProduct(string productId,
            UpdateProductOptions options);

        /// <summary>
        /// Return the details of a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Product GetProductById(string id);
    }
}
