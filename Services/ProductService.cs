using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyCrm.Model;
using TinyCrm.Model.Options;

namespace TinyCrm.Services
{
    public class ProductService : IProductService
    { 
        private List<Product> ProductList = new List<Product>();
    
        public bool AddProduct(AddProductOptions options)
        {
        if (options == null) {
            return false;
        }
        if (string.IsNullOrWhiteSpace(options.Id)) {
            return false;
        }
       
        var product = GetProductById(options.Id);
        if (product != null) {
            return false;
        }
        if (GetProductById(options.Id) != null) {
            return false;
        }
        if (string.IsNullOrWhiteSpace(options.Name)) {
            return false;
        }
        if (options.Price <= 0) {
            return false;
        }
        if (options.ProductCategory == Model.ProductCategory.Invalid) {
            return false;
        }
        var prod1 = new Product();

        prod1.Id = options.Id;
        prod1.Name = options.Name;
        prod1.Price = options.Price;
        prod1.Category = options.ProductCategory;

        ProductList.Add(prod1);

        return true;
    }

        public Product GetProductById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) {
                return null;
            }
            return ProductList.Where(s => s.Id.Equals(id))
                 .SingleOrDefault();
        }

        public bool UpdateProduct(string productId, UpdateProductOptions options)
        {
            if (options == null) {
                return false;
            }
            var product = GetProductById(productId);
            if (product == null) {
                product.Description = options.Description;
            }

            if (!string.IsNullOrWhiteSpace(options.Description)) {

            }

            if (options.Price != null) {
                if (options.Price <= 0) {
                    return false;
                } else {
                    product.Price = options.Price.Value;
                }
            }

            if (options.Discount != null && options.Discount < 0) {
                return false;
            }



            return true;
        }
    }
}
