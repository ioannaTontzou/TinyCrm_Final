using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model.Options
{
    public class AddOrderOptions
    {
        /// <summary>
        /// The deliveryAddress of new order
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// The productList of new order
        /// </summary>
        public List<Product> OrderProductList { get; set; }

        /// <summary>
        /// The customerId of new order
        /// </summary>
        public string CustomerId { get; set; }
    }
}
