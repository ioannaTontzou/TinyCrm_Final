using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model
{
   public class Product
    {
        /// <summary>
        /// Id of a product
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of a product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Price of a product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Discount price of an order
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Description of a product
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Enumaration for product category
        /// </summary>
        public ProductCategory Category { get; set; }
    }
}
