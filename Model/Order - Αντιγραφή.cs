using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyCrm.Model
{
   public  class Order
    {
        private string Orderid;
        /// <summary>
        /// CustomerName of an order
        /// </summary>
       
        
        public OrderStatus OrderStatus { get; set; }
        /// <summary>
        /// Id of an order
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// CustomerId of an order
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// DeliveryAddress of an order
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// TotalMount of an order
        /// </summary>
        public decimal TotalMount { get; set; }

        /// <summary>
        /// ProductList of an order
        /// </summary>
        public List<Product> OrderProductList;
        //public string OrderStatus { get; set; }
    }
}
