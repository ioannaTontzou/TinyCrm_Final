using TinyCrm.Core.Model.Options;
using TinyCrm.Core.Model;
using System.Linq;
using System.Threading.Tasks;

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
       Task<ApiResult<Product>> AddProductAsync(AddProductOptions options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        ApiResult<Product> UpdateProduct(string productId,
            UpdateProductOptions options);

        IQueryable<Product> SearchProduct(SearchProductOptions options);

        ApiResult<Product> GetProductById(string id);
    }
}
